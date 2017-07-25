angular.module('upload').controller('ListUploadSearchResultsCtrl', ['$scope', 'ListUploadServices', '$timeout', '$location','UPLOAD_CONSTANTS','$state','ErrorService',
    function ($scope, ListUploadServices, $timeout, $location, UPLOAD_CONSTANTS, $state, ErrorService) {
    var initialize = function(){
        $scope.listUpload = {
            columnDef:ListUploadServices.getSearchResultsColumnDef(),
            results: ListUploadServices.getListUploadSearchData(),
            totalItems: 0,
            pleaseWait: false,
            errorMessage:""
        }
        $scope.listUpload.gridOptions = ListUploadServices.getGridOptions($scope.listUpload.columnDef);

        if ($scope.listUpload.results.length>0) {
            $scope.listUpload.gridOptions.data = $scope.listUpload.results;
            $scope.listUpload.totalItems = $scope.listUpload.gridOptions.data.length;

        } else {
            $location.url('/upload/listUpload');
        }
      
    }
    initialize();


    var callTransactionDetailService = function (row) {
        ListUploadServices.getTransactionDetails(row.entity.trans_key).then(function (results) {
            $scope.listUpload.pleaseWait = false;
            // console.log(results)
            if (results.length > 0) {
                //call the modal popup for showing results
                ListUploadServices.clearListUploadTransData();

                var myResult = {
                    uploadType: row.entity.upld_typ,
                    results: results
                }
                ListUploadServices.setListUploadTransData(myResult);
                $state.go('upload.listUpload.results.details');


            }
            else {
                $scope.listUpload.errorMessage = "No Results!";
            }

        },
          function (error) {
              $scope.pleaseWait = false;
              ErrorService.messagePopup(error);
          });
    };

    $scope.pageChanged = function (page) {
        $scope.searchResultsGridApi.pagination.seek(page);
    };
   
    // this method is called from list upload service 
    $scope.listUpload.getTransactionDetails = function (row) {
        $scope.listUpload.errorMessage = "";
        $scope.listUpload.pleaseWait = true;
        //console.log(row.entity.trans_key);

        ListUploadServices.getPermissions().then(function (result) {
            ListUploadServices.setUserPermisions(result.data);
            //call only after the permissions have been set
            callTransactionDetailService(row);
        }, function (error) {
            //ErrorService.messagePopup(error);
            ListUploadServices.setUserPermisions(null);
            callTransactionDetailService(row);
        });     
    }


    $scope.exportListUploadSearchResults = function () {
        var params = ListUploadServices.getListUploadSearchParams();
        ListUploadServices.getListUploadSearchExportData(params).then(function (res) { }, function (err) { });
    }


}]);