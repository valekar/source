﻿

//function nameTypes() {
//    //// return  ['Alias', 'Honor Roll', 'Maiden Name', 'Nick Name', 'Presedential Salutaion', 'Primary Name', 'Unknown'];
//    return [
//        { id: 'AN', value: 'Alias' },
//        { id: 'HRN', value: 'Hondcaoll' },
//        { id: 'MN', value: 'MaidCDwame' },
//        { id: 'NN', value: 'Nick Name' },
//        { id: 'PSN', value: 'Presedeal Salutaion' },
//        { id: 'PN', value: 'Primary Name' },
//        { id: 'U', value: 'Unknown' }
//    ]
//}

angular.module('constituent').controller('AddNameInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', 'constituentCRUDoperations', 'dropDownService','$rootScope',
    function ($scope, $filter, $uibModalInstance, $uibModal, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices,
    uibDateParser, constituentApiService, constituentCRUDoperations, dropDownService,$rootScope) {
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.name = {
            selected: {
                nameTypeCode: 'PN',
                nameType: 'Primary Name'
            },
            nameTypes: [],
            masterId: params.masterId,
            startDate: ConstUtilServices.getStartDate(),
            rowCode: CRUD_CONSTANTS.ROW_CODE,
            DWTimestamp: ConstUtilServices.getStartDate(),
            caseNo: caseNo,
            sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
            constType: $sessionStorage.type,

            cnstPrsnNameRegex: { prefix: /^[a-zA-Z. ]+$/, firstName: /^[a-zA-Z- ]+$/, lastName: /^[a-zA-Z- ]+$/, middleName: /^[a-zA-Z-. ]+$/, suffix: /^[a-zA-Z-. ]+$/, nickName: /^[a-zA-Z- ]+$/, maidenName: /^[a-zA-Z- ]+$/ }
        }

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_NAME).success(function (result) { $scope.name.nameTypes = result; }).error(function (result) { });

    $scope.submit = function () {

        if ($scope.myForm.$valid) {

            myApp.showPleaseWait();
            var postParams = {
                "masterid": $scope.name.masterId,
                "prefixname": $scope.name.prefixname,
                "firstname": $scope.name.firstname,
                "lastname": $scope.name.lastname,
                "middlename": $scope.name.middlename,
                "suffixname": $scope.name.suffixname,
                "nickname": $scope.name.nickname,
                "maidenname": $scope.name.maidenname,
                "NameTypeCode": $scope.name.selected.nameTypeCode,
                "SourceSystemCode": $scope.name.sourceSys,
                "CaseNumber": $scope.name.caseNo,
                "ConstType": $scope.name.constType
            };


            myApp.showPleaseWait();
            $('#modalCover').css("pointer-events", "none");
            //do something
            var output_msg;
            var trans_key;
            var finalMessage;
            var ReasonOrTransKey;
            var ConfirmationMessage;
            constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.NAME.ADD).success(function (result) {
                console.log(result);
                output_msg = result[0].o_outputMessage;
                trans_key = result[0].o_transaction_key;

                if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                    var output = {
                        finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                        ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                        ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                    }

                    constituentApiService.getApiDetails($scope.name.masterId, HOME_CONSTANTS.CONST_NAME).success(function (result) {
                        params.grid.data = '';
                        params.grid.data.length = 0;
                        //changed by srini
                        var newResult = filterConstituentData(result);
                        params.grid.data = newResult;
                        constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_NAME);
                        constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_NAME);
                        myApp.hidePleaseWait();
                        $uibModalInstance.close(output);

                    }).error(function (result) {
                        myApp.hidePleaseWait();
                        $uibModalInstance.dismiss('cancel');
                        nameErrorPopup($rootScope, result);
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
                nameErrorPopup($rootScope, result);
            });
        }

    };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);

/********************* Edit Name Modal Instance ***************/
angular.module('constituent').controller('EditNameInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', 'constituentCRUDoperations', 'dropDownService','$rootScope',
    function ($scope, $filter, $uibModalInstance, $uibModal, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices,
    uibDateParser, constituentApiService, constituentCRUDoperations, dropDownService, $rootScope) {
        console.log(params.row);
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        var row = params.row;

        // editRow.row_stat_cd = "L";

        $scope.name = {
            selected: {
                nameTypeCode: row.cnst_prsn_nm_typ_cd,
                nameType: ''
            },
            nameTypes: [],
            prefixname: row.cnst_prsn_prefix_nm,
            firstname: row.cnst_prsn_first_nm,
            lastname: row.cnst_prsn_last_nm,
            middlename: row.cnst_prsn_middle_nm,
            nickname: row.cnst_prsn_nick_nm,
            maidenname: row.cnst_prsn_mom_maiden_nm,
            suffixname: row.cnst_prsn_suffix_nm,
            //nameTypes: nameTypes(),
            masterId: params.masterId,
            startDate: row.cnst_nm_strt_dt,
            rowCode: row.row_stat_cd,
            DWTimestamp: row.dw_srcsys_trans_ts,
            caseNo: caseNo,
            sourceSys: row.arc_srcsys_cd,
            constType: $sessionStorage.type,
            cnstPrsnNameRegex: { prefix: /^[a-zA-Z. ]+$/, firstName: /^[a-zA-Z- ]+$/, lastName: /^[a-zA-Z- ]+$/, middleName: /^[a-zA-Z-. ]+$/, suffix: /^[a-zA-Z- ]+$/, nickName: /^[a-zA-Z- ]+$/, maidenName: /^[a-zA-Z- ]+$/ }
        };


      

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_NAME).success(
            function (result) {
                $scope.name.nameTypes = result;
                angular.forEach($scope.name.nameTypes, function (v, k) {
                    if (v.id == row.cnst_prsn_nm_typ_cd) {
                        //\\ $scope.name.nameTypeCode = v.id;
                        $scope.name.selected.nameType = v.value;
                    }
                });
            }).error(function (result) { });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "masterid": $scope.name.masterId,
                    "prefixname": $scope.name.prefixname,
                    "firstname": $scope.name.firstname,
                    "lastname": $scope.name.lastname,
                    "middlename": $scope.name.middlename,
                    "suffixname": $scope.name.suffixname,
                    "nickname": $scope.name.nickname,
                    "maidenname": $scope.name.maidenname,
                    "NameTypeCode": $scope.name.selected.nameTypeCode,
                    "OldSourceSystemCode": row.arc_srcsys_cd,
                    "SourceSystemCode": row.arc_srcsys_cd,
                    "OldBestLOSInd": row.cnst_prsn_nm_best_los_ind,
                    "OldNameTypeCode": row.cnst_prsn_nm_typ_cd,
                    "BestLOS": row.cnst_prsn_nm_best_los_ind,
                    "CaseNumber": $scope.name.caseNo,
                    "ConstType": $scope.name.constType
                };


                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                // var finalresults = constituentDataServices.getConstnameGrid();
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.NAME.EDIT).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.name.masterId, HOME_CONSTANTS.CONST_NAME).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_NAME);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_NAME);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            nameErrorPopup($rootScope, result);
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
                    nameErrorPopup($rootScope, result);
                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);




/*********************Delete Name Modal Controller****************/
angular.module('constituent').controller('DeleteNameInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService','dropDownService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', 'constituentCRUDoperations', '$rootScope',
    function ($scope, $filter, $uibModalInstance, $uibModal, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, dropDownService,
    ConstUtilServices, uibDateParser, constituentApiService, constituentCRUDoperations, $rootScope) {

        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        console.log(params.row);
        var row = params.row;

        // editRow.row_stat_cd = "L";

        $scope.name = {
            selected: {
                nameTypeCode: row.cnst_prsn_nm_typ_cd,
                nameType: ''
            },
            nameTypes: [],
            prefixname: row.cnst_prsn_prefix_nm,
            firstname: row.cnst_prsn_first_nm,
            lastname: row.cnst_prsn_last_nm,
            middlename: row.cnst_prsn_middle_nm,
            nickname: row.cnst_prsn_nick_nm,
            maidenname: row.cnst_prsn_mom_maiden_nm,
            suffixname: row.cnst_prsn_suffix_nm,
            //nameTypes: nameTypes(),
            masterId: params.masterId,
            startDate: row.cnst_nm_strt_dt,
            rowCode: row.row_stat_cd,
            DWTimestamp: row.dw_srcsys_trans_ts,
            caseNo: caseNo,
            constType: $sessionStorage.type,
            sourceSys: row.arc_srcsys_cd
        };

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_NAME).success(
            function (result) {
                $scope.name.nameTypes = result;
                angular.forEach($scope.name.nameTypes, function (v, k) {
                    if (v.id == row.cnst_prsn_nm_typ_cd) {
                        //\\ $scope.name.nameTypeCode = v.id;
                        $scope.name.selected.nameType = v.value;
                    }
                });
            }).error(function (result) { });

       
        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "masterid": $scope.name.masterId,
                    "prefixname": $scope.name.prefixname,
                    "firstname": $scope.name.firstname,
                    "lastname": $scope.name.lastname,
                    "middlename": $scope.name.middlename,
                    "suffixname": $scope.name.suffixname,
                    "nickname": $scope.name.nickname,
                    "maidenname": $scope.name.maidenname,
                    "NameTypeCode": $scope.name.selected.nameTypeCode,
                    "OldSourceSystemCode": row.arc_srcsys_cd,
                    "SourceSystemCode": row.arc_srcsys_cd,
                    "OldBestLOSInd": row.cnst_prsn_nm_best_los_ind,
                    "OldNameTypeCode": row.cnst_prsn_nm_typ_cd,
                    "BestLOS": row.cnst_prsn_nm_best_los_ind,
                    "CaseNumber": $scope.name.caseNo,
                    "ConstType": $scope.name.constType
                };
                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.NAME.DELETE).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.name.masterId, HOME_CONSTANTS.CONST_NAME).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_NAME);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_NAME);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            nameErrorPopup($rootScope, result);
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
                    nameErrorPopup($rootScope, result);
                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);


function orgNameTypes() {
    // return  ['Alias', 'Honor Roll', 'Maiden Name', 'Nick Name', 'Presedential Salutaion', 'Primary Name', 'Unknown'];
    return []
}


////ORG ADD/EDIT/DELETE ////
angular.module('constituent').controller('AddOrgNameInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', 'constituentCRUDoperations', 'dropDownService','$rootScope',
    function ($scope, $filter, $uibModalInstance, $uibModal, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices,
    uibDateParser, constituentApiService, constituentCRUDoperations, dropDownService, $rootScope) {
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.orgName = {
            selected: {
                orgNameTypeCode: 'PN',
                orgNameType: 'Primary Name',
                note: "",
            },
            nameTypes: [],
            masterId: params.masterId,
            startDate: ConstUtilServices.getStartDate(),
            rowCode: CRUD_CONSTANTS.ROW_CODE,
            DWTimestamp: ConstUtilServices.getStartDate(),
            caseNo: caseNo,
            notes: CRUD_CONSTANTS.NOTES,
            sourceSys: CRUD_CONSTANTS.SOURCE_SYS,
            constType: $sessionStorage.type,
            cnstOrgNameRegex: { orgNm: /^[a-zA-Z0-9-.', ]+$/ }
        }

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_ORG_NAME).success(function (result) { $scope.orgName.nameTypes = result; }).error(function (result) { });

     $scope.submit = function () {

         if ($scope.myForm.$valid) {

             myApp.showPleaseWait();
             var postParams = {
                 "masterid": $scope.orgName.masterId,
                 "OrgName": $scope.orgName.name,
                 "OrgNameTypeCode": $scope.orgName.selected.orgNameTypeCode,
                 "SourceSystemCode": $scope.orgName.sourceSys,
                 "Notes": $scope.orgName.selected.note,
                 "CaseNumber": $scope.orgName.caseNo,
                 "ConstType": $scope.orgName.constType
             };


             myApp.showPleaseWait();
             $('#modalCover').css("pointer-events", "none");
             //do something
             var output_msg;
             var trans_key;
             var finalMessage;
             var ReasonOrTransKey;
             var ConfirmationMessage;
             constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.ORG_NAME.ADD).success(function (result) {
                 console.log(result);
                 output_msg = result[0].o_outputMessage;
                 trans_key = result[0].o_transaction_key;

                 if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                     var output = {
                         finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                         ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                         ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                     }

                     constituentApiService.getApiDetails($scope.orgName.masterId, HOME_CONSTANTS.CONST_ORG_NAME).success(function (result) {
                         params.grid.data = '';
                         params.grid.data.length = 0;
                         //changed by srini
                         var newResult = filterConstituentData(result);
                         params.grid.data = newResult;
                         constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_ORG_NAME);
                         constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_ORG_NAME);
                         myApp.hidePleaseWait();
                         $uibModalInstance.close(output);

                     }).error(function (result) {
                         myApp.hidePleaseWait();
                         $uibModalInstance.dismiss('cancel');
                         nameErrorPopup($rootScope, result);
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
                 nameErrorPopup($rootScope, result);
             });
         }

     };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);


/********************* Edit ORg Name Modal Instance ***************/
angular.module('constituent').controller('EditOrgNameInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', 'constituentCRUDoperations', 'dropDownService','$rootScope',
    function ($scope, $filter, $uibModalInstance, $uibModal, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices,
    uibDateParser, constituentApiService, constituentCRUDoperations, dropDownService, $rootScope) {
        console.log(params.row);
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        var row = params.row;


        $scope.orgName = {
            selected: {
                orgNameTypeCode: '',
                orgNameType: '',
                note: "",
            },
            nameTypes: [],
            name: row.cnst_org_nm,
            masterId: params.masterId,
            startDate: row.cnst_org_nm_strt_dt,
            endDate: row.cnst_org_nm_end_dt,
            rowCode: row.row_stat_cd,
            DWTimestamp: row.dw_srcsys_trans_ts,
            caseNo: caseNo,
            sourceSys: row.arc_srcsys_cd,
            loadID: row.load_id,
            notes: CRUD_CONSTANTS.NOTES,
            constType: $sessionStorage.type,
            cnstOrgNameRegex: { orgNm: /^[a-zA-Z0-9-.', ]+$/ }
        };


        

        dropDownService.getDropDown(HOME_CONSTANTS.CONST_ORG_NAME).success(
            function (result) {
                $scope.orgName.nameTypes = result;
                angular.forEach($scope.orgName.nameTypes, function (v, k) {
                    if (v.id == row.cnst_org_nm_typ_cd) {
                        $scope.orgName.selected.orgNameType = v.value;
                        $scope.orgName.selected.orgNameTypeCode = v.id;
                    }
                });
            }).error(function (result) { });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "masterid": $scope.orgName.masterId,
                    "OrgName": $scope.orgName.name,
                    "OrgNameTypeCode": $scope.orgName.selected.orgNameTypeCode,
                    "OldSourceSystemCode": row.arc_srcsys_cd,
                    "SourceSystemCode": row.arc_srcsys_cd,
                    "OldBestLOSInd": row.cnst_org_nm_best_los_ind,
                    "OldOrgNameTypeCode": row.cnst_org_nm_typ_cd,
                    "BestLOS": row.cnst_org_nm_best_los_ind,
                    "Notes": $scope.orgName.selected.note,
                    "CaseNumber": $scope.orgName.caseNo,
                    "ConstType": $scope.orgName.constType
                };


                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                // var finalresults = constituentDataServices.getConstnameGrid();
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.ORG_NAME.EDIT).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.orgName.masterId, HOME_CONSTANTS.CONST_ORG_NAME).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_ORG_NAME);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_ORG_NAME);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            nameErrorPopup($rootScope, result);
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
                    nameErrorPopup($rootScope, result);
                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);




