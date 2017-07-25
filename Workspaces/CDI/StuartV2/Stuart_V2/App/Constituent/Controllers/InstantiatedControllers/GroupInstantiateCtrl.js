angular.module('constituent').controller("AddGrpMembershpInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'dropDownService',
    'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, dropDownService,
    ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.grpMembershp = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                groupName: "",
                groupNameCode: ""
            },
            groupNames: [],
            assignmentMethod:"Constituent Maintenance",
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            caseNo: caseNo,
            constType: $sessionStorage.type
        };

        dropDownService.getDropDown(HOME_CONSTANTS.GRP_MEMBERSHIP).success(function (result) { $scope.grpMembershp.groupNames = result; }).error(function (result) { });

        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.grpMembershp.masterId,
                    "GroupKey": $scope.grpMembershp.selected.groupNameCode,
                    "AssignmentMethod": $scope.grpMembershp.assignmentMethod,
                    "Notes": $scope.grpMembershp.selected.note,
                    "CaseNumber": $scope.grpMembershp.caseNo,
                    "ConstType": $scope.grpMembershp.constType
                };




                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.GRP_MEMBERSHIP.ADD).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.grpMembershp.masterId, HOME_CONSTANTS.GRP_MEMBERSHIP).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.GRP_MEMBERSHIP);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.GRP_MEMBERSHIP);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            groupErrorPopup($rootScope, result);
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
                    groupErrorPopup($rootScope, result);
                });
            };

           
        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

    }]);

angular.module('constituent').controller("EditGrpMembershpInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService',
    'dropDownService', 'ConstUtilServices', 'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService,
    dropDownService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        console.log(params.row);
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };

        $scope.grpMembershp = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                groupName: "",
                groupNameCode: params.row.grp_key
            },
            groupNames: [],
            assignmentMethod: params.row.assgnmnt_mthd,
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            caseNo: caseNo,
            constType: $sessionStorage.type
        };

        dropDownService.getDropDown(HOME_CONSTANTS.GRP_MEMBERSHIP).success(function (result) {
            $scope.grpMembershp.groupNames = result;
            angular.forEach($scope.grpMembershp.groupNames, function (v, k) {
                console.log(v);
                if ($scope.grpMembershp.selected.groupNameCode == v.id) {
                    $scope.grpMembershp.selected.groupName = v.value;
                }

            });
        }).error(function (result) { });

       


        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.grpMembershp.masterId,
                    "GroupKey": $scope.grpMembershp.selected.groupNameCode,
                    "AssignmentMethod": $scope.grpMembershp.assignmentMethod,
                    "Notes": $scope.grpMembershp.selected.note,
                    "OldGroupKey": params.row.grp_key,
                    "OldAssignmentMethod": params.row.assgnmnt_mthd,
                    "CaseNumber": $scope.grpMembershp.caseNo,
                    "ConstType": $scope.grpMembershp.constType

                };




                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.GRP_MEMBERSHIP.EDIT).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.EDIT_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.grpMembershp.masterId, HOME_CONSTANTS.GRP_MEMBERSHIP).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.GRP_MEMBERSHIP);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.GRP_MEMBERSHIP);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            groupErrorPopup($rootScope, result);
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
                    groupErrorPopup($rootScope, result);
                });
            };

          
        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

    }]);

angular.module('constituent').controller("DeleteGrpMembershpInstantiateCtrl", ['$scope', '$filter', '$uibModalInstance', '$localStorage', '$sessionStorage', 'dropDownService', 'globalServices',
    'params', 'constMultiDataService', 'constMultiGridService', 'uiGridConstants', 'constituentCRUDapiService', 'ConstUtilServices',
    'uibDateParser', 'constituentApiService', '$rootScope', function ($scope, $filter, $uibModalInstance, $localStorage, $sessionStorage, dropDownService, globalServices,
    params, constMultiDataService, constMultiGridService, uiGridConstants, constituentCRUDapiService, ConstUtilServices, uibDateParser, constituentApiService, $rootScope) {
        console.log(params.row);
        //case Prepopulation
        if (globalServices.getCaseTabCaseNo() != null) {
            var caseNo = globalServices.getCaseTabCaseNo();
        };
        $scope.grpMembershp = {
            selected: {
                note: CRUD_CONSTANTS.DEFAULT_NOTE,
                groupName: "",
                groupNameCode: params.row.grp_key
            },
            groupNames: [],
            assignmentMethod: params.row.assgnmnt_mthd,
            notes: CRUD_CONSTANTS.NOTES,
            masterId: params.masterId,
            caseNo: caseNo,
            constType: $sessionStorage.type
        };

        dropDownService.getDropDown(HOME_CONSTANTS.GRP_MEMBERSHIP).success(function (result) {
            $scope.grpMembershp.groupNames = result;
            angular.forEach($scope.grpMembershp.groupNames, function (v, k) {
                if ($scope.grpMembershp.selected.groupNameCode == v.id) {
                    $scope.grpMembershp.selected.groupName = v.value;
                }

            });
        }).error(function (result) { });


        $scope.submit = function () {
            if ($scope.myForm.$valid) {
                myApp.showPleaseWait();

                //$scope.parseDate($scope.birth.fullDate);

                var postParams = {
                    "MasterID": $scope.grpMembershp.masterId,
                    "GroupKey": $scope.grpMembershp.selected.groupNameCode,
                    "AssignmentMethod": $scope.grpMembershp.assignmentMethod,
                    "Notes": $scope.grpMembershp.selected.note,
                    "OldGroupKey": params.row.grp_key,
                    "OldAssignmentMethod": params.row.assgnmnt_mthd,
                    "CaseNumber": $scope.grpMembershp.caseNo,
                    "ConstType": $scope.grpMembershp.constType

                };




                angular.element('#modalCover').css("pointer-events", "none");
                //do something
                var output_msg;
                var trans_key;
                var finalMessage;
                var ReasonOrTransKey;
                var ConfirmationMessage;

                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.GRP_MEMBERSHIP.DELETE).success(function (result) {
                    console.log(result);
                    output_msg = result[0].o_outputMessage;
                    trans_key = result[0].o_transaction_key;

                    if (output_msg == CRUD_CONSTANTS.PROCEDURE.SUCCESS) {
                        var output = {
                            finalMessage: CRUD_CONSTANTS.DELETE_SUCCESS_MESSAGE,
                            ReasonOrTransKey: CRUD_CONSTANTS.SUCCESS_REASON + trans_key,
                            ConfirmationMessage: CRUD_CONSTANTS.SUCCESS_CONFIRM
                        }

                        constituentApiService.getApiDetails($scope.grpMembershp.masterId, HOME_CONSTANTS.GRP_MEMBERSHIP).success(function (result) {
                            params.grid.data = '';
                            params.grid.data.length = 0;
                            var newResult = filterConstituentData(result);
                            params.grid.data = newResult;
                            constMultiDataService.setData(newResult, HOME_CONSTANTS.GRP_MEMBERSHIP);
                            constMultiDataService.setFullData(result, HOME_CONSTANTS.GRP_MEMBERSHIP);
                            myApp.hidePleaseWait();
                            $uibModalInstance.close(output);

                        }).error(function (result) {
                            myApp.hidePleaseWait();
                            $uibModalInstance.dismiss('cancel');
                            groupErrorPopup($rootScope, result);
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
                    groupErrorPopup($rootScope, result);
                });
            };


        }
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

    }]);


function groupErrorPopup($rootScope, result) {
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