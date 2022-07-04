using MLT.Domain.Contracts.InfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MLT.Desktop.AppUsers.Helpers
{
    internal class AtdWorker
    {
        private const string CountryMark = "00000000";
        private const string RegionMark = "000000";
        private const string DistrictMark = "000";

        private readonly List<AtdInfo> originalCountries;
        private readonly List<AtdInfo> originalRegions;
        private readonly List<AtdInfo> originalDistricts;
        private readonly List<AtdInfo> originalCities;

        public List<AtdInfo> Countries { get; private set; }
        public List<AtdInfo> Regions { get; private set; }
        public List<AtdInfo> Districts { get; private set; }
        public List<AtdInfo> Cities { get; private set; }

        public AtdInfo CurrentCountry { get; private set; }
        public AtdInfo CurrentRegion { get; private set; }
        public AtdInfo CurrentDistrict { get; private set; }
        public AtdInfo CurrentCity { get; private set; }

        


        public AtdWorker(List<AtdInfo> atdInfos)
        {
            originalCountries = atdInfos.Where(p => p.Code.EndsWith(CountryMark)).ToList();
            atdInfos.RemoveAll(x => x.Code.EndsWith(CountryMark));
            originalRegions = atdInfos.Where(p => p.Code.EndsWith(RegionMark)).ToList();
            atdInfos.RemoveAll(x => x.Code.EndsWith(RegionMark));
            originalDistricts = atdInfos.Where(p => p.Code.EndsWith(DistrictMark)).ToList();
            atdInfos.RemoveAll(x => x.Code.EndsWith(DistrictMark));
            originalCities = atdInfos.ToList();

            Countries = originalCountries;
            Regions = originalRegions;
            Districts = originalDistricts;
            Cities = originalCities;
        }

        public void ReCalc(AtdInfo atdInfo)
        {
            var unZeroCount = 10;
            if (atdInfo.Code.EndsWith(DistrictMark))
            {
                unZeroCount = 10 - DistrictMark.Length;
            }
            if (atdInfo.Code.EndsWith(RegionMark))
            {
                unZeroCount = 10 - RegionMark.Length;
            }
            if (atdInfo.Code.EndsWith(CountryMark))
            {
                unZeroCount = 10 - CountryMark.Length;
            }

            CurrentCountry = originalCountries.FirstOrDefault(p => p.Code.StartsWith(atdInfo.Code.Substring(0, 2)));
            CurrentRegion = originalRegions.FirstOrDefault(p => p.Code.StartsWith(atdInfo.Code.Substring(0, 4)));
            CurrentDistrict = originalDistricts.FirstOrDefault(p => p.Code.StartsWith(atdInfo.Code.Substring(0, 7)));
            CurrentCity = originalCities.FirstOrDefault(p => p.Code.StartsWith(atdInfo.Code));

            if (unZeroCount >= 2)
            {
                Regions = originalRegions.Where(p => p.Code.StartsWith(atdInfo.Code.Substring(0, 2))).ToList();
            }
            else
            { 
                Regions = originalRegions.Where(p => p.Code.StartsWith(atdInfo.Code.Substring(0, unZeroCount))).ToList();
            }

            if (unZeroCount >= 4)
            {
                Districts = originalDistricts.Where(p => p.Code.StartsWith(atdInfo.Code.Substring(0, 4))).ToList();
            }
            else
            {
                Districts = originalDistricts.Where(p => p.Code.StartsWith(atdInfo.Code.Substring(0, unZeroCount))).ToList();
            }

            if (unZeroCount >= 7)
            {
                Cities = originalCities.Where(p => p.Code.StartsWith(atdInfo.Code.Substring(0, 7))).ToList();
            }
            else
            {
                Cities = originalCities.Where(p => p.Code.StartsWith(atdInfo.Code.Substring(0, unZeroCount))).ToList();
            }
        }
        
    }
}
