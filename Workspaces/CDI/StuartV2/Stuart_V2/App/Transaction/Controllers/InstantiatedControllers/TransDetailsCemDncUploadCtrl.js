﻿angular.module('transaction').controller('TransDetailsCemDncUploadCtrl', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$localStorage', 'TransactionDataServices', '$state', '$rootScope', 'TransactionMultiGridServices', 'TransactionApiServices', 'TransactionMultiDataServices','TRANS_CONSTANTS',
function ($scope, uiGridConstants, $uibModal, $stateParams, $localStorage, TransactionDataServices,
    $state, $rootScope, TransactionMultiGridServices, TransactionApiServices, TransactionMultiDataServices,TRANS_CONSTANTS) {

    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.BaseURL = BasePath;

        var options = TransactionMultiGridServices.getGridOptions(uiGridConstants, TRANS_CONSTANTS.TRANS_CEM_DNC_UPLD_DETAILS);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

  

    var savedDetailsParams = TransactionDataServices.getSavedTransMultiSearchParams();

    $scope.trans_typ_dsc = savedDetailsParams.trans_typ_dsc;
    $scope.sub_trans_typ_dsc = savedDetailsParams.sub_trans_typ_dsc;

    $scope.trans_key = savedDetailsParams.trans_key;

    $scope.backToDetailsPage = function () {
        $state.go('transaction.search.results', {});
    };

    //get the trans_key from URL;
    var params = {
        "trans_key": $scope.trans_key,
        "trans_typ_dsc": $scope.trans_typ_dsc,
        "sub_trans_typ_dsc": $scope.sub_trans_typ_dsc
    };

    if (params) {
        $scope.pleaseWait = { "display": "block" };
        TransactionApiServices.getApiCemDncUploadDetails(params).success(function (results) {
            $scope.gridOptions.data = "";
            $scope.gridOptions.data.length = 0;
            $scope.gridOptions.data = results;
            //added by srini for pagination
            $scope.totalItems = $scope.gridOptions.data.length;
            // console.log(results);
            $scope.toggleDetails = { "display": "block" };
            $scope.toggleHeader = { "display": "block" };
            $scope.pleaseWait = { "display": "none" };
        }).error(function (result) {
            $scope.pleaseWait = { "display": "none" };
            if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
            }
        });
    }

    //added by srini for pagination
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

}]);



