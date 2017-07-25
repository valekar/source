
LOCATOR_CONSTANTS = {
    LOCATOR_RESULTS: 'LocatorEmailResults',   
};

angular.module('locator').controller('LocatorEmailDetailsController', ['$scope', '$parse', '$http', '$localStorage', '$sessionStorage', '$stateParams', 'uiGridConstants', '$uibModal', '$stateParams', 
    'LocatorServices', 'LocatorEmailMultiDataService', '$timeout', '$rootScope', 'LocatorEmailDataServices', 'LocatorEmailCRUDapiService', '$q', 'mainService', '$window', '$rootScope', 'LocatorEmailCRUDoperations','$state',
function ($scope, $parse, $http, $localStorage, $sessionStorage, $stateParams, uiGridConstants, $uibModal, $stateParams, 
    LocatorServices,   LocatorEmailMultiDataService,  $timeout, $rootScope, LocatorEmailDataServices,  LocatorEmailCRUDapiService, $q, mainService, $window, $rootScope ,LocatorEmailCRUDoperations,$state) 
{
            var params = $stateParams.locator_addr_key;
            var BASE_URL = BasePath + 'App/Locator/Views/Multi';
            var savedDetailsParams;
            $scope.pleaseWait = { "display": "block" };
            var postParams = { "LocatorEmailInputSearchModel": [] };
            var searchParams = {
                "LocEmailKey": params,
                "LocEmailId": null,
                "IntAssessCode": null,
                "ExtAssessCode": null,
                "ExactMatch": null,
                "Type":"Details"
            };

            postParams["LocatorEmailInputSearchModel"].push(searchParams);

           
            LocatorServices.getLocatorEmailAdvSearchResultsByID(searchParams).success(function (result) {
                LocatorEmailDataServices.setSearchResutlsDataDetail(result);
                LocatorEmailDataServices.getSearchResultsDataDetail();

                LocatorServices.getLocatorEmailConstAdvSearchResultsByID(searchParams).success(function (result) {
                    LocatorEmailDataServices.setSearchResutlsDataDetailConstituent(result);
                    var _result = LocatorEmailDataServices.getSearchResultsDataDetailConstituent();
                    //console.log(_result)
                   if (_result.length > 0)
                   {
                       $scope.pleaseWait = { "display": "none" };
                       $scope.LocatorTemplate = BASE_URL + '/LocatorEmailInfo.tpl.html';
                   }
                });
                
               var full = "";
               $scope.full = result;
               


            }).error(function (result) {
                $scope.pleaseWait = { "display": "none" };
                if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    $state.go('locator.email.results', {});
                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                    $state.go('locator.email.results', {});
                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
            });

            $scope.commonEditGridRow = function (row, grid) {                
                var pageName = "SearchResults";
               // console.log(row);
                commonEditGridRow($scope, row, grid, LocatorEmailCRUDoperations, $uibModal, $stateParams, $scope.searchResultsGridOptions, pageName, LOCATOR_CONSTANTS.LOCATOR_RESULTS);

            }

}]);

function commonEditGridRow($scope, row, grid, LocatorEmailCRUDoperations, $uibModal, $stateParams, gridOption, pageName, constant) {
  
    //console.log($stateParams);
    LocatorEmailCRUDoperations.getEditModal($scope, $uibModal, $stateParams, grid, row, pageName, constant);
}


