using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class ShowAllDetails
    {
        /* Method to query all Person Name columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of PersonNameDetailsOutput object consisting of all the columns in the person name table
         */
        public IList<Entities.Constituents.PersonNameDetailsOutput> showPersonNameDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.PersonNameDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Organization Name columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of OrgNameDetailsOutput object consisting of all the columns in the organization name table
         */
        public IList<Entities.Constituents.OrgNameDetailsOutput> showOrgNameDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.OrgNameDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Address columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of AddressDetailsOutput object consisting of all the columns in the Address table
         */
        public IList<Entities.Constituents.AddressDetailsOutput> showAddressDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.AddressDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Phone columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of PhoneDetailsOutput object consisting of all the columns in the Phone type 2 table
         */
        public IList<Entities.Constituents.PhoneDetailsOutput> showPhoneDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.PhoneDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Email columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of EmailDetailsOutput object consisting of all the columns in the Email type 2 table
         */
        public IList<Entities.Constituents.EmailDetailsOutput> showEmailDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.EmailDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Birth columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of BirthDetailsOutput object consisting of all the columns in the Birth table
         */
        public IList<Entities.Constituents.BirthDetailsOutput> showBirthDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.BirthDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Death columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of DeathDetailsOutput object consisting of all the columns in the Death table
         */
        public IList<Entities.Constituents.DeathDetailsOutput> showDeathDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.DeathDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Contact Preference columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of ContactPreferenceDetailsOutput object consisting of all the columns in the Contact Preference table
         */
        public IList<Entities.Constituents.ContactPreferenceDetailsOutput> showContactPreferenceDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.ContactPreferenceDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Characteristics columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of CharacteristicsDetailsOutput object consisting of all the columns in the Characteristics table
         */
        public IList<Entities.Constituents.CharacteristicsDetailsOutput> showCharacteristicsDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.CharacteristicsDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all External Bridge columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of ExternalBridgeDetailsOutput object consisting of all the columns in the External Bridge table
         */
        public IList<Entities.Constituents.ExternalBridgeDetailsOutput> showExternalBridgeDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.ExternalBridgeDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Internal Bridge columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of InternalBridgeDetailsOutput object consisting of all the columns in the Internal Bridge table
         */
        public IList<Entities.Constituents.InternalBridgeDetailsOutput> showInternalBridgeDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.InternalBridgeDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all Master Metrics columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of MasterMetricsDetailsOutput object consisting of all the columns in the Master Metrics table
         */
        public IList<Entities.Constituents.MasterMetricsDetailsOutput> showMasterMetricsDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.MasterMetricsDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }

        /* Method to query all FSA Relationship columns
         * Input Parameters : ShowDetailsInput
         * Output Parameter : List of FSARelationshipDetailsOutput object consisting of all the columns in the FSA Relationship table
         */
        public IList<Entities.Constituents.FSARelationshipDetailsOutput> showFSARelationshipDetails(ARC.Donor.Data.Entities.Constituents.ShowDetailsInput ShowDetailsInput)
        {
            Repository rep = new Repository();
            var SearchResults = rep.ExecuteSqlQuery<Entities.Constituents.FSARelationshipDetailsOutput>(SQL.Constituents.ShowAllDetails.getAllDetailsSQL(ShowDetailsInput)).ToList();
            return SearchResults;
        }
    }
}
