angular.module('upload').controller('ListUploadTransDetailsCtrl', ['$scope', '$localStorage', 'ListUploadServices','$window','$state','uiGridConstants','$timeout','$location',
    function ($scope, $localStorage, ListUploadServices, $window, $state, uiGridConstants, $timeout, $location) {
      
 
    var initialize = function () {

            if (angular.isUndefined(ListUploadServices.getListUploadTransData().results)) {
                //just to remove the errors 
                $scope.listUpload = {};

                $state.go('upload.listUpload.results');
                

            }
            else {
                var params = ListUploadServices.getListUploadTransData();
                console.log(params);
                $scope.listUpload = {
                    transResults: params.results,
                    totalItems: params.results.length,
                    uploadType: params.uploadType,
                    pageHeader: "",
                    pleaseWait: false,
                    columnDef: [],
                    userPermissions: ListUploadServices.getUserPermissions()
                }

                if ($scope.listUpload.userPermissions.constituent_tb_access != 'N') {
                    $scope.userPermisssions = true;
                    
                }
                else {
                    $scope.userPermisssions = false;
                } 
                
              
                if ($scope.listUpload.uploadType == "Email-only Upload") {
                    $scope.listUpload.pageHeader = "Email-only Upload";
                }
                else if ($scope.listUpload.uploadType == "Group Membership Upload") {
                    $scope.listUpload.pageHeader = "Group Membership Upload";
                }
                else if ($scope.listUpload.uploadType == "Name and Email Upload") {
                    $scope.listUpload.pageHeader = "Name and Email Upload";
                }

                $scope.listUpload.columnDef = ListUploadServices.getTransColumnDef($scope);
                if ($scope.listUpload.transResults.length > 0) {
                    $scope.listUpload.gridOptions = ListUploadServices.getGridOptions($scope.listUpload.columnDef);
                    $scope.listUpload.gridOptions.data = $scope.listUpload.transResults;

                    $scope.listUpload.gridOptions.onRegisterApi = function (gridApi) {
                        $scope.listUpload.gridApi = gridApi;
                    }

                }
                else {
                    $state.go('upload.listUpload.results');
                }

            }
        
    }
    initialize();

   
  
    $scope.listUpload.pageChanged = function (page) {
        $scope.listUpload.gridApi.pagination.seek(page);
    };

    // this is triggered in grid
    $scope.listUpload.getConstituentDetails = function (row) {
        console.log(row);
        $localStorage.listUpload = {
            masterId: row.entity.constituent_id

        };
        $window.location.href =  BasePath + 'constituent/saerch';
        // $window.open(BasePath + "constituent/search");
    }

    $scope.listUpload.toggleFiltering = function () {
        $scope.listUpload.gridOptions.enableFiltering = !$scope.listUpload.gridOptions.enableFiltering;
        $scope.listUpload.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    
    $scope.listUpload.openWindow = function (row) {
        console.log(row);
        $localStorage.listUpload = {
            masterId: row.entity.constituent_id

        };
        $timeout(function () {
            $window.open(BasePath + "constituent/search");
        }, 100)
       
    }


    $scope.listUpload.exportTransData = function () {
        //console.log($scope.listUpload.transResults[0]);
        ListUploadServices.getListUploadTransExportData($scope.listUpload.transResults[0].trans_key).then(function () { }, function () { });
    }


}]);