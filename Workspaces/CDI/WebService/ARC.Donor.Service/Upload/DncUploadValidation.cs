using ARC.Donor.Business.Upload;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Utility.Extensions;
using System.Text.RegularExpressions;

namespace ARC.Donor.Service.Upload
{
    public class DncUploadValidation
    {

        public DncValidationOutput validateDncRecords(DncValidationInput dncValidateIP,ListDncUploadInput listDncUploadInput)
        {
            DncValidationOutput dncValidateOP = new DncValidationOutput();
          
            List<DncUploadParams> listDncInputRecords = listDncUploadInput.dncUploadInputList;

            //1. mandatory fields should not be empty
            List<DncUploadParams> _invalidDncRecords = new List<DncUploadParams>();

            List<bool> _invalidAddressLine1 = new List<bool>();
            List<bool> _invalidAddressLine2 = new List<bool>();
            List<bool> _invalidCity = new List<bool>();
            List<bool> _invalidState = new List<bool>();
            List<bool> _invalidZip = new List<bool>();
            List<bool> _invalidPhoneNum = new List<bool>();
            List<bool> _invalidEmailAddress = new List<bool>();
            List<bool> _invalidSourceSystemCode = new List<bool>();
            List<bool> _invalidSourceSystemId = new List<bool>();
            List<bool> _invalidMasterId = new List<bool>();
            List<bool> _invalidLineOfServices = new List<bool>();
            List<bool> _invalidCommChannels = new List<bool>();
           
            List<bool> _invalidFirstName = new List<bool>();
            List<bool> _invalidLastName = new List<bool>();
            List<bool> _invalidMiddleName = new List<bool>();
            List<bool> _invalidSuffix = new List<bool>();
            List<bool> _invalidPrefix =new List<bool>();
            List<bool> _invalidOrgName = new List<bool>();

            //invalid masters from data layer
            List<string> invalidMasterIdsFromDL = null;
            List<string> invalidSourceSystemCDsFromDL = null;
            List<string> invalidSourceSystemIdsFromDL = null;
            List<string> invalidCommChannelsDL = null;
            List<string> invalidLineOfServicesDL = null;


            List<DncUploadParams> tempValidList1 = listDncUploadInput.dncUploadInputList;

            DncValidationInput newDncValidationInput1= new DncValidationInput();
            newDncValidationInput1._sourceSystemCode = tempValidList1.Select(x => x.arc_srcsys_cd).ToList();
            newDncValidationInput1._sourceSystemId = tempValidList1.Select(x => x.cnst_srcsys_id).ToList();
            newDncValidationInput1._lineOfServiceCodes = tempValidList1.Select(x => x.line_of_service_cd).ToList();
            newDncValidationInput1._commChannels = tempValidList1.Select(x => x.comm_chan).ToList();


            Mapper.CreateMap<Business.Upload.DncValidationInput, Donor.Data.Entities.Upload.DncValidationInput>();

            var _input1 = Mapper.Map<Business.Upload.DncValidationInput, Donor.Data.Entities.Upload.DncValidationInput>(newDncValidationInput1);

            Data.Upload.DncUploadDetails gd = new Data.Upload.DncUploadDetails();


            // source system codes
            var _validSourceSystemCDsTask = Task.Factory.StartNew(() => emptyValidateDetails());
            if ((newDncValidationInput1._sourceSystemCode != null || newDncValidationInput1._sourceSystemCode.Count != 0))
            {
                _validSourceSystemCDsTask = Task.Factory.StartNew(() => gd.validateSourceSystemCodes(_input1));
              
            }
            // end of source system codes

            // source system Ids
            var _validSourceSystemIdsTask = Task.Factory.StartNew(() => emptyValidateDetails());
            if ((newDncValidationInput1._sourceSystemId != null || newDncValidationInput1._sourceSystemId.Count != 0))
            {
                _validSourceSystemIdsTask = Task.Factory.StartNew(() => gd.validateSourceSystemIds(_input1));
              
            }          
            // end of source system ids

            // validate communication channel
            var _validCommChannelsTask = Task.Factory.StartNew(() => emptyValidateDetails());
            if ((newDncValidationInput1._commChannels != null || newDncValidationInput1._commChannels.Count != 0))
            {
                _validCommChannelsTask = Task.Factory.StartNew(() => gd.validateCommChannels(_input1));
            }
            //end of validate communication channel

            //validate line of service
            var _validLineOfServicesTask = Task.Factory.StartNew(() => emptyValidateDetails());
            if ((newDncValidationInput1._lineOfServiceCodes != null || newDncValidationInput1._lineOfServiceCodes.Count != 0))
            {
                _validLineOfServicesTask = Task.Factory.StartNew(() => gd.validateLineOfService(_input1));
            }
           // end of validate line of service
            //check for masterIds
            int dummy = 0;
            //temp valid list
            List<DncUploadParams> tempValidList2 = listDncUploadInput.dncUploadInputList.
                 Intersect(listDncUploadInput.dncUploadInputList.Where(y => y.init_mstr_id.Length > 0 && int.TryParse(y.init_mstr_id, out dummy))).ToList();
            DncValidationInput newDncValidationInput2 = new DncValidationInput();

            newDncValidationInput2._masterIds = tempValidList2.Select(x => x.init_mstr_id).ToList();

            var _input2 = Mapper.Map<Business.Upload.DncValidationInput, Donor.Data.Entities.Upload.DncValidationInput>(newDncValidationInput2);

            var _validMasterIdsTask = Task.Factory.StartNew(() => emptyValidateDetails());
            if (newDncValidationInput2._masterIds != null || newDncValidationInput2._masterIds.Count != 0)
            {
                if (!newDncValidationInput2._masterIds.All(x => x.Equals("")))
                {
                    _validMasterIdsTask = Task.Factory.StartNew(() => gd.validateMasterIdDetails(_input2));
                }
            }



            Task.WaitAll(_validSourceSystemIdsTask, _validSourceSystemCDsTask, _validMasterIdsTask, _validCommChannelsTask, _validLineOfServicesTask);


            //process source system codes
            var _validSourceSystemCDs = _validSourceSystemCDsTask.Result;
            if (_validSourceSystemCDs != null)
            {
                //if(_validSourceSystemCDs.Result!=null){
                     invalidSourceSystemCDsFromDL =
                    newDncValidationInput1._sourceSystemCode.Where(x => !string.IsNullOrEmpty(x) && !_validSourceSystemCDs.Result.Select(y => y).Contains(x)).ToList();
                //}
               
            }

            //process source system ids
            var _validSourceSystemIds = _validSourceSystemIdsTask.Result;
            if (_validSourceSystemIds != null)
            {
                //if(_validSourceSystemIds.Result!=null){
                     invalidSourceSystemIdsFromDL =
                    newDncValidationInput1._sourceSystemId.Where(x => !string.IsNullOrEmpty(x) && !_validSourceSystemIds.Result.Select(y => y).Contains(x)).ToList();
                //}
               
            }
            
            // sprocess communication channels
            var _validCommChannels = _validCommChannelsTask.Result;
            if (_validCommChannels != null)
            {
                if (_validCommChannels.Result != null)
                {
                    invalidCommChannelsDL =
                  newDncValidationInput1._commChannels.Where(x => !string.IsNullOrEmpty(x) && !_validCommChannels.Result.Select(y => y).Contains(x)).ToList();
                }
               
            }
            //process line of services
            var _validLineOfServices = _validLineOfServicesTask.Result;
            if (_validLineOfServices != null)
            {
                if (_validLineOfServices.Result != null)
                {
                    invalidLineOfServicesDL =
                                      newDncValidationInput1._lineOfServiceCodes.Where(x => !string.IsNullOrEmpty(x) && !_validLineOfServices.Result.Select(y => y).Contains(x)).ToList();
                }
               
            }
            //process master ids
            var _validMasterIds = _validMasterIdsTask.Result;
            if (_validMasterIds!= null)
            {
                if (_validMasterIds.Result != null)
                {
                    invalidMasterIdsFromDL =
                    newDncValidationInput2._masterIds.Where(x => !string.IsNullOrEmpty(x) && !_validMasterIds.Result.Select(y => y).Contains(x)).ToList();
                }
                
            }


           int i = 0;
           foreach(DncUploadParams record in listDncInputRecords){

               bool isEmptyFirstName = record.prsn_frst_nm.Equals("");
               bool isEmptyLastName = record.prsn_lst_nm.Equals("");
               bool isEmptyPrefix = record.prefix_nm.Equals("");
               bool isEmptySuffix = record.suffix_nm.Equals("");
               bool isEmptyMiddleName = record.prsn_mid_nm.Equals("");

               bool isEmptyOrgName = record.cnst_org_nm.Equals("");

               bool isEmptyAddressLine1 = record.cnst_addr_line1.Equals("");
               bool isEmptyAddressLine2 = record.cnst_addr_line2.Equals("");
               bool isEmptyCity = record.cnst_addr_city.Equals("");
               bool isEmptyState = record.cnst_addr_state.Equals("");
               int resultNotNeeded =0;
               bool isEmptyZip = record.cnst_addr_zip5.Equals("") || !int.TryParse(record.cnst_addr_zip5, out resultNotNeeded);
               bool isEmptyEmailAddress = record.cnst_email_addr.Equals("") || !(record.cnst_email_addr.Contains("@") && record.cnst_email_addr.Contains("."));
               Regex regex = new Regex(@"^((((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4})|(\d{10} ?))$");
               Match match = regex.Match(record.cnst_phn_num);
               bool isEmptyPhoneNum = record.cnst_phn_num.Equals("") || !match.Success;

               bool isEmptyMasterId = record.init_mstr_id.Equals("");
               bool isRightMasterId = int.TryParse(record.init_mstr_id, out resultNotNeeded);


               bool isEmptySourceSystemCode = record.arc_srcsys_cd.Equals("");
               bool isEmptySourceSystemId = record.cnst_srcsys_id.Equals("");
               //bool isEmptySourceSystemId = !int.TryParse(record.cnst_srcsys_id, out resultNotNeeded);

               //compulsary validation
               bool isEmptyCommChannel = record.comm_chan.Equals("");
               bool isEmptyLOS = record.line_of_service_cd.Equals("");

               StringBuilder notes = new StringBuilder();

               bool isInvalidRecord = false;

               if (isEmptyAddressLine1 && isEmptyAddressLine2 && isEmptyCity && isEmptyState && isEmptyZip && isEmptyEmailAddress && isEmptyPhoneNum && isEmptyMasterId &&
                   isEmptySourceSystemCode && isEmptySourceSystemId && isEmptyFirstName && isEmptyLastName && isEmptyMiddleName && isEmptyPrefix && isEmptySuffix && isEmptyOrgName)
               {
                   _invalidPrefix.Insert(i, true);
                   _invalidFirstName.Insert(i, true);
                   _invalidMiddleName.Insert(i, true);
                   _invalidLastName.Insert(i, true);
                   _invalidSuffix.Insert(i, true);

                   _invalidOrgName.Insert(i, true);

                   _invalidAddressLine1.Insert(i,true);
                   _invalidAddressLine2.Insert(i,true);
                   _invalidCity.Insert(i,true);
                   _invalidState.Insert(i,true);
                   _invalidZip.Insert(i,true);
                   _invalidPhoneNum.Insert(i,true);
                   _invalidEmailAddress.Insert(i,true);
                   _invalidSourceSystemCode.Insert(i,true);
                   _invalidSourceSystemId.Insert(i,true);
                   _invalidMasterId.Insert(i,true);

                   notes.Append(ERROR_MESSAGE.EMPTY_FIELDS);

                   if (isEmptyCommChannel){
                       _invalidCommChannels.Insert(i, true);
                       notes.Append(ERROR_MESSAGE.COMMUNICATION_CHANNEL);
                   } 
                   else
                       _invalidCommChannels.Insert(i, false);
                   if (isEmptyLOS){
                       _invalidLineOfServices.Insert(i, true);
                       notes.Append(ERROR_MESSAGE.LINE_OF_SERVICE_CODE);
                   }

                   else
                   {
                       _invalidLineOfServices.Insert(i, false);
                   }
                       
                   isInvalidRecord = true;                  
                   
               }
               else if ((!isEmptyFirstName || !isEmptyLastName || !isEmptyMiddleName || !isEmptyPrefix || !isEmptySuffix) && !isEmptyOrgName)
               {
                 //  if (!isEmptyFirstName)
                 //  {

                       _invalidFirstName.Insert(i, true);

                //   }
                  // if (!isEmptyLastName)
                  // {
                       _invalidLastName.Insert(i, true);
                 //  }
                  // if (!isEmptyMiddleName)
                  // {
                       _invalidMiddleName.Insert(i, true);
                  // }
                  // if (!isEmptyPrefix)
                  // {
                       _invalidPrefix.Insert(i, true);
                  // }
                 //  if (!isEmptySuffix)
                  // {
                       _invalidSuffix.Insert(i, true);
                  // }
                  // if (!isEmptyOrgName)
                  // {
                       _invalidOrgName.Insert(i, true);
                  // }
                   _invalidAddressLine1.Insert(i, false);
                   _invalidAddressLine2.Insert(i, false);
                   _invalidCity.Insert(i, false);
                   _invalidState.Insert(i, false);
                   _invalidZip.Insert(i, false);
                   _invalidPhoneNum.Insert(i, false);
                   _invalidEmailAddress.Insert(i, false);
                   _invalidSourceSystemCode.Insert(i, false);
                   _invalidSourceSystemId.Insert(i, false);
                   _invalidMasterId.Insert(i, false);

                   notes.Append(ERROR_MESSAGE.NAME_ORG);

                   if (isEmptyCommChannel)
                   {
                       _invalidCommChannels.Insert(i, true);
                       notes.Append(ERROR_MESSAGE.COMMUNICATION_CHANNEL);
                   }
                   else
                       _invalidCommChannels.Insert(i, false);
                   if (isEmptyLOS)
                   {
                       _invalidLineOfServices.Insert(i, true);
                       notes.Append(ERROR_MESSAGE.LINE_OF_SERVICE_CODE);
                   }

                   else
                   {
                       _invalidLineOfServices.Insert(i, false);
                   }

                   isInvalidRecord = true;    
               }
               //check if source system id and source system codes are present
               else
               {
                   // check if atleast one locator value is present
                   if (isEmptyAddressLine1 && isEmptyAddressLine2 && isEmptyCity && isEmptyState && isEmptyZip && isEmptyEmailAddress && isEmptyPhoneNum && isEmptyFirstName && isEmptyLastName && isEmptyMiddleName && isEmptySuffix && isEmptyPrefix && isEmptyOrgName)
                   {
                       if ((isEmptyMasterId || !isRightMasterId) && (isEmptySourceSystemId && isEmptySourceSystemCode))
                       {
                           if (!_invalidFirstName.ElementAtOrDefault(i))
                           {
                               _invalidFirstName.Insert(i, false);   
                           }
                           if (!_invalidMiddleName.ElementAtOrDefault(i))
                           {
                               _invalidMiddleName.Insert(i, false);
                           }
                           if (!_invalidLastName.ElementAtOrDefault(i))
                           {
                               _invalidLastName.Insert(i, false);
                           }
                           if (!_invalidPrefix.ElementAtOrDefault(i))
                           {
                               _invalidPrefix.Insert(i, false);
                           }
                           if (!_invalidSuffix.ElementAtOrDefault(i))
                           {
                               _invalidSuffix.Insert(i, false);
                           }

                           if (!_invalidOrgName.ElementAtOrDefault(i))
                           {
                               _invalidOrgName.Insert(i, false);
                           }  

                           if (!_invalidAddressLine1.ElementAtOrDefault(i))
                           {
                               _invalidAddressLine1.Insert(i, false);
                           }
                           if (!_invalidAddressLine2.ElementAtOrDefault(i))
                           {
                               _invalidAddressLine2.Insert(i, false);
                           }
                           if (!_invalidCity.ElementAtOrDefault(i))
                           {
                               _invalidCity.Insert(i, false);
                           }
                           if (!_invalidState.ElementAtOrDefault(i))
                           {
                               _invalidState.Insert(i, false);
                           }
                           if (!_invalidZip.ElementAtOrDefault(i))
                           {
                               _invalidZip.Insert(i, false);
                           }
                           if (!_invalidPhoneNum.ElementAtOrDefault(i))
                           {
                               _invalidPhoneNum.Insert(i, false);
                           }
                           if (!_invalidEmailAddress.ElementAtOrDefault(i))
                           {
                               _invalidEmailAddress.Insert(i, false);
                           }
                           if (!_invalidSourceSystemCode.ElementAtOrDefault(i))
                           {
                               _invalidSourceSystemCode.Insert(i, false);
                           }
                           if (!_invalidSourceSystemId.ElementAtOrDefault(i))
                           {
                               _invalidSourceSystemId.Insert(i, false);
                           }
                           if (!_invalidCommChannels.ElementAtOrDefault(i))
                           {
                               _invalidCommChannels.Insert(i, false);
                           }
                           if (!_invalidLineOfServices.ElementAtOrDefault(i))
                           {
                               _invalidLineOfServices.Insert(i, false);
                           }
                          
                         

                           notes.Append(ERROR_MESSAGE.MASTER_ID);
                           _invalidMasterId.Insert(i, true);

                           isInvalidRecord = true;

                       }

                       //if source system ID is empty
                       if (!isEmptySourceSystemCode && isEmptySourceSystemId)
                       {
                           _invalidSourceSystemId.Insert(i, true);

                           _invalidSourceSystemCode.Insert(i, false);
                           _invalidPrefix.Insert(i, false);
                           _invalidFirstName.Insert(i, false);
                           _invalidMiddleName.Insert(i, false);
                           _invalidLastName.Insert(i, false);
                           _invalidSuffix.Insert(i, false);

                           _invalidOrgName.Insert(i, false);

                           _invalidAddressLine1.Insert(i, false);
                           _invalidAddressLine2.Insert(i, false);
                           _invalidCity.Insert(i, false);
                           _invalidState.Insert(i, false);
                           _invalidZip.Insert(i, false);
                           _invalidPhoneNum.Insert(i, false);
                           _invalidEmailAddress.Insert(i, false);

                           _invalidMasterId.Insert(i, false);
                           _invalidCommChannels.Insert(i, false);
                           _invalidLineOfServices.Insert(i, false);

                       


                           notes.Append(ERROR_MESSAGE.SOURCE_SYSTEM_ID);

                           isInvalidRecord = true;

                       }
                       //if source system CODE is empty 
                       //bool ttest = invalidSourceSystemCDsFromDL.Contains(record.arc_srcsys_cd);
                       if (isEmptySourceSystemCode && !isEmptySourceSystemId)
                       {
                           _invalidSourceSystemCode.Insert(i, true);

                           _invalidSourceSystemId.Insert(i, false);
                           _invalidPrefix.Insert(i, false);
                           _invalidFirstName.Insert(i, false);
                           _invalidMiddleName.Insert(i, false);
                           _invalidLastName.Insert(i, false);
                           _invalidSuffix.Insert(i, false);
                           _invalidOrgName.Insert(i, false);
                           _invalidAddressLine1.Insert(i, false);
                           _invalidAddressLine2.Insert(i, false);
                           _invalidCity.Insert(i, false);
                           _invalidState.Insert(i, false);
                           _invalidZip.Insert(i, false);
                           _invalidPhoneNum.Insert(i, false);
                           _invalidEmailAddress.Insert(i, false);
                           _invalidCommChannels.Insert(i, false);
                           _invalidLineOfServices.Insert(i, false);
                           _invalidMasterId.Insert(i, false);
                         

                           notes.Append(ERROR_MESSAGE.SOURCE_SYSTEM_CODE);
                           isInvalidRecord = true;
                       }

                   }
                   else
                   {
                       bool masterIdIsSet = false;
                       if (!isRightMasterId && record.init_mstr_id.Length>0)
                       {
                           _invalidPrefix.Insert(i, false);
                           _invalidFirstName.Insert(i, false);
                           _invalidMiddleName.Insert(i, false);
                           _invalidLastName.Insert(i, false);
                           _invalidSuffix.Insert(i, false);
                           _invalidOrgName.Insert(i, false);
                           _invalidAddressLine1.Insert(i, false);
                           _invalidAddressLine2.Insert(i, false);
                           _invalidCity.Insert(i, false);
                           _invalidState.Insert(i, false);
                           _invalidZip.Insert(i, false);
                           _invalidPhoneNum.Insert(i, false);
                           _invalidEmailAddress.Insert(i, false);
                           _invalidSourceSystemCode.Insert(i, false);
                           _invalidSourceSystemId.Insert(i, false);
                           _invalidCommChannels.Insert(i, false);
                           _invalidLineOfServices.Insert(i, false);


                           _invalidMasterId.Insert(i, true);
                           masterIdIsSet = true;
                           notes.Append(ERROR_MESSAGE.MASTER_ID);
                           isInvalidRecord = true;

                       }

                       //if source system ID is empty
                       if ((!isEmptySourceSystemCode && record.arc_srcsys_cd.Length>0) && isEmptySourceSystemId)
                       {
                           _invalidSourceSystemId.Insert(i, true);

                           _invalidSourceSystemCode.Insert(i, false);
                           _invalidPrefix.Insert(i, false);
                           _invalidFirstName.Insert(i, false);
                           _invalidMiddleName.Insert(i, false);
                           _invalidLastName.Insert(i, false);
                           _invalidSuffix.Insert(i, false);
                           _invalidOrgName.Insert(i, false);
                           _invalidAddressLine1.Insert(i, false);
                           _invalidAddressLine2.Insert(i, false);

                           _invalidCity.Insert(i, false);
                           _invalidState.Insert(i, false);
                           _invalidZip.Insert(i, false);
                           _invalidPhoneNum.Insert(i, false);
                           _invalidEmailAddress.Insert(i, false);

                           if (masterIdIsSet)
                               _invalidMasterId.Insert(i, true);
                           else
                               _invalidMasterId.Insert(i, false);

                           _invalidCommChannels.Insert(i, false);
                           _invalidLineOfServices.Insert(i, false);
                           notes.Append(ERROR_MESSAGE.SOURCE_SYSTEM_ID);
                           isInvalidRecord = true;

                       }
                       //if source system CODE is empty 
                       //bool ttest = invalidSourceSystemCDsFromDL.Contains(record.arc_srcsys_cd);
                       if (isEmptySourceSystemCode && (!isEmptySourceSystemId && record.cnst_srcsys_id.Length>0))
                       {
                           _invalidSourceSystemCode.Insert(i, true);

                           _invalidSourceSystemId.Insert(i, false);

                           _invalidPrefix.Insert(i, false);
                           _invalidFirstName.Insert(i, false);
                           _invalidMiddleName.Insert(i, false);
                           _invalidLastName.Insert(i, false);
                           _invalidSuffix.Insert(i, false);
                           _invalidOrgName.Insert(i, false);
                           _invalidAddressLine1.Insert(i, false);
                           _invalidAddressLine2.Insert(i, false);
                           _invalidCity.Insert(i, false);
                           _invalidState.Insert(i, false);
                           _invalidZip.Insert(i, false);
                           _invalidPhoneNum.Insert(i, false);
                           _invalidEmailAddress.Insert(i, false);
                           _invalidCommChannels.Insert(i, false);
                           _invalidLineOfServices.Insert(i, false);
                           _invalidMasterId.Insert(i, false);
                           notes.Append(ERROR_MESSAGE.SOURCE_SYSTEM_CODE);
                           isInvalidRecord = true;
                       }

                     
                   }

                

                  
               }
            
               if (isInvalidRecord)
               {
                   if (invalidMasterIdsFromDL.Contains(record.init_mstr_id))
                   {
                       _invalidMasterId.Insert(i, true);
                       notes.Append(ERROR_MESSAGE.MASTER_ID);
                      // isInvalidRecord = true;
                   }

                   if (invalidCommChannelsDL.Contains(record.comm_chan) || isEmptyCommChannel)
                   {
                       _invalidCommChannels.Insert(i, true);
                       notes.Append(ERROR_MESSAGE.COMMUNICATION_CHANNEL);
                   }

                   if (invalidLineOfServicesDL.Contains(record.line_of_service_cd) || isEmptyLOS)
                   {
                       _invalidLineOfServices.Insert(i, true);
                       notes.Append(ERROR_MESSAGE.LINE_OF_SERVICE_CODE);
                   }
                   if (invalidSourceSystemIdsFromDL.Contains(record.cnst_srcsys_id))
                   {
                       _invalidSourceSystemId.Insert(i, true);
                       notes.Append(ERROR_MESSAGE.SOURCE_SYSTEM_ID);
                      
                   }
                   if (invalidSourceSystemCDsFromDL.Contains(record.arc_srcsys_cd))
                   {
                       _invalidSourceSystemCode.Insert(i, true);
                       notes.Append(ERROR_MESSAGE.SOURCE_SYSTEM_CODE);
                   }
               
                   
               }
               else
               {
                   bool masterIsWorng  = false;
                   bool lineOfServiceIsWrong = false;
                   bool commChanIsWrong = false;
                   bool sourceSystemIdIsWrong = false;
                   bool sourceSystemCdIsWrong = false;

                   if (invalidMasterIdsFromDL.Contains(record.init_mstr_id))
                   {
                       masterIsWorng = true;
                       notes.Append(ERROR_MESSAGE.MASTER_ID);
                       isInvalidRecord = true;
                   }

                   if (invalidCommChannelsDL.Contains(record.comm_chan) || isEmptyCommChannel)
                   {
                       commChanIsWrong = true;
                       notes.Append(ERROR_MESSAGE.COMMUNICATION_CHANNEL);
                       isInvalidRecord = true;
                   }

                   if (invalidLineOfServicesDL.Contains(record.line_of_service_cd) || isEmptyLOS)
                   {
                       lineOfServiceIsWrong = true;
                       notes.Append(ERROR_MESSAGE.LINE_OF_SERVICE_CODE);
                       isInvalidRecord = true;
                   }

                   if (invalidSourceSystemIdsFromDL.Contains(record.cnst_srcsys_id))
                   {
                       sourceSystemIdIsWrong = true;
                       notes.Append(ERROR_MESSAGE.SOURCE_SYSTEM_ID);
                       isInvalidRecord = true;
                   }
                   if (invalidSourceSystemCDsFromDL.Contains(record.arc_srcsys_cd))
                   {
                       sourceSystemCdIsWrong = true;
                       notes.Append(ERROR_MESSAGE.SOURCE_SYSTEM_CODE);
                       isInvalidRecord = true;
                   }


                   if (isInvalidRecord)
                   {
                       if (masterIsWorng)
                       {
                           _invalidMasterId.Insert(i, true);
                       }
                       else
                       {
                           _invalidMasterId.Insert(i, false);
                       }

                       if (sourceSystemCdIsWrong)
                       {
                           _invalidSourceSystemCode.Insert(i, true);
                       }
                       else
                       {
                           _invalidSourceSystemCode.Insert(i, false);
                       }

                       if (sourceSystemIdIsWrong)
                       {
                           _invalidSourceSystemId.Insert(i, true);
                       }
                       else
                       {
                           _invalidSourceSystemId.Insert(i, false);
                       }

                       _invalidPrefix.Insert(i, false);
                       _invalidFirstName.Insert(i, false);
                       _invalidMiddleName.Insert(i, false);
                       _invalidLastName.Insert(i, false);
                       _invalidSuffix.Insert(i, false);
                       _invalidOrgName.Insert(i, false);
                       _invalidAddressLine1.Insert(i, false);
                       _invalidAddressLine2.Insert(i, false);
                       _invalidCity.Insert(i, false);
                       _invalidState.Insert(i, false);
                       _invalidZip.Insert(i, false);
                       _invalidPhoneNum.Insert(i, false);
                       _invalidEmailAddress.Insert(i, false);
                       if (commChanIsWrong) 
                       {
                           _invalidCommChannels.Insert(i, true);
                       }
                       else
                       {
                           _invalidCommChannels.Insert(i, false);
                       }
                       if (lineOfServiceIsWrong)
                       {
                           _invalidLineOfServices.Insert(i, true);
                       }
                       else
                       {
                           _invalidLineOfServices.Insert(i, false);
                       }
                      // dncValidateOP.dncInvalidList.Add(record);
                       //i++;
                   }
                 
               }


               if (isInvalidRecord)
               {
                   record.msg = notes.ToString();
                   dncValidateOP.dncInvalidList.Add(record);                  
                   i++;
               }


             
           }// end of foreach
   
        
         
            // one assumption that has been made is that the list will be stored in the order of adding of elements
           dncValidateOP._invalidFirstName = _invalidFirstName;
           dncValidateOP._invalidLastName = _invalidLastName;
           dncValidateOP._invalidMiddleName = _invalidMiddleName;
           dncValidateOP._invalidPrefix = _invalidPrefix;
           dncValidateOP._invalidSuffix = _invalidSuffix;
           dncValidateOP._invalidOrgName = _invalidOrgName;

           dncValidateOP._invalidAddressLine1 = _invalidAddressLine1;
           dncValidateOP._invaldAddressLine2 = _invalidAddressLine2;
           dncValidateOP._invalidCity = _invalidCity;
           dncValidateOP._invalidState = _invalidState;
           dncValidateOP._invalidZip = _invalidZip;
           dncValidateOP._invalidEmailAddresses = _invalidEmailAddress;
           dncValidateOP._invalidPhoneNumber = _invalidPhoneNum;

           dncValidateOP._invalidSourceSystemCode = _invalidSourceSystemCode;
           dncValidateOP._invalidSourceSystemId = _invalidSourceSystemId;
           dncValidateOP._invalidMasterIds = _invalidMasterId;

           dncValidateOP._invalidLineOfServiceCodes = _invalidLineOfServices;
           dncValidateOP._invalidCommChannels = _invalidCommChannels;

           dncValidateOP.dncValidList = listDncInputRecords.Except(dncValidateOP.dncInvalidList).ToList();


           dncValidateOP.dncUploadedFileOutputInfo = new ARC.Donor.Business.Upload.DncUploadedFileInfo()
           {
               fileExtension = dncValidateIP.uploadedFileExtension,
               fileName = dncValidateIP.uploadedFileName,
               intFileSize = dncValidateIP.uploadedFileSize
           };

            return dncValidateOP;

        }


        private async Task<IList<string>> emptyValidateDetails()
        {
            return new List<string>();
        }



    }



    
}
