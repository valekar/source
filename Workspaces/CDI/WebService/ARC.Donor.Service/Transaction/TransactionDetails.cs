using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Transaction
{
    public class TransactionDetails
    {
        public IList<Business.Transaction.TransactionMerge> getMergeTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionMerge(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionMerge, Business.Transaction.TransactionMerge>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionMerge>, IList<Business.Transaction.TransactionMerge>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionEmail> getEmailTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionEmail(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionEmail, Business.Transaction.TransactionEmail>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionEmail>, IList<Business.Transaction.TransactionEmail>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionAddress> getAddressTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionAddress(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionAddress, Business.Transaction.TransactionAddress>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionAddress>, IList<Business.Transaction.TransactionAddress>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionBirth> getBirthTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionBirth(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionBirth, Business.Transaction.TransactionBirth>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionBirth>, IList<Business.Transaction.TransactionBirth>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionCharacteristics> getCharacteristicsTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionCharacteristics(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionCharacteristics, Business.Transaction.TransactionCharacteristics>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionCharacteristics>, IList<Business.Transaction.TransactionCharacteristics>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionContactPreference> getContactPreferenceTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionContactPreference(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionContactPreference, Business.Transaction.TransactionContactPreference>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionContactPreference>, IList<Business.Transaction.TransactionContactPreference>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionDeath> getDeathTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionDeath(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionDeath, Business.Transaction.TransactionDeath>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionDeath>, IList<Business.Transaction.TransactionDeath>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionPersonName> getPersonNameTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionPersonName(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionPersonName, Business.Transaction.TransactionPersonName>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionPersonName>, IList<Business.Transaction.TransactionPersonName>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionOrgName> getOrgNameTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionOrgName(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionOrgName, Business.Transaction.TransactionOrgName>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionOrgName>, IList<Business.Transaction.TransactionOrgName>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionPhone> getPhoneTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionPhone(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionPhone, Business.Transaction.TransactionPhone>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionPhone>, IList<Business.Transaction.TransactionPhone>>(AcctLst);
            return result;
        }

       
        public IList<Business.Transaction.TransactionOrgTransformations> getOrgTransformationsTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionOrgTransformations(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionOrgTransformations, Business.Transaction.TransactionOrgTransformations>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionOrgTransformations>, IList<Business.Transaction.TransactionOrgTransformations>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionOrgAffiliator> getOrgAffiliatorTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionOrgAffiliator(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionOrgAffiliator, Business.Transaction.TransactionOrgAffiliator>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionOrgAffiliator>, IList<Business.Transaction.TransactionOrgAffiliator>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionAffiliatorTags> getAffiliatorTagsTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionAffiliatorTags(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionAffiliatorTags, Business.Transaction.TransactionAffiliatorTags>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionAffiliatorTags>, IList<Business.Transaction.TransactionAffiliatorTags>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionAffiliatorTagsUpload> getAffiliatorTagsUploadTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionAffiliatorTagsUpload(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionAffiliatorTagsUpload, Business.Transaction.TransactionAffiliatorTagsUpload>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionAffiliatorTagsUpload>, IList<Business.Transaction.TransactionAffiliatorTagsUpload>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionAffiliatorHierarchy> getAffiliatorHierarchyTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionAffiliatorHierarchy(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionAffiliatorHierarchy, Business.Transaction.TransactionAffiliatorHierarchy>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionAffiliatorHierarchy>, IList<Business.Transaction.TransactionAffiliatorHierarchy>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionUploadDetails> getUploadDetailsTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionUploadDetails(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionUploadDetails, Business.Transaction.TransactionUploadDetails>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionUploadDetails>, IList<Business.Transaction.TransactionUploadDetails>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionUnmergeRequestLog> getUnmergeRequestLogTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionUnmergeRequestLog(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionUnmergeRequestLog, Business.Transaction.TransactionUnmergeRequestLog>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionUnmergeRequestLog>, IList<Business.Transaction.TransactionUnmergeRequestLog>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionUnmergeProcessLog> getUnmergeProcessLogTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionUnmergeProcessLog(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionUnmergeProcessLog, Business.Transaction.TransactionUnmergeProcessLog>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionUnmergeProcessLog>, IList<Business.Transaction.TransactionUnmergeProcessLog>>(AcctLst);
            return result;
        }
        public IList<Business.Transaction.TransactionNAICS> getNAICSTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionNAICS(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionNAICS, Business.Transaction.TransactionNAICS>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionNAICS>, IList<Business.Transaction.TransactionNAICS>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionNAICSUpload> getNAICSUploadTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionNAICSUpload(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionNAICSUpload, Business.Transaction.TransactionNAICSUpload>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionNAICSUpload>, IList<Business.Transaction.TransactionNAICSUpload>>(AcctLst);
            return result;
        }
        public IList<Business.Transaction.TransactionOrgEmailDomain> getOrgEmailDomainTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionOrgEmailDomain(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionOrgEmailDomain, Business.Transaction.TransactionOrgEmailDomain>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionOrgEmailDomain>, IList<Business.Transaction.TransactionOrgEmailDomain>>(AcctLst);
            return result;
        }
        public IList<Business.Transaction.TransactionOrgConfirmation> getOrgConfirmationTransactionDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionOrgConfirmation(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionOrgConfirmation, Business.Transaction.TransactionOrgConfirmation>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionOrgConfirmation>, IList<Business.Transaction.TransactionOrgConfirmation>>(AcctLst);
            return result;
        }

        // added by srini for CEM surfacing
        public IList<Business.Transaction.TransCemDNCDetails> getTransCemDNCDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransCemDNCDetails(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransCemDNCDetails, Business.Transaction.TransCemDNCDetails>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransCemDNCDetails>, IList<Business.Transaction.TransCemDNCDetails>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransCemMsgPrefDetails> getTransCemMsgPrefDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransCemMsgPrefDetails(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransCemMsgPrefDetails, Business.Transaction.TransCemMsgPrefDetails>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransCemMsgPrefDetails>, IList<Business.Transaction.TransCemMsgPrefDetails>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransCemPrefLocDetails> getTransCemPrefLocDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransCemPrefLocDetails(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransCemPrefLocDetails, Business.Transaction.TransCemPrefLocDetails>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransCemPrefLocDetails>, IList<Business.Transaction.TransCemPrefLocDetails>>(AcctLst);
            return result;
        }


        public IList<Business.Transaction.TransCemGrpMembership> getTransCemGrpMembershipDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransCemGrpMembershipDetails(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransCemGrpMembership, Business.Transaction.TransCemGrpMembership>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransCemGrpMembership>, IList<Business.Transaction.TransCemGrpMembership>>(AcctLst);
            return result;
        }


        public IList<Business.Transaction.TransactionDncUploadDetails> getTransDncUploadDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransDncUploadDetails(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionDncUploadDetails, Business.Transaction.TransactionDncUploadDetails>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionDncUploadDetails>, IList<Business.Transaction.TransactionDncUploadDetails>>(AcctLst);
            return result;
        }
        public IList<Business.Transaction.TransactionMsgPrefUploadDetails> getTransMsgPrefUploadDetails(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransMsgPrefUploadDetails(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionMsgPrefUploadDetails, Business.Transaction.TransactionMsgPrefUploadDetails>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionMsgPrefUploadDetails>, IList<Business.Transaction.TransactionMsgPrefUploadDetails>>(AcctLst);
            return result;
        }
        //adding ends here
        public IList<Business.Transaction.TransactionEOAffiliationUpload> getTransactionEOAffiliationUpload(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionEOAffiliationUpload(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionEOAffiliationUpload, Business.Transaction.TransactionEOAffiliationUpload>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionEOAffiliationUpload>, IList<Business.Transaction.TransactionEOAffiliationUpload>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionEOSiteUpload> getTransactionEOSiteUpload(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionEOSiteUpload(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionEOSiteUpload, Business.Transaction.TransactionEOSiteUpload>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionEOSiteUpload>, IList<Business.Transaction.TransactionEOSiteUpload>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransactionEOUpload> getTransactionEOUpload(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransactionEOUpload(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransactionEOUpload, Business.Transaction.TransactionEOUpload>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransactionEOUpload>, IList<Business.Transaction.TransactionEOUpload>>(AcctLst);
            return result;
        }

        public IList<Business.Transaction.TransEOCharacteristics> getTransEOCharacteristics(int NoOfRecs, int PageNum, string Transaction_Key)
        {
            Data.Transaction.TransactionDetails gd = new Data.Transaction.TransactionDetails();
            var AcctLst = gd.getTransEOCharacteristics(NoOfRecs, PageNum, Transaction_Key);
            Mapper.CreateMap<Data.Entities.Transaction.TransEOCharacteristics, Business.Transaction.TransEOCharacteristics>();

            var result = Mapper.Map<IList<Data.Entities.Transaction.TransEOCharacteristics>, IList<Business.Transaction.TransEOCharacteristics>>(AcctLst);
            return result;
        }

        
    }
}
