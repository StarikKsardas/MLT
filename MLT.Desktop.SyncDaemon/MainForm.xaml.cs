using Microsoft.Extensions.Logging;
using MLT.Data.Contracts.Helpers;
using MLT.Data.Repositories.DatabaseContext;
using MLT.Domain.Contracts.Services;
using MLT.Domain.Services.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace MLT.Desktop.SyncDaemon
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {

        private readonly ILogger<MainForm> logger;
        private readonly ILatentService latentService;
        private readonly IQueryService queryService;
        private readonly IInformationService informationService;
        private readonly ConcurrentQueue<string> queue = new ConcurrentQueue<string>();        

        private DispatcherTimer syncDbTimer;
        private DispatcherTimer queueTimer;

        public MainForm(ILogger<MainForm> logger, ILatentService latentService, IQueryService queryService, IInformationService informationService)
        {
            InitializeComponent();
            this.logger = logger;
            this.latentService = latentService;
            this.queryService = queryService;
            this.informationService = informationService;

            syncDbTimerStart();
            queueTimerStart();
        }

        private void myNotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.Show();
            this.Activate();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void syncDbTimerStart()
        {
            syncDbTimer = new DispatcherTimer();
            syncDbTimer.Tick += syncDbTimerTick;
            syncDbTimer.Interval += new TimeSpan(0, 0, 0, 30);
            syncDbTimer.Start();
        }

        private void queueTimerStart()
        {
            queueTimer = new DispatcherTimer();
            queueTimer.Tick += queueTimerTick;
            queueTimer.Interval += new TimeSpan(0, 0, 0, 30);
            queueTimer.Start();
        }

        private void syncDbTimerTick(object sender, EventArgs e)
        {
            try
            {
               
                var ids = latentService.GetDifferenceIds();
                foreach (var current in ids)
                {
                    queue.Enqueue($"Latent:{current}");
                }
                latentService.SyncLatent(ids);
                informationService.DeatachedAll();
                if (queryService.CalculateChanges())
                {
                    foreach (var current in queryService.IdsToDelete)
                    {
                        queue.Enqueue($"Delete query:{current}");
                    }

                    foreach (var current in queryService.IdsToAdd)
                    {
                        queue.Enqueue($"Add query:{current}");
                    }
                    queryService.DeleteDifference();
                    informationService.DeatachedAll();
                    queryService.AddDifference();
                    informationService.DeatachedAll();
                }
            }
            catch (Exception ex)
            {
                queue.Enqueue(ex.ToString());
            }
        }

        private void queueTimerTick(object sender, EventArgs e)
        {            
            while (queue.TryDequeue(out string message))
            {
                tbMultiLine.Text += Environment.NewLine + message;
                logger.LogInformation(message, null);
            }
        }


        
    }
}
