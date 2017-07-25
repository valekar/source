angular.module('constituent').controller("AddCharacterInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'dropDownService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService,
    dropDownService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.characteristics = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                characteristicsType: "Sales for Company",
                characteristicsTypeCode: "SLS"
            },
            characterTypes: [],
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
            startDate: ConstUtilServices.getStartDate(),
            rowCode: CRUD_CONSTANTS.ROW_CODE,
            caseNo: caseNo,
            constType: $sessionStorage.type
        };

        dropDownService.getDropDown(HOME_CONSTANTS.CHARACTERISTICS).success(function (result) { $scope.characteristics.characterTypes = result; }).error(function (result) { });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.characteristics.masterId,
                    "CharacteristicValue": $scope.characteristics.characterValue,
                    "CharacteristicTypeCode": $scope.characteristics.selected.characteristicsTypeCode,
                    "Notes": $scope.characteristics.selected.note,
                    "SourceSystemCode": $scope.characteristics.sourceSys,
                    "CaseNumber": $scope.characteristics.caseNo,
                    "ConstType": $scope.characteristics.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.CHARACTERISITICS.ADD).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.characteristics.masterId, HOME_CONSTANTS.CHARACTERISTICS).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CHARACTERISTICS);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CHARACTERISTICS);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            characterErrorPopup($rootScope, result);
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
                    characterErrorPopup($rootScope, result);
                });


            };


        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

    }]);

function characterErrorPopup($rootScope, result) {
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


angular.module('constituent').controller("EditCharacterInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'dropDownService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, dropDownService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        console.log(params.row);
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        var row = params.row;

        $scope.characteristics = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                characteristicsType: "",
                characteristicsTypeCode: row.cnst_chrctrstc_typ_cd
            },
            characterValue: row.cnst_chrctrstc_val,
            characterTypes: [],
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            sourceSys: row.arc_srcsys_cd,
            startDate: row.cnst_chrctrstc_strt_dt,
            rowCode: row.row_stat_cd,
            endDate: row.cnst_chrctrstc_end_dt,
            DWTimestamp: row.dw_srcsys_trans_ts,
            loadId: row.load_id,
            caseNo: caseNo,
            constType: $sessionStorage.type
        };

        dropDownService.getDropDown(HOME_CONSTANTS.CHARACTERISTICS).success(
            function (result) {
                $scope.characteristics.characterTypes = result;


                angular.forEach($scope.characteristics.characterTypes, function (v, k) {
                    if (v.id == $scope.characteristics.selected.characteristicsTypeCode) {
                        $scope.characteristics.selected.characteristicsType = v.value;
                    }
                });
            }).error(function (result) { });


        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.characteristics.masterId,
                    "CharacteristicValue": $scope.characteristics.characterValue,
                    "CharacteristicTypeCode": $scope.characteristics.selected.characteristicsTypeCode,
                    "Notes": $scope.characteristics.selected.note,
                    "SourceSystemCode": $scope.characteristics.sourceSys,
                    "OldCharacteristicValue": row.cnst_chrctrstc_val,
                    "OldSourceSystemCode": row.arc_srcsys_cd,
                    "OldCharacteristicTypeCode": row.cnst_chrctrstc_typ_cd,
                    "CaseNumber": $scope.characteristics.caseNo,
                    "ConstType": $scope.characteristics.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.CHARACTERISITICS.EDIT).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.characteristics.masterId, HOME_CONSTANTS.CHARACTERISTICS).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CHARACTERISTICS);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CHARACTERISTICS);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            characterErrorPopup($rootScope, result);
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
                    characterErrorPopup($rootScope, result);
                });


            };


        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

    }]);

angular.module('constituent').controller("DeleteCharacterInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'dropDownService', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'ConstUtilServices', 'uibDateParser',
    'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, dropDownService, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        console.log(params.row);
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        var row = params.row;

        $scope.characteristics = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                characteristicsType: "",
                characteristicsTypeCode: row.cnst_chrctrstc_typ_cd
            },
            characterValue: row.cnst_chrctrstc_val,
            characterTypes: [],
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            sourceSys: row.arc_srcsys_cd,
            startDate: row.cnst_chrctrstc_strt_dt,
            rowCode: row.row_stat_cd,
            endDate: row.cnst_chrctrstc_end_dt,
            DWTimestamp: row.dw_srcsys_trans_ts,
            loadId: row.load_id,
            caseNo: caseNo,
            constType: $sessionStorage.type
        };

        dropDownService.getDropDown(HOME_CONSTANTS.CHARACTERISTICS).success(
            function (result) {
                $scope.characteristics.characterTypes = result;


                angular.forEach($scope.characteristics.characterTypes, function (v, k) {
                    if (v.id == $scope.characteristics.selected.characteristicsTypeCode) {
                        $scope.characteristics.selected.characteristicsType = v.value;
                    }
                });
            }).error(function (result) { });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.characteristics.masterId,
                    "CharacteristicValue": $scope.characteristics.characterValue,
                    "CharacteristicTypeCode": $scope.characteristics.selected.characteristicsTypeCode,
                    "Notes": $scope.characteristics.selected.note,
                    "SourceSystemCode": $scope.characteristics.sourceSys,
                    "OldCharacteristicValue": row.cnst_chrctrstc_val,
                    "OldSourceSystemCode": row.arc_srcsys_cd,
                    "OldCharacteristicTypeCode": row.cnst_chrctrstc_typ_cd,
                    "CaseNumber": $scope.characteristics.caseNo,
                    "ConstType": $scope.characteristics.constType
                };

                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.CHARACTERISITICS.DELETE).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.characteristics.masterId, HOME_CONSTANTS.CHARACTERISTICS).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CHARACTERISTICS);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CHARACTERISTICS);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            characterErrorPopup($rootScope, result);
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
                    characterErrorPopup($rootScope, result);
                });


            };


        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

    }]);

