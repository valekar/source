using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Transaction
{
    public class TransactionDetails
    {
        public IList<Entities.Transaction.TransactionMerge> getTransactionMerge(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionMerge>(SQL.Transaction.TransactionDetails.getTransactionMergeSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionEmail> getTransactionEmail(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionEmail>(SQL.Transaction.TransactionDetails.getTransactionEmailSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionAddress> getTransactionAddress(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionAddress>(SQL.Transaction.TransactionDetails.getTransactionAddressSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionBirth> getTransactionBirth(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionBirth>(SQL.Transaction.TransactionDetails.getTransactionBirthSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionCharacteristics> getTransactionCharacteristics(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionCharacteristics>(SQL.Transaction.TransactionDetails.getTransactionCharactericticsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionContactPreference> getTransactionContactPreference(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionContactPreference>(SQL.Transaction.TransactionDetails.getTransactionContactPreferenceSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionDeath> getTransactionDeath(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionDeath>(SQL.Transaction.TransactionDetails.getTransactionDeathSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionPersonName> getTransactionPersonName(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionPersonName>(SQL.Transaction.TransactionDetails.getTransactionPersonNameSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionOrgName> getTransactionOrgName(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionOrgName>(SQL.Transaction.TransactionDetails.getTransactionOrgNameSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionPhone> getTransactionPhone(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionPhone>(SQL.Transaction.TransactionDetails.getTransactionPhoneSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

       
        public IList<Entities.Transaction.TransactionOrgTransformations> getTransactionOrgTransformations(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionOrgTransformations>(SQL.Transaction.TransactionDetails.getTransactionOrgTransformationSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionOrgAffiliator> getTransactionOrgAffiliator(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionOrgAffiliator>(SQL.Transaction.TransactionDetails.getTransactionOrgAffiliatorSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionAffiliatorTags> getTransactionAffiliatorTags(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionAffiliatorTags>(SQL.Transaction.TransactionDetails.getTransactionAffiliatorTagsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionAffiliatorTagsUpload> getTransactionAffiliatorTagsUpload(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionAffiliatorTagsUpload>(SQL.Transaction.TransactionDetails.getTransactionAffiliatorTagsUploadSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionAffiliatorHierarchy> getTransactionAffiliatorHierarchy(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionAffiliatorHierarchy>(SQL.Transaction.TransactionDetails.getTransactionAffiliatorHierarchySQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionUploadDetails> getTransactionUploadDetails(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionUploadDetails>(SQL.Transaction.TransactionDetails.getTransactionUploadSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionUnmergeRequestLog> getTransactionUnmergeRequestLog(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionUnmergeRequestLog>(SQL.Transaction.TransactionDetails.getTransactionUnmergeRequestLogSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionUnmergeProcessLog> getTransactionUnmergeProcessLog(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionUnmergeProcessLog>(SQL.Transaction.TransactionDetails.getTransactionUnmergeProcessLogSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        //Orgler starts
        public IList<Entities.Transaction.TransactionNAICS> getTransactionNAICS(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionNAICS>(SQL.Transaction.TransactionDetails.getTransactionNAICSSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
        public IList<Entities.Transaction.TransactionNAICSUpload> getTransactionNAICSUpload(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionNAICSUpload>(SQL.Transaction.TransactionDetails.getTransactionNAICSUploadSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
        public IList<Entities.Transaction.TransactionOrgEmailDomain> getTransactionOrgEmailDomain(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionOrgEmailDomain>(SQL.Transaction.TransactionDetails.getTransactionOrgEmailDomainSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
        public IList<Entities.Transaction.TransactionOrgConfirmation> getTransactionOrgConfirmation(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionOrgConfirmation>(SQL.Transaction.TransactionDetails.getTransactionOrgConfirmationSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
        //orgler ends

        //added by srini for CEM surfacing 
        public IList<Entities.Transaction.TransCemDNCDetails> getTransCemDNCDetails(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransCemDNCDetails>(SQL.Transaction.TransactionDetails.getTransCemDncSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransCemMsgPrefDetails> getTransCemMsgPrefDetails(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransCemMsgPrefDetails>(SQL.Transaction.TransactionDetails.getTransCemMsgPrefSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }


        public IList<Entities.Transaction.TransCemPrefLocDetails> getTransCemPrefLocDetails(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransCemPrefLocDetails>(SQL.Transaction.TransactionDetails.getTransCemPrefLocSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransCemGrpMembership> getTransCemGrpMembershipDetails(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransCemGrpMembership>(SQL.Transaction.TransactionDetails.getTransCemGrpMembershipSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionDncUploadDetails> getTransDncUploadDetails(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionDncUploadDetails>(SQL.Transaction.TransactionDetails.getTransDncUploadSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionMsgPrefUploadDetails> getTransMsgPrefUploadDetails(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionMsgPrefUploadDetails>(SQL.Transaction.TransactionDetails.getTransMsgPrefUploadSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
        //adding ends here

        public IList<Entities.Transaction.TransactionEOAffiliationUpload> getTransactionEOAffiliationUpload(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionEOAffiliationUpload>(SQL.Transaction.TransactionDetails.getTransactionEOAffiliationUploadSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionEOSiteUpload> getTransactionEOSiteUpload(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionEOSiteUpload>(SQL.Transaction.TransactionDetails.getTransactionEOSiteUploadSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransactionEOUpload> getTransactionEOUpload(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransactionEOUpload>(SQL.Transaction.TransactionDetails.getTransactionEOUploadSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Transaction.TransEOCharacteristics> getTransEOCharacteristics(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository("TDOrglerEF");
            var AcctLst = rep.ExecuteSqlQuery<Entities.Transaction.TransEOCharacteristics>(SQL.Transaction.TransactionDetails.getTransactionEOCharacteristicsSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }
        
        


    }
}
