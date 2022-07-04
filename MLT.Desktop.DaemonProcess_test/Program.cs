using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;

namespace MLT.Desktop.DaemonProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("mobilelatent_private_key.json")                
            });

            var registrationToken = "eS9FuubiRKCG2TOKzht19M:APA91bGMMGMUwi-4ROo8952MOqtakS6U-" +
                "hcL1C5kfZRFNWnQT2smSSZVfGpnDt_aWEdnMH9ivHu0sRyJb6gY3sGeLtf-GLId4ilbJB04dmJuB41b-" +
                "jhO610WbfhynecAHhDw-KU2q2Co";

            var message = new Message()
            {
                Token = registrationToken,
                Notification = new Notification()
                { 
                    Title = "Test From Code",
                    Body = "Here is your test"
                }                 
            };

            string response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
            Console.WriteLine(response);
        }
    }
}
