function prefTypes() {
    return [];
}

function prefValues() {
    return []
}

angular.module('constituent').controller("AddContactPrefInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'dropDownService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, dropDownService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };

        $scope.contactPref = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                prefType: "DNC All",
                prefTypeCode: "DNC All",
                prefValue: "ALL CHANNELS",
                prefValueCode: "ALL CHANNELS"
            },

            prefTypes: [],
            prefValues: [],
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
            startDate: ConstUtilServices.getStartDate(),
            rowCode: CRUD_CONSTANTS.ROW_CODE,
            caseNo: caseNo,
            constType: $sessionStorage.type
            //  DWTimestamp: ConstUtilServices.getStartDate()
        };

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_PREF).success(function (result) { $scope.contactPref.prefTypes = result; }).error(function (result) { });
        dropDownService.getDropDown(HOME_CONSTANTS.CONST_PREF_VAL).success(function (result) { $scope.contactPref.prefValues = result; }).error(function (result) { });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.contactPref.masterId,
                    "ContactPrefcValue": $scope.contactPref.selected.prefValueCode,
                    "ContactPrefcTypeCode": $scope.contactPref.selected.prefTypeCode,
                    "Notes": $scope.contactPref.selected.note,
                    "SourceSystemCode": $scope.contactPref.sourceSys,
                    "CaseNumber": $scope.contactPref.caseNo,
                    "ConstType": $scope.contactPref.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.CONTACT_PREF.ADD).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.contactPref.masterId, HOME_CONSTANTS.CONST_PREF).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_PREF);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_PREF);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            contactErrorPopup($rootScope, result);
                        });


                    }
                    else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.FAILURE_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                            ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                   
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    contactErrorPopup($rootScope, result);
                });
            };


        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);



angular.module('constituent').controller("EditContactPrefInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'dropDownService', 'ConstUtilServices', 'uibDateParser', 'constituentApiService','$rootScope',
    function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService,
    dropDownService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {

        console.log(params.row);
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        var row = params.row;
        $scope.contactPref = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                prefType: "",
                prefTypeCode: row.cntct_prefc_typ_cd,
                prefValue: "",
                prefValueCode: row.cntct_prefc_val
            },

            prefTypes: [],
            prefValues: [],
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            sourceSys: row.arc_srcsys_cd,
            startDate: row.cnst_cntct_prefc_strt_ts,
            endDate: row.cnst_cntct_prefc_end_ts,
            rowCode: row.row_stat_cd,
            loadId: row.load_id,
            caseNo: caseNo,
            constType: $sessionStorage.type
            //  DWTimestamp: ConstUtilServices.getStartDate()
        };


        dropDownService.getDropDown(HOME_CONSTANTS.CONST_PREF).success(
            function (result) {
                $scope.contactPref.prefTypes = result;
                angular.forEach($scope.contactPref.prefTypes, function (v, k) {
                    if ($scope.contactPref.selected.prefTypeCode == v.id) {
                        $scope.contactPref.selected.prefType = v.value;
                    }
                });
            }).error(function (result) { });
        dropDownService.getDropDown(HOME_CONSTANTS.CONST_PREF_VAL).success(
            function (result) {
                $scope.contactPref.prefValues = result;
                angular.forEach($scope.contactPref.prefValues, function (v, k) {
                    if ($scope.contactPref.selected.prefValueCode == v.id) {
                        $scope.contactPref.selected.prefValue = v.value;
                    }
                });

            }).error(function (result) { });

      
        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.contactPref.masterId,
                    "ContactPrefcValue": $scope.contactPref.selected.prefValueCode,
                    "ContactPrefcTypeCode": $scope.contactPref.selected.prefTypeCode,
                    "Notes": $scope.contactPref.selected.note,
                    "SourceSystemCode": $scope.contactPref.sourceSys,
                    "OldContactPrefcValue": row.cntct_prefc_typ_cd,
                    "OldContactPrefcTypeCode": row.cntct_prefc_val,
                    "OldSourceSystemCode": row.arc_srcsys_cd,
                    "CaseNumber": $scope.contactPref.caseNo,
                    "ConstType": $scope.contactPref.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.CONTACT_PREF.EDIT).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.contactPref.masterId, HOME_CONSTANTS.CONST_PREF).sucess(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_PREF);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_PREF);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            contactErrorPopup($rootScope, result);
                        });


                    }
                    else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_FAILURE_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                            ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                   
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    contactErrorPopup($rootScope, result);
                });
            };
        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

    }]);


angular.module('constituent').controller("DeleteContactPrefInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'dropDownService', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, dropDownService, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {

        console.log(params.row);
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        var row = params.row;
        $scope.contactPref = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                prefType: "",
                prefTypeCode: row.cntct_prefc_typ_cd,
                prefValue: "",
                prefValueCode: row.cntct_prefc_val
            },

            prefTypes: [],
            prefValues: [],
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            sourceSys: row.arc_srcsys_cd,
            startDate: row.cnst_cntct_prefc_strt_ts,
            endDate: row.cnst_cntct_prefc_end_ts,
            rowCode: row.row_stat_cd,
            loadId: row.load_id,
            caseNo: caseNo,
            constType: $sessionStorage.type
            //  DWTimestamp: ConstUtilServices.getStartDate()
        };


        dropDownService.getDropDown(HOME_CONSTANTS.CONST_PREF).success(
           function (result) {
               $scope.contactPref.prefTypes = result;
               angular.forEach($scope.contactPref.prefTypes, function (v, k) {
                   if ($scope.contactPref.selected.prefTypeCode == v.id) {
                       $scope.contactPref.selected.prefType = v.value;
                   }
               });
           }).error(function (result) { });
        dropDownService.getDropDown(HOME_CONSTANTS.CONST_PREF_VAL).success(
            function (result) {
                $scope.contactPref.prefValues = result;
                angular.forEach($scope.contactPref.prefValues, function (v, k) {
                    if ($scope.contactPref.selected.prefValueCode == v.id) {
                        $scope.contactPref.selected.prefValue = v.value;
                    }
                });

            }).error(function (result) { });



        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.contactPref.masterId,
                    "ContactPrefcValue": $scope.contactPref.selected.prefValueCode,
                    "ContactPrefcTypeCode": $scope.contactPref.selected.prefTypeCode,
                    "Notes": $scope.contactPref.selected.note,
                    "SourceSystemCode": $scope.contactPref.sourceSys,
                    "OldContactPrefcValue": row.cntct_prefc_val,
                    "OldContactPrefcTypeCode": row.cntct_prefc_typ_cd,
                    "OldSourceSystemCode": row.arc_srcsys_cd,
                    "CaseNumber": $scope.contactPref.caseNo,
                    "ConstType": $scope.contactPref.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.CONTACT_PREF.DELETE).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.contactPref.masterId, HOME_CONSTANTS.CONST_PREF).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_PREF);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_PREF);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            contactErrorPopup($rootScope, result);
                        });


                    }
                    else if (output_msg == CRUD_CONSTANTS.PROCEDURE.DUPLICATE || output_msg == CRUD_CONSTANTS.PROCEDURE.NOT_PRESENT) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_FAILURE_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.FAIULRE_REASON,
                            ConfirmationMessage: CRUD_CONSTANTS.FAILURE_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }
                    
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    contactErrorPopup($rootScope, result);
                });
            };
        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

    }]);

function contactErrorPopup($rootScope, result) {
    if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
        messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, CONSTITUENT_MESSAGES.ACCESS_DENIED_HEADER);
    }
    else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
        messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, CONSTITUENT_MESSAGES.TIMED_OUT_HEADER);
    }
    else if (result == CRUD_CONSTANTS.DB_ERROR) {
        messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, CONSTITUENT_MESSAGES.TIMED_OUT_HEADER);

    }
}