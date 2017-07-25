angular.module('transaction').controller('TransactionDetailsUnmergeController', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$localStorage', 'TransactionDataServices', '$state', '$window', '$rootScope', 'TransactionMultiGridServices', 'TransactionApiServices', 'TransactionMultiDataServices','TransactionServices', '$q','$timeout',
function ($scope, uiGridConstants, $uibModal, $stateParams, $localStorage, TransactionDataServices,
    $state, $window, $rootScope, TransactionMultiGridServices, TransactionApiServices, TransactionMultiDataServices,TransactionServices, $q,$timeout) {

    var initialize = function () {

       // console.log("Grid Params");
       // console.log($scope.grid_1);
       // console.log($scope.grid_2);
        $scope.toggleDetails = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.toggleUnmergeButtons = { "display": "none" };
        $scope.BaseURL = BasePath;

        var options_rq = TransactionMultiGridServices.getGridOptions(uiGridConstants, HOME_TRANS_CONSTANTS.TRANS_UNMERGE_REQUEST_LOG);
        $scope.gridOptions_RQ = options_rq;
        $scope.gridOptions_RQ.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

        var options_pr = TransactionMultiGridServices.getGridOptions(uiGridConstants, HOME_TRANS_CONSTANTS.TRANS_UNMERGE_PROCESS_LOG);
        $scope.gridOptions_PR = options_pr;
        $scope.gridOptions_PR.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

        /***** added by srini for show/hide of buttons *****/
        $scope.showMergeUnmergeSection = true;
        if (TransactionDataServices.getDenyApprovePermission()) {
            $scope.showMergeUnmergeSection = false;
        }
        /***** added by srini for show/hide of buttons *****/




    };
    initialize();

   // console.log("In Unmerge Controller .............");

    var savedDetailsParams = TransactionDataServices.getSavedTransMultiSearchParams();

   // console.log("Saved Search Params");
   // console.log(savedDetailsParams);

    $scope.trans_typ_dsc = savedDetailsParams.trans_typ_dsc;
    $scope.sub_trans_typ_dsc = savedDetailsParams.sub_trans_typ_dsc;

    $scope.trans_stat = savedDetailsParams.trans_stat;

    //console.log("Transaction Status here ");
   // console.log($scope.trans_stat);

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

   // console.log("Printing the params for unmerge");
   // console.log(params);

    if (params) {

        var firstPromise = TransactionApiServices.getApiUnmergeProcessDetails(params).success(function (results) {

            $scope.gridOptions_PR.data = "";
            $scope.gridOptions_PR.data.length = 0;
            $scope.gridOptions_PR.data = results;
            //added by srini for pagination
            $scope.totalItems = $scope.gridOptions_PR.data.length;
           // console.log(results);

            //Store the master id array for merge research
            $scope.unmergeResearchRows = $scope.gridOptions_PR.data;

            var arc_srcsys_cds = [];
            var srcsys_cnst_uids = [];
            var request_ids = [];

            angular.forEach($scope.unmergeResearchRows, function (v, k) {
                arc_srcsys_cds.push(v.arc_srcsys_cd);
                srcsys_cnst_uids.push(v.srcsys_cnst_uid);
                request_ids.push(v.dw_request_tracking_key);
            });

            $scope.arc_srcsys_cds = arc_srcsys_cds;

            $scope.srcsys_cnst_uids = srcsys_cnst_uids;

            $scope.request_ids = request_ids;

           // console.log($scope.arc_srcsys_cds);
           // console.log($scope.srcsys_cnst_uids);
           // console.log($scope.request_ids);

            var detailData = [];

            detailData[0] = {
                "Detail1": null, "Detail2": null, "Detail3": null, "Detail4": null, "Detail5": null,
                "RequestTrackingId1": null, "RequestTrackingId2": null, "RequestTrackingId3": null, "RequestTrackingId4": null,
                "RequestTrackingId5": null, "header": "Request Tracking Id"
            }

            detailData[1] = {
                "Detail1": null, "Detail2": null, "Detail3": null, "Detail4": null, "Detail5": null,
                "RequestTrackingId1": null, "RequestTrackingId2": null, "RequestTrackingId3": null, "RequestTrackingId4": null,
                "RequestTrackingId5": null, "header": "Source System Code"
            }

            detailData[2] = {
                "Detail1": null, "Detail2": null, "Detail3": null, "Detail4": null, "Detail5": null,
                "RequestTrackingId1": null, "RequestTrackingId2": null, "RequestTrackingId3": null, "RequestTrackingId4": null,
                "RequestTrackingId5": null, "header": "Source System UId"
            }

            for (var i = 0; i < $scope.arc_srcsys_cds.length; i++) {
                var detailIndex = "Detail" + (i + 1);
                detailData[1][detailIndex] = $scope.arc_srcsys_cds[i];
            }

            for (var i = 0; i < $scope.srcsys_cnst_uids.length; i++) {
                var detailIndex = "Detail" + (i + 1);
                detailData[2][detailIndex] = $scope.srcsys_cnst_uids[i];
            }

            for (var i = 0; i < $scope.request_ids.length; i++) {
                var detailIndex = "RequestTrackingId" + (i + 1);
                detailData[0][detailIndex] = $scope.request_ids[i];
                detailData[1][detailIndex] = $scope.request_ids[i];
                detailData[2][detailIndex] = $scope.request_ids[i];
            }


           // console.log("Detail Data Param at the end ");
           // console.log(detailData);

            $scope.detailData = detailData;

        }).error(function (result) {
            $scope.pleaseWait = { "display": "none" };
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });

        var secondPromise = TransactionApiServices.getApiUnmergeRequestDetails(params).success(function (results) {
            $scope.gridOptions_RQ.data = "";
            $scope.gridOptions_RQ.data.length = 0;
            $scope.gridOptions_RQ.data = results;
           // console.log(results);

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
        $q.all([firstPromise, secondPromise]).then(function () {
            $scope.toggleDetails = { "display": "block" };
            if ($scope.trans_stat.toUpperCase().indexOf("WAITING FOR APPROVAL") != -1) {
                $scope.toggleUnmergeButtons = { "display": "block" };
            }
            $scope.toggleHeader = { "display": "block" };
            $scope.pleaseWait = { "display": "none" };
        });

    }

    //added by srini for pagination
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };


    $scope.$on('unmergeGridRefreshResult', function (event, arg) {
        $scope.toggleUnmergeButtons = { "display": "none" };
    });

    $scope.$on('unmergeRejectGridRefreshResult', function (event, arg) {
        $scope.toggleUnmergeButtons = { "display": "none" };
    });

    $scope.ApproveUnmerge = function () {
       // console.log("In the approve unmerge button click");
       // console.log($scope.trans_stat);
        var approverParams = {
            "TransactionKey": $scope.trans_key,
            "TransactionStatus": "In Progress" //Hard coded status for approved

        }


      //  console.log("Approver Params are ");
        myApp.showPleaseWait();
        TransactionApiServices.postUpdateTransactionStatusDetails(approverParams).success(function (results) {
            if (results.data == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                $scope.pleaseWait = { "display": "none" };
                myApp.hidePleaseWait();
                angular.element("#accessDeniedModal").modal();
            }
            else 
            {
                var searchResultsParam = null;

                if (TransactionDataServices.getTransSavedSearchParams()) {

                    searchResultsParam = TransactionDataServices.getTransSavedSearchParams();
                }
                else {
                    //Or get it from local storage itself
                    searchResultsParam = $localStorage.transAdvSearchParams;

                }

               // console.log("Search Results Params that are set  ...");
               // console.log(searchResultsParam);

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

          //  console.log("After Final Modal close and redirection ...");
           // console.log(searchResultsParam);

            //Trigger the event to hide the approve , reject buttons
            $rootScope.$broadcast('unmergeGridRefreshResult', searchResultsParam);
            $("#accessTransDeniedModal").modal('hide');
        }
    };

    $scope.RejectUnmerge = function () {
       // console.log("In the reject unmerge button click");
       // console.log($scope.trans_stat);
        var rejectParams = {
            "TransactionKey": $scope.trans_key,
            "TransactionStatus": "Rejected", //Hard coded status for rejected
           // "ApproverName": ""
        }

       // console.log("Reject Params are ");
       // console.log(rejectParams);
        myApp.showPleaseWait();
        TransactionApiServices.postUpdateTransactionStatusDetails(rejectParams).success(function (results) {
            if (results.data == TRANSACTION_CRUD_CONSTANTS.ACCESS_DENIED) {
                $scope.pleaseWait = { "display": "none" };
                myApp.hidePleaseWait();
                angular.element("#accessDeniedRejectModal").modal();
            }
            else
            {
                var searchResultsParam = null;

                if (TransactionDataServices.getTransSavedSearchParams()) {

                    searchResultsParam = TransactionDataServices.getTransSavedSearchParams();
                }
                else {
                    //Or get it from local storage itself
                    searchResultsParam = $localStorage.transAdvSearchParams;

                }

               // console.log(searchResultsParam);

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

            var searchResultsParam = TransactionDataServices.getTransSavedSearchParams();

           // console.log("After Final Modal close and redirection ...");
           // console.log(searchResultsParam);

            //Trigger the event to hide the approve , reject buttons
            $rootScope.$broadcast('unmergeRejectGridRefreshResult', searchResultsParam);
            $("#accessTransDeniedModal").modal('hide');
            $("#accessDeniedRejectModal").modal('hide');
        }
    };

    $scope.Research = function () {

       // console.log("In Research");
      //  console.log($scope.arc_srcsys_cds);
       // console.log($scope.srcsys_cnst_uids);
       // console.log($scope.request_ids);
       // console.log($scope.detailData);

       // console.log("In the research unmerge to make a dummy server call");
      //  console.log($scope.trans_stat);
        var researchParams = {
            "TransactionKey": $scope.trans_key,
            "TransactionStatus": "Rejected", //Hard coded status for rejected
            //"ApproverName": ""
        }

       // console.log("Research Params are ");
       // console.log(researchParams);

        //Make a dummy server call to check for logged in user's access

        myApp.showPleaseWait();

        TransactionApiServices.postUnmergeTransactionResearchDetails(researchParams).success(function (results) {
           // console.log("User have access");

            function pleaseWaitTimeoutPromise() {
                return $timeout(function () {
                    return myApp.hidePleaseWait()
                }, 2000);
            }
            
            //Once the promise is returned proceed with the rest
            pleaseWaitTimeoutPromise().then(function (result) {
                getUnmergeResearchPopup($scope, $uibModal, $scope.detailData, $rootScope);
            });

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




    };

}]);

angular.module('transaction').controller('RejectUnmergeCtrl', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$rootScope', '$state', '$uibModalInstance', 'rejectParams', 'TransactionApiServices', 'TransactionDataServices',
function ($scope, uiGridConstants, $uibModal, $stateParams, $rootScope,
    $state, $uibModalInstance, rejectParams, TransactionApiServices, TransactionDataServices) {

   

}]);

angular.module('transaction').controller('UnmergeResearchCtrl', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$rootScope', '$state', '$uibModalInstance', 'params', function ($scope, uiGridConstants,
     $uibModal, $stateParams, $rootScope, $state, $uibModalInstance, params) {

       // console.log("Params in merge research controller");
      //  console.log(params);

        $scope.unmergeResearch = {
            srcsysuid: {},
            srcsys: {},
            requestid: {},

            toggleResearchCompare: false,
            pleaseWait: true,
            toggleUnmergeResearchSection: true,

        };

       // console.log("Row in params parameter");
       // console.log(params.row);

        //this is how i get the result from the backend so had to process like this
        angular.forEach(params.row, function (v, k) {
            if (v.header == "Request Tracking Id") {
                $scope.unmergeResearch.requestTrackingId1 = v.RequestTrackingId1;
                $scope.unmergeResearch.requestTrackingId2 = v.RequestTrackingId2;
                $scope.unmergeResearch.requestTrackingId3 = v.RequestTrackingId3;
                $scope.unmergeResearch.requestTrackingId4 = v.RequestTrackingId4;
                $scope.unmergeResearch.requestTrackingId5 = v.RequestTrackingId5;
            }
            else if (v.header == "Source System Code") {
                $scope.unmergeResearch.srcsys.detail1 = v.Detail1;
                $scope.unmergeResearch.srcsys.detail2 = v.Detail2;
                $scope.unmergeResearch.srcsys.detail3 = v.Detail3;
                $scope.unmergeResearch.srcsys.detail4 = v.Detail4;
                $scope.unmergeResearch.srcsys.detail5 = v.Detail5;
            }
            else if (v.header == "Source System UId") {
                $scope.unmergeResearch.srcsysuid.detail1 = v.Detail1;
                $scope.unmergeResearch.srcsysuid.detail2 = v.Detail2;
                $scope.unmergeResearch.srcsysuid.detail3 = v.Detail3;
                $scope.unmergeResearch.srcsysuid.detail4 = v.Detail4;
                $scope.unmergeResearch.srcsysuid.detail5 = v.Detail5;
            }

        });

        $scope.unmergeResearch.toggleResearchCompare = true;

        $scope.unmergeResearch.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }

}]);

getUnmergeResearchPopup = function ($scope, $uibModal, rows, $rootScope) {
   // console.log("Rows param in getUnmergeResearchPopup function");
   // console.log(rows);
    var templateUrl = BasePath + 'App/Transaction/Views/Multi/UnmergeResearch.tpl.html';
    var controller = 'UnmergeResearchCtrl';
    OpenUnmergeResearchModal($scope, $uibModal, null, templateUrl, controller, null, rows, 'lg', $rootScope);
};

OpenUnmergeResearchModal = function ($scope, $uibModal, $stateParams, templ, ctrl, grid, row, size, $rootScope) {
    var masterId = '';

    var params = {
        masterId: null,
        row: row,
        grid: grid

    }

   // console.log("In Open Modal method");
   // console.log(params);

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


var myApp;
myApp = myApp || (function () {
    //var pleaseWaitDiv = $('<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');
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