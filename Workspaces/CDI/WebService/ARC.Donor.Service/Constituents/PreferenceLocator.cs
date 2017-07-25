using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ARC.Donor.Service.Constituents
{

   public class PreferenceLocator
    {

       public IList<Business.Constituents.PreferenceLocator> getPreferenceLocators(int NoOfRecs, int PageNum, string Master_Id)
       {
           Data.Constituents.PreferenceLocator gd = new Data.Constituents.PreferenceLocator();
           var AcctLst = gd.getPreferenceLocator(NoOfRecs, PageNum, Master_Id);
           Mapper.CreateMap<Data.Entities.Constituents.PreferenceLocator, Business.Constituents.PreferenceLocator>();
           var result = Mapper.Map<IList<Data.Entities.Constituents.PreferenceLocator>, IList<Business.Constituents.PreferenceLocator>>(AcctLst);
           return result;
       }

       public IList<Business.Constituents.AllPreferenceLocator> getAllPreferenceLocators(int NoOfRecs, int PageNum, string Master_Id)
       {
           Data.Constituents.PreferenceLocator gd = new Data.Constituents.PreferenceLocator();
           var AcctLst = gd.getAllPreferenceLocators(NoOfRecs, PageNum, Master_Id);
           Mapper.CreateMap<Data.Entities.Constituents.AllPreferenceLocator, Business.Constituents.AllPreferenceLocator>();
           var result = Mapper.Map<IList<Data.Entities.Constituents.AllPreferenceLocator>, IList<Business.Constituents.AllPreferenceLocator>>(AcctLst);
           return result;
       }

       public IList<Business.Constituents.PreferenceLocatorOptions> getPreferenceLocatorsOptions(int NoOfRecs, int PageNum, string Master_Id)
       {
           Data.Constituents.PreferenceLocator gd = new Data.Constituents.PreferenceLocator();
           var AcctLst = gd.getPreferenceLocatorOptions(NoOfRecs, PageNum, Master_Id);
           Mapper.CreateMap<Data.Entities.Constituents.PreferenceLocatorOptions, Business.Constituents.PreferenceLocatorOptions>();
           var result = Mapper.Map<IList<Data.Entities.Constituents.PreferenceLocatorOptions>, IList<Business.Constituents.PreferenceLocatorOptions>>(AcctLst);
           return result;
       }

       public IList<ARC.Donor.Business.Constituents.PreferenceLocatorOutput> addPreferenceLocator(ARC.Donor.Business.Constituents.PreferenceLocatorInput prefLocatorInput)
       {
           Mapper.CreateMap<Business.Constituents.PreferenceLocatorInput, Data.Entities.Constituents.PreferenceLocatorInput>();
           var Input = Mapper.Map<Business.Constituents.PreferenceLocatorInput, Data.Entities.Constituents.PreferenceLocatorInput>(prefLocatorInput);
           Data.Constituents.PreferenceLocator gd = new Data.Constituents.PreferenceLocator();
           var AcctLst = gd.addPreferenceLocator(Input);
           Mapper.CreateMap<Data.Entities.Constituents.PreferenceLocatorOutput, Business.Constituents.PreferenceLocatorOutput>();
           var result = Mapper.Map<IList<Data.Entities.Constituents.PreferenceLocatorOutput>, IList<Business.Constituents.PreferenceLocatorOutput>>(AcctLst);
           return result;
       }

       public IList<ARC.Donor.Business.Constituents.PreferenceLocatorOutput> deletePreferenceLocator(ARC.Donor.Business.Constituents.PreferenceLocatorInput prefLocatorInput)
       {
           Mapper.CreateMap<Business.Constituents.PreferenceLocatorInput, Data.Entities.Constituents.PreferenceLocatorInput>();
           var Input = Mapper.Map<Business.Constituents.PreferenceLocatorInput, Data.Entities.Constituents.PreferenceLocatorInput>(prefLocatorInput);
           Data.Constituents.PreferenceLocator gd = new Data.Constituents.PreferenceLocator();
           var AcctLst = gd.deletePreferenceLocator(Input);
           Mapper.CreateMap<Data.Entities.Constituents.PreferenceLocatorOutput, Business.Constituents.PreferenceLocatorOutput>();
           var result = Mapper.Map<IList<Data.Entities.Constituents.PreferenceLocatorOutput>, IList<Business.Constituents.PreferenceLocatorOutput>>(AcctLst);
           return result;
       }

       public IList<ARC.Donor.Business.Constituents.PreferenceLocatorOutput> editPreferenceLocator(ARC.Donor.Business.Constituents.PreferenceLocatorInput prefLocatorInput)
       {
           Mapper.CreateMap<Business.Constituents.PreferenceLocatorInput, Data.Entities.Constituents.PreferenceLocatorInput>();
           var Input = Mapper.Map<Business.Constituents.PreferenceLocatorInput, Data.Entities.Constituents.PreferenceLocatorInput>(prefLocatorInput);
           Data.Constituents.PreferenceLocator gd = new Data.Constituents.PreferenceLocator();
           var AcctLst = gd.editPreferenceLocator(Input);
           Mapper.CreateMap<Data.Entities.Constituents.PreferenceLocatorOutput, Business.Constituents.PreferenceLocatorOutput>();
           var result = Mapper.Map<IList<Data.Entities.Constituents.PreferenceLocatorOutput>, IList<Business.Constituents.PreferenceLocatorOutput>>(AcctLst);
           return result;
       }



    }
}
