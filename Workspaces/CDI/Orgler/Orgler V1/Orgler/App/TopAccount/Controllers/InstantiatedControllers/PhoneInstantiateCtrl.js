/// <reference path="../../Views/multi/ConstMultiPhone.tpl.html" />
// changing for minimization purpose
topAccMod.controller('AddPhoneInstantiateCtrl', ['$http', '$scope', '$filter', '$uibModalInstance', '$uibModal', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants',
    'constituentCRUDapiService', 'dropDownService', 'ConstUtilServices', '$rootScope', 'constituentApiService',
    function ($http, $scope, $filter, $uibModalInstance, $uibModal, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants,
    constituentCRUDapiService, dropDownService, ConstUtilServices, $rootScope, constituentApiService) {
    $scope.phoneTypes = [];

    //case Prepopulation
    if (globalServices.getCaseTabCaseNo() != null) {
        var caseNo = globalServices.getCaseTabCaseNo();
    };

    $scope.notes = CRUD_CONSTANTS.NOTES;

    $scope.selected = {
        phoneType: "Home",
        phoneId: "H",
        notes: CRUD_CONSTANTS.DEFAULT_NOTE
    }
    $scope.masterId = params.masterId;
    $scope.sourceSys = CRUD_CONSTANTS.SOURCE_SYS;
    $scope.startDate = ConstUtilServices.getStartDate();
    $scope.rowCode = CRUD_CONSTANTS.ROW_CODE;
    $scope.DWTimestamp = ConstUtilServices.getStartDate();
    $scope.phone = {
        caseNo: caseNo,
        constType: "OR"
    };
    //$scope.phone.
    $scope.cnstPhoneNumberRegex = /^((((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4})|(\d{10} ?))$/;

    dropDownService.getDropDown(HOME_CONSTANTS.CONST_PHONE).success(function (result) {
        $scope.phoneTypes = result;
        console.log(result);
    }).error(function (result) { });

    $scope.submit = function () {
        console.log($scope.myForm);
        if ($scope.myForm.$valid) {
            //added in crud services
           
            var postParams = AddPostParams(HOME_CONSTANTS.CONST_PHONE, $scope);

            //console.log(postParams);

            var PushParams = AddPushParams(HOME_CONSTANTS.CONST_PHONE, $scope);
            myApp.showPleaseWait();
            angular.element('#modalCover').css("pointer-events", "none");
            //do something
            var output_msg;
            var trans_key;
            var finalMessage;
            var ReasonOrTransKey;
            var ConfirmationMessage;
            var phoneData = constMultiDataService.getData(HOME_CONSTANTS.CONST_PHONE);
            constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.PHONE.ADD).success(function (result) {
                console.log(result);
                output_msg = result[0].o_outputMessage;
                trans_key = result[0].o_transaction_key;
                if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                    var output = {
                        finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                        ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                        ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                    }
                 

                    constituentApiService.getApiDetails($scope.masterId, HOME_CONSTANTS.CONST_PHONE).success(function (result) {

                        // params.grid.data = result;
                        //we got it from parent controller
                        params.grid.data = '';
                        params.grid.data.length = 0;
                        var newResult = filterConstituentData(result);

                        //while (params.grid.data.length) {
                        //    params.grid.data.shift();
                        //}

                        //Array.prototype.push.apply(params.grid.data, newResult);

                        params.grid.data = newResult;
                        constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_PHONE);
                        constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_PHONE);
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                        MessagePopup($rootScope, "Success", output.finalMessage + output.ReasonOrTransKey);

                    }).error(function (result) {
                        myApp.hidePleaseWait();
                        $uibModalInstance.dismiss('cancel');
                        addressErrorPopup($rootScope, result);
                    });



                    // console.log(phoneData);                  
                }
                else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE) {
                    var output = {
                        finalMessage: CRUD_CONSTANTS.FAILURE_MESSAGE,
                        ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                        ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                    }
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    MessagePopup($rootScope, "Duplicate", output.finalMessage + output.ReasonOrTransKey);
                }
               
            }).error(function (result) {
                myApp.hidePleaseWait();
                $uibModalInstance.dismiss('cancel');
                addressErrorPopup($rootScope, result);
            });
        }
        else {
            if ($scope.myForm.phoneNo.$error.pattern) {
                openErrorPopup(" Invalid Phone Format.The valid formats are xxx-xxx-xxxx, xxxxxxxxxx, (xxx)xxx-xxxx, (xxx) xxx-xxxx", "Validation Error!",$uibModal);
            }
        }
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
    function MessagePopup($rootScope, headerText, bodyText) {
        $rootScope.newAccountModalPopupHeaderText = headerText;
        $rootScope.newAccountModalPopupBodyText = bodyText;
        angular.element(topAccountMessageDialogBox).modal({ backdrop: "static" });
    }
    function addressErrorPopup($rootScope, result) {
        if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
            MessagePopup($rootScope, "Error: Access Denied", CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE);
        }
        else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            MessagePopup($rootScope, "Error: Timed Out", CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE);
        }
        else if (result == CRUD_CONSTANTS.DB_ERROR) {
            MessagePopup($rootScope, "Error: Timed Out", CRUD_CONSTANTS.DB_ERROR_MESSAGE);
        }
    }
    }]);

function AddPostParams(type, $scope) {
    switch (type) {
        case HOME_CONSTANTS.CONST_PHONE: {
            return {
                "MasterID": $scope.masterId,
                "PhoneNumber": $scope.phoneNo,
                "PhoneTypeCode": $scope.selected.phoneId,
                "SourceSystemCode": "STRX",
                //"SourceSystemCd": $scope.selected.sourcesystemitem,
                "Notes": $scope.selected.notes,
                "CaseNumber": $scope.phone.caseNo,
                "ConstType": $scope.phone.constType
            };
            break;
        }
    }
}


function AddPushParams(type, $scope) {
    switch (type) {
        case HOME_CONSTANTS.CONST_PHONE: {
            return {
                "cnst_phn_num": $scope.phoneNo,
                "arc_srcsys_cd": "STRX",
                "cnst_srcsys_id": ""
            }
        }
    }
}

/// <reference path="../../Views/multi/ConstMultiPhone.tpl.html" />
topAccMod.controller('EditPhoneInstantiateCtrl', ['$http', '$scope', '$filter', '$uibModalInstance', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'dropDownService', 'ConstUtilServices', 'constituentApiService', '$rootScope',
    function ($http, $scope, $filter, $uibModalInstance, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, dropDownService, ConstUtilServices, constituentApiService, $rootScope) {

    // console.log(params.row);
    var row = params.row;
    //console.log(params.grid);
    //case Prepopulation
    if (globalServices.getCaseTabCaseNo() != null) {
        var caseNo = globalServices.getCaseTabCaseNo();
    };


    $scope.phone = {
        selected: {
            phoneId: row.phn_typ_cd,
            notes: CRUD_CONSTANTS.DEFAULT_NOTE
        },
        phoneTypes: [],
        constType: "OR"
    };

    dropDownService.getDropDown(HOME_CONSTANTS.CONST_PHONE).success(
        function (result) {
            $scope.phone.phoneTypes = result;
            angular.forEach($scope.phone.phoneTypes, function (v, k) {

                if (row.phn_typ_cd == v.id) {
                    $scope.phone.selected.phoneType = v.value;
                    $scope.phone.selected.phoneId = v.id;
                }

            });
        }).error(function (result) { });

   

    $scope.phone.masterId = params.masterId;
    //$scope.phoneNo = row.cnst_phn_num;
    $scope.phone.phoneNoExn = row.cnst_phn_extsn_num;
    $scope.phone.sourceSys = row.arc_srcsys_cd;
    $scope.phone.phoneNo = row.cnst_phn_num;
    $scope.phone.loadId = row.load_id;
    //scope.phone.phoneType = row.phn_typ_cd;
    $scope.phone.startDate = row.cnst_phn_strt_ts;
    $scope.phone.dwStartDate = row.dw_srcsys_trans_ts;
    $scope.phone.actInd = row.act_ind;
    $scope.phone.rowCode = row.row_stat_cd;
    $scope.phone.caseNo = caseNo;
    $scope.phone.notes = CRUD_CONSTANTS.NOTES;
    $scope.phone.cnstPhoneNumberRegex = /^((((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4})|(\d{10} ?))$/;


    $scope.submit = function () {
        if ($scope.myForm.$valid) {
            var postParams = {
                "MasterID": $scope.phone.masterId,
                "OldSourceSystemCode": row.arc_srcsys_cd,
                "OldPhoneTypeCode": row.phn_typ_cd,
                "OldBestLOSInd": row.cnst_phn_best_ind,
                "PhoneNumber": $scope.phone.phoneNo,
                "ConstType": "L",
                "PhoneTypeCode": $scope.phone.selected.phoneId,
                "Notes": $scope.phone.selected.notes,
                "SourceSystemCode": row.arc_srcsys_cd,
                "BestLOS": row.cnst_phn_best_ind,
                "CaseNumber": $scope.phone.caseNo,
                "ConstType": $scope.phone.constType
            }

            myApp.showPleaseWait();
            $('#modalCover').css("pointer-events", "none");
            //do something
            var output_msg;
            var trans_key;
            var finalMessage;
            var ReasonOrTransKey;
            var ConfirmationMessage;
            //var finalresults = constituentDataServices.getConstnameGrid();


            constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.PHONE.EDIT).success(function (result) {
                //console.log(result);
                output_msg = result[0].o_outputMessage;
                trans_key = result[0].o_transaction_key;

                if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                    var output = {
                        finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                        ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                        ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                    }

                    constituentApiService.getApiDetails($scope.phone.masterId, HOME_CONSTANTS.CONST_PHONE).success(function (result) {

                        // params.grid.data = result;
                        //we got it from parent controller
                        params.grid.data = '';
                        params.grid.data.length = 0;
                        var newResult = filterConstituentData(result);
                        params.grid.data = newResult;
                        constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_PHONE);
                        constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_PHONE);
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                        MessagePopup($rootScope, "Success", output.finalMessage + output.ReasonOrTransKey);

                    }).error(function (result) {
                        myApp.hidePleaseWait();
                        $uibModalInstance.dismiss('cancel');
                        addressErrorPopup($rootScope, result);
                    });

                    // console.log(phoneData);                  
                }
                else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                    var output = {
                        finalMessage: CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE,
                        ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                        ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                    }
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    MessagePopup($rootScope, "Duplicate", output.finalMessage + output.ReasonOrTransKey);
                }
              
            }).error(function (result) {
                myApp.hidePleaseWait();
                $uibModalInstance.dismiss('cancel');
                addressErrorPopup($rootScope, result);
            });
        }
    }


    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
    function MessagePopup($rootScope, headerText, bodyText) {
        $rootScope.newAccountModalPopupHeaderText = headerText;
        $rootScope.newAccountModalPopupBodyText = bodyText;
        angular.element(topAccountMessageDialogBox).modal({ backdrop: "static" });
    }
    function addressErrorPopup($rootScope, result) {
        if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
            MessagePopup($rootScope, "Error: Access Denied", CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE);
        }
        else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            MessagePopup($rootScope, "Error: Timed Out", CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE);
        }
        else if (result == CRUD_CONSTANTS.DB_ERROR) {
            MessagePopup($rootScope, "Error: Timed Out", CRUD_CONSTANTS.DB_ERROR_MESSAGE);
        }
    }
}]);



topAccMod.controller("DeletePhoneInstantiateCtrl", ['$http', '$scope', '$filter', '$uibModalInstance', 'dropDownService', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'ConstUtilServices', 'constituentApiService', '$rootScope',
    function ($http, $scope, $filter, $uibModalInstance,  dropDownService, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, constituentApiService, $rootScope) {
    var row = params.row;
    //case Prepopulation
    if (globalServices.getCaseTabCaseNo() != null) {
        var caseNo = globalServices.getCaseTabCaseNo();
    };
    $scope.phone = {
        selected: {
            phoneId: row.phn_typ_cd,
            notes: CRUD_CONSTANTS.DEFAULT_NOTE
        },
        phoneTypes: [],
        constType: "OR"
    };

    dropDownService.getDropDown(HOME_CONSTANTS.CONST_PHONE).success(
       function (result) {
           angular.forEach($scope.phone.phoneTypes, function (v, k) {

               if (params.row.phn_typ_cd == v.id) {
                   $scope.phone.selected.phoneType = v.option;
                   $scope.phone.selected.phoneId = v.id
               }

           });
       }).error(function (result) { });

   

    $scope.phone.masterId = params.masterId;
    //$scope.phoneNo = row.cnst_phn_num;
    $scope.phone.phoneNoExn = row.cnst_phn_extsn_num;
    $scope.phone.sourceSys = row.arc_srcsys_cd;
    $scope.phone.phoneNo = row.cnst_phn_num;
    $scope.phone.loadId = row.load_id;
    //scope.phone.phoneType = row.phn_typ_cd;
    $scope.phone.startDate = row.cnst_phn_strt_ts;
    $scope.phone.dwStartDate = row.dw_srcsys_trans_ts;
    $scope.phone.actInd = row.act_ind;
    $scope.phone.rowCode = row.row_stat_cd;
    $scope.phone.caseNo = caseNo;


    $scope.submit = function () {
        if ($scope.myForm.$valid) {
            var postParams = {
                "MasterID": $scope.phone.masterId,
                "OldSourceSystemCode": row.arc_srcsys_cd,
                "OldPhoneTypeCode": row.phn_typ_cd,
                "OldBestLOSInd": row.cnst_phn_best_ind,
                "PhoneNumber": $scope.phone.phoneNo,
                "ConstType": "L",
                "PhoneTypeCode": $scope.phone.selected.phoneId,
                "Notes": $scope.phone.selected.notes,
                "SourceSystemCode": row.arc_srcsys_cd,
                "BestLOS": row.cnst_phn_best_ind,
                "CaseNumber": $scope.phone.caseNo,
                "ConstType": $scope.phone.constType
            }

            myApp.showPleaseWait();
            $('#modalCover').css("pointer-events", "none");
            //do something
            var output_msg;
            var trans_key;
            var finalMessage;
            var ReasonOrTransKey;
            var ConfirmationMessage;
            //var finalresults = constituentDataServices.getConstnameGrid();


            constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.PHONE.DELETE).success(function (result) {
                //console.log(result);
                output_msg = result[0].o_outputMessage;
                trans_key = result[0].o_transaction_key;

                if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                    var output = {
                        finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                        ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                        ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                    }

                    constituentApiService.getApiDetails($scope.phone.masterId, HOME_CONSTANTS.CONST_PHONE).success(function (result) {

                        params.grid.data = '';
                        params.grid.data.length = 0;
                        //changed by srini
                        var newResult = filterConstituentData(result);
                        params.grid.data = newResult;
                        constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_PHONE);
                        constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_PHONE);
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                        MessagePopup($rootScope, "Success", output.finalMessage + output.ReasonOrTransKey);

                    }).error(function (result) {
                        myApp.hidePleaseWait();
                        $uibModalInstance.dismiss('cancel');
                        addressErrorPopup($rootScope, result);
                    });

                    // console.log(phoneData);                  
                }
                else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                    var output = {
                        finalMessage: CRUD_CONSTANTS.DELETE_FAILURE_MESSAGE,
                        ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                        ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                    }
                    myApp.hidePleaseWait();
                    $uibModalInstance.close(output);
                    MessagePopup($rootScope, "Duplicate", output.finalMessage + output.ReasonOrTransKey);
                }
              
            }).error(function (result) {
                myApp.hidePleaseWait();
                $uibModalInstance.dismiss('cancel');
                addressErrorPopup($rootScope, result);
            });
        }
    }


    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
    function MessagePopup($rootScope, headerText, bodyText) {
        $rootScope.newAccountModalPopupHeaderText = headerText;
        $rootScope.newAccountModalPopupBodyText = bodyText;
        angular.element(topAccountMessageDialogBox).modal({ backdrop: "static" });
    }
    function addressErrorPopup($rootScope, result) {
        if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
            MessagePopup($rootScope, "Error: Access Denied", CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE);
        }
        else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            MessagePopup($rootScope, "Error: Timed Out", CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE);
        }
        else if (result == CRUD_CONSTANTS.DB_ERROR) {
            MessagePopup($rootScope, "Error: Timed Out", CRUD_CONSTANTS.DB_ERROR_MESSAGE);
        }
    }
}]);



