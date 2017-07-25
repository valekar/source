
LOCATOR_CONSTANTS = {
    LOCATOR_RESULTS: 'LocatorAddressResults',    
};

angular.module('locator').controller('LocatorAddressDetailsController', ['$scope', '$parse', '$http', '$localStorage', '$sessionStorage', '$stateParams', 'uiGridConstants', '$uibModal', '$stateParams', 
    'LocatorAddressServices', 'LocatorAddressMultiDataService', '$timeout', '$rootScope', 'LocatorAddressDataServices', 'LocatorAddressCRUDapiService', '$q', 'mainService', '$window', '$rootScope','LocatorAddressCRUDoperations','$state',
function ($scope, $parse, $http, $localStorage, $sessionStorage, $stateParams, uiGridConstants, $uibModal, $stateParams, 
    LocatorAddressServices, LocatorAddressMultiDataService, $timeout, $rootScope, LocatorAddressDataServices, LocatorAddressCRUDapiService, $q, mainService, $window, $rootScope, LocatorAddressCRUDoperations,$state)
{
    var params = $stateParams.locator_addr_key;
    //console.log(params);

            var BASE_URL = BasePath + 'App/Locator/Views/Multi';
            var savedDetailsParams;
            $scope.pleaseWait = { "display": "block" };
            var postParams = { "LocatorAddressInputSearchModel": [] };
            var pageName = "SearchResults";
            var searchParams = {               
                "LocAddrKey":params,
                "LocAddressLine": "",
                "LocCity": "",
                "LocState": "",
                "LocZip": "",
                "LocDelType": "",
                "LocDelCode": "",
                "LocAssessCode": "",
            };

            postParams["LocatorAddressInputSearchModel"].push(searchParams);

           
    LocatorAddressServices.getLocatorAddressAdvSearchResultsByID(searchParams).success(function (result) {
                
                LocatorAddressDataServices.setSearchResutlsDataDetail(result);
                LocatorAddressDataServices.getSearchResultsDataDetail();
               
               var full = "";
               $scope.full = result;
                $scope.pleaseWait = { "display": "none" }; 
                $scope.LocatorTemplate = BASE_URL + '/LocatorAddressInfo.tpl.html';


            }).error(function (result) {
                $scope.pleaseWait = { "display": "none" };
                if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    $state.go('locator.address.results', {});
                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                    $state.go('locator.address.results', {});
                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
            });

    $scope.commonEditGridRow = function (row, grid) {

        commonEditGridRow($scope, row, grid, LocatorAddressCRUDoperations, $uibModal, $stateParams, $scope.searchResultsGridOptions, pageName, LOCATOR_CONSTANTS.LOCATOR_RESULTS);

    }

    $scope.getAddressConstituent = function (addressConstituent) {
      
        gettingAddressConstituent(addressConstituent);        
    }

    $scope.getAddressAssessments = function (addressAssessments) {
       
        gettingAddressAssessments(addressAssessments);
       
    }
    var Const_data = "";
    var Asses_data = "";
    function gettingAddressConstituent(addressConstituent)
    {
        $rootScope.AddressConstchk_value = addressConstituent;       
        
        if (!addressConstituent && Const_data == "") {
            $rootScope.AddressConstchk_value = false;            
            Const_data = params;
            $scope.disableConstView = { "display": "block" }; // then Constituents grid will visible.
            $scope.pleaseWait = { "display": "block" };
            LocatorAddressServices.getLocatorAddressConstituentsData(searchParams).success(function (result) {
                LocatorAddressDataServices.setSearchResutlsDataDetail_Constituents(result);
                LocatorAddressDataServices.getSearchResultsDataDetail_Constituents();
                $scope.pleaseWait = { "display": "none" };
                $scope.LocatorAddressConstituents = BASE_URL + '/LocatorAddressConstituentsInfo.tpl.html';


            }).error(function (result) {
                $scope.pleaseWait = { "display": "none" };
                if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    $state.go('locator.address.results', {});
                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                    $state.go('locator.address.results', {});
                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
            });
        }
        else {
            
            if (!addressConstituent) {               
                $scope.disableConstView = { "display": "block" };                
                $rootScope.AddressConstchk_value = false;
                
            } else {
                $rootScope.AddressConstchk_value = true;                
                $scope.disableConstView = { "display": "none" }; // then Constituents grid will hidden. }
            }
        }
    }

    function gettingAddressAssessments(addressAssessments) {
        
        $rootScope.AddressAsseschk_value = addressAssessments;        
        if (!addressAssessments && Asses_data == "")
        {
            $rootScope.AddressAsseschk_value = false;
            Asses_data = params;
            $scope.disableAssesView = { "display": "block" }; // then Assessement grid will visible.
            $scope.pleaseWait = { "display": "block" };
            LocatorAddressServices.getLocatorAddressAssessmentData(searchParams).success(function (result) {
                LocatorAddressDataServices.setSearchResutlsDataDetail_Address_Assessments(result);
                LocatorAddressDataServices.getSearchResultsDataDetail_Address_Assessments();
                var full = "";
                $scope.full = result;
                $scope.pleaseWait = { "display": "none" };
                $scope.LocatorAddressAssessment = BASE_URL + '/LocatorAddressAssessmentsInfo.tpl.html';


            }).error(function (result) {
                $scope.pleaseWait = { "display": "none" };
                if (result == LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                    $state.go('locator.address.results', {});
                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                }
                else if (result == LOCATOR_CRUD_CONSTANTS.DB_ERROR) {
                    $state.go('locator.address.results', {});
                    messagePopup($rootScope, LOCATOR_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

                }
            });
        }
        else {
            if (!addressAssessments)
            {
                
                $scope.disableAssesView = { "display": "block" };
                $rootScope.AddressAsseschk_value = false;                
            } else
            {
                $rootScope.AddressAsseschk_value = true;
                
                $scope.disableAssesView = { "display": "none" }; // then Assessement grid will hidden. 
                
            }
        }
    }
    
}]);

function commonEditGridRow($scope, row, grid, LocatorAddressCRUDoperations, $uibModal, $stateParams, gridOption, pageName, constant) {
    
    LocatorAddressCRUDoperations.getEditModal($scope, $uibModal, $stateParams, grid, row, pageName, constant);
}


