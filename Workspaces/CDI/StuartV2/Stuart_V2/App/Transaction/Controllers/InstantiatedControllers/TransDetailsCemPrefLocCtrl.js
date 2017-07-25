angular.module('transaction').controller('TransDetailsPrefLocCtrl', ['$scope', 'uiGridConstants', '$uibModal',
    '$stateParams', '$localStorage', 'TransactionDataServices', '$state', '$rootScope', 'TransactionMultiGridServices', 'TransactionApiServices', 'TransactionMultiDataServices', 'TRANS_CONSTANTS',
    function ($scope, uiGridConstants, $uibModal, $stateParams, $localStorage, TransactionDataServices,
    $state, $rootScope, TransactionMultiGridServices, TransactionApiServices, TransactionMultiDataServices, TRANS_CONSTANTS) {


        var initialize = function () {
            $scope.toggleDetails = false;
            $scope.pleaseWait = false;
            $scope.BaseURL = BasePath;

            var options = TransactionMultiGridServices.getGridOptions(uiGridConstants, TRANS_CONSTANTS.TRANS_CEM_PREF_LOC_DETAILS);
            $scope.gridOptions = options;
            $scope.gridOptions.onRegisterApi = function (grid) {
                $scope.gridApi = grid;
            }


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
                TransactionApiServices.getApiCemPrefLocDetails(params).success(function (results) {
                    // console.log("Got data back from service in Grp Membership controller");
                    // console.log(results);
                    $scope.gridOptions.data = "";
                    $scope.gridOptions.data.length = 0;
                    $scope.gridOptions.data = results;
                    //added by srini for pagination
                    $scope.totalItems = $scope.gridOptions.data.length;
                    // console.log(results);
                    $scope.toggleDetails = true;
                    $scope.toggleHeader = true;
                    $scope.pleaseWait = false;
                }).error(function (result) {
                    $scope.pleaseWait = false;
                    if (result == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                    }
                    else if (result == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                        messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                    }
                });;
            }

        };
        initialize();




        //added by srini for pagination
        $scope.pageChanged = function (page) {
            $scope.gridApi.pagination.seek(page);
        };

    }]);