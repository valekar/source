angular.module('upload').controller('GroupMembershipReferenceController', ['$scope', '$location', '$log',
    '$window', '$localStorage', 'mainService', '$state', '$rootScope', '$http', '$uibModal', '$uibModalStack', 'uiGridConstants', 'GroupMembershipReferenceServices',
function ($scope, $location, $log, $window, $localStorage, mainService, $state, $rootScope, $http, $uibModal, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices) {



    var initialize = function () {
        $scope.pleaseWait = { "display": "block" };

        $scope.groupMembershipReferenceDetailsVisible = false;

        var columnDefs = GroupMembershipReferenceServices.getReferenceColumnDef();

        var options = GroupMembershipReferenceServices.getReferenceGridOptions(uiGridConstants, columnDefs);
        $scope.groupMembershipReferenceGridOptions = options;
        $scope.groupMembershipReferenceGridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        };

        $scope.pageChanged = function (page) {
            $scope.gridApi.pagination.seek(page);
        };


        var _previousResults = GroupMembershipReferenceServices.getReferenceDataRecord();
        console.log(_previousResults);

        if (_previousResults.length > 0 && _previousResults != "Error") {

            $scope.searchResultsGridOptions = GroupMembershipReferenceServices.getGroupMembershipResultsGridLayout($scope.groupMembershipReferenceGridOptions, uiGridConstants, _previousResults);
            $scope.totalItems = _previousResults.length;
            $scope.pleaseWait = { "display": "none" };
        }
        else
        {
            GroupMembershipReferenceServices.getGroupMembershipReferenceData().success(function (results) {

                $scope.groupMembershipReferenceGridOptions.data = "";
                $scope.groupMembershipReferenceGridOptions.data.length = 0;
                $scope.groupMembershipReferenceDetailsVisible = true;
                $scope.groupMembershipReferenceGridOptions.data = results;
                $scope.totalItems = $scope.groupMembershipReferenceGridOptions.data.length;
                GroupMembershipReferenceServices.setReferenceDataRecord(results);
                $scope.pleaseWait = { "display": "none" };
            }).error(function (result) {
                $scope.pleaseWait = { "display": "none" };
                if (result == REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT) {
                    messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                if (result == REFERENCE_DATA_CONSTANTS.DB_ERROR) {
                    messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
        }
    };

    initialize();

    //Grid Refresh event after reference data record insertion/updation
    $rootScope.$on('groupMembershipGridRefresh', function (event, arg) {
        $scope.pleaseWait = { "display": "block" };

        GroupMembershipReferenceServices.getGroupMembershipReferenceData().success(function (results) {

            $scope.groupMembershipReferenceGridOptions.data = "";
            $scope.groupMembershipReferenceGridOptions.data.length = 0;
            $scope.groupMembershipReferenceDetailsVisible = true;
            $scope.groupMembershipReferenceGridOptions.data = results;
            $scope.totalItems = $scope.groupMembershipReferenceGridOptions.data.length;
            GroupMembershipReferenceServices.setReferenceDataRecord(results);
            $scope.pleaseWait = { "display": "none" };
        }).error(function (result) {
            $scope.pleaseWait = { "display": "none" };
            if (result == REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT) {
                messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            if (result == REFERENCE_DATA_CONSTANTS.DB_ERROR) {
                messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    });

    $scope.AddNewReferenceDataRecord = function (param) {
        getAddNewRecordPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices);
    };

    $scope.deleteReferenceDataRecord = function (row, grid) {
        GroupMembershipReferenceServices.setReferenceDataRow(row);
        openReferenceDataDeleteConfirmationModal($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices);

    };

    $scope.editReferenceDataRecord = function (row, grid) {
        GroupMembershipReferenceServices.setReferenceDataRow(row);
        openReferenceDataEditConfirmationModal($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices)
    };

}]);

angular.module('upload').controller("GroupMembershipReferenceValidationCtrl",['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'GroupMembershipReferenceServices', '$rootScope', '$localStorage', '$filter', 'referenceDataDropDownService', '$uibModalStack',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, GroupMembershipReferenceServices, $rootScope, $localStorage, $filter, referenceDataDropDownService, $uibModalStack) {

    $scope.referenceValidation = {
        groupKey: GroupMembershipReferenceServices.getGroupKey(),
        transKey: GroupMembershipReferenceServices.getTransKey(),
        outputHeader: GroupMembershipReferenceServices.getReferenceDataOutputMessage()
    };

    $scope.referenceValidation.back = function () {
        $uibModalInstance.dismiss('cancel');

        $state.go('upload.groupmembershipreferencetable', {});

        $rootScope.$emit('groupMembershipGridRefresh', {});
    }

}]);

angular.module('upload').controller("GroupMembershipReferenceAddRecordCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'GroupMembershipReferenceServices', '$rootScope', '$localStorage', '$filter', 'referenceDataDropDownService', '$uibModalStack','$q',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, GroupMembershipReferenceServices, $rootScope, $localStorage, $filter, referenceDataDropDownService, $uibModalStack, $q) {

    $scope.referenceAddRecord = {
        groupCode: "",
        groupName: "",
        groupTypes: [],
        groupType: {},
        subGroupTypes: [],
        subGroupType: {},
        assignmentMethods: [],
        assignmentMethod: {},
        groupOwnerMail: "",
        toggleValidationSummary: false,
        validationSummary: "** The group code or the group name already exists."
    };

    referenceDataDropDownService.getDropDown(REFERENCE_DATA_CONSTANTS.GROUP_TYPE).success(function (result) {
        $scope.referenceAddRecord.groupTypes = result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });

    referenceDataDropDownService.getDropDown(REFERENCE_DATA_CONSTANTS.SUB_GROUP_TYPE).success(function (result) {

        result.forEach(function (sub) {
            sub.group = Number(sub.value.split('|')[1]);
        });

        $scope.referenceAddRecord.subGroupTypes = result;

    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });

    referenceDataDropDownService.getDropDown(REFERENCE_DATA_CONSTANTS.GROUP_ASSIGNMENT_METHOD).success(function (result) {
        $scope.referenceAddRecord.assignmentMethods = result;
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });
    $scope.referenceAddRecord.populateGroupTypeDetails = function (groupType) {
        $scope.data = $scope.referenceAddRecord.groupType;
        $scope.referenceAddRecord.subGroupType = $scope.referenceAddRecord.subGroupTypes[0];
    };
    $scope.AddNewGroupMembershipReferenceRecord = function (param) {

        $scope.referenceAddRecord.toggleValidationSummary = false;

        var referenceDataRecord = GroupMembershipReferenceServices.getReferenceDataRecord();

        var groupCodeExists = 0;

        var groupNameExists = 0;

        var keepGoing = true;

        //Filters the inactive records to check against the existing group code , group name 
        referenceDataRecord = referenceDataRecord.filter(isInactive);

        function isInactive(obj) {
            return obj.row_stat_cd != "L";
        };

        angular.forEach(referenceDataRecord, function (k, v) {
            if (keepGoing) {
                if (k.grp_cd == $scope.referenceAddRecord.groupCode) {
                    groupCodeExists = 1;
                    keepGoing = false;
                }
            }
        });

        keepGoing = true;

        angular.forEach(referenceDataRecord, function (k, v) {
            if (keepGoing) {
                if (k.grp_nm == $scope.referenceAddRecord.groupName) {
                    groupNameExists = 1;
                    keepGoing = false;
                }
            }
        });

        if (groupNameExists) {
            $scope.referenceAddRecord.toggleValidationSummary = true;
            referenceDataGroupNameExists();
        }
        if (groupCodeExists) {
            $scope.referenceAddRecord.toggleValidationSummary = true;
            referenceDataGroupCodeExists();
        }

        if ($scope.referenceAddRecord.toggleValidationSummary == false) {

            myApp.showPleaseWait();
            //Close the modal first
            $uibModalInstance.dismiss('cancel');

            $scope.addNewGrpMbrshpRefRecordParams = {
                "groupCode": $scope.referenceAddRecord.groupCode,
                "groupName": $scope.referenceAddRecord.groupName,
                "groupType": $scope.referenceAddRecord.groupType == null ? "" : $scope.referenceAddRecord.groupType.value.split('|')[0],
                "subGroupType": $scope.referenceAddRecord.subGroupType == null ? "" : $scope.referenceAddRecord.subGroupType.value.split('|')[0],
                "groupAssignmentMethod": $scope.referenceAddRecord.assignmentMethod.value != null ? $scope.referenceAddRecord.assignmentMethod.value.split('|')[0] : "",
                "groupOwnerMail": $scope.referenceAddRecord.groupOwnerMail
            };

            GroupMembershipReferenceServices.postAddNewGrpMbrshpRefRecord($scope.addNewGrpMbrshpRefRecordParams).success(function (result) {

                myApp.hidePleaseWait();

                GroupMembershipReferenceServices.setGroupKey(result.groupKey);
                GroupMembershipReferenceServices.setTransKey(result.transKey);

                var firstPromise = GroupMembershipReferenceServices.getGroupMembershipReferenceData().success(function (result) {

                    GroupMembershipReferenceServices.setReferenceDataRecord(result);
                    myApp.hidePleaseWait();

                }).error(function (result) {
                    myApp.hidePleaseWait();

                    if (result == REFERENCE_DATA_CONSTANTS.ACCESS_DENIED) {
                        angular.element("#accessDeniedModal").modal();
                    }
                    else if (result == REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT) {
                        messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == REFERENCE_DATA_CONSTANTS.DB_ERROR) {
                        messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                    }
                });

                $q.all([firstPromise]).then(function () {
                    GroupMembershipReferenceServices.setReferenceDataOutputMessage("Reference Data Record Inserted Successfully");
                    getReferenceValidationResultPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, GroupMembershipReferenceServices);
                });
                
            }).error(function (result) {
                myApp.hidePleaseWait();

                if (result == REFERENCE_DATA_CONSTANTS.ACCESS_DENIED) {
                    angular.element("#accessDeniedModal").modal();
                }
                else if (result == REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT) {
                    messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == REFERENCE_DATA_CONSTANTS.DB_ERROR) {
                    messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
        } 
    }

    $scope.referenceAddRecord.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }

}]);

angular.module('upload').controller("GroupMembershipReferenceDeleteConfirmCtrl",['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'GroupMembershipReferenceServices', '$rootScope', '$localStorage', '$filter', 'referenceDataDropDownService', '$uibModalStack','$q',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, GroupMembershipReferenceServices, $rootScope, $localStorage, $filter, referenceDataDropDownService, $uibModalStack,$q) {

    var row_details = GroupMembershipReferenceServices.getReferenceDataRow();

    $scope.referenceDelete = {
        groupCode: "",
        groupName: "",
        groupType: "",
        assignmentMethod: "",
        ownerEmail: ""
    };

    $scope.referenceDelete = {
        groupCode: row_details.grp_cd,
        groupName: row_details.grp_nm,
        groupType: row_details.grp_typ,
        assignmentMethod: row_details.grp_assgnmnt_mthd,
        ownerEmail: row_details.grp_owner
    };

    $scope.referenceDataDeleteParam = {
        groupKey:row_details.grp_key
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }

    $scope.referenceDataDeleteSubmit = function () {
        myApp.showPleaseWait();
        //Close the modal first
        $uibModalInstance.dismiss('cancel');

        GroupMembershipReferenceServices.postDeleteGrpMbrshpRefRecord($scope.referenceDataDeleteParam).success(function (result) {
            myApp.hidePleaseWait();

            GroupMembershipReferenceServices.setGroupKey(result.groupKey);
            GroupMembershipReferenceServices.setTransKey(result.transKey);

            var firstPromise = GroupMembershipReferenceServices.getGroupMembershipReferenceData().success(function (result) {

                GroupMembershipReferenceServices.setReferenceDataRecord(result);

            }).error(function (result) {

                if (result == REFERENCE_DATA_CONSTANTS.ACCESS_DENIED) {
                    angular.element("#accessDeniedModal").modal();
                }
                else if (result == REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT) {
                    messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == REFERENCE_DATA_CONSTANTS.DB_ERROR) {
                    messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });

            $q.all([firstPromise]).then(function () {
                GroupMembershipReferenceServices.setReferenceDataOutputMessage("Reference Data Record Inactivated Successfully");
                getReferenceValidationResultPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, GroupMembershipReferenceServices);
            });
                
        }).error(function (result) {
            myApp.hidePleaseWait();

            if (result == REFERENCE_DATA_CONSTANTS.ACCESS_DENIED) {
                angular.element("#accessDeniedModal").modal();
            }
            else if (result == REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT) {
                messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == REFERENCE_DATA_CONSTANTS.DB_ERROR) {
                messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    }

}]);

angular.module('upload').controller("GroupMembershipReferenceEditConfirmCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'GroupMembershipReferenceServices', '$rootScope', '$localStorage', '$filter', 'referenceDataDropDownService', '$uibModalStack','$q',
function ($scope, $state, $uibModal, $uibModalInstance, uiGridConstants, GroupMembershipReferenceServices, $rootScope, $localStorage, $filter, referenceDataDropDownService, $uibModalStack, $q) {

    var row_details = GroupMembershipReferenceServices.getReferenceDataRow();

    $scope.referenceEdit = {
        groupCode: row_details.grp_cd,
        groupName: row_details.grp_nm,
        groupTypes: [],
        groupType:{},
        assignmentMethods: [],
        assignmentMethod:{},
        ownerEmail: row_details.grp_owner,
        subGroupTypes: [],
        subGroupType: {},
        groupKey: row_details.grp_key,
        toggleValidationSummary: false,
        validationSummary: "** The group code or the group name already exists."
    };

    $scope.pleaseWait = { "display": "block" };

    var firstPromise = referenceDataDropDownService.getDropDown(REFERENCE_DATA_CONSTANTS.GROUP_TYPE).success(function (result) {
        $scope.referenceEdit.groupTypes = result;

        angular.forEach(result,function (v) {
            if(v.value == row_details.grp_typ)
            {
                $scope.referenceEdit.groupType = v;
            }
        });
    }).error(function (result) {
        alert("Unable to retrieve dropdown values");
    });


    $q.all([firstPromise]).then(function () {
        referenceDataDropDownService.getDropDown(REFERENCE_DATA_CONSTANTS.SUB_GROUP_TYPE).success(function (result) {

            result.forEach(function (sub) {
                sub.group = Number(sub.value.split('|')[1]);
            });

            $scope.referenceEdit.subGroupTypes = result;

            angular.forEach(result, function (v) {
                if (v.group == $scope.referenceEdit.groupType.id && row_details.sub_grp_typ.trim() == v.value.split('|')[0].trim()) {
                    $scope.referenceEdit.subGroupType = v;
                }
            });

        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });

        referenceDataDropDownService.getDropDown(REFERENCE_DATA_CONSTANTS.GROUP_ASSIGNMENT_METHOD).success(function (result) {
            $scope.referenceEdit.assignmentMethods = result;

            angular.forEach(result, function (v) {
                if (v.value == row_details.grp_assgnmnt_mthd) {
                    $scope.referenceEdit.assignmentMethod = v;
                }
            });

        }).error(function (result) {
            alert("Unable to retrieve dropdown values");
        });

        $scope.pleaseWait = { "display": "none" };
    });

    $scope.referenceEdit.cancel = function () {
        $uibModalInstance.dismiss('cancel');

    }

    $scope.referenceDataEditSubmit = function () {

        $scope.referenceEdit.toggleValidationSummary = false;

        var referenceDataRecord = GroupMembershipReferenceServices.getReferenceDataRecord();

        var groupCodeExists = 0;

        var groupNameExists = 0;

        var keepGoing = true;

        //Filters the inactive records to check against the existing group code , group name 
        referenceDataRecord = referenceDataRecord.filter(isInactiveAndBeingEdited);

        function isInactiveAndBeingEdited(obj) {
            return (obj.row_stat_cd != "L" && (obj.grp_cd != row_details.grp_cd || obj.grp_nm != row_details.grp_nm));
        };

        angular.forEach(referenceDataRecord, function (k, v) {
            if (keepGoing) {
                if (k.grp_cd != row_details.grp_cd && k.grp_cd == $scope.referenceEdit.groupCode) {
                    groupCodeExists = 1;
                    keepGoing = false;
                }
            }
        });

        keepGoing = true;

        angular.forEach(referenceDataRecord, function (k, v) {
            if (keepGoing) {
                if (k.grp_nm == $scope.referenceEdit.groupName) {
                    groupNameExists = 1;
                    keepGoing = false;
                }
            }
        });

        if (groupNameExists) {
            $scope.referenceEdit.toggleValidationSummary = true;
            referenceDataGroupNameExists();
        }
        if (groupCodeExists) {
            $scope.referenceEdit.toggleValidationSummary = true;
            referenceDataGroupCodeExists();
        }

        

        if ($scope.referenceEdit.toggleValidationSummary == false) {

            $uibModalInstance.dismiss('cancel');

            myApp.showPleaseWait();

            var referenceEditParams = {
                groupCode: $scope.referenceEdit.groupCode,
                groupName: $scope.referenceEdit.groupName,
                groupOwnerMail: $scope.referenceEdit.ownerEmail,
                groupKey: $scope.referenceEdit.groupKey,
                groupType: $scope.referenceEdit.groupType.value,
                subGroupType: $scope.referenceEdit.subGroupType.value == undefined ? "" : $scope.referenceEdit.subGroupType.value.split('|')[0],
                assignmentMethod: $scope.referenceEdit.assignmentMethod.value
            };
            GroupMembershipReferenceServices.postEditGrpMbrshpRefRecord(referenceEditParams).success(function (result) {

                if (result.transOutput.toUpperCase() == "SUCCESS") {

                    GroupMembershipReferenceServices.setGroupKey(result.groupKey);
                    GroupMembershipReferenceServices.setTransKey(result.transKey);

                    var firstPromise = GroupMembershipReferenceServices.getGroupMembershipReferenceData().success(function (result) {

                        GroupMembershipReferenceServices.setReferenceDataRecord(result);
                        myApp.hidePleaseWait();

                    }).error(function (result) {
                        myApp.hidePleaseWait();

                        if (result == REFERENCE_DATA_CONSTANTS.ACCESS_DENIED) {
                            angular.element("#accessDeniedModal").modal();
                        }
                        else if (result == REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT) {
                            messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                        }
                        else if (result == REFERENCE_DATA_CONSTANTS.DB_ERROR) {
                            messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                        }
                    });

                    $q.all([firstPromise]).then(function () {
                        GroupMembershipReferenceServices.setReferenceDataOutputMessage("Reference Data Record Updated Successfully");
                        getReferenceValidationResultPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, GroupMembershipReferenceServices);
                    });

                }
            }).error(function (result) {
                myApp.hidePleaseWait();

                if (result == REFERENCE_DATA_CONSTANTS.ACCESS_DENIED) {
                    angular.element("#accessDeniedModal").modal();
                }
                else if (result == REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT) {
                    messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == REFERENCE_DATA_CONSTANTS.DB_ERROR) {
                    messageRefPopup($rootScope, REFERENCE_DATA_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                }
            });
        }
    }

}]);

 function getAddNewRecordPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices) {
    var templateUrl = BasePath + 'App/Upload/Views/GroupMembershipReferenceAddRecord.tpl.html';
    var controller = 'GroupMembershipReferenceAddRecordCtrl';
    OpenUploadModal($scope, $uibModal, null, templateUrl, controller, null, null, 'sm', $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices);
 }

 function getReferenceValidationResultPopup($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices) {
     var templateUrl = BasePath + 'App/Upload/Views/GroupMembershipRefenceDataValidation.tpl.html';
     var controller = 'GroupMembershipReferenceValidationCtrl';
     OpenUploadModal($scope, $uibModal, null, templateUrl, controller, null, null, 'sm', $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices);
 }

 function openReferenceDataDeleteConfirmationModal($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices) {
     var templateUrl = BasePath + 'App/Upload/Views/GroupMembershipReferenceDataDelete.tpl.html';
     var controller = 'GroupMembershipReferenceDeleteConfirmCtrl';
     OpenUploadModal($scope, $uibModal, null, templateUrl, controller, null, null, 'sm', $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices);
 }

 function openReferenceDataEditConfirmationModal($scope, $uibModal, $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices) {
     var templateUrl = BasePath + 'App/Upload/Views/GroupMembershipReferenceDataEdit.tpl.html';
     var controller = 'GroupMembershipReferenceEditConfirmCtrl';
     OpenUploadModal($scope, $uibModal, null, templateUrl, controller, null, null, 'sm', $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices);
 }

function OpenUploadModal($scope, $uibModal, $stateParams, templ, ctrl, grid, row, size, $rootScope, $state, $uibModalStack, uiGridConstants, GroupMembershipReferenceServices) {

var CssClass = '';
if (size === 'lg') {
    CssClass = 'app-modal-window';
}

    var ModalInstance = ModalInstance || $uibModal.open({
        animation: $scope.animationsEnabled,
        templateUrl: templ,
        controller: ctrl,  // Logic in instantiated controller 
        windowClass: CssClass,
        backdrop: 'static', /*  this prevent user interaction with the background  */
        keyboard: false
    });

    ModalInstance.result.then(function (result) {
       // console.log("Modal Instance Result " + result);
       // console.log("State Param before");
        //console.log($state);
    })
}

function messageRefPopup($rootScope, message, header) {
    $rootScope.message = message;
    $rootScope.header = header;
    angular.element("#iErrorModal").modal();
}

function referenceDataGroupNameExists() {
    return true;
}

function referenceDataGroupCodeExists() {
    return true;
}

var myApp;
myApp = myApp || (function () {

    var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="basicModal" aria-hidden="true" tabindex="-1"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h3>Processing...</h3></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar" style="width: 100%;"><span class="sr-only">60% Complete</span></div></div></div></div></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal('show');
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        },
    };
})();