
angular.module('case').factory('CaseCRUDoperations', ['$http', '$rootScope', function ($http, $rootScope) {
    var CRUD_TEMPLATE_URL = {
        CASEINFO: {
            ADD: BasePath + "App/Case/Views/common/CaseInfoCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Case/Views/common/CaseInfoCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Case/Views/common/CaseInfoCRUD/Delete.tpl.html"

        },
        CASELOCINFO: {
            ADD: BasePath + "App/Case/Views/common/CaseLocInfoCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Case/Views/common/CaseLocInfoCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Case/Views/common/CaseLocInfoCRUD/Delete.tpl.html"
        },
        CASENOTES: {
            ADD: BasePath + "App/Case/Views/common/CaseNotesCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Case/Views/common/CaseNotesCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Case/Views/common/CaseNotesCRUD/Delete.tpl.html"
        }
    };

    return {
        getAddModal: function ($scope, $uibModal, $stateParams, row, type, pageName, grid) {
            var templateUrl = '', controller = '';

            switch (type) {
                case CASE_CONSTANTS.CASE_LOCINFO: {
                    templateUrl = CRUD_TEMPLATE_URL.CASELOCINFO.ADD;
                    controller = 'AddCaseLocInfoInstantiateCtrl';
                    break;
                }

                case CASE_CONSTANTS.CASE_NOTES: {
                    templateUrl = CRUD_TEMPLATE_URL.CASENOTES.ADD;
                    controller = 'AddCaseNotesInstantiateCtrl';
                    break;
                }
            }
            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, '',pageName, $rootScope);
        },
        getEditModal: function ($scope, $uibModal, $stateParams, grid, row, pageName, type) {
            var templateUrl = '', controller = '';
           // console.log("CRUD TEMPLATE URL");
           // console.log(CRUD_TEMPLATE_URL.CASEINFO.EDIT);
            switch (type) {
                case CASE_CONSTANTS.CASE_RESULTS: {
                    templateUrl = CRUD_TEMPLATE_URL.CASEINFO.EDIT;
                    controller = 'EditCaseInfoInstantiateCtrl';
                    //grid = $scope.constPhoneGridOptions;
                    break;
                }
                case CASE_CONSTANTS.CASE_INFO: {
                    templateUrl = CRUD_TEMPLATE_URL.CASEINFO.EDIT;
                    controller = 'EditCaseInfoInstantiateCtrl';
                    //grid = $scope.constPhoneGridOptions;
                    break;
                }
                case CASE_CONSTANTS.CASE_LOCINFO: {
                    templateUrl = CRUD_TEMPLATE_URL.CASELOCINFO.EDIT;
                    controller = 'EditCaseLocInfoInstantiateCtrl';
                    break;
                }
                case CASE_CONSTANTS.CASE_NOTES: {
                    templateUrl = CRUD_TEMPLATE_URL.CASENOTES.EDIT;
                    controller = 'EditCaseNotesInstantiateCtrl';
                    break;
                }
            }

            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, '', pageName, $rootScope);
        },
        getDeleteModal: function ($scope, $uibModal, $stateParams, grid, row, pageName, type) {
            var templateUrl = '', controller = '';

            switch (type) {
                case CASE_CONSTANTS.CASE_RESULTS: {
                    templateUrl = CRUD_TEMPLATE_URL.CASEINFO.DELETE;
                    controller = 'DeleteCaseInfoInstantiateCtrl';
                    //grid = $scope.constPhoneGridOptions;
                    break;
                }
                case CASE_CONSTANTS.CASE_INFO: {
                    templateUrl = CRUD_TEMPLATE_URL.CASEINFO.DELETE;
                    controller = 'DeleteCaseInfoInstantiateCtrl';
                    break;
                }
                case CASE_CONSTANTS.CASE_LOCINFO: {
                    templateUrl = CRUD_TEMPLATE_URL.CASELOCINFO.DELETE;
                    controller = 'DeleteCaseLocInfoInstantiateCtrl';
                    break;
                }
                case CASE_CONSTANTS.CASE_NOTES: {
                    templateUrl = CRUD_TEMPLATE_URL.CASENOTES.DELETE;
                    controller = 'DeleteCaseNotesInstantiateCtrl';
                    break;
                }
            }

            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, '', pageName, $rootScope);
        },
        getCasePopup: function ($scope, $uibModal) {
            var templateUrl = BasePath + 'App/Constituent/Views/common/CreateCase.tpl.html';
            var controller = 'CaseCtrl';
            OpenModal($scope, $uibModal, null, templateUrl, controller, null, null, '');
        },
        getCartComparePopup: function ($scope, $uibModal, rows) {
            var templateUrl = BasePath + 'App/Constituent/Views/CartCompare.tpl.html';
            var controller = 'CartCompareCtrl';
            OpenModal($scope, $uibModal, null, templateUrl, controller, null, rows, 'lg');
        }
    }

}]);




//Service for CRUD Operations only
angular.module('case').factory('CaseCRUDapiService', ['$http', function ($http) {

    var CRUDapiUrls = {};


    //ADD New API URLS for CRUD operations below
    CRUDapiUrls[CASE_CRUD_CONSTANTS.CASEINFO.EDIT] = BasePath + "CaseNative/updatecaseinfo";
    CRUDapiUrls[CASE_CRUD_CONSTANTS.CASEINFO.DELETE] = BasePath + "CaseNative/deletecaseinfo";

    CRUDapiUrls[CASE_CRUD_CONSTANTS.CASELOCINFO.ADD] = BasePath + "CaseNative/addcaselocinfo";
    CRUDapiUrls[CASE_CRUD_CONSTANTS.CASELOCINFO.EDIT] = BasePath + "CaseNative/updatecaselocinfo";
    CRUDapiUrls[CASE_CRUD_CONSTANTS.CASELOCINFO.DELETE] = BasePath + "CaseNative/deletecaselocinfo";

    CRUDapiUrls[CASE_CRUD_CONSTANTS.CASENOTES.ADD] = BasePath + "CaseNative/addcasenotesinfo";
    CRUDapiUrls[CASE_CRUD_CONSTANTS.CASENOTES.EDIT] = BasePath + "CaseNative/updatecasenotesinfo";
    CRUDapiUrls[CASE_CRUD_CONSTANTS.CASENOTES.DELETE] = BasePath + "CaseNative/deletecasenotesinfo";

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
    var case_key = '';
    if ($stateParams !== null) {
        case_key = $stateParams.case_key;
    }

    var params = {
        case_key: typeof $stateParams !== null ? case_key : null,
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
        if (result.clearGridFlag) {
            $scope.caseLocInfoGridOptions.data = '[]';
            $scope.caseNotesGridOptions.data = '[]';
            $scope.caseTransDetailsGridOptions.data = '[]';
            $scope.caseLocInfoGridOptions.data.length = 0;
            $scope.caseNotesGridOptions.data.length = 0;
            $Scope.caseTransDetailsGridOptions.data.length = 0;
        }
        modalMessage($rootScope, result);
    });

}


angular.module('case').factory('CaseUtilServices', ['$filter', function ($filter) {
    return {
        getStartDate: function () {
            return $filter('date')(new Date(), 'yyyy-mm-dd');
        }
    }
}]);



function modalMessage($rootScope, result) {
    $rootScope.FinalMessage = result.finalMessage;
    $rootScope.ReasonOrTransKey = result.ReasonOrTransKey;
    $rootScope.ConfirmationMessage = result.ConfirmationMessage;
    angular.element("#caseConfirmationModal").modal({ backdrop: "static" });
}



function messagePopup($rootScope, message, header) {
    $rootScope.FinalMessage = message;
    $rootScope.ConfirmationMessage = header;
    $rootScope.ReasonOrTransKey = '';
    angular.element("#caseConfirmationModal").modal({ backdrop: "static" });
}