using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Orgler.AccountMonitoring
{
    public class ConfirmAccount
    {
        /* Method name: confirmAccount
        * Input Parameters: An object of ConfirmAccountInput class
        * Output Parameters: A list of TransactionResult class  
        * Purpose: This method confirms(closes) the account for data stewarding */
        public IList<ARC.Donor.Business.Orgler.AccountMonitoring.TransactionResult> confirmAccount(ARC.Donor.Business.Orgler.AccountMonitoring.ConfirmAccountInput confirmAccountInput)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.ConfirmAccountInput, Data.Entities.Orgler.AccountMonitoring.ConfirmAccountInput>();
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.TransactionResult, Business.Orgler.AccountMonitoring.TransactionResult>();

            //map the input from buisness object to the data layer object
            var Input = Mapper.Map<Business.Orgler.AccountMonitoring.ConfirmAccountInput, Data.Entities.Orgler.AccountMonitoring.ConfirmAccountInput>(confirmAccountInput);

            //Instantiate the data layer object for confirm functionality
            Data.Orgler.AccountMonitoring.ConfirmAccount confirmAccount = new Data.Orgler.AccountMonitoring.ConfirmAccount();

            //call the data layer method to confirm the account in the database
            var AcctLst = confirmAccount.confirmAccount(Input);

            //map the output from data layer to the business layer
            var result = Mapper.Map<IList<Data.Entities.Orgler.AccountMonitoring.TransactionResult>, IList<Business.Orgler.AccountMonitoring.TransactionResult>>(AcctLst);

            //return the results back to the controller
            return result;
        }
    }
}