/********************* Delete Org Name Modal Instance ***************/
angular.module('constituent').controller('DeleteOrgNameInstantiateCtrl', ['$scope', '$filter', '$uibModalInstance', '$uibModal', '$localStorage', '$sessionStorage', 'dropDownService', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', 'constituentCRUDoperations','$rootScope',
    function ($scope, $filter, $uibModalInstance, $uibModal, $localStorage, $sessionStorage, dropDownService, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices,
    uibDateParser, constituentApiService, constituentCRUDoperations, $rootScope) {
        console.log(params.row);
        var row = params.row;

        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.orgName = {
            selected: {
                orgNameTypeCode: '',
                orgNameType: '',
                note: "",
            },
            nameTypes: [],
            name: row.cnst_org_nm,
            masterId: params.masterId,
            startDate: row.cnst_org_nm_strt_dt,
            endDate: row.cnst_org_nm_end_dt,
            rowCode: row.row_stat_cd,
            DWTimestamp: row.dw_srcsys_trans_ts,
            caseNo: caseNo,
            sourceSys: row.arc_srcsys_cd,
            loadID: row.load_id,
            constType: $sessionStorage.type,
            notes: CRUD_CONSTANTS.NOTES,
        };


        dropDownService.getDropDown(HOME_CONSTANTS.CONST_ORG_NAME).success(
           function (result) {
               $scope.orgName.nameTypes = result;
               angular.forEach($scope.orgName.nameTypes, function (v, k) {
                   if (v.id == row.cnst_org_nm_typ_cd) {
                       $scope.orgName.selected.orgNameType = v.value;
                       $scope.orgName.selected.orgNameTypeCode = v.id;
                   }
               });
           }).error(function (result) { });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();
                var postParams = {
                    "masterid": $scope.orgName.masterId,
                    "OrgName": $scope.orgName.name,
                    "OrgNameTypeCode": $scope.orgName.selected.orgNameTypeCode,
                    "OldSourceSystemCode": row.arc_srcsys_cd,
                    "SourceSystemCode": row.arc_srcsys_cd,
                    "OldBestLOSInd": row.cnst_org_nm_best_los_ind,
                    "OldOrgNameTypeCode": row.cnst_org_nm_typ_cd,
                    "BestLOS": row.cnst_org_nm_best_los_ind,
                    "Notes": $scope.orgName.selected.note,
                    "CaseNumber": $scope.orgName.caseNo,
                    "ConstType": $scope.orgName.constType
                };


                $('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;
                // var finalresults = constituentDataServices.getConstnameGrid();
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.ORG_NAME.DELETE).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.orgName.masterId, HOME_CONSTANTS.CONST_ORG_NAME).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            //changed by srini
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.CONST_ORG_NAME);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.CONST_ORG_NAME);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

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
                    nameErrorPopup($rootScope, result);
                });
            }

        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);


function nameErrorPopup($rootScope, result) {
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