function delivery() {
    return [
                    { id: "1", value: "Deliverable" },
                    { id: "0", value: "Undeliverable" }
    ];
}


angular.module('constituent').controller("AddAddressInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$uibModal','globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', 'constituentCRUDoperations', 'dropDownService','$localStorage','$sessionStorage','$rootScope',
    function ($scope, $filter, $uibModalInstance, $uibModal,globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser,
    constituentApiService, constituentCRUDoperations, dropDownService, $localStorage, $sessionStorage, $rootScope) {
        console.log(params.row);
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }

        $scope.address = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                delivery: "Deliverable",
                deliveryId: "1",
                addressTypeCode: "H",
                addressType: "Home"
            },
            deliveries: delivery(),
            addressTypes: [],
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
            startDate: ConstUtilServices.getStartDate(),
            rowCode: CRUD_CONSTANTS.ROW_CODE,
            DWTimestamp: ConstUtilServices.getStartDate(),

            caseNo: caseNo,
            constType: $sessionStorage.type,
            cnstZipRegex: { zip4: /^(?!00000)\d{4}$/, zip: /^(?!0000)\d{5}$/ }
        };

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_ADDRESS)
            .success(function (result) { $scope.address.addressTypes = result; }).
            error(function (result) { }),

        $scope.submit = function () {

            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "MasterID": $scope.address.masterId,
                    "AddressLine1": $scope.address.addressLine1,
                    "AddressLine2": $scope.address.addressLine2,
                    "City": $scope.address.city,
                    "State": $scope.address.state,
                    "Country": $scope.address.country,
                    "Zip4": $scope.address.zip4,
                    "Zip5": $scope.address.zip,
                    "UndeliveredIndicator": $scope.address.selected.deliveryId,
                    "SourceSystemCode": $scope.address.sourceSys,
                    "AddressTypeCode": $scope.address.selected.addressTypeCode,
                    "Notes": $scope.address.selected.note,
                    "CaseNumber": $scope.address.caseNo,
                    "ConstType":$scope.address.constType
                }
                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.ADDRESS.ADD).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.address.masterId, HOME_CONSTANTS.CONST_ADDRESS).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            myApp.hidePleaseWait();
                            //changed by srini
                            $uibModalInstance.close(output);
                          //  var newResult = filterConstituentData(result);
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_ADDRESS);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_ADDRESS);
                           // constMultiDataService.setData(result, ;

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            errorPopup(result);
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
                    errorPopup(result);
                });
            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };


        $scope.createCase = function () {
            constituentCRUDoperations.getCasePopup($scope, $uibModal);
        }


        function errorPopup(result) {
            if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
            }
            else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
        }

    }]);


angular.module('constituent').controller("EditAddressInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'dropDownService','$rootScope',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, dropDownService, $rootScope, ConstUtilServices,
    uibDateParser, constituentApiService) {
        console.log(params.row);
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }

        $scope.address = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                delivery: "",
                deliveryId: params.row.cnst_addr_undeliv_ind,
                addressTypeCode: params.row.addr_typ_cd,
                addressType: ""
            },
            deliveries: delivery(),
            addressTypes: [],
            addressLine1: params.row.cnst_addr_line1_addr,
            addressLine2: params.row.cnst_addr_line2_addr,
            notes: CRUD_CONSTANTS.NOTES,
            city: params.row.cnst_addr_city_nm,
            state: params.row.cnst_addr_state_cd,
            zip4: params.row.cnst_addr_zip_4_cd,
            zip: params.row.cnst_addr_zip_5_cd,
            loadId: params.row.load_id,
            country: params.row.cnst_addr_county_nm,
            masterId: params.masterId,
            sourceSys: params.row.arc_srcsys_cd,
            endDate: params.row.cnst_addr_end_dt,
            startDate: params.row.cnst_addr_strt_ts,
            rowCode: params.row.row_stat_cd,
            DWTimestamp: params.row.dw_srcsys_trans_ts,
            caseNo: caseNo,
            constType: $sessionStorage.type,
            cnstZipRegex: { zip4: /^(?!00000)\d{4}$/, zip: /^(?!0000)\d{5}$/ }
        };

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_ADDRESS).success(
            function (result) {

                $scope.address.addressTypes = result;
                angular.forEach($scope.address.addressTypes, function (v, k) {
                    if (v.id == params.row.addr_typ_cd) {
                        $scope.address.selected.addressType = v.value
                        $scope.address.selected.addressTypeCode = v.id;
                    }
                })
            }).error(function (result) {
                errorPopup(result);
            });;

        angular.forEach($scope.address.deliveries, function (v, k) {
            if (v.id == params.row.cnst_addr_undeliv_ind) {
                $scope.address.selected.delivery = v.value;
                $scope.address.selected.deliveryId = v.id;
            }
        });

       

        $scope.submit = function () {

            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "MasterID": $scope.address.masterId,
                    "AddressLine1": $scope.address.addressLine1,
                    "AddressLine2": $scope.address.addressLine2,
                    "City": $scope.address.city,
                    "State": $scope.address.state,
                    "Country": $scope.address.country,
                    "Zip4": $scope.address.zip4,
                    "Zip5": $scope.address.zip,
                    "UndeliveredIndicator": $scope.address.selected.deliveryId,
                    "SourceSystemCode": $scope.address.sourceSys,
                    "AddressTypeCode": $scope.address.selected.addressTypeCode,
                    "Notes": $scope.address.selected.note,
                    "OldSourceSystemCode": params.row.arc_srcsys_cd,
                    "OldAddressTypeCode": params.row.addr_typ_cd,
                    "OldBestLOSInd": params.row.cnst_addr_best_los_ind,
                    "CaseNumber": $scope.address.caseNo,
                    "ConstType": $scope.address.constType
                }
                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.ADDRESS.EDIT).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.address.masterId, HOME_CONSTANTS.CONST_ADDRESS).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_ADDRESS);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_ADDRESS);
                            myApp.hidePleaseWait();                           
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            errorPopup(result);
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
                   /* else if (result.data == CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }*/
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    errorPopup(result);
                });;

            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        function errorPopup(result) {
            if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
            }
            else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
        }
    }]);




angular.module('constituent').controller("DeleteAddressInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'dropDownService', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService','$rootScope',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, dropDownService, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, $rootScope, ConstUtilServices, uibDateParser, constituentApiService) {
        console.log(params.row);
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        }
        $scope.address = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                delivery: "",
                deliveryId: params.row.cnst_addr_undeliv_ind,
                addressTypeCode: params.row.addr_typ_cd,
                addressType: ""
            },
            deliveries: delivery(),
            addressTypes: [],
            addressLine1: params.row.cnst_addr_line1_addr,
            addressLine2: params.row.cnst_addr_line2_addr,
            notes: CRUD_CONSTANTS.NOTES,
            city: params.row.cnst_addr_city_nm,
            state: params.row.cnst_addr_state_cd,
            zip4: params.row.cnst_addr_zip_4_cd,
            zip: params.row.cnst_addr_zip_5_cd,
            loadId: params.row.load_id,
            country: params.row.cnst_addr_county_nm,
            masterId: params.masterId,
            sourceSys: params.row.arc_srcsys_cd,
            endDate: params.row.cnst_addr_end_dt,
            startDate: params.row.cnst_addr_strt_ts,
            rowCode: params.row.row_stat_cd,
            DWTimestamp: params.row.dw_srcsys_trans_ts,
            caseNo: caseNo,
            constType: $sessionStorage.type
            
        };



        angular.forEach($scope.address.deliveries, function (v, k) {
            if (v.id == params.row.cnst_addr_undeliv_ind) {
                $scope.address.selected.delivery = v.value;
                $scope.address.selected.deliveryId = v.id;
            }
        });



        dropDownService.getDropDown(HOME_CONSTANTS.CONST_ADDRESS).success(
            function (result) {

                $scope.address.addressTypes = result;
                angular.forEach($scope.address.addressTypes, function (v, k) {
                    if (v.id == params.row.addr_typ_cd) {
                        $scope.address.selected.addressType = v.value
                        $scope.address.selected.addressTypeCode = v.id;
                    }
                })
            }).error(function (result) {
                errorPopup(result);
            });


        $scope.submit = function () {

            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "MasterID": $scope.address.masterId,
                    "AddressLine1": $scope.address.addressLine1,
                    "AddressLine2": $scope.address.addressLine2,
                    "City": $scope.address.city,
                    "State": $scope.address.state,
                    "Country": $scope.address.country,
                    "Zip4": $scope.address.zip4,
                    "Zip5": $scope.address.zip,
                    "UndeliveredIndicator": $scope.address.selected.deliveryId,
                    "SourceSystemCode": $scope.address.sourceSys,
                    "AddressTypeCode": $scope.address.selected.addressTypeCode,
                    "Notes": $scope.address.selected.note,
                    "OldSourceSystemCode": params.row.arc_srcsys_cd,
                    "OldAddressTypeCode": params.row.addr_typ_cd,
                    "OldBestLOSInd": params.row.cnst_addr_best_los_ind,
                    "CaseNumber": $scope.address.caseNo,
                    "ConstType": $scope.address.constType
                }
                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.ADDRESS.DELETE).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.address.masterId, HOME_CONSTANTS.CONST_ADDRESS).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_ADDRESS);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_ADDRESS);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            errorPopup(result);
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
                    /*else if (result.data == CRUD_CONSTANTS.ACCESS_DENIED) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE,
                            ConfirmationMessage: CRUD_CONSTANTS.ACCESS_DENIED_CONFIRM
                        }
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);
                    }*/
                }).error(function (result) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    errorPopup(result);
                });;

            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        function errorPopup(result) {
            if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
            }
            else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
        }
    }]);