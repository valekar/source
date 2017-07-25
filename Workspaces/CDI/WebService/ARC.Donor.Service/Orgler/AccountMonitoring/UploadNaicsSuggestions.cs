using System;
using AutoMapper;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using ARC.Donor.Business.Orgler.AccountMonitoring;

namespace ARC.Donor.Service.Orgler.AccountMonitoring
{
    public class UploadNaicsSuggestions
    {
        /* Method name: PostNaicsSuggestionsUploadData
          * Input Parameters: An object of NAICSSuggestionsDetails class
          * Output Parameters: An object of NAICSSuggestionsSubmitOutput class  
          * Purpose: This method upload the NAICS Suggestions for (approve,reject) */
        public Business.Orgler.AccountMonitoring.NAICSSuggestionsSubmitOutput PostNaicsSuggestionsUploadData(Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails naicsSuggestionsUploadDetails)
        {
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.UploadNAICSSuggestionsInput, Data.Entities.Orgler.AccountMonitoring.UploadNAICSSuggestionsInput>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.UploadNAICSSuggestionsFileInfo, Data.Entities.Orgler.AccountMonitoring.UploadNAICSSuggestionsFileInfo>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.ListUploadNAICSSuggestionsInput, Data.Entities.Orgler.AccountMonitoring.ListUploadNAICSSuggestionsInput>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper, Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper>();
            //Mapper.CreateMap<Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails, Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsDetails>();

            var input = Mapper.Map<Business.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper, Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper>(naicsSuggestionsUploadDetails.ListNAICSSuggestionsJsonFileDetailsInput[naicsSuggestionsUploadDetails.ListNAICSSuggestionsJsonFileDetailsInput.Count - 1]);

            Data.Orgler.AccountMonitoring.UploadNaicsSuggestions nsd = new Data.Orgler.AccountMonitoring.UploadNaicsSuggestions();

            string strUserName = naicsSuggestionsUploadDetails.ListUploadNAICSSuggestionsDetails.NAICSSuggestionsInputList[0].user_id;
            var trans_key = nsd.getNaicsSuggestionsUploadTransKeyDetails(strUserName);          
                
           var naicsSugguploadSubmitOutput = UploadNaicsSuggestionsData(naicsSuggestionsUploadDetails, trans_key);  
            return naicsSugguploadSubmitOutput;
        }
        public Business.Orgler.AccountMonitoring.NAICSSuggestionsSubmitOutput UploadNaicsSuggestionsData(Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails nsu, long trans_key)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.UploadNAICSSuggestionsInput, Data.Entities.Orgler.AccountMonitoring.UploadNAICSSuggestionsInput>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.UploadNAICSSuggestionsFileInfo, Data.Entities.Orgler.AccountMonitoring.UploadNAICSSuggestionsFileInfo>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.ListUploadNAICSSuggestionsInput, Data.Entities.Orgler.AccountMonitoring.ListUploadNAICSSuggestionsInput>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper, Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails, Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsDetails>();
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsSubmitOutput, Business.Orgler.AccountMonitoring.NAICSSuggestionsSubmitOutput>();
            //map the input from buisness object to the data layer object
            var input = Mapper.Map<Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails, Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsDetails>(nsu);

            //Instantiate the data layer object for upload functionality
            Data.Orgler.AccountMonitoring.UploadNaicsSuggestions nsd = new Data.Orgler.AccountMonitoring.UploadNaicsSuggestions();
            Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsSubmitOutput nso = new Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsSubmitOutput();

            //map the output from data layer to the business layer
            var output = Mapper.Map<Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsSubmitOutput, Business.Orgler.AccountMonitoring.NAICSSuggestionsSubmitOutput>(nso);
            output.insertFlag = 1;
            output.insertOutput = "Success";
            output.transactionKey = trans_key;
            output.ListNAICSSuggestionsJsonFileDetailsHelperDetailsInput = nsu.ListNAICSSuggestionsJsonFileDetailsInput;
            Task taskList = null;
            //call the data layer method to upload the data asynchronously to the database
            // Task<IList<Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails>> taskList = new Task<IList<Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails>>(input.ListUploadNAICSSuggestionsDetails.NAICSSuggestionsInputList.Count);
            //Task<Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails>[ taskList = null;
            foreach (var naicsSuggUploadItem in input.ListUploadNAICSSuggestionsDetails.NAICSSuggestionsInputList)
            {
             
                try
                {
                taskList = Task.Run(() => nsd.insertNaicsSuggestionUploadDetails(naicsSuggUploadItem,
                                                    input.ListNAICSSuggestionsJsonFileDetailsInput, trans_key));
                    
                }
                catch
                {
                    output.insertFlag = 0;
                    output.insertOutput = "Failure";                   
                    output.ListNAICSSuggestionsJsonFileDetailsHelperDetailsInput = nsu.ListNAICSSuggestionsJsonFileDetailsInput;

                    break;
                }               
            }
            Task.WaitAll(taskList);

