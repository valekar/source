
function setFullDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
    constMultiGridService, constant, gridOp) {
    var _constDetailsData = constMultiDataService.getFullData(constant);
    gridOp = constMultiGridService.getMultiGridLayout(gridOp, uiGridConstants, _constDetailsData, constant);
    var finalResults = {
        itemCount: _constDetailsData.length,
        gridOp: gridOp
    }
    //  console.log(finalResults);
    return finalResults

}


function setDataGrid($scope, constituentApiService, uiGridConstants, constMultiDataService,
    constMultiGridService, constant, gridOp) {
    var _constDetailsData = constMultiDataService.getData(constant);
    gridOp = constMultiGridService.getMultiGridLayout(gridOp, uiGridConstants, _constDetailsData, constant);
    var finalResults = {
        itemCount: _constDetailsData.length,
        gridOp: gridOp
    }
    //  console.log(finalResults);
    return finalResults

}

function commonAddGridRow($scope, row, constituentCRUDoperations, $uibModal, $stateParams, type, gridOption) {
    constituentCRUDoperations.getAddModal($scope, $uibModal, $stateParams, null, type, gridOption);
}

function commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, gridOption, constant) {
    constituentCRUDoperations.getEditModal($scope, $uibModal, $stateParams, gridOption, row, constant);
}


function commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, gridOption, constant) {
    constituentCRUDoperations.getDeleteModal($scope, $uibModal, $stateParams, gridOption, row, constant);
}

function callConstSetup($scope, constituentApiService, postObj, uiGridConstants, constMultiDataService,
    constMultiGridService, constant, gridOp, $rootScope) {

    var _constDetailsData = constMultiDataService.getData(constant);
    if (_constDetailsData.length <= 0) {
        constituentApiService.getApiDetails(postObj, constant).success(function (result) {
            //set the value in cache
            var newResult = filterConstituentData(result);
            constMultiDataService.setData(newResult, constant);
            constMultiDataService.setFullData(result, constant);
            //call the grid layout and set the data
            gridOp = constMultiGridService.getMultiGridLayout(gridOp, uiGridConstants, newResult, constant);
            //show the grid
            constMultiGridService.getToggleDetails($scope, true, constant);

        }).error(function (result) {

            constMultiGridService.getToggleDetails($scope, true, constant);
            if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                angular.element("#accessDeniedModal").modal();
            }
            else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
        });
    }
    else {
        //set the cache data  to grid
        gridOp = constMultiGridService.getMultiGridLayout(gridOp, uiGridConstants, _constDetailsData, constant);
        //show the grid
        constMultiGridService.getToggleDetails($scope, true, constant);
    }

    var finalResults = {
        itemCount: constMultiDataService.getData(constant).length,
        gridOp: gridOp
    }
    //  console.log(finalResults);
    return finalResults;
}

//actie records
function filterConstituentData(result) {
    var newResult = [];
    angular.forEach(result, function (v, k) {
        //console.log(v);
        if ("row_stat_cd" in v) {
            if (v.row_stat_cd == "L") {

            }
            else if ("transNotes" in v) {
                if (v.transNotes.toLowerCase().indexOf("deleted") <= 0) {
                    newResult.push(v);

                }
            }
            else if (v.row_stat_cd == "U") {
                var allKeys = Object.keys(v);
                var endDateKey = allKeys.find(EndDate);
                //active records
                if (v[endDateKey].indexOf("9999") > 0) {
                    newResult.push(v);
                }
            }
            else {
                newResult.push(v);
            }
        }
        else if ("transNotes" in v) {
            if (v.transNotes.toLowerCase().indexOf("deleted") <= 0) {
                newResult.push(v);
            }
        }
        else {
            newResult.push(v);
        }

    });

    return newResult;
}

function EndDate(element, ind, array) {
    if (element.endsWith("_ts")) {
        return element;
    }
}

var myApp;
myApp = myApp || (function () {
    //var pleaseWaitDiv = $('<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');
    var pleaseWaitDiv = $('<div class="modal fade " id="pleaseWaitDialog"  data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="basicModal" aria-hidden="true" tabindex="-1"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h3>Processing...</h3></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar" style="width: 100%;"><span class="sr-only">60% Complete</span></div></div></div></div></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal('show');
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        },
    };
})();



angular.module('constituent').controller("ShowDetailsCtrl", ['$scope', '$uibModal', '$stateParams', '$rootScope', 'showDetailsPostApiService',
    'constMultiDataService', 'constituentCRUDapiService',
    function ($scope, $uibModal, $stateParams, $rootScope, showDetailsPostApiService, constMultiDataService, constituentCRUDapiService) {

        $scope.details = {
            title: "",
            togglePleaseWait: false
        };

        $scope.details.showDetails = function (type) {
            switch (type) {
                case HOME_CONSTANTS.CONST_BIRTH: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "birth",
                        ConstituentId: $stateParams.constituentId
                    };

                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Birth";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CONST_BIRTH, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }
                case HOME_CONSTANTS.CONST_DEATH: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "death",
                        ConstituentId: $stateParams.constituentId
                    };

                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Death";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CONST_DEATH, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }
                case HOME_CONSTANTS.CONST_EMAIL: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "email",
                        ConstituentId: $stateParams.constituentId
                    };

                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Email";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CONST_EMAIL, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }
                case HOME_CONSTANTS.CONST_NAME: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "namein",
                        ConstituentId: $stateParams.constituentId
                    };

                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Individual Name";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CONST_NAME, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }
                case HOME_CONSTANTS.CONST_ORG_NAME: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "nameor",
                        ConstituentId: $stateParams.constituentId
                    };

                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Organization Name";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CONST_ORG_NAME, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }
                case HOME_CONSTANTS.CONST_EXT_BRIDGE: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "externalbridge",
                        ConstituentId: $stateParams.constituentId
                    };

                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "External Bridge";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CONST_EXT_BRIDGE, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }
                case HOME_CONSTANTS.CONST_PHONE: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "phone",
                        ConstituentId: $stateParams.constituentId
                    };

                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Phone";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CONST_PHONE, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }

                case HOME_CONSTANTS.INTERNAL_BRIDGE: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "internalbridge",
                        ConstituentId: $stateParams.constituentId
                    };

                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Internal Bridge";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_INTERNAL_BRIDGE, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }
                case HOME_CONSTANTS.CONST_PREF: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "contactpreference",
                        ConstituentId: $stateParams.constituentId
                    };
                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Contact Preference";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CONT_PREF, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }
                case HOME_CONSTANTS.CHARACTERISTICS: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "characteristics",
                        ConstituentId: $stateParams.constituentId
                    };
                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Characteristics";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CHARACTERISTICS, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }
                case HOME_CONSTANTS.CONST_ADDRESS: {
                    var postParams = {
                        DetailsType: "cdi",
                        DetailsName: "address",
                        ConstituentId: $stateParams.constituentId
                    };
                    $scope.details.togglePleaseWait = true;
                    $scope.details.title = "Address";
                    callShowDetailsService($scope, $uibModal, $rootScope, HOME_CONSTANTS.SHOW_CONST_ADDRESS, showDetailsPostApiService, postParams, $scope.details.title);
                    break;
                }

            }
        }
    }]);


function callShowDetailsService($scope, $uibModal, $rootScope, type, showDetailsPostApiService, postParams, title) {
    var templ = BasePath + "App/Constituent/Views/common/ShowDetails.tpl.html";
    var ctrl = "ShowPopupDetailsCtrl";
    showDetailsPostApiService.postApiService(postParams, type).success(function (result) {
        // console.log(title);
        OpenShowDetailsModal($scope, $uibModal, templ, ctrl, type, result, $rootScope, title);
        $scope.details.togglePleaseWait = false;
    }).error(function (result) {

        $scope.details.togglePleaseWait = false;
        if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
            //angular.element("#accessDeniedModal").modal();
            messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
        }
        else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == CRUD_CONSTANTS.DB_ERROR) {
            messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }
    });
}


angular.module('constituent').controller("ShowPopupDetailsCtrl", ['$scope', 'params', '$uibModalInstance', 'showDetailGridService', 'uiGridConstants', 'constMultiDataService', '$timeout',
    function ($scope, params, $uibModalInstance, showDetailGridService, uiGridConstants, constMultiDataService, $timeout) {
        angular.element('#modalCover').css("pointer-events", "none");


        $scope.showDetails = {
            gridOptions: showDetailGridService.getShowGridOptions(params.type),
            result: [],
            toggleDetails: false,
            title: params.title,
            currentPage: 1,
            totalItems: 0
        }


        $scope.showDetails.gridOptions.onRegisterApi = function (grid) {
            $scope.showDetails.gridApi = grid;
        }

        $scope.showDetails.pageChanged = function (page) {
            $scope.showDetails.gridApi.pagination.seek($scope.showDetails.currentPage);
        }

        $scope.showDetails.result = params.result;
        $timeout(function () {

            $scope.showDetails.gridOptions.data = '';
            $scope.showDetails.gridOptions.data.length = 0;
            $scope.showDetails.gridOptions.data = $scope.showDetails.result;
            $scope.showDetails.totalItems = $scope.showDetails.result.length;

        }, 100);




        $timeout(function () {
            $scope.showDetails.toggleDetails = true;
        }, 200);

        $scope.showDetails.back = function () {
            $uibModalInstance.dismiss('cancel');
            $scope.$parent.toggleConstBirthLoader = false;
        }

    }]);

angular.module('constituent').controller('MasterDetailCtrl', ['$scope', 'constituentApiService', '$stateParams', 'constMultiDataService', '$rootScope',
    function ($scope, constituentApiService, $stateParams, constMultiDataService, $rootScope) {

        $scope.master = {
            togglePleaseWait: true,
            toggleMasterDetails: false,
            masterResult: {},
            oldMasterIdResult: {},
            privateResult: {}
        }


        if (constMultiDataService.getData(HOME_CONSTANTS.MASTER_DETAIL).length > 0) {
            $scope.master.masterResult = result[0];
            $scope.master.toggleMasterDetails = true;
            $scope.master.togglePleaseWait = false;
        }
        else {
            constituentApiService.getApiDetails($stateParams.constituentId, HOME_CONSTANTS.MASTER_DETAIL).success(function (result) {
                constMultiDataService.setData(result[0], HOME_CONSTANTS.MASTER_DETAIL);
                $scope.master.masterResult = result[0];
                $scope.master.toggleMasterDetails = true;
                $scope.master.togglePleaseWait = false;
            }).error(function (result) {
                if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                    $scope.master.togglePleaseWait = false;
                    //angular.element("#accessDeniedModal").modal();
                    messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
                }
                else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == CRUD_CONSTANTS.DB_ERROR) {
                    messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
            });
        }



        constituentApiService.getApiDetails($stateParams.constituentId, HOME_CONSTANTS.OLD_MASTER_IDS).success(function (result) {

            // console.log(result);
            $scope.master.oldMasterIdResult = "";
            for (var i = 0; i < result.length; i++) {
                if (i == 0) {
                    $scope.master.oldMasterIdResult += result[i].constituent_id;
                }
                else {
                    $scope.master.oldMasterIdResult += ", " + result[i].constituent_id;
                }

            }
            // $scope.master.oldMasterIdResult = result[0];

        }).error(function (result) {
            if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                $scope.master.togglePleaseWait = false;
                //angular.element("#accessDeniedModal").modal();
                messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
            }
            else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
        });

        constituentApiService.getApiDetails($stateParams.constituentId, HOME_CONSTANTS.PRIVATE).success(function (result) {
            if (result.length <= 0) {
                $scope.master.privateResult = "No";
            }
            else {
                $scope.master.privateResult = result[0].private_indicator;
            }


        }).error(function (result) {
            if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                $scope.master.togglePleaseWait = false;

                messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
            }
            else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
        });;




    }]);

function detailsFailModal($scope, output) {
    $scope.FinalMessage = output.finalMessage;
    $scope.ConfirmationMessage = output.ConfirmationMessage;
    $("#iDetailsConfirmationModal").modal();
}