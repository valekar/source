angular.module('constituent').controller('ConstRelationshipCtrl',
    ['$scope', '$stateParams', 'HandleResults', 'ExtendedServices', 'GridServices', 'StoreData', 'ExtendedDataServices', '$sessionStorage', 'uiGridConstants', '$rootScope',
    function ($scope, $stateParams, HandleResults, ExtendedServices, GridServices, StoreData, ExtendedDataServices, $sessionStorage, uiGridConstants, $rootScope) {
        /*
        StoreData : this is present in ExtendedServices.js file
    
        */
        var initialize = function () {
            $scope.relationship = {
                columnDef: ExtendedServices.getConsRelationshipCols(),
                //gridOptions: 
                toggleDetails: false,
                togglePleaseWait: true,
                totalItems: 0,
                currentPage: 1,
                masterId: $stateParams.constituentId,
                distinctRecordCount: 0

            }
            $scope.relationship.gridOptions = GridServices.getGridOptions($scope.relationship.columnDef);
            //console.log("INitialized");
            $scope.relationship.gridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.relationship.gridApi = gridApi;
            }


            showOrgRelationshipDetails()
            //console.log("In Relationship ctrl");
        }
        initialize();

        $scope.$on('org_relationship', function (event, args) {

            if (args.org_relationship) {
                $scope.relationship.togglePleaseWait = true;
                showOrgRelationshipDetails();

            } else {
                $scope.relationship.togglePleaseWait = false;
                $scope.relationship.toggleDetails = false;
            }

        });


        $scope.relationship.pageChanged = function (page) {
            $scope.relationship.gridApi.pagination.seek(page);
        }

        //filter for search results
        $rootScope.toggleOrgRelationshipSearchFilter = function () {
            //console.log("called DNC");
            $scope.relationship.gridOptions.enableFiltering = !$scope.relationship.gridOptions.enableFiltering;
            $scope.relationship.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
        }


        //showing records 
        function showOrgRelationshipDetails() {
            if (StoreData.getRelationshipFullList().length <= 0) {
                refreshGrid();
            }
            else {
                var res = StoreData.getRelationshipFullList();
                $scope.relationship.gridOptions = GridServices.refreshGridData($scope.relationship.gridOptions, res);
                $scope.relationship.distinctRecordCount = res[0].distinct_records_count;
                $scope.relationship.togglePleaseWait = false;
                $scope.relationship.toggleDetails = true;
            }
        }

        //referesh grid after add/edit/delete operations
        function refreshGrid() {

            var org_cnst_mstr_id = $scope.relationship.masterId;

            ExtendedDataServices.getOrgRelationship(org_cnst_mstr_id, false)
            .then(function (result) {
                //console.log(result);
                StoreData.setRelationshipList(result, filterConstituentData(result));
                $scope.relationship.gridOptions = GridServices.refreshGridData($scope.relationship.gridOptions, StoreData.getRelationshipFullList());
                $scope.relationship.togglePleaseWait = false;
                $scope.relationship.toggleDetails = true;
                $scope.relationship.totalItems = StoreData.getRelationshipFullList().length;
                if (StoreData.getRelationshipFullList().length > 0) {
                    $scope.relationship.distinctRecordCount = StoreData.getRelationshipFullList()[0].distinct_records_count;
                }
            },
            function (error) {
                $scope.relationship.togglePleaseWait = false;
                HandleResults(error);
            });
        }

    }]);