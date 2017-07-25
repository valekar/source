using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Constituents
{
    public class PersonNameDetails
    {
        public IList<Business.Constituents.PersonNameDetailsOutput> showPersonNameDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showPersonNameDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.PersonNameDetailsOutput, Business.Constituents.PersonNameDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PersonNameDetailsOutput>, IList<Business.Constituents.PersonNameDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class OrgNameDetails
    {
        public IList<Business.Constituents.OrgNameDetailsOutput> showOrgNameDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showOrgNameDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.OrgNameDetailsOutput, Business.Constituents.OrgNameDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.OrgNameDetailsOutput>, IList<Business.Constituents.OrgNameDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class AddressDetails
    {
        public IList<Business.Constituents.AddressDetailsOutput> showAddressDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showAddressDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.AddressDetailsOutput, Business.Constituents.AddressDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.AddressDetailsOutput>, IList<Business.Constituents.AddressDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class PhoneDetails
    {
        public IList<Business.Constituents.PhoneDetailsOutput> showPhoneDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showPhoneDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.PhoneDetailsOutput, Business.Constituents.PhoneDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.PhoneDetailsOutput>, IList<Business.Constituents.PhoneDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class EmailDetails
    {
        public IList<Business.Constituents.EmailDetailsOutput> showEmailDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showEmailDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.EmailDetailsOutput, Business.Constituents.EmailDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.EmailDetailsOutput>, IList<Business.Constituents.EmailDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class BirthDetails
    {
        public IList<Business.Constituents.BirthDetailsOutput> showBirthDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showBirthDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.BirthDetailsOutput, Business.Constituents.BirthDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.BirthDetailsOutput>, IList<Business.Constituents.BirthDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class DeathDetails
    {
        public IList<Business.Constituents.DeathDetailsOutput> showDeathDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showDeathDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.DeathDetailsOutput, Business.Constituents.DeathDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.DeathDetailsOutput>, IList<Business.Constituents.DeathDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class ContactPreferenceDetails
    {
        public IList<Business.Constituents.ContactPreferenceDetailsOutput> showContactPreferenceDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showContactPreferenceDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ContactPreferenceDetailsOutput, Business.Constituents.ContactPreferenceDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ContactPreferenceDetailsOutput>, IList<Business.Constituents.ContactPreferenceDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class CharacteristicsDetails
    {
        public IList<Business.Constituents.CharacteristicsDetailsOutput> showCharacteristicsDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showCharacteristicsDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.CharacteristicsDetailsOutput, Business.Constituents.CharacteristicsDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.CharacteristicsDetailsOutput>, IList<Business.Constituents.CharacteristicsDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class ExternalBridgeDetails
    {
        public IList<Business.Constituents.ExternalBridgeDetailsOutput> showExternalBridgeDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showExternalBridgeDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.ExternalBridgeDetailsOutput, Business.Constituents.ExternalBridgeDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.ExternalBridgeDetailsOutput>, IList<Business.Constituents.ExternalBridgeDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class InternalBridgeDetails
    {
        public IList<Business.Constituents.InternalBridgeDetailsOutput> showInternalBridgeDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showInternalBridgeDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.InternalBridgeDetailsOutput, Business.Constituents.InternalBridgeDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.InternalBridgeDetailsOutput>, IList<Business.Constituents.InternalBridgeDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class MasterMetricsDetails
    {
        public IList<Business.Constituents.MasterMetricsDetailsOutput> showMasterMetricsDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showMasterMetricsDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.MasterMetricsDetailsOutput, Business.Constituents.MasterMetricsDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MasterMetricsDetailsOutput>, IList<Business.Constituents.MasterMetricsDetailsOutput>>(AcctLst);
            return result;
        }
    }

    public class FSARelationshipDetails
    {
        public IList<Business.Constituents.FSARelationshipDetailsOutput> showFSARelationshipDetails(ARC.Donor.Business.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.ShowDetailsInput, Data.Entities.Constituents.ShowDetailsInput>(ShowDetailsInput);
            Data.Constituents.ShowAllDetails gd = new Data.Constituents.ShowAllDetails();
            var AcctLst = gd.showFSARelationshipDetails(Input);
            Mapper.CreateMap<Data.Entities.Constituents.FSARelationshipDetailsOutput, Business.Constituents.FSARelationshipDetailsOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.FSARelationshipDetailsOutput>, IList<Business.Constituents.FSARelationshipDetailsOutput>>(AcctLst);
            return result;
        }
    }
}
