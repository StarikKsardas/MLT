using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using MLT.Web.Contracts.WebModels;
using MLT.Web.Services.Rest.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MLT.Web.Services.Rest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LatentController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IImageService imageService;
        private readonly ILatentService latentService;
             
        public LatentController(ILogger<LatentController> logger, IMapper mapper, IImageService imageService, ILatentService latentService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.imageService = imageService;
            this.latentService = latentService;
        }

        [Authorize]
        [HttpPost("SendLatent")]
        public string SendLatent([FromBody] LatentWeb latentWeb)
        {            
            var result = "OK";
            try
            {
                var imageInfo = imageService.Base64StringToImageInfo(latentWeb.ImageBase64);
                var normalizedImageInfo = imageService.NormalizeImage(imageInfo);
                var wsqImage = imageService.ImageInfoToWsq(imageInfo, 500);

                var latentInfo = mapper.Map<LatentWeb, LatentInfo>(latentWeb);
                var userInfo = (UserInfo)HttpContext.Items["UserInfo"];
                latentInfo.FirstAbrPlace = userInfo.AbrPlace;
                latentInfo.FirstPlaceCode = userInfo.PlaceCode;
                latentInfo.FirstUserName = $"{userInfo.Login}{userInfo.Id}";
                latentInfo.WsqImage = wsqImage;
                latentService.Add(latentInfo);
            }
            catch(Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }
    }
}
