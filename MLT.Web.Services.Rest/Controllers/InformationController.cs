using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using MLT.Web.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLT.Web.Services.Rest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IInformationService informationService;
        private readonly IMessageService messageService;

        public InformationController(IInformationService informationService, IMapper mapper, ILogger<InformationController> logger, IMessageService messageService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.informationService = informationService;
            this.messageService = messageService;
        }

        [HttpGet("GetEntrancePlaces")]
        public IEnumerable<SingleClassifierWeb> GetEntrancePlaces()
        {
            var entrancePlaceInfos = informationService.GetAllEntrancePlace();
            var result = mapper.Map<IEnumerable<EntrancePlaceInfo>, IEnumerable<SingleClassifierWeb>>(entrancePlaceInfos);
            return result;
        }

        [HttpGet("GetEntranceTypes")]
        public IEnumerable<DualClassifierWeb> GetEntranceTypes()
        {
            var entranceTypesInfos = informationService.GetAllEntranceTypes();
            var result = mapper.Map<IEnumerable<EntranceTypeInfo>, IEnumerable<DualClassifierWeb>>(entranceTypesInfos);
            return result;
        }

        [HttpGet("GetCrimeTypes")]
        public IEnumerable<DualClassifierWeb> GetCrimeTypes()
        {
            var crimeTypesInfos = informationService.GetAllCrimeTypes();
            var result = mapper.Map<IEnumerable<CrimeTypeInfo>, IEnumerable<DualClassifierWeb>>(crimeTypesInfos);
            return result;
        }

        [HttpGet("TestRabbit")]
        public void TestRabbit()
        {
            messageService.SendMessage<string>("HELLO WORLD!", "SDEREV PIDOR");
        }
    }
}
