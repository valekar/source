angular.module('constituent').controller('ConstAlternateIdsCtrl',
    ['$scope', '$stateParams', 'HandleResults', 'ExtendedServices', 'GridServices', 'StoreData', 'ExtendedDataServices','$sessionStorage','uiGridConstants', '$rootScope',
    function ($scope, $stateParams, HandleResults, ExtendedServices, GridServices, StoreData, ExtendedDataServices, $sessionStorage, uiGridConstants, $rootScope) {
    /*
    StoreData : this is present in ConstCemServices.js file

    */
    var initialize = function () {
        $scope.alternate = {
            columnDef: ExtendedServices.getConsAlternateCols(),
            //gridOptions: 
            toggleDetails: false,
            togglePleaseWait: true,
            totalItems: 0,
            currentPage: 1,
            masterId: $stateParams.constituentId,
            distinctRecordCount: 0
            
        }
        $scope.alternate.gridOptions = GridServices.getGridOptions($scope.alternate.columnDef);
        //console.log("INitialized");
        $scope.alternate.gridOptions.onRegisterApi = function (gridApi) {
            //set gridApi on scope
            $scope.alternate.gridApi = gridApi;
        }

       
        showAlternateDetails()
    }
    initialize();

    $scope.$on('alternate_ids', function (event, args) {

        if (args.alternate_ids) {
            $scope.alternate.togglePleaseWait = true;
            showAlternateDetails();

        } else {
            $scope.alternate.togglePleaseWait = false;
            $scope.alternate.toggleDetails = false;
        }

    });


    $scope.alternate.pageChanged = function (page) {
        $scope.alternate.gridApi.pagination.seek(page);
    }

        //filter for search results
    $rootScope.toggleAlternateIdsSearchFilter = function () {
        //console.log("called DNC");
        $scope.alternate.gridOptions.enableFiltering = !$scope.alternate.gridOptions.enableFiltering;
        $scope.alternate.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }


    //showing records 
    function showAlternateDetails() {
        if (StoreData.getAlternateIdsList().length <= 0) {
            refreshGrid();
        }
        else {
            var res = StoreData.getAlternateIdsList();
            $scope.alternate.gridOptions = GridServices.refreshGridData($scope.alternate.gridOptions, res);
            $scope.alternate.distinctRecordCount = res[0].distinct_records_count;
            $scope.alternate.togglePleaseWait = false;
            $scope.alternate.toggleDetails = true;
        }
    }

    //referesh grid after add/edit/delete operations
    function refreshGrid() {
        var postParams = {
            cnst_typ_cd: $sessionStorage.type,
            cnst_mstr_id: $scope.alternate.masterId
        }


        ExtendedDataServices.getAlternateIds(postParams, false)
        .then(function (result) {
            //console.log(result);
            StoreData.addAlternateIdsList(result, filterConstituentData(result));
            $scope.alternate.gridOptions = GridServices.refreshGridData($scope.alternate.gridOptions, StoreData.getAlternateIdsList());
            $scope.alternate.togglePleaseWait = false;
            $scope.alternate.toggleDetails = true;
            //console.log(StoreData.getAlternateIdsList());
            $scope.alternate.totalItems = StoreData.getAlternateIdsList().length;
            if (StoreData.getAlternateIdsList().length > 0) {
                $scope.alternate.distinctRecordCount = StoreData.getAlternateIdsList()[0].distinct_records_count;
            }
        },
        function (error) {
            $scope.alternate.togglePleaseWait = false;
            HandleResults(error);
        });
    }

}]);