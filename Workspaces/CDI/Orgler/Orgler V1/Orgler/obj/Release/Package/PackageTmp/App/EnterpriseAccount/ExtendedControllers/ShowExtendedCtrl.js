angular.module('enterpriseAccountModule').controller("ShowExtendedCtrl",
    ["$scope", "params", "$uibModalInstance", "$uibModal", "ExtendedServices", "ExtendedColumnServices","EXTENDED_FUNC",
    function ($scope, params, $uibModalInstance, $uibModal, ExtendedServices, ExtendedColumnServices, EXTENDED_FUNC) {

        // console.log(params);
        var initialize = function () {
            $scope.showDetails = {
                toggleDetails: true,
                title: '',
                columnDef: ''
            }

            if (params.type == EXTENDED_FUNC.CHARACTERISTICS) {
                $scope.showDetails.title = "Characteristics";
                $scope.showDetails.columnDef = ExtendedColumnServices.getAllCharacteristicsColumns();
            }
            

            $scope.showDetails.gridOptions = ExtendedServices.getGridOptions($scope.showDetails.columnDef);
            //console.log("INitialized");
            $scope.showDetails.gridOptions.onRegisterApi = function (gridApi) {
                //set gridApi on scope
                $scope.showDetails.gridApi = gridApi;
            }
            $scope.showDetails.gridOptions = ExtendedServices.refreshGridData($scope.showDetails.gridOptions, params.results);

        }
        initialize();


        $scope.showDetails.back = function () {
            $uibModalInstance.dismiss('cancel');
        }

    }]);