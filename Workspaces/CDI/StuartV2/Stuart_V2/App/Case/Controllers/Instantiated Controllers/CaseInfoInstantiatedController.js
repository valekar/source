

function IntakeChannels(caseDropDownService) {

    caseDropDownService.getDropDown(CASE_CONSTANTS.CASE_INTAKECHANNEL).success(function (result) {
       // console.log("Intake Channel");
       // console.log(angular.toJson(result));
        return result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });

    //return [
    //    { id: '', value: '' },
    //    { id: 'RCO Contact Us', value: 'RCO Contact Us' },
    //    { id: 'Email', value: 'Email' },
    //    { id: 'White Mail', value: 'White Mail' },
    //    { id: 'Phone', value: 'Phone' },
    //    { id: 'Twitter', value: 'Twitter' },
    //    { id: 'Facebook', value: 'Facebook' },
    //    { id: 'Internal', value: 'Internal' }
    //]
}

function IntakeOwnerDepts(caseDropDownService) {
    caseDropDownService.getDropDown(CASE_CONSTANTS.CASE_INTAKEOWNERDEPT).success(function (result) {
       // console.log("Intake Owner Depts");
       // console.log(angular.toJson(result));
        return result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });


    //return [
    //{ id: '', value: '' },
    //{ id: 'Biomed Customer Care', value: 'Biomed Customer Care' },
    //{ id: 'Fundraising Donor Services', value: 'Fundraising Donor Services' },
    //{ id: 'PHSS TSC', value: 'PHSS TSC' },
    //{ id: 'Public Inquiry', value: 'Public Inquiry' },
    //{ id: 'Social Media', value: 'Social Media' },
    //{ id: 'MKT Data', value: 'MKT Data' },
    //{ id: 'IT', value: 'IT' },
    //{ id: 'SAF', value: 'SAF' }
    //]
}

function CRMSystems() {
    //return [
    //    { id: '', value: '' },
    //    { id: 'CDI Hygiene', value: 'CDI Hygiene' },
    //    { id: 'SFDC PI', value: 'SFDC PI' },
    //    { id: 'SFDC PHSS', value: 'SFDC PHSS' }
    //]
}

function CaseTypes() {
    //return [
    //    { id: '', value: '' },
    //    { id: 'Internal Matching', value: 'Internal Matching' },
    //    { id: 'Do Not Contact', value: 'Do Not Contact' },
    //    { id: 'Indentification Investigation', value: 'Identification Investigation' },
    //    { id: 'Data Hygiene', value: 'Data Hygiene' },
    //    { id: 'NCOA, Death, or Undeliverable Update', value: 'NCOA, Death, or Undeliverable Update' },
    //    { id: 'Constituent Contact Update Request', value: 'Constituent Contact Update Request' },
    //    { id: 'Escalate to CDI Team', value: 'Escalate to CDI Team' }
    //]
}

function Statuses() {
    return [
        { id: '', value: '' },
        { id: 'Open', value: 'Open' },
        { id: 'Closed', value: 'Closed' },
    ]
}

/********************* Edit Name Modal Instance ***************/
angular.module('case').controller('EditCaseInfoInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal',
    'params', 'pageName', 'CaseMultiDataService', 'CaseMultiGridService', 'uiGridConstants', 'CaseCRUDapiService', '$rootScope',
    'uibDateParser', 'CaseApiService', 'CaseCRUDoperations', 'CaseServices', 'CaseDataServices', 'caseDropDownService', function ($scope, $filter, $uibModalInstance, $uibModal,
    params, pageName, CaseMultiDataService, CaseMultiGridService, uiGridConstants, CaseCRUDapiService, $rootScope, uibDateParser, CaseApiService, CaseCRUDoperations, CaseServices, CaseDataServices, caseDropDownService) {
       // console.log(params.row);
        var row = params.row;
       // console.log("row");
      //  console.log(row);
        // editRow.row_stat_cd = "L";
        $scope.inlineOptions = {
            minDate: new Date(),
            showWeeks: true
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2050, 1, 1),
            minDate: new Date(2000, 1, 1),
            startingDay: 1
        };

        $scope.altInputFormats = ['MM/dd/yyyy', 'M/d/yyyy', 'MM/d/yyyy', 'M/dd/yyyy'];

        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();

        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };

        $scope.popup1 = {
            opened: false
        };

        $scope.formats = ['MM/dd/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
        if (row.typ_key_desc) {
            if (row.typ_key_dsc.indexOf('Merge Conflicts') > -1)
                $scope.CaseTypeState = true;
            else
                $scope.CaseTypeState = false;
        }
        else {
            $scope.CaseTypeState = false;
        }
        
        $scope.caseInfo = {
            selected: {
                intakeChannelType: row.intake_chan_value,
                intakeChannelTypeCode: '',
                intakeOwnerDeptType: row.intake_owner_dept_value,
                intakeOwnerDeptTypeCode: '',
                CRMSystemType: row.ref_src_dsc,
                CRMSystemTypeCode: '',
                CaseType: row.typ_key_dsc,
                CaseTypeCode: '',
                StatusType: row.status,
                StatusTypeCode: ''
            },
           // intakeChannels: IntakeChannels(caseDropDownService),
           // intakeOwnerDepts: IntakeOwnerDepts(),
           // CRMSystems: CRMSystems(),
           // CaseTypes: CaseTypes(),
            Statuses: Statuses(),
            caseKey: row.case_key,
            caseName: row.case_nm,
            caseDesc: row.case_desc,
            CRMSystem: row.ref_id,
            constName: row.cnst_nm,
            CaseReportDate:row.report_dt ? new Date(row.report_dt): null,
            userName: row.crtd_by_usr_id
        };

        caseDropDownService.getDropDown(CASE_CONSTANTS.CASE_INTAKECHANNEL).success(function (result) {
           // console.log("Intake Channel");
           // console.log(angular.toJson(result));
            $scope.caseInfo.intakeChannels = result;
        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });

        caseDropDownService.getDropDown(CASE_CONSTANTS.CASE_INTAKEOWNERDEPT).success(function (result) {
          //  console.log("Intake Owner Depts");
          //  console.log(angular.toJson(result));
            $scope.caseInfo.intakeOwnerDepts = result;
            //return result;
        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });

        caseDropDownService.getDropDown(CASE_CONSTANTS.CASE_CRMSYSTEM).success(function (result) {
           // console.log("Intake Owner Depts");
           // console.log(angular.toJson(result));
            $scope.caseInfo.CRMSystems = result;
            //return result;
        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });


        caseDropDownService.getDropDown(CASE_CONSTANTS.CASE_CASETYPE).success(function (result) {
          //  console.log("Intake Owner Depts");
          //  console.log(angular.toJson(result));
            $scope.caseInfo.CaseTypes = result;
            //return result;
        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });

        angular.forEach($scope.caseInfo.intakeChannels, function (v, k) {
            if (v.id == row.intake_chan_value) {
                //\\ $scope.name.nameTypeCode = v.id;
                $scope.caseInfo.selected.intakeChanneType = v.value;
            }
        });

        angular.forEach($scope.caseInfo.IntakeOwnerDepts, function (v, k) {
            if (v.id == row.intake_owner_dept_value) {
                //\\ $scope.name.nameTypeCode = v.id;
                $scope.caseInfo.selected.intakeOwnerDeptType = v.value;
            }
        });

        angular.forEach($scope.caseInfo.CRMSystems, function (v, k) {
            if (v.id == row.ref_src_key) {
                //\\ $scope.name.nameTypeCode = v.id;
                $scope.caseInfo.selected.CRMSystemType = v.value;
            }
        });

        angular.forEach($scope.caseInfo.CaseTypes, function (v, k) {
            if (v.id == row.typ_key_dsc) {
                //\\ $scope.name.nameTypeCode = v.id;
                $scope.caseInfo.selected.CaseType = v.value;
            }
        });

        angular.forEach($scope.caseInfo.Statuses, function (v, k) {
            if (v.id == row.status) {
                //\\ $scope.name.nameTypeCode = v.id;
                $scope.caseInfo.selected.StatusType = v.value;
            }
        });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var CaseReportDate = $scope.caseInfo.CaseReportDate ? $filter('date')(new Date($scope.caseInfo.CaseReportDate), 'MM/dd/yyyy') : null;
                var postParams = {
                    "case_seq": $scope.caseInfo.caseKey,
                    "case_nm": $scope.caseInfo.caseName,
                    "case_desc": $scope.caseInfo.caseDesc,
                    "ref_src_desc": $scope.caseInfo.selected.CRMSystemType,
                    "ref_id": $scope.caseInfo.CRMSystem,
                    "typ_key_desc": $scope.caseInfo.selected.CaseType,
                    "intake_chan_desc": $scope.caseInfo.selected.intakeChannelType,
                    "intake_owner_dept_desc": $scope.caseInfo.selected.intakeOwnerDeptType,
                    "cnst_nm": $scope.caseInfo.constName,
                    "crtd_by_usr_id": $scope.caseInfo.userName,
                    "status": $scope.caseInfo.selected.StatusType,
                    "report_dt": CaseReportDate,
                    "attchmnt_url": null
                };


                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                CaseCRUDapiService.getCRUDOperationResult(postParams, CASE_CRUD_CONSTANTS.CASEINFO.EDIT).success(function (result) {
                   // console.log("Case Info CRUD Result");
                  //  console.log(postParams);
                   // console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }
                        var searchparam =
                            {
                                "CaseInputSearchModel":
                                [
                                    {
                                        "CaseId": $scope.caseInfo.caseKey
                                    }
                                ]
                            }
                       // console.log(params);
                        var SearchResultsParam = CaseDataServices.getSavedSearchParams(); //This gets the search params for the search results and refreshes the search results section
                       // console.log("getSavedSearchParams");
                      //  console.log(SearchResultsParam);
                        // if (params.grid.data.length > 1) {
                        if(pageName == "SearchResults"){

                            CaseServices.getCaseAdvSearchResults(SearchResultsParam).success(function (result) {

                                myApp.hidePleaseWait();

                                // emailData.push(PushParams);
                               
                               // console.log("case key");
                               // console.log($scope.caseInfo.caseKey);
                                params.grid.data = '';
                                params.grid.data.length = 0;
                                params.grid.data = result;
                              //  console.log("Case Details");
                              //  console.log(CaseDataServices.getCaseDetails());
                                CaseDataServices.setSearchResutlsData(result);
                                $uibModalInstance.close(output);
                                messagePopup($rootScope, CASE_CRUD_CONSTANTS.SUCCESS_MESSAGE, CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS);
                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }
                                else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }

                            });
                           

                        }
                        else {
                            CaseServices.getCaseAdvSearchResults(SearchResultsParam).success(function (result) {
                                CaseDataServices.setSearchResutlsData(result);
                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }
                                else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                }

                            });
                            CaseServices.getCaseAdvSearchResults(searchparam).success(function (result) {
                                myApp.hidePleaseWait();
                               // console.log("case key");
                              //  console.log($scope.caseInfo.caseKey);
                                params.grid.data = '';
                                params.grid.data.length = 0;
                                params.grid.data = result;
                                CaseDataServices.setCaseDetails(result[0]["case_desc"]);
                               // console.log("Case Details");
                              //  console.log(CaseDataServices.getCaseDetails());

                                // emailData.push(PushParams);

                                CaseMultiDataService.setData(result, CASE_CONSTANTS.CASE_INFO);
                                $uibModalInstance.close(output);
                                messagePopup($rootScope, CASE_CRUD_CONSTANTS.SUCCESS_MESSAGE, CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS);
                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                }
                                else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                                    myApp.hidePleaseWait();
                                    $uibModalInstance.close(output);
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                                }
                            });
                        }
                      
                    }
                    else if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE,

                            ConfirmationMessage: CASE_CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE, CASE_CRUD_CONSTANTS.FAILURE_CONFIRM);
                    }
                    //else if (result.data == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                    //    debugger;
                    //    var output = {
                    //        finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                    //        ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                    //    }
                    //    myApp.hidePleaseWait();
                    //    $uibModalInstance.close(output);
                    //}
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM);
                    }
                    else if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                    }

                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);




/*********************Delete Name Modal Controller****************/
angular.module('case').controller('DeleteCaseInfoInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal',
    'params', 'pageName', 'CaseMultiDataService', 'CaseMultiGridService', 'uiGridConstants', 'CaseCRUDapiService', 'CaseServices', '$rootScope',
    'CaseUtilServices', 'uibDateParser', 'CaseApiService', 'CaseCRUDoperations', 'CaseDataServices', '$rootScope', function ($scope, $filter, $uibModalInstance, $uibModal,
    params, pageName, CaseMultiDataService, CaseMultiGridService, uiGridConstants, CaseCRUDapiService, CaseServices, $rootScope, CaseUtilServices, uibDateParser, CaseApiService, CaseCRUDoperations, CaseDataServices, $rootScope) {
       // console.log(params.row);
        var row = params.row;

        // editRow.row_stat_cd = "L";

        $scope.caseInfo = {
            selected: {
                intakeChannelType: row.intake_chan_value,
                intakeChannelTypeCode: '',
                intakeOwnerDeptType: row.intake_owner_dept_value,
                intakeOwnerDeptTypeCode: '',
                CRMSystemType: row.ref_src_dsc,
                CRMSystemTypeCode: '',
                CaseType: row.typ_key_dsc,
                CaseTypeCode: '',
                StatusType: row.status,
                StatusTypeCode: ''
            },
            intakeChannel: row.intake_chan_value,
            intakeDept: row.intake_owner_dept_value,
            CRMSystem: row.ref_src_dsc,
            CaseType: row.typ_key_dsc,
            Status: row.status,
            caseKey: row.case_key,
            caseName: row.case_nm,
            caseDesc: row.case_desc,
            CRMSystemID: row.ref_id,
            constName: row.cnst_nm,
            ReportedDt: row.report_dt,
            userName: row.crtd_by_usr_id
        };


        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "case_nm": $scope.caseInfo.caseKey
                };

                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
               // console.log("Post params - delete");
              //  console.log(postParams);

                CaseCRUDapiService.getCRUDOperationResult(postParams, CASE_CRUD_CONSTANTS.CASEINFO.DELETE).success(function (result) {
                   // console.log(result);
                    output_msg = result[0].o_outputMessage;
                  //  console.log(output_msg);
                  //  console.log(result);
                    if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.SUCCESS_CONFIRM,
                            clearGridFlag: true
                        }

                        var searchparam =
                             {
                                 "CaseInputSearchModel":
                                 [
                                     {
                                         "CaseId": $scope.caseInfo.caseKey

                                     }
                                 ]
                             }

                        var SearchResultsParam = CaseDataServices.getSavedSearchParams();
                        //if (params.grid.data.length > 1) {
                        if(pageName == "SearchResults") {
                            CaseServices.getCaseAdvSearchResults(SearchResultsParam).success(function (result) {

                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);

                                // emailData.push(PushParams);

                              //  console.log("case key");
                              //  console.log($scope.caseInfo.caseKey);
                                params.grid.data = '';
                                params.grid.data.length = 0;
                                params.grid.data = result;
                             //   console.log("Case Details");
                             //   console.log(CaseDataServices.getCaseDetails());
                                $uibModalInstance.close(output);
                                
                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                }
                                else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                                }

                            });

                        }
                        else {
                            CaseServices.getCaseAdvSearchResults(SearchResultsParam).success(function (result) {
                                CaseDataServices.setSearchResutlsData(result);
                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                }
                                else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                                }

                            });

                            CaseServices.getCaseAdvSearchResults(searchparam).success(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                             //   console.log("case key");
                             //   console.log($scope.caseInfo.caseKey);
                                params.grid.data = '';
                                params.grid.data.length = 0;
                                params.grid.data = result;
                                if (result > 0)
                                    CaseDataServices.setCaseDetails(result[0]["case_desc"]);
                                else
                                    CaseDataServices.setCaseDetails("<empty>");
                                
                                // emailData.push(PushParams);

                                CaseMultiDataService.setData(result, CASE_CONSTANTS.CASE_INFO);
                                CaseMultiDataService.setData('', CASE_CONSTANTS.CASE_LOCINFO);
                                CaseMultiDataService.setData('', CASE_CONSTANTS.CASE_NOTES);
                                CaseMultiDataService.setData('', CASE_CONSTANTS.CASE_TRANSACTION);
                               
                                //$parent.caseLocInfoGridOptions.data = '';
                                $uibModalInstance.close(output);
                            }).error(function (result) {
                                myApp.hidePleaseWait();
                                $uibModalInstance.close(output);
                                if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                                }
                                else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                                }

                            });

                        }
                    }
                    else if (output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CASE_CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT || output_msg == CASE_CRUD_CONSTANTS.CASEINFO.DELETE_FAILURE_REASON) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.CASEINFO.DELETE_FAILURE_REASON,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }

                    messagePopup($rootScope, CASE_CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE, CASE_CRUD_CONSTANTS.SUCCESS_CONFIRM);
                    //else if (result.data == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                    //    var output = {
                    //        finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                    //        ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM,
                    //    }
                    //    myApp.hidePleaseWait();
                    //    $uibModalInstance.close(output, true); // True or false is set to check whether to clear the grids or not.
                    //}
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    if (result == CASE_CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM,
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output, true); // True or false is set to check whether to clear the grids or not.
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, CASE_CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM);
                    }
                    else if (result == CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == CASE_CRUD_CONSTANTS.DB_ERROR) {
                        messagePopup($rootScope, CASE_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                    }

                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);