            if (output.insertOutput == "Success")
            {              
                output.transactionKey = trans_key;
            }
            //Return the output object to the service
            return output;
        }

        /* Method name: PostNaicsSuggestionsUploadData
                  * Input Parameters: An object of NAICSSuggestionsDetails class
                  * Output Parameters: An object of NAICSSuggestionsSubmitOutput class  
                  * Purpose: This method upload the NAICS Suggestions for (approve,reject) */
        public Task UpdateNaicsSuggestionsUploadData(Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails naicsSuggestionsUploadDetails)
        {
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.UploadNAICSSuggestionsInput, Data.Entities.Orgler.AccountMonitoring.UploadNAICSSuggestionsInput>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.UploadNAICSSuggestionsFileInfo, Data.Entities.Orgler.AccountMonitoring.UploadNAICSSuggestionsFileInfo>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.ListUploadNAICSSuggestionsInput, Data.Entities.Orgler.AccountMonitoring.ListUploadNAICSSuggestionsInput>();
            Mapper.CreateMap<Business.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper, Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper>();
            //Mapper.CreateMap<Business.Orgler.AccountMonitoring.NAICSSuggestionsDetails, Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsDetails>();

            var input = Mapper.Map<Business.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper, Data.Entities.Orgler.AccountMonitoring.NAICSSuggestionsJsonFileDetailsHelper>(naicsSuggestionsUploadDetails.ListNAICSSuggestionsJsonFileDetailsInput[naicsSuggestionsUploadDetails.ListNAICSSuggestionsJsonFileDetailsInput.Count - 1]);

            Data.Orgler.AccountMonitoring.UploadNaicsSuggestions nsd = new Data.Orgler.AccountMonitoring.UploadNaicsSuggestions();           

            var naicsSugguploadSubmitOutput = nsd.updateNaicsSuggestionUploadDetails();
           
            return naicsSugguploadSubmitOutput;
        }

        public string getUploadStatus(int NoOfRecords, int PageNumber, long transactionKey)
        {
            //Map the various buisness objects and data layer objects using the Mapper class
            Mapper.CreateMap<Data.Entities.Orgler.AccountMonitoring.PotentialMergeOutput, ARC.Donor.Business.Orgler.AccountMonitoring.PotentialMergeOutput>();

            //Instantiate the data layer object for potential merge functionality
            ARC.Donor.Data.Orgler.AccountMonitoring.UploadNaicsSuggestions pm = new ARC.Donor.Data.Orgler.AccountMonitoring.UploadNaicsSuggestions();

            //call the data layer method to get potential merge results from DB
            var searchResults = pm.getUploadMetricResults(NoOfRecords, PageNumber, transactionKey);

            Data.Entities.Orgler.AccountMonitoring.UploadMetric uploadMetric = searchResults[0];
            string strStatus = string.Empty;
            if(int.Parse(uploadMetric.rej_cnt) > 0)
                strStatus = "Rejections";
            else if(int.Parse(uploadMetric.trgt_cnt) == 0)
                strStatus = "None";
            else strStatus = "Success";

            return strStatus;

        } 

        /* Method name: sendUploadStatusMail
          * Input Parameters: strEmailMessage: Email message to the user,loggedInUser: User ID to send the email
          * Output Parameters: Null
          * Purpose: This method Sends the Email-Notification to the User */
        //public void sendUploadStatusMail(string strEmailMessage, string loggedInUser)
        //{
        //    //Send email to the uploader
        //    string fromAddress, ccAddress, bccAddress, ToAddress;
        //    if (ConfigurationManager.AppSettings["FromEmail"] != null)
        //        fromAddress = ConfigurationManager.AppSettings["FromEmail"].ToString();
        //    else
        //        fromAddress = "";

        //    if (ConfigurationManager.AppSettings["ToAddress"] != null)
        //        ToAddress = ConfigurationManager.AppSettings["ToAddress"].ToString();
        //    else
        //        ToAddress = "";

        //    if (ConfigurationManager.AppSettings["CCEmail"] != null)
        //        ccAddress = ConfigurationManager.AppSettings["CCEmail"].ToString();
        //    else
        //        ccAddress = "";

        //    if (ConfigurationManager.AppSettings["BCCEmail"] != null)
        //        bccAddress = ConfigurationManager.AppSettings["BCCEmail"].ToString();
        //    else
        //        bccAddress = "";

        //    var sendMail = new Mail();
        //    sendMail.ToAddress = loggedInUser; // Changed Email Address hard coded to configurable from Web config(Only domain name will take from config)
        //    sendMail.FromAddress = fromAddress;
        //    sendMail.ccAddress = ccAddress;
        //    sendMail.bccAddress = bccAddress;
        //    sendMail.Subject = "Upload Naics Suggestions";
        //    sendMail.Body = strEmailMessage;

        //    sendMail.sendMail();
        //}

    }
}

 
