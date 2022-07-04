using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MLT.Data.Contracts.Repositories;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using MLT.Web.Contracts.WebModels;
using MLT.Web.Services.Rest.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLT.Web.Services.Rest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly ILogger<AnswerController> logger;
        private readonly ILatentService latentService;
        private readonly IMapper mapper;

        public AnswerController(ILogger<AnswerController> logger, ILatentService latentService, IMapper mapper)
        {
            this.logger = logger;
            this.latentService = latentService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetUserLatents")]
        public IActionResult GetUserLatents()
        {
            try
            {
                var userInfo = (UserInfo)HttpContext.Items["UserInfo"];
                var answers = latentService.GetAllUserLatents(userInfo);
                var result = JsonConvert.SerializeObject(mapper.Map<IEnumerable<LatentInfo>, IEnumerable<AnswerWeb>>(answers));                
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
