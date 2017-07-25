using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARC.Donor.Service.Constituents
{
    public class Name
    {
        public IList<Business.Constituents.PersonName> getConstituentPersonName(int NoOfRecs, int PageNum, string Master_Id)
        {
            Mapper.CreateMap<Data.Entities.Constituents.PersonName, Business.Constituents.PersonName>();   
            Data.Constituents.Name gd = new Data.Constituents.Name();
            var AcctLst = gd.getConstituentPersonName(NoOfRecs, PageNum, Master_Id);
            var result = Mapper.Map<IList<Data.Entities.Constituents.PersonName>, IList<Business.Constituents.PersonName>>(AcctLst);
            return result;
        }

        public IList<Business.Constituents.OrgName> getConstituentOrgName(int NoOfRecs, int PageNum, string Master_Id)
        {
            Mapper.CreateMap<Data.Entities.Constituents.OrgName, Business.Constituents.OrgName>();   
            Data.Constituents.Name gd = new Data.Constituents.Name();
            var AcctLst = gd.getConstituentOrgName(NoOfRecs, PageNum, Master_Id);
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgName>, IList<Business.Constituents.OrgName>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentPersonNameOutput> addConstituentPersonName(ARC.Donor.Business.Constituents.ConstituentPersonNameInput ConstNameInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentPersonNameInput, Data.Entities.Constituents.ConstituentPersonNameInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentPersonNameInput, Data.Entities.Constituents.ConstituentPersonNameInput>(ConstNameInput);           
 
            Data.Constituents.Name gd = new Data.Constituents.Name();
            var AcctLst = gd.addConstituentPersonName(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentPersonNameOutput, Business.Constituents.ConstituentPersonNameOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentPersonNameOutput>, IList<Business.Constituents.ConstituentPersonNameOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentPersonNameOutput> deleteConstituentPersonName(ARC.Donor.Business.Constituents.ConstituentPersonNameInput ConstNameInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentPersonNameInput, Data.Entities.Constituents.ConstituentPersonNameInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentPersonNameInput, Data.Entities.Constituents.ConstituentPersonNameInput>(ConstNameInput);           
 
            Data.Constituents.Name gd = new Data.Constituents.Name();
            var AcctLst = gd.deleteConstituentPersonName(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentPersonNameOutput, Business.Constituents.ConstituentPersonNameOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentPersonNameOutput>, IList<Business.Constituents.ConstituentPersonNameOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentPersonNameOutput> updateConstituentPersonName(ARC.Donor.Business.Constituents.ConstituentPersonNameInput ConstNameInput)
        {

            Mapper.CreateMap<Business.Constituents.ConstituentPersonNameInput, Data.Entities.Constituents.ConstituentPersonNameInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentPersonNameInput, Data.Entities.Constituents.ConstituentPersonNameInput>(ConstNameInput);           
 
            Data.Constituents.Name gd = new Data.Constituents.Name();
            var AcctLst = gd.updateConstituentPersonName(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentPersonNameOutput, Business.Constituents.ConstituentPersonNameOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentPersonNameOutput>, IList<Business.Constituents.ConstituentPersonNameOutput>>(AcctLst);
            return result;
        }

        /*Add,Update and Delete Services for Org Name*/
        public IList<ARC.Donor.Business.Constituents.ConstituentOrgNameOutput> addConstituentOrgName(ARC.Donor.Business.Constituents.ConstituentOrgNameInput ConstNameInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentOrgNameInput, Data.Entities.Constituents.ConstituentOrgNameInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentOrgNameInput, Data.Entities.Constituents.ConstituentOrgNameInput>(ConstNameInput);

            Data.Constituents.Name gd = new Data.Constituents.Name();
            var AcctLst = gd.addConstituentOrgName(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentOrgNameOutput, Business.Constituents.ConstituentOrgNameOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentOrgNameOutput>, IList<Business.Constituents.ConstituentOrgNameOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentOrgNameOutput> deleteConstituentOrgName(ARC.Donor.Business.Constituents.ConstituentOrgNameInput ConstNameInput)
        {
            Mapper.CreateMap<Business.Constituents.ConstituentOrgNameInput, Data.Entities.Constituents.ConstituentOrgNameInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentOrgNameInput, Data.Entities.Constituents.ConstituentOrgNameInput>(ConstNameInput);

            Data.Constituents.Name gd = new Data.Constituents.Name();
            var AcctLst = gd.deleteConstituentOrgName(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentOrgNameOutput, Business.Constituents.ConstituentOrgNameOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentOrgNameOutput>, IList<Business.Constituents.ConstituentOrgNameOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.ConstituentOrgNameOutput> updateConstituentOrgName(ARC.Donor.Business.Constituents.ConstituentOrgNameInput ConstNameInput)
        {

            Mapper.CreateMap<Business.Constituents.ConstituentOrgNameInput, Data.Entities.Constituents.ConstituentOrgNameInput>();
            var Input = Mapper.Map<Business.Constituents.ConstituentOrgNameInput, Data.Entities.Constituents.ConstituentOrgNameInput>(ConstNameInput);

            Data.Constituents.Name gd = new Data.Constituents.Name();
            var AcctLst = gd.updateConstituentOrgName(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ConstituentOrgNameOutput, Business.Constituents.ConstituentOrgNameOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ConstituentOrgNameOutput>, IList<Business.Constituents.ConstituentOrgNameOutput>>(AcctLst);
            return result;
        }
    }
}