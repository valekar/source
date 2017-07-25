

angular.module('locator').factory('LocatorAddressCRUDoperations', ['$http', '$rootScope', function ($http, $rootScope) {
    var CRUD_TEMPLATE_URL = {
        LOCATOR: {
            ADD: BasePath + "App/Locator/Views/common/LocatorAddressInfoCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Locator/Views/common/LocatorAddressInfoCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Locator/Views/common/LocatorAddressInfoCRUD/Delete.tpl.html"

        }
    };

    return {
        getAddModal: function ($scope, $uibModal, $stateParams, row, type, pageName, grid) {
            var templateUrl = '', controller = '';

            
            templateUrl = CRUD_TEMPLATE_URL.LOCATOR.ADD;
                    controller = 'AddCaseLocInfoInstantiateCtrl';
                  
            
            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, '', pageName, $rootScope);
        },

        getEditModal: function ($scope, $uibModal, $stateParams, grid, row, pageName, type) {
         
            var templateUrl = '', controller = '';
                    //alert(LOCATOR_CONSTANTS.LOCATOR_RESULTS);
                    templateUrl = CRUD_TEMPLATE_URL.LOCATOR.EDIT;
                    controller = 'EditLocatorAddressInstantiateCtrl';
                    //grid = $scope.constPhoneGridOptions;

            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, '', pageName, $rootScope);
        },

        getDeleteModal: function ($scope, $uibModal, $stateParams, grid, row, pageName, type) {
            var templateUrl = '', controller = '';

           
                    templateUrl = CRUD_TEMPLATE_URL.LOCATOR.DELETE;
                    controller = 'DeleteCaseInfoInstantiateCtrl';

            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, '', pageName, $rootScope);
        },
    }

}]);




//Service for CRUD Operations only
angular.module('locator').factory('LocatorAddressCRUDapiService', ['$http', function ($http) {

    var CRUDapiUrls = {};

    //alert(LOCATOR_CRUD_CONSTANTS.LocatorAddressINFO.EDIT);
    //ADD New API URLS for CRUD operations below
    CRUDapiUrls[LOCATOR_CRUD_CONSTANTS.LOCATORADDRESSINFO] = BasePath + "LocatorNative/updateLocatorAddress";
   
    var getCRUDOperationData = function (postParams, requestType) {       
        return $http.post(CRUDapiUrls[requestType], JSON.stringify(postParams), {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
            return result;
        }).error(function (result) {
           // console.log(result);
            return result;
        });
    }

    return {
        getCRUDOperationResult: function (PostParams, requestType) {
            return getCRUDOperationData(PostParams, requestType);
        }
    }
}]);



function OpenModal($scope, $uibModal, $stateParams, templ, ctrl, grid, row, size, pageName, $rootScope) {
    var LocAddrKey = '';
    if ($stateParams !== null) {
        LocAddrKey = $stateParams.LocEmailKey;
    }

    var params = {
        LocEmailKey: typeof $stateParams !== null ? LocAddrKey : null,
        row: row,
        grid: grid

    }
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
            },
            pageName: function () {
                return pageName;
            }
        }
    });

    ModalInstance.result.then(function (result) {
        console.log(result)
       modalMessage($rootScope, result);
        
    });

}


angular.module('locator').factory('LocatorAddressUtilServices', ['$filter', function ($filter) {
    return {
        getStartDate: function () {
            return $filter('date')(new Date(), 'yyyy-mm-dd');
        }
    }
}]);



function modalMessage($rootScope, result)
{
    console.log(result.finalMessage);
    $rootScope.FinalMessage = result.finalMessage;
    console.log($rootScope.FinalMessage);
    //$rootScope.ReasonOrTransKey = result.ReasonOrTransKey;
    $rootScope.ConfirmationMessage = result.ConfirmationMessage;
    angular.element("#LocatorConfirmationModal").modal({ backdrop: "static" });
}



function messagePopup($rootScope, message, header) {
    $rootScope.FinalMessage = message;
    $rootScope.ConfirmationMessage = header;
    $rootScope.ReasonOrTransKey = '';
    angular.element("#LocatorConfirmationModal").modal({ backdrop: "static" });
}