using ARC.Donor.Business.Upload;
using ARC.Utility.Extensions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ARC.Donor.Service.Upload
{
    public class MsgPrefValidation
    {
        public MsgPrefValidationOutput validateMsgPrefRecords(MsgPrefValidationInput msgPrefValidateIP, ListMsgPrefUploadInput listMsgPrefUploadInput)
        {
            List<MsgPrefUploadParams> listMsgPrefInputRecords = listMsgPrefUploadInput.msgPrefUploadInputList;
            MsgPrefValidationOutput msgPrefValidateOP = new MsgPrefValidationOutput();
            //1. mandatory fields should not be empty
            List<MsgPrefUploadParams> _invalidmsgPrefRecords = new List<MsgPrefUploadParams>();

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
            List<bool> _invalidLineOfService = new List<bool>();
            List<bool> _invalidCommChannel = new List<bool>();
            
            List<bool> _invalidFirstName = new List<bool>();
            List<bool> _invalidLastName = new List<bool>();
            List<bool> _invalidMiddleName = new List<bool>();
            List<bool> _invalidSuffix = new List<bool>();
            List<bool> _invalidPrefix = new List<bool>();
            List<bool> _invalidOrgName = new List<bool>();


            List<bool> _invalidMsgPrefType = new List<bool>();
            List<bool> _invalidMsgPrefValue = new List<bool>();

            

            

            //invalid masters from data layer
            List<string> invalidMasterIdsFromDL = null;
            List<string> invalidSourceSystemCDsFromDL = null;
            List<string> invalidSourceSystemIdsFromDL = null;
           // List<string> invalidCommChannelsDL = null;
           // List<string> invalidLineOfServicesDL = null;
           // List<string> invalidMsgPrefTypes = null;
           // List<string> invalidMsgPrefValues = null;

            List<MsgPrefUploadParams> tempValidList1 = listMsgPrefUploadInput.msgPrefUploadInputList;

            MsgPrefValidationInput newMsgPrefValidationInput1 = new MsgPrefValidationInput();
            newMsgPrefValidationInput1._sourceSystemCode = tempValidList1.Select(x => x.arc_srcsys_cd).ToList();
            newMsgPrefValidationInput1._sourceSystemId = tempValidList1.Select(x => x.cnst_srcsys_id).ToList();
           // newMsgPrefValidationInput1._lineOfServiceCodes = tempValidList1.Select(x => x.line_of_service_cd).ToList();
           // newMsgPrefValidationInput1._commChannels = tempValidList1.Select(x => x.comm_chan).ToList();
           // newMsgPrefValidationInput1._msgPrefTypes = tempValidList1.Select(x => x.msg_prefc_typ).ToList();
            //newMsgPrefValidationInput1._msgPrefValues = tempValidList1.Select(x => x.msg_prefc_val).ToList();

            Mapper.CreateMap<Business.Upload.MsgPrefValidationInput, Donor.Data.Entities.Upload.MsgPrefValidationInput>();

            var _input1 = Mapper.Map<Business.Upload.MsgPrefValidationInput, Donor.Data.Entities.Upload.MsgPrefValidationInput>(newMsgPrefValidationInput1);

            Data.Upload.MsgPrefUploadDetails gd = new Data.Upload.MsgPrefUploadDetails();


            // source system codes
            var _validSourceSystemCDsTask = Task.Factory.StartNew(() => emptyValidateDetails());
            if ((newMsgPrefValidationInput1._sourceSystemCode != null || newMsgPrefValidationInput1._sourceSystemCode.Count != 0))
            {
                _validSourceSystemCDsTask = Task.Factory.StartNew(() => gd.validateSourceSystemCodes(_input1));

            }
            // end of source system codes

            // source system Ids
            var _validSourceSystemIdsTask = Task.Factory.StartNew(() => emptyValidateDetails());
            if ((newMsgPrefValidationInput1._sourceSystemId != null || newMsgPrefValidationInput1._sourceSystemId.Count != 0))
            {
                _validSourceSystemIdsTask = Task.Factory.StartNew(() => gd.validateSourceSystemIds(_input1));

            }

            int dummy = 0;
            //temp valid list
            List<MsgPrefUploadParams> tempValidList2 = listMsgPrefUploadInput.msgPrefUploadInputList.
                 Intersect(listMsgPrefUploadInput.msgPrefUploadInputList.Where(y => y.init_mstr_id.Length > 0 && int.TryParse(y.init_mstr_id, out dummy))).ToList();
            MsgPrefValidationInput newMsgPrefValidationInput2 = new MsgPrefValidationInput();

            newMsgPrefValidationInput2._masterIds = tempValidList2.Select(x => x.init_mstr_id).ToList();

            var _input2 = Mapper.Map<Business.Upload.MsgPrefValidationInput, Donor.Data.Entities.Upload.MsgPrefValidationInput>(newMsgPrefValidationInput2);

            var _validMasterIdsTask = Task.Factory.StartNew(() => emptyValidateDetails());
            if (newMsgPrefValidationInput2._masterIds != null || newMsgPrefValidationInput2._masterIds.Count != 0)
            {
                if (!newMsgPrefValidationInput2._masterIds.All(x => x.Equals("")))
                {
                    _validMasterIdsTask = Task.Factory.StartNew(() => gd.validateMasterIdDetails(_input2));
                }
            }

           
            
            IList<MsgPrefRefCode> _validMsgPrefRefCodes = null;          
            var accList = gd.getMsgPrefRefCodes();
            Mapper.CreateMap<Donor.Data.Entities.Upload.MsgPrefRefCode, Business.Upload.MsgPrefRefCode>();
            _validMsgPrefRefCodes = Mapper.Map<IList<Donor.Data.Entities.Upload.MsgPrefRefCode>, IList<Business.Upload.MsgPrefRefCode>>(accList);
            

            Task.WaitAll(_validSourceSystemIdsTask, _validSourceSystemCDsTask, _validMasterIdsTask);


            //process source system codes
            var _validSourceSystemCDs = _validSourceSystemCDsTask.Result;
            if (_validSourceSystemCDs != null)
            {
                //if(_validSourceSystemCDs.Result!=null){
                invalidSourceSystemCDsFromDL =
               newMsgPrefValidationInput1._sourceSystemCode.Where(x => !string.IsNullOrEmpty(x) && !_validSourceSystemCDs.Result.Select(y => y).Contains(x)).ToList();
                //}

            }

            //process source system ids
            var _validSourceSystemIds = _validSourceSystemIdsTask.Result;
            if (_validSourceSystemIds != null)
            {
                //if(_validSourceSystemIds.Result!=null){
                invalidSourceSystemIdsFromDL =
               newMsgPrefValidationInput1._sourceSystemId.Where(x => !string.IsNullOrEmpty(x) && !_validSourceSystemIds.Result.Select(y => y).Contains(x)).ToList();
                //}

            }

            //process master ids
            var _validMasterIds = _validMasterIdsTask.Result;
            if (_validMasterIds != null)
            {
                if (_validMasterIds.Result != null)
                {
                    invalidMasterIdsFromDL =
                    newMsgPrefValidationInput2._masterIds.Where(x => !string.IsNullOrEmpty(x) && !_validMasterIds.Result.Select(y => y).Contains(x)).ToList();
                }

            }


            int i = 0;
            foreach (MsgPrefUploadParams record in listMsgPrefInputRecords)
            {

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
                int resultNotNeeded = 0;
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
                bool isEmptyMsgPrefType = record.msg_prefc_typ.Equals("");
                bool isEmptyMsgPrefValue = record.msg_prefc_val.Equals("");
                StringBuilder notes = new StringBuilder();

                bool isInvalidRecord = false;
               // _invalidAddressLine1.Replace(i, false);
                if (isEmptyAddressLine1 && isEmptyAddressLine2 && isEmptyCity && isEmptyState && isEmptyZip && isEmptyEmailAddress && isEmptyPhoneNum && isEmptyMasterId &&
                    isEmptySourceSystemCode && isEmptySourceSystemId && isEmptyFirstName && isEmptyLastName && isEmptyMiddleName && isEmptyPrefix && isEmptySuffix && isEmptyOrgName)
                {
                    _invalidPrefix.InsertOrReplace(i, true);
                    _invalidFirstName.InsertOrReplace(i, true);
                    _invalidMiddleName.InsertOrReplace(i, true);
                    _invalidLastName.InsertOrReplace(i, true);
                    _invalidSuffix.InsertOrReplace(i, true);

                    _invalidOrgName.InsertOrReplace(i, true);
                    _invalidAddressLine1.InsertOrReplace(i, true);
                    _invalidAddressLine2.InsertOrReplace(i, true);
                    _invalidCity.InsertOrReplace(i, true);
                    _invalidState.InsertOrReplace(i, true);
                    _invalidZip.InsertOrReplace(i, true);
                    _invalidPhoneNum.InsertOrReplace(i, true);
                    _invalidEmailAddress.InsertOrReplace(i, true);
                    _invalidSourceSystemCode.InsertOrReplace(i, true);
                    _invalidSourceSystemId.InsertOrReplace(i, true);
                    _invalidMasterId.InsertOrReplace(i, true);

                    notes.AppendLine(ERROR_MESSAGE.EMPTY_FIELDS);

                    isInvalidRecord = true;
                }
                else if ((!isEmptyFirstName || !isEmptyLastName || !isEmptyMiddleName || !isEmptyPrefix || !isEmptySuffix) && !isEmptyOrgName)
                {

                    _invalidFirstName.InsertOrReplace(i, true);
                    _invalidLastName.InsertOrReplace(i, true);
                    _invalidMiddleName.InsertOrReplace(i, true);
                    _invalidPrefix.InsertOrReplace(i, true);
                    _invalidSuffix.InsertOrReplace(i, true);
                    _invalidOrgName.InsertOrReplace(i, true);

                    _invalidAddressLine1.InsertOrReplace(i, false);
                    _invalidAddressLine2.InsertOrReplace(i, false);
                    _invalidCity.InsertOrReplace(i, false);
                    _invalidState.InsertOrReplace(i, false);
                    _invalidZip.InsertOrReplace(i, false);
                    _invalidPhoneNum.InsertOrReplace(i, false);
                    _invalidEmailAddress.InsertOrReplace(i, false);
                    _invalidSourceSystemCode.InsertOrReplace(i, false);
                    _invalidSourceSystemId.InsertOrReplace(i, false);
                    _invalidMasterId.InsertOrReplace(i, false);

                    notes.Append(ERROR_MESSAGE.NAME_ORG);

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
                            _invalidFirstName.InsertOrReplace(i, false);
                            _invalidLastName.InsertOrReplace(i, false);
                            _invalidMiddleName.InsertOrReplace(i, false);
                            _invalidPrefix.InsertOrReplace(i, false);
                            _invalidSuffix.InsertOrReplace(i, false);
                            _invalidOrgName.InsertOrReplace(i, false);
                            _invalidAddressLine1.InsertOrReplace(i, false);
                            _invalidAddressLine2.InsertOrReplace(i, false);
                            _invalidCity.InsertOrReplace(i, false);
                            _invalidState.InsertOrReplace(i, false);
                            _invalidZip.InsertOrReplace(i, false);
                            _invalidPhoneNum.InsertOrReplace(i, false);
                            _invalidEmailAddress.InsertOrReplace(i, false);
                            _invalidSourceSystemCode.InsertOrReplace(i, false);
                            _invalidSourceSystemId.InsertOrReplace(i, false);
                            _invalidCommChannel.InsertOrReplace(i, false);
                            _invalidLineOfService.InsertOrReplace(i, false);
                            _invalidMsgPrefType.InsertOrReplace(i, false);
                            _invalidMsgPrefValue.InsertOrReplace(i, false);
                            _invalidMasterId.InsertOrReplace(i, true);
                            notes.AppendLine(ERROR_MESSAGE.MASTER_ID);
                            isInvalidRecord = true;

                        }

                        //if source system ID is empty
                        if (!isEmptySourceSystemCode && isEmptySourceSystemId)
                        {
                            _invalidSourceSystemId.Insert(i, true);

                            _invalidSourceSystemCode.Insert(i, false);

                            _invalidFirstName.InsertOrReplace(i, false);
                            _invalidLastName.InsertOrReplace(i, false);
                            _invalidMiddleName.InsertOrReplace(i, false);
                            _invalidPrefix.InsertOrReplace(i, false);
                            _invalidSuffix.InsertOrReplace(i, false);
                            _invalidOrgName.InsertOrReplace(i, false);

                            _invalidAddressLine1.InsertOrReplace(i, false);
                            _invalidAddressLine2.InsertOrReplace(i, false);
                            _invalidCity.InsertOrReplace(i, false);
                            _invalidState.InsertOrReplace(i, false);
                            _invalidZip.InsertOrReplace(i, false);
                            _invalidPhoneNum.InsertOrReplace(i, false);
                            _invalidEmailAddress.InsertOrReplace(i, false);
                            _invalidCommChannel.InsertOrReplace(i, false);
                            _invalidLineOfService.InsertOrReplace(i, false);
                            _invalidMsgPrefType.InsertOrReplace(i, false);
                            _invalidMsgPrefValue.InsertOrReplace(i, false);
                            _invalidMasterId.InsertOrReplace(i, false);


                            notes.AppendLine(ERROR_MESSAGE.SOURCE_SYSTEM_ID);
                            isInvalidRecord = true;

                        }
                        //if source system CODE is empty 
                        //bool ttest = invalidSourceSystemCDsFromDL.Contains(record.arc_srcsys_cd);
                        if (isEmptySourceSystemCode && !isEmptySourceSystemId)
                        {
                            _invalidSourceSystemCode.InsertOrReplace(i, true);

                            _invalidSourceSystemId.InsertOrReplace(i, false);

                            _invalidFirstName.InsertOrReplace(i, false);
                            _invalidLastName.InsertOrReplace(i, false);
                            _invalidMiddleName.InsertOrReplace(i, false);
                            _invalidPrefix.InsertOrReplace(i, false);
                            _invalidSuffix.InsertOrReplace(i, false);
                            _invalidOrgName.InsertOrReplace(i, false);

                            _invalidAddressLine1.InsertOrReplace(i, false);
                            _invalidAddressLine2.InsertOrReplace(i, false);
                            _invalidCity.InsertOrReplace(i, false);
                            _invalidState.InsertOrReplace(i, false);
                            _invalidZip.InsertOrReplace(i, false);
                            _invalidPhoneNum.InsertOrReplace(i, false);
                            _invalidEmailAddress.InsertOrReplace(i, false);
                            _invalidCommChannel.InsertOrReplace(i, false);
                            _invalidLineOfService.InsertOrReplace(i, false);
                            _invalidMsgPrefType.InsertOrReplace(i, false);
                            _invalidMsgPrefValue.InsertOrReplace(i, false);

                            _invalidMasterId.InsertOrReplace(i, false);

                            notes.AppendLine(ERROR_MESSAGE.SOURCE_SYSTEM_CODE);
                            isInvalidRecord = true;
                        }

                    }
                    else
                    {
                        bool masterIdIsSet = false;
                        if (!isRightMasterId && record.init_mstr_id.Length > 0)
                        {

                            _invalidFirstName.InsertOrReplace(i, false);
                            _invalidLastName.InsertOrReplace(i, false);
                            _invalidMiddleName.InsertOrReplace(i, false);
                            _invalidPrefix.InsertOrReplace(i, false);
                            _invalidSuffix.InsertOrReplace(i, false);
                            _invalidOrgName.InsertOrReplace(i, false);

                            _invalidAddressLine1.InsertOrReplace(i, false);
                            _invalidAddressLine2.InsertOrReplace(i, false);
                            _invalidCity.InsertOrReplace(i, false);
                            _invalidState.InsertOrReplace(i, false);
                            _invalidZip.InsertOrReplace(i, false);
                            _invalidPhoneNum.InsertOrReplace(i, false);
                            _invalidEmailAddress.InsertOrReplace(i, false);
                            _invalidSourceSystemCode.InsertOrReplace(i, false);
                            _invalidSourceSystemId.InsertOrReplace(i, false);
                            _invalidCommChannel.InsertOrReplace(i, false);
                            _invalidLineOfService.InsertOrReplace(i, false);
                            _invalidMsgPrefType.InsertOrReplace(i, false);
                            _invalidMsgPrefValue.InsertOrReplace(i, false);

                            _invalidMasterId.Insert(i, true);
                            masterIdIsSet = true;
                            notes.AppendLine(ERROR_MESSAGE.MASTER_ID);
                            isInvalidRecord = true;

                        }

                        //if source system ID is empty
                        if ((!isEmptySourceSystemCode && record.arc_srcsys_cd.Length > 0) && isEmptySourceSystemId)
                        {
                            _invalidSourceSystemId.InsertOrReplace(i, true);

                            _invalidSourceSystemCode.InsertOrReplace(i, false);

                            _invalidFirstName.InsertOrReplace(i, false);
                            _invalidLastName.InsertOrReplace(i, false);
                            _invalidMiddleName.InsertOrReplace(i, false);
                            _invalidPrefix.InsertOrReplace(i, false);
                            _invalidSuffix.InsertOrReplace(i, false);
                            _invalidOrgName.InsertOrReplace(i, false);

                            _invalidAddressLine1.InsertOrReplace(i, false);
                            _invalidAddressLine2.InsertOrReplace(i, false);

                            _invalidCity.InsertOrReplace(i, false);
                            _invalidState.InsertOrReplace(i, false);
                            _invalidZip.InsertOrReplace(i, false);
                            _invalidPhoneNum.InsertOrReplace(i, false);
                            _invalidEmailAddress.InsertOrReplace(i, false);
                            _invalidCommChannel.InsertOrReplace(i, false);
                            _invalidLineOfService.InsertOrReplace(i, false);
                            _invalidMsgPrefType.InsertOrReplace(i, false);
                            _invalidMsgPrefValue.InsertOrReplace(i, false);
                            if (masterIdIsSet)
                                _invalidMasterId.InsertOrReplace(i, true);
                            else
                                _invalidMasterId.InsertOrReplace(i, false);


                            notes.AppendLine(ERROR_MESSAGE.SOURCE_SYSTEM_ID);
                            isInvalidRecord = true;

                        }
                        //if source system CODE is empty 
                        //bool ttest = invalidSourceSystemCDsFromDL.Contains(record.arc_srcsys_cd);
                        if (isEmptySourceSystemCode && (!isEmptySourceSystemId && record.cnst_srcsys_id.Length > 0))
                        {
                            _invalidSourceSystemCode.InsertOrReplace(i, true);

                            _invalidSourceSystemId.InsertOrReplace(i, false);

                            _invalidFirstName.InsertOrReplace(i, false);
                            _invalidLastName.InsertOrReplace(i, false);
                            _invalidMiddleName.InsertOrReplace(i, false);
                            _invalidPrefix.InsertOrReplace(i, false);
                            _invalidSuffix.InsertOrReplace(i, false);
                            _invalidOrgName.InsertOrReplace(i, false);

                            _invalidAddressLine1.InsertOrReplace(i, false);
                            _invalidAddressLine2.InsertOrReplace(i, false);
                            _invalidCity.InsertOrReplace(i, false);
                            _invalidState.InsertOrReplace(i, false);
                            _invalidZip.InsertOrReplace(i, false);
                            _invalidPhoneNum.InsertOrReplace(i, false);
                            _invalidEmailAddress.InsertOrReplace(i, false);
                            _invalidCommChannel.InsertOrReplace(i, false);
                            _invalidLineOfService.InsertOrReplace(i, false);
                            _invalidMsgPrefType.InsertOrReplace(i, false);
                            _invalidMsgPrefValue.InsertOrReplace(i, false);

                            _invalidMasterId.InsertOrReplace(i, false);
                            notes.AppendLine(ERROR_MESSAGE.SOURCE_SYSTEM_CODE);
                            isInvalidRecord = true;
                        }

                    }

                }


                // check if 4 mandatory columns are present
                if (isEmptyCommChannel)
                {
                    _invalidCommChannel.Insert(i, true);
                    notes.Append(ERROR_MESSAGE.COMMUNICATION_CHANNEL);
                }
                else
                {
                    _invalidCommChannel.Insert(i, false);
                }
                if (isEmptyLOS)
                {
                    _invalidLineOfService.Insert(i, true);
                    notes.Append(ERROR_MESSAGE.LINE_OF_SERVICE_CODE);
                }
                else
                {
                    _invalidLineOfService.Insert(i, false);
                }
                if (isEmptyMsgPrefType)
                {
                    _invalidMsgPrefType.Insert(i, true);
                    notes.Append(ERROR_MESSAGE.MESSAGE_PREF_TYPE);
                }
                else
                {
                    _invalidMsgPrefType.Insert(i, false);
                }
                if (isEmptyMsgPrefValue)
                {
                    _invalidMsgPrefValue.Insert(i, true);
                    notes.Append(ERROR_MESSAGE.MESSAGE_PREF_VALUE);
                }
                else
                {
                    _invalidMsgPrefValue.Insert(i, false);
                }
                   

                if (isEmptyMsgPrefType || isEmptyMsgPrefValue || isEmptyLOS || isEmptyCommChannel)
                {

                    // check if value already exists
                    if (!_invalidFirstName.ElementAtOrDefault(i))
                    {
                        _invalidFirstName.InsertOrReplace(i, false);
                    }
                    if (!_invalidMiddleName.ElementAtOrDefault(i))
                    {
                        _invalidMiddleName.InsertOrReplace(i, false);
                    }
                    if (!_invalidLastName.ElementAtOrDefault(i))
                    {
                        _invalidLastName.InsertOrReplace(i, false);
                    }
                    if (!_invalidPrefix.ElementAtOrDefault(i))
                    {
                        _invalidPrefix.InsertOrReplace(i, false);
                    }
                    if (!_invalidSuffix.ElementAtOrDefault(i))
                    {
                        _invalidSuffix.InsertOrReplace(i, false);
                    }
                    if (!_invalidOrgName.ElementAtOrDefault(i))
                    {
                        _invalidOrgName.InsertOrReplace(i, false);
                    }

                    if (!_invalidAddressLine1.ElementAtOrDefault(i))
                    {
                        _invalidAddressLine1.InsertOrReplace(i, false);
                    }
                    if (!_invalidAddressLine2.ElementAtOrDefault(i))
                    {
                        _invalidAddressLine2.InsertOrReplace(i, false);
                    }
                    if (!_invalidCity.ElementAtOrDefault(i))
                    {
                        _invalidCity.InsertOrReplace(i, false);
                    }
                    if (!_invalidState.ElementAtOrDefault(i))
                    {
                        _invalidState.InsertOrReplace(i, false);
                    }
                    if (!_invalidZip.ElementAtOrDefault(i))
                    {
                        _invalidZip.InsertOrReplace(i, false);
                    }
                    if (!_invalidPhoneNum.ElementAtOrDefault(i))
                    {
                        _invalidPhoneNum.InsertOrReplace(i, false);
                    }
                    if (!_invalidEmailAddress.ElementAtOrDefault(i))
                    {
                        _invalidEmailAddress.InsertOrReplace(i, false);
                    }
                    if (!_invalidSourceSystemCode.ElementAtOrDefault(i))
                    {
                        _invalidSourceSystemCode.InsertOrReplace(i, false);
                    }
                    if (!_invalidSourceSystemId.ElementAtOrDefault(i))
                    {
                        _invalidSourceSystemId.InsertOrReplace(i, false);
                    }

                    _invalidMasterId.InsertOrReplace(i, false);

                    isInvalidRecord = true;
                }

                // checking for 4 mandatory fields
                else
                {
                    //check if the combination of msg pref val , msg pref type , comm channel , line of service exists. if not mark it as false
                    var msgPrefCombo = _validMsgPrefRefCodes.Any(x => x.comm_chan.ToLower() == record.comm_chan.ToLower() && x.line_of_service_cd.ToLower() == record.line_of_service_cd.ToLower() &&
                        x.msg_prefc_typ.ToLower() == record.msg_prefc_typ.ToLower() && x.msg_prefc_val.ToLower() == record.msg_prefc_val.ToLower());

                    if (!msgPrefCombo)
                    {
                        if (_invalidCommChannel.Count > 0)
                        {
                            _invalidCommChannel.InsertOrReplace(i, true);
                        }
                        if (_invalidLineOfService.Count > 0)
                        {
                           _invalidLineOfService.InsertOrReplace(i, true);
                        }
                        if (_invalidMsgPrefType.Count>0)
                        {
                            _invalidMsgPrefType.InsertOrReplace(i, true);
                        }
                        if (_invalidMsgPrefValue.Count > 0)
                        {
                           _invalidMsgPrefValue.InsertOrReplace(i, true);
                        }

                        // check if value already exists
                        if (!_invalidFirstName.ElementAtOrDefault(i))
                        {
                            _invalidFirstName.InsertOrReplace(i, false);
                        }
                        if (!_invalidMiddleName.ElementAtOrDefault(i))
                        {
                            _invalidMiddleName.InsertOrReplace(i, false);
                        }
                        if (!_invalidLastName.ElementAtOrDefault(i))
                        {
                            _invalidLastName.InsertOrReplace(i, false);
                        }
                        if (!_invalidPrefix.ElementAtOrDefault(i))
                        {
                            _invalidPrefix.InsertOrReplace(i, false);
                        }
                        if (!_invalidSuffix.ElementAtOrDefault(i))
                        {
                            _invalidSuffix.InsertOrReplace(i, false);
                        }
                        if (!_invalidOrgName.ElementAtOrDefault(i))
                        {
                            _invalidOrgName.InsertOrReplace(i, false);
                        }

                        if (!_invalidAddressLine1.ElementAtOrDefault(i))
                        {
                            _invalidAddressLine1.InsertOrReplace(i, false);
                        }
                        if (!_invalidAddressLine2.ElementAtOrDefault(i))
                        {
                            _invalidAddressLine2.InsertOrReplace(i, false);
                        }
                        if (!_invalidCity.ElementAtOrDefault(i))
                        {
                            _invalidCity.InsertOrReplace(i, false);
                        }
                        if (!_invalidState.ElementAtOrDefault(i))
                        {
                            _invalidState.InsertOrReplace(i, false);
                        }
                        if (!_invalidZip.ElementAtOrDefault(i))
                        {
                            _invalidZip.InsertOrReplace(i, false);
                        }
                        if (!_invalidPhoneNum.ElementAtOrDefault(i))
                        {
                            _invalidPhoneNum.InsertOrReplace(i, false);
                        }
                        if (!_invalidEmailAddress.ElementAtOrDefault(i))
                        {
                            _invalidEmailAddress.InsertOrReplace(i, false);
                        }
                        if (!_invalidSourceSystemCode.ElementAtOrDefault(i))
                        {
                            _invalidSourceSystemCode.InsertOrReplace(i, false);
                        }
                        if (!_invalidSourceSystemId.ElementAtOrDefault(i))
                        {
                            _invalidSourceSystemId.InsertOrReplace(i, false);
                        }
                       
                        _invalidMasterId.InsertOrReplace(i, false);
                        notes.AppendLine(ERROR_MESSAGE.MESSAGE_PREF_COMBINATION);
                        isInvalidRecord = true;
                    }
                }
                                    

              if (isInvalidRecord)
                {
                    if (invalidMasterIdsFromDL.Contains(record.init_mstr_id))
                    {
                        notes.AppendLine(ERROR_MESSAGE.MASTER_ID);
                        _invalidMasterId.InsertOrReplace(i, true);                      
                    }
                    if (invalidSourceSystemIdsFromDL.Contains(record.cnst_srcsys_id))
                    {
                        notes.AppendLine(ERROR_MESSAGE.SOURCE_SYSTEM_ID);
                        _invalidSourceSystemId.InsertOrReplace(i, true);

                    }
                    if (invalidSourceSystemCDsFromDL.Contains(record.arc_srcsys_cd))
                    {
                        notes.AppendLine(ERROR_MESSAGE.SOURCE_SYSTEM_CODE);
                        _invalidSourceSystemCode.InsertOrReplace(i, true);
                    }


                }
                else
                {
                    bool masterIsWorng = false;
                   // bool lineOfServiceIsWrong = false;
                    //bool commChanIsWrong = false;
                    bool sourceSystemIdIsWrong = false;
                    bool sourceSystemCdIsWrong = false;

                    if (invalidMasterIdsFromDL.Contains(record.init_mstr_id))
                    {
                        masterIsWorng = true;
                        notes.AppendLine(ERROR_MESSAGE.MASTER_ID);
                        isInvalidRecord = true;
                    }

                  

                    if (invalidSourceSystemIdsFromDL.Contains(record.cnst_srcsys_id))
                    {
                        sourceSystemIdIsWrong = true;
                        notes.AppendLine(ERROR_MESSAGE.SOURCE_SYSTEM_ID);
                        isInvalidRecord = true;
                    }
                    if (invalidSourceSystemCDsFromDL.Contains(record.arc_srcsys_cd))
                    {
                        sourceSystemCdIsWrong = true;
                        notes.AppendLine(ERROR_MESSAGE.SOURCE_SYSTEM_CODE);
                        isInvalidRecord = true;
                    }


                    if (isInvalidRecord)
                    {
                        if (masterIsWorng)
                        {
                            _invalidMasterId.InsertOrReplace(i, true);
                        }
                        else
                        {
                            _invalidMasterId.InsertOrReplace(i, false);
                        }

                        if (sourceSystemCdIsWrong)
                        {
                            _invalidSourceSystemCode.InsertOrReplace(i, true);
                        }
                        else
                        {
                            _invalidSourceSystemCode.InsertOrReplace(i, false);
                        }

                        if (sourceSystemIdIsWrong)
                        {
                            _invalidSourceSystemId.InsertOrReplace(i, true);
                        }
                        else
                        {
                            _invalidSourceSystemId.InsertOrReplace(i, false);
                        }

                        _invalidFirstName.InsertOrReplace(i, false);
                        _invalidLastName.InsertOrReplace(i, false);
                        _invalidMiddleName.InsertOrReplace(i, false);
                        _invalidPrefix.InsertOrReplace(i, false);
                        _invalidSuffix.InsertOrReplace(i, false);
                        _invalidOrgName.InsertOrReplace(i, false);

                        _invalidAddressLine1.InsertOrReplace(i, false);
                        _invalidAddressLine2.InsertOrReplace(i, false);
                        _invalidCity.InsertOrReplace(i, false);
                        _invalidState.InsertOrReplace(i, false);
                        _invalidZip.InsertOrReplace(i, false);
                        _invalidPhoneNum.InsertOrReplace(i, false);
                        _invalidEmailAddress.InsertOrReplace(i, false);
                        _invalidCommChannel.InsertOrReplace(i, false);
                        _invalidLineOfService.InsertOrReplace(i, false);
                        _invalidMsgPrefType.InsertOrReplace(i, false);
                        _invalidMsgPrefValue.InsertOrReplace(i, false);
                      
                    }

                }


                if (isInvalidRecord)
                {
                    record.msg = notes.ToString();
                    msgPrefValidateOP.msgPrefInvalidList.Add(record);
                    i++;
                }



            }// end of foreach



            // one assumption that has been made is that the list will be stored in the order of adding of elements
            msgPrefValidateOP._invalidFirstName = _invalidFirstName;
            msgPrefValidateOP._invalidLastName = _invalidLastName;


            msgPrefValidateOP._invalidMiddleName = _invalidMiddleName;
            msgPrefValidateOP._invalidPrefix = _invalidPrefix;
            msgPrefValidateOP._invalidSuffix = _invalidSuffix;
            msgPrefValidateOP._invalidOrgName = _invalidOrgName;

            msgPrefValidateOP._invalidAddressLine1 = _invalidAddressLine1;
            msgPrefValidateOP._invaldAddressLine2 = _invalidAddressLine2;
            msgPrefValidateOP._invalidCity = _invalidCity;
            msgPrefValidateOP._invalidState = _invalidState;
            msgPrefValidateOP._invalidZip = _invalidZip;
            msgPrefValidateOP._invalidEmailAddresses = _invalidEmailAddress;
            msgPrefValidateOP._invalidPhoneNumber = _invalidPhoneNum;

            msgPrefValidateOP._invalidSourceSystemCode = _invalidSourceSystemCode;
            msgPrefValidateOP._invalidSourceSystemId = _invalidSourceSystemId;
            msgPrefValidateOP._invalidMasterIds = _invalidMasterId;

            msgPrefValidateOP._invalidLineOfServiceCodes = _invalidLineOfService;
            msgPrefValidateOP._invalidCommChannels = _invalidCommChannel;
            msgPrefValidateOP._invalidMsgPrefTypes = _invalidMsgPrefType;
            msgPrefValidateOP._invalidMsgPrefValues = _invalidMsgPrefValue;

            msgPrefValidateOP.msgPrefValidList = listMsgPrefInputRecords.Except(msgPrefValidateOP.msgPrefInvalidList).ToList();


            msgPrefValidateOP.msgPrefUploadedFileOutputInfo = new ARC.Donor.Business.Upload.MsgPrefUploadedFileInfo()
            {
                fileExtension = msgPrefValidateIP.uploadedFileExtension,
                fileName = msgPrefValidateIP.uploadedFileName,
                intFileSize = msgPrefValidateIP.uploadedFileSize
            };

            return msgPrefValidateOP;


        }


        private async Task<IList<string>> emptyValidateDetails()
        {
            return new List<string>();
        }

     

    }


    public static class MyExtensions 
    {


        public static void InsertOrReplace<T>(this List<T> list, int index,T item)
        {

            try
            {
                if (list.Count > 0)
                {
                    list[index] = item;
                }
                else
                {
                    list.Insert(index, item);
                }
            }
            catch(Exception e)
            {
                list.Insert(index, item);
            }

        }

    

       


    }

    
}
