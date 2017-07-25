angular.module('transaction').controller('TransactionDetailsMergeController', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$localStorage', 'TransactionDataServices', '$state', '$window', '$rootScope', 'TransactionMultiGridServices', 'TransactionApiServices', 'TransactionMultiDataServices','TransactionServices', '$q',
function ($scope, uiGridConstants, $uibModal, $stateParams, $localStorage, TransactionDataServices,
    $state, $window, $rootScope, TransactionMultiGridServices, TransactionApiServices, TransactionMultiDataServices,TransactionServices, $q) {

    var initialize = function () {

        console.log("Grid Params");
        console.log($scope.grid_1);
        console.log($scope.grid_2);
        $scope.toggleDetails = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.toggleMergeButtons = { "display": "none" };
        $scope.BaseURL = BasePath;

        var options = TransactionMultiGridServices.getGridOptions(uiGridConstants, HOME_TRANS_CONSTANTS.TRANS_MERGE);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;

        }

        console.log("Merge Grid Options");
        /***** added by srini for show/hide of buttons *****/
        $scope.showMergeUnmergeSection = true;
        if (TransactionDataServices.getDenyApprovePermission()) {
            $scope.showMergeUnmergeSection = false;
        }
        /***** added by srini for show/hide of buttons *****/


    };
    initialize();


    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };



    console.log("In Merge Controller .............");

    var savedDetailsParams = TransactionDataServices.getSavedTransMultiSearchParams();

    console.log("Saved Search Params");
    console.log(savedDetailsParams);

    $scope.trans_typ_dsc = savedDetailsParams.trans_typ_dsc;
    $scope.sub_trans_typ_dsc = savedDetailsParams.sub_trans_typ_dsc;

    $scope.trans_stat = savedDetailsParams.trans_stat;

    console.log("Transaction Status here ");
    console.log($scope.trans_stat);

    $scope.trans_key = savedDetailsParams.trans_key;

    $scope.backToDetailsPage = function () {
        $state.go('transaction.search.results', {});
    };

    //get the consId from URL;
    var params = {
        "trans_key": $scope.trans_key,
        "trans_typ_dsc": $scope.trans_typ_dsc,
        "sub_trans_typ_dsc": $scope.sub_trans_typ_dsc
    };

    console.log("Printing the params for unmerge");
    console.log(params);

    if (params) {

        var firstPromise = TransactionApiServices.getApiMergeDetails(params).success(function (results) {

            $scope.gridOptions.data = "";
            $scope.gridOptions.data.length = 0;
            $scope.gridOptions.data = results;
            $scope.totalItems = $scope.gridOptions.data.length;
            console.log(results);

            //Store the master id array for merge research
            $scope.mergeResearchRows = $scope.gridOptions.data;

            var masterIds = [];
            var cnstTypes = [];

            angular.forEach($scope.mergeResearchRows, function (v, k) {
                masterIds.push(v.cnst_mstr_id);
                cnstTypes.push(v.cnst_type);
            });

            $scope.masterIds = masterIds;

            if (cnstTypes[0] == "Individual") {
                $scope.mergeResearchCnstType = "IN";
            }
            else {
                $scope.mergeResearchCnstType = "OR";
            }

            console.log($scope.masterIds);
            console.log($scope.mergeResearchCnstType);

        }).error(function (result) {
            $scope.pleaseWait = { "display": "none" };
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });

        $scope.pleaseWait = { "display": "block" };
        $q.all([firstPromise]).then(function () {
            $scope.toggleDetails = { "display": "block" };
            if ($scope.trans_stat.toUpperCase().indexOf("WAITING FOR APPROVAL") != -1) {
                $scope.toggleMergeButtons = { "display": "block" };
            }
            $scope.toggleHeader = { "display": "block" };
            $scope.pleaseWait = { "display": "none" };
        });

    }

    $scope.$on('mergeApproveGridRefreshResult', function (event, arg) {
        $scope.toggleMergeButtons = { "display": "none" };
    });

    $scope.$on('mergeRejectGridRefreshResult', function (event, arg) {
        $scope.toggleMergeButtons = { "display": "none" };
    });

    $scope.ApproveMerge = function () {
        console.log("In the approve unmerge button click");
        console.log($scope.trans_stat);
        var approverParams = {
            "TransactionKey": $scope.trans_key,
            "TransactionStatus": "In Progress" //Hard coded status for approved
        }

        console.log("Approver Params are ");
        myApp.showPleaseWait();

        TransactionApiServices.postUpdateTransactionStatusDetails(approverParams).success(function (results) {
            if (results.data == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                $scope.pleaseWait = { "display": "none" };
                myApp.hidePleaseWait();
                angular.element("#accessDeniedModal").modal();
            }
            else {

                var searchResultsParam = null;

                if (TransactionDataServices.getTransSavedSearchParams()) {

                    searchResultsParam = TransactionDataServices.getTransSavedSearchParams();
                }
                else {
                    //Or get it from local storage itself
                    searchResultsParam = $localStorage.transAdvSearchParams;

                }

                console.log("Search Results Params that are set  ...");
                console.log(searchResultsParam);

                //Fire the transaction search again 

                TransactionServices.getTransactionAdvSearchResults(searchResultsParam).success(function (result) {
                    TransactionDataServices.setTransSearchResultsData(result);
                    $localStorage.lastTransSearchResultData = result;
                    myApp.hidePleaseWait();

                    $("#accessTransDeniedModal").modal('show');
                }).error(function (result) {
                    myApp.hidePleaseWait();

                    if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                        angular.element("#accessDeniedModal").modal();
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                    }
                });



            }
        }).error(function (result) {
            myApp.hidePleaseWait();
            if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                angular.element("#accessDeniedModal").modal();
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });

        $scope.closeTransModal = function () {

            //Get the saved search params for search results page
            var searchResultsParam = TransactionDataServices.getTransSavedSearchParams();

            console.log("After Final Modal close and redirection ...");
            console.log(searchResultsParam);

            //Trigger the event to hide the approve , reject buttons
            $rootScope.$broadcast('mergeApproveGridRefreshResult', searchResultsParam);
            $("#accessTransDeniedModal").modal('hide');
        }
    };

    $scope.RejectMerge = function () {
        console.log("In the reject merge button click");
        console.log($scope.trans_stat);
        var rejectParams = {
            "TransactionKey": $scope.trans_key,
            "TransactionStatus": "Rejected" //Hard coded status for rejected
        }

        console.log("Reject Params are ");
        console.log(rejectParams);
        myApp.showPleaseWait();

        TransactionApiServices.postUpdateTransactionStatusDetails(rejectParams).success(function (results) {
            if (results.data == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                $scope.pleaseWait = { "display": "none" };
                angular.element("#accessDeniedRejectModal").modal();
            }
            else {

                var searchResultsParam = null;

                if (TransactionDataServices.getTransSavedSearchParams()) {

                    searchResultsParam = TransactionDataServices.getTransSavedSearchParams();
                }
                else {
                    //Or get it from local storage itself
                    searchResultsParam = $localStorage.transAdvSearchParams;

                }

                console.log(searchResultsParam);

                //Fire the transaction search again 

                TransactionServices.getTransactionAdvSearchResults(searchResultsParam).success(function (result) {
                    TransactionDataServices.setTransSearchResultsData(result);
                    $localStorage.lastTransSearchResultData = result;
                    myApp.hidePleaseWait();
                    $("#accessDeniedRejectModal").modal('show');
                }).error(function (result) {
                    myApp.hidePleaseWait();

                    if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                    }
                });



            }
        }).error(function (result) {
            myApp.hidePleaseWait();

            if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                angular.element("#accessDeniedModal").modal();
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });

        $scope.closeTransModal = function () {

            //Get the saved search params for search results page
            var searchResultsParam = TransactionDataServices.getTransSavedSearchParams();

            console.log("After Final Modal close and redirection ...");
            console.log(searchResultsParam);

            //Trigger the event to hide the approve , reject buttons
            $rootScope.$broadcast('mergeRejectGridRefreshResult', searchResultsParam);
            $("#accessTransDeniedModal").modal('hide');
            $("#accessDeniedRejectModal").modal('hide');
        }

    };

    $scope.Research = function () {

        $scope.pleaseWait = { "display": "block" };

        console.log("In Research");
        console.log($scope.masterIds);

        console.log($scope.mergeResearchCnstType);

        var postParams = {
            "ConstituentType": $scope.mergeResearchCnstType,
            "MasterId": $scope.masterIds
        }

        TransactionApiServices.getMergeResearchResultDetails(postParams).success(function (result) {
            $scope.pleaseWait = { "display": "none" };
            if (result.data == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                $scope.pleaseWait = { "display": "none" };
                angular.element("#accessDeniedModal").modal();
            }
            else {
                var sendResult = {
                    data: result,
                    rows: $scope.mergeResearchRows
                }

                getMergeResearchPopup($scope, $uibModal, sendResult, $rootScope);
            }
        }).error(function (result) {
            $scope.pleaseWait = { "display": "none" };
            if (result == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                angular.element("#accessDeniedModal").modal();
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });


    };

}]);

angular.module('transaction').controller('ApproveMergeCtrl', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$rootScope', '$state', '$uibModalInstance', 'approverParams', 'TransactionApiServices', 'TransactionDataServices',
function ($scope, uiGridConstants, $uibModal, $stateParams, $rootScope,
    $state, $uibModalInstance, approverParams, TransactionApiServices, TransactionDataServices) {


}]);

angular.module('transaction').controller('RejectMergeCtrl', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$rootScope', '$state', '$uibModalInstance', 'rejectParams', 'TransactionApiServices', 'TransactionDataServices',
function ($scope, uiGridConstants, $uibModal, $stateParams, $rootScope,
    $state, $uibModalInstance, rejectParams, TransactionApiServices, TransactionDataServices) {

  
}]);

angular.module('transaction').controller('MergeResearchCtrl', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$rootScope', '$state', '$uibModalInstance', 'params', function ($scope, uiGridConstants,
     $uibModal, $stateParams, $rootScope, $state, $uibModalInstance, params) {

        console.log("Params in merge research controller");
        console.log(params);

        $scope.mergeResearch = {
            dsp: {},
            bridgePresence: {},
            name: {},
            address: {},
            phone: {},
            email: {},
            birth: {},
            death: {},
            toggleResearchCompare: false,
            pleaseWait: true,
            toggleMergeResearchSection: true,


        };

        console.log("Row in params parameter");
        console.log(params.row);

        //this is how i get the result from the backend so had to process like this
        angular.forEach(params.row.data, function (v, k) {
            if (v.header == "Master Id") {
                $scope.mergeResearch.masterId1 = v.MasterId1;
                $scope.mergeResearch.masterId2 = v.MasterId2;
                $scope.mergeResearch.masterId3 = v.MasterId3;
                $scope.mergeResearch.masterId4 = v.MasterId4;
                $scope.mergeResearch.masterId5 = v.MasterId5;
            }
            else if (v.header == "Dsp Id") {
                $scope.mergeResearch.dsp.detail1 = v.Detail1;
                $scope.mergeResearch.dsp.detail2 = v.Detail2;
                $scope.mergeResearch.dsp.detail3 = v.Detail3;
                $scope.mergeResearch.dsp.detail4 = v.Detail4;
                $scope.mergeResearch.dsp.detail5 = v.Detail5;
            }
            else if (v.header == "Bridge Presence") {
                $scope.mergeResearch.bridgePresence.detail1 = v.Detail1;
                $scope.mergeResearch.bridgePresence.detail2 = v.Detail2;
                $scope.mergeResearch.bridgePresence.detail3 = v.Detail3;
                $scope.mergeResearch.bridgePresence.detail4 = v.Detail4;
                $scope.mergeResearch.bridgePresence.detail5 = v.Detail5;
            }
            else if (v.header == "Name") {
                $scope.mergeResearch.name.detail1 = v.Detail1;
                $scope.mergeResearch.name.detail2 = v.Detail2;
                $scope.mergeResearch.name.detail3 = v.Detail3;
                $scope.mergeResearch.name.detail4 = v.Detail4;
                $scope.mergeResearch.name.detail5 = v.Detail5;
            }
            else if (v.header == "Address") {
                $scope.mergeResearch.address.detail1 = v.Detail1;
                $scope.mergeResearch.address.detail2 = v.Detail2;
                $scope.mergeResearch.address.detail3 = v.Detail3;
                $scope.mergeResearch.address.detail4 = v.Detail4;
                $scope.mergeResearch.address.detail5 = v.Detail5;
            }
            else if (v.header == "Email") {
                $scope.mergeResearch.email.detail1 = v.Detail1;
                $scope.mergeResearch.email.detail2 = v.Detail2;
                $scope.mergeResearch.email.detail3 = v.Detail3;
                $scope.mergeResearch.email.detail4 = v.Detail4;
                $scope.mergeResearch.email.detail5 = v.Detail5;
            }
            else if (v.header == "Phone") {
                $scope.mergeResearch.phone.detail1 = v.Detail1;
                $scope.mergeResearch.phone.detail2 = v.Detail2;
                $scope.mergeResearch.phone.detail3 = v.Detail3;
                $scope.mergeResearch.phone.detail4 = v.Detail4;
                $scope.mergeResearch.phone.detail5 = v.Detail5;
            }
            else if (v.header == "Death") {
                $scope.mergeResearch.death.detail1 = v.Detail1;
                $scope.mergeResearch.death.detail2 = v.Detail2;
                $scope.mergeResearch.death.detail3 = v.Detail3;
                $scope.mergeResearch.death.detail4 = v.Detail4;
                $scope.mergeResearch.death.detail5 = v.Detail5;
            }
            else if (v.header == "Birth") {
                $scope.mergeResearch.birth.detail1 = v.Detail1;
                $scope.mergeResearch.birth.detail2 = v.Detail2;
                $scope.mergeResearch.birth.detail3 = v.Detail3;
                $scope.mergeResearch.birth.detail4 = v.Detail4;
                $scope.mergeResearch.birth.detail5 = v.Detail5;
            }
        });

        $scope.mergeResearch.toggleResearchCompare = true;

        $scope.mergeResearch.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }

    }]);


getMergeResearchPopup = function ($scope, $uibModal, rows, $rootScope) {
    console.log("Rows param in getMergeResearchPopup function");
    console.log(rows);
    var templateUrl = BasePath + 'App/Transaction/Views/Multi/MergeResearch.tpl.html';
    var controller = 'MergeResearchCtrl';
    OpenMergeResearchModal($scope, $uibModal, null, templateUrl, controller, null, rows, 'lg', $rootScope);
};

OpenMergeResearchModal = function ($scope, $uibModal, $stateParams, templ, ctrl, grid, row, size, $rootScope) {
    var masterId = '';

    var params = {
        masterId: null,
        row: row,
        grid: grid

    }

    console.log("In Open Modal method");
    console.log(params);

    var CssClass = '';
    if (size === 'lg') {
        CssClass = 'app-modal-window';
    }

    var ModalInstance = ModalInstance || $uibModal.open({
        animation: $scope.animationsEnabled,
        templateUrl: templ,
        controller: ctrl,  // Logic in instantiated controller 
        windowClass: CssClass,
        backdrop: 'static',
        resolve: {
            params: function () {
                return params;
            }
        }
    });

    ModalInstance.result.then(function (result) {
        modalMessage($rootScope, result);
    })

};