﻿
newAccMod.factory('constituentCRUDoperations', ['$http', '$rootScope', function ($http, $rootScope) {
    var CRUD_TEMPLATE_URL = {
        PHONE : {
            ADD: BasePath + "App/Shared/Views/common/phoneCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Shared/Views/common/phoneCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Shared/Views/common/phoneCRUD/Delete.tpl.html",
            DETAIL: BasePath + "App/Shared/Views/Common/ConstDetailsPhoneTemplate.html"
        },
        EMAIL: {
            ADD: BasePath + "App/Shared/Views/common/emailCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Shared/Views/common/emailCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Shared/Views/common/emailCRUD/Delete.tpl.html",
            DETAIL: BasePath + "App/Shared/Views/Common/ConstDetailsEmailTemplate.html"
        },
        DEATH: {
            ADD: BasePath + "App/Constituent/Views/common/deathCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Constituent/Views/common/deathCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Constituent/Views/common/deathCRUD/Delete.tpl.html"
        },
        BIRTH: {
            ADD: BasePath + "App/Constituent/Views/common/birthCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Constituent/Views/common/birthCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Constituent/Views/common/birthCRUD/Delete.tpl.html"
        },
        ADDRESS: {
            ADD: BasePath + "App/Shared/Views/common/addressCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Shared/Views/common/addressCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Shared/Views/common/addressCRUD/Delete.tpl.html",
            DETAIL: BasePath + "App/Shared/Views/Common/ConstDetailsAddressTemplate.html"
        },
        PREF: {
            ADD: BasePath + "App/Constituent/Views/common/contactPrefCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Constituent/Views/common/contactPrefCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Constituent/Views/common/contactPrefCRUD/Delete.tpl.html"
        },
        GRP_MEMBERSHP: {
            ADD: BasePath + "App/Constituent/Views/common/grpMemberCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Constituent/Views/common/grpMemberCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Constituent/Views/common/grpMemberCRUD/Delete.tpl.html"
        },
        CHARACTERISTICS: {
            ADD: BasePath + "App/Constituent/Views/common/characterCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Constituent/Views/common/characterCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Constituent/Views/common/characterCRUD/Delete.tpl.html"
        },
        NAME: {
            ADD: BasePath + "App/Constituent/Views/common/nameCRUD/Add.tpl.html",
            EDIT: BasePath + "App/Constituent/Views/common/nameCRUD/Edit.tpl.html",
            DELETE: BasePath + "App/Constituent/Views/common/nameCRUD/Delete.tpl.html"
        },
        ORG_NAME: {
            ADD: BasePath + "App/Shared/Views/common/nameCRUD/OrgAdd.tpl.html",
            EDIT: BasePath + "App/Shared/Views/common/nameCRUD/OrgEdit.tpl.html",
            DELETE: BasePath + "App/Shared/Views/common/nameCRUD/OrgDelete.tpl.html",
            DETAIL: BasePath + "App/Shared/Views/Common/ConstDetailsOrgName.tpl.html"
        },
        AFFILIATOR: {
            ADD: BasePath + "App/Constituent/Views/common/affiliatorCRUD/Add.tpl.html",
            DELETE: BasePath + "App/Constituent/Views/common/affiliatorCRUD/Delete.tpl.html"
        },
        //Added for new sections
        EMAIL_DOMAINS: {
            ADD: BasePath + "App/Constituent/Views/common/emailDomainCRUD/Add.tpl.html",
            DELETE: BasePath + "App/Constituent/Views/common/emailDomainCRUD/Delete.tpl.html"
        },
        NAICS_CODES: {
            ADD: BasePath + "App/Constituent/Views/common/naicsCodesCRUD/Add.tpl.html",
            DELETE: BasePath + "App/Constituent/Views/common/naicsCodesCRUD/Delete.tpl.html"
        },
        CONST_POTENTIALMERGE: {
            DETAIL: BasePath + "App/Shared/Views/Common/ConstPotentialMergeTemplate.html"

        },
        CONST_POTENTIALUNMERGE: {
            DETAIL: BasePath + "App/Shared/Views/Common/ConstDetailsExternalBridgeTemplate.html"

        }
    };

    return {      
        getAddModal: function ($scope, $uibModal, $stateParams, row, type, grid) {
            var templateUrl = '', controller = '';
            var popupSize = '';
            switch (type) {
                case HOME_CONSTANTS.CONST_PHONE: {
                    templateUrl = CRUD_TEMPLATE_URL.PHONE.ADD
                    controller = 'AddPhoneInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_EMAIL: {
                    templateUrl = CRUD_TEMPLATE_URL.EMAIL.ADD;
                    controller = 'AddEmailInstantiateCtrl';
                    break;
                }

                case HOME_CONSTANTS.CONST_DEATH: {
                    templateUrl = CRUD_TEMPLATE_URL.DEATH.ADD;
                    controller = 'AddDeathInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_BIRTH: {
                    templateUrl = CRUD_TEMPLATE_URL.BIRTH.ADD;
                    controller = 'AddBirthInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_ADDRESS: {
                    templateUrl = CRUD_TEMPLATE_URL.ADDRESS.ADD;
                    controller = 'AddAddressInstantiateCtrl';
                    break;
                }
               /* case HOME_CONSTANTS.CONST_ADDRESS: {
                    templateUrl = 'App/Constituent/Views/common/AddAddressContent.html';
                    controller = 'AddAddressInstantiateCtrl';
                    break;
                }*/
                case HOME_CONSTANTS.CONST_PREF: {
                    templateUrl = CRUD_TEMPLATE_URL.PREF.ADD;
                    controller = 'AddContactPrefInstantiateCtrl';
                    break;
                }

                case HOME_CONSTANTS.GRP_MEMBERSHIP: {
                    templateUrl = CRUD_TEMPLATE_URL.GRP_MEMBERSHP.ADD;
                    controller = 'AddGrpMembershpInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CHARACTERISTICS: {
                    templateUrl = CRUD_TEMPLATE_URL.CHARACTERISTICS.ADD;
                    controller = 'AddCharacterInstantiateCtrl';
                    break;
                }

                case HOME_CONSTANTS.CONST_NAME: {
                    templateUrl = CRUD_TEMPLATE_URL.NAME.ADD;
                    controller = 'AddNameInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_ORG_NAME: {
                    templateUrl = CRUD_TEMPLATE_URL.ORG_NAME.ADD;
                    controller = 'AddOrgNameInstantiateCtrl';
                    break;

                }
                case HOME_CONSTANTS.AFFILIATOR: {
                    templateUrl = CRUD_TEMPLATE_URL.AFFILIATOR.ADD;
                    controller = 'AddAffiliatorInstantiateCtrl';
                    break;

                }
                case HOME_CONSTANTS.EMAIL_DOMAINS: {
                    templateUrl = CRUD_TEMPLATE_URL.EMAIL_DOMAINS.ADD;
                    controller = 'AddEmailDomainInstantiateCtrl';
                    break;

                }
                case HOME_CONSTANTS.NAICS_CODES: {
                    templateUrl = CRUD_TEMPLATE_URL.NAICS_CODES.ADD;
                    controller = 'AddNAICSCodesInstantiateCtrl';
                    popupSize = 'lg';
                    break;

                }
            }
            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, popupSize, $rootScope);
        },
        getDetailModal: function ($scope, $uibModal, $stateParams, grid, row, type) {
            var templateUrl = '', controller = '';

            switch (type) {                
                case HOME_CONSTANTS.CONST_ORG_NAME: {
                    templateUrl = CRUD_TEMPLATE_URL.ORG_NAME.DETAIL;
                    controller = 'constituentOrgNameDetailsController';
                    break;

                }
                case HOME_CONSTANTS.CONST_ADDRESS: {
                    templateUrl = CRUD_TEMPLATE_URL.ADDRESS.DETAIL;
                    controller = 'constituentDetailsAddressController';
                    break;

                }
                case HOME_CONSTANTS.CONST_PHONE: {
                    templateUrl = CRUD_TEMPLATE_URL.PHONE.DETAIL;
                    controller = 'constituentDetailsPhoneController';
                    break;

                }
                case HOME_CONSTANTS.CONST_EMAIL: {
                    templateUrl = CRUD_TEMPLATE_URL.EMAIL.DETAIL;
                    controller = 'constituentDetailsEmailController';
                    break;

                }
                case HOME_CONSTANTS.CONST_POTENTIALMERGE: {
                    templateUrl = CRUD_TEMPLATE_URL.CONST_POTENTIALMERGE.DETAIL;
                    controller = 'constituentDetailsPotentialMergeController';
                    break;

                }

                case HOME_CONSTANTS.CONST_EXT_BRIDGE: {
                    templateUrl = CRUD_TEMPLATE_URL.CONST_POTENTIALUNMERGE.DETAIL;
                    controller = 'constituentDetailsPotentialUnMergeController';
                    break;

                }
            }

            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, 'lg', $rootScope);
        },
        getEditModal: function ($scope, $uibModal, $stateParams, grid, row, type) {
            var templateUrl = '', controller = '';

            switch (type) {
                case HOME_CONSTANTS.CONST_PHONE: {
                    templateUrl =CRUD_TEMPLATE_URL.PHONE.EDIT;
                    controller = 'EditPhoneInstantiateCtrl';
                    //grid = $scope.constPhoneGridOptions;
                    break;
                }
                case HOME_CONSTANTS.CONST_EMAIL: {
                    templateUrl = CRUD_TEMPLATE_URL.EMAIL.EDIT;
                    controller = 'EditEmailInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_DEATH: {
                    templateUrl = CRUD_TEMPLATE_URL.DEATH.EDIT;
                    controller = 'EditDeathInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_BIRTH: {
                    templateUrl = CRUD_TEMPLATE_URL.BIRTH.EDIT;
                    controller = 'EditBirthInstantiateCtrl';
                    break;
                }
                    
                /*case HOME_CONSTANTS.CONST_ADDRESS: {
                    templateUrl = 'EditAddressContent.html';
                    controller = 'EditAddressInstantiateCtrl';
                    break;
                }*/
                case HOME_CONSTANTS.CONST_ADDRESS: {
                    templateUrl = CRUD_TEMPLATE_URL.ADDRESS.EDIT;
                    controller = 'EditAddressInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_PREF: {
                    templateUrl = CRUD_TEMPLATE_URL.PREF.EDIT;
                    controller = 'EditContactPrefInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.GRP_MEMBERSHIP: {
                    templateUrl = CRUD_TEMPLATE_URL.GRP_MEMBERSHP.EDIT;
                    controller = 'EditGrpMembershpInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CHARACTERISTICS: {
                    templateUrl = CRUD_TEMPLATE_URL.CHARACTERISTICS.EDIT;
                    controller = 'EditCharacterInstantiateCtrl';
                    break;
                }

                case HOME_CONSTANTS.CONST_NAME: {
                    templateUrl = CRUD_TEMPLATE_URL.NAME.EDIT;
                    controller = 'EditNameInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_ORG_NAME: {
                    templateUrl = CRUD_TEMPLATE_URL.ORG_NAME.EDIT;
                    controller = 'EditOrgNameInstantiateCtrl';
                    break;

                }
                //case HOME_CONSTANTS.CONST_ORG_NAME: {
                //    templateUrl = CRUD_TEMPLATE_URL.ORG_NAME.DETAIL;
                //    controller = 'constituentOrgNameDetailsController';
                //    break;

                //}
            }

            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, '', $rootScope);
        },
        getDeleteModal: function ($scope, $uibModal, $stateParams, grid, row, type) {
            var templateUrl = '', controller = '';

            switch (type) {
                case HOME_CONSTANTS.CONST_PHONE: {
                    templateUrl = CRUD_TEMPLATE_URL.PHONE.DELETE;
                    controller = 'DeletePhoneInstantiateCtrl';
                    //grid = $scope.constPhoneGridOptions;
                    break;
                }
                case HOME_CONSTANTS.CONST_EMAIL: {
                    templateUrl = CRUD_TEMPLATE_URL.EMAIL.DELETE;
                    controller = 'DeleteEmailInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_DEATH: {
                    templateUrl = CRUD_TEMPLATE_URL.DEATH.DELETE;
                    controller = 'DeleteDeathInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_BIRTH: {
                    templateUrl = CRUD_TEMPLATE_URL.BIRTH.DELETE;
                    controller = 'DeleteBirthInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_ADDRESS: {
                    templateUrl = CRUD_TEMPLATE_URL.ADDRESS.DELETE;
                    controller = 'DeleteAddressInstantiateCtrl';
                    break;
                }
                /*case HOME_CONSTANTS.CONST_ADDRESS: {
                    templateUrl = 'DeleteAddressContent.html';
                    controller = 'DeleteAddressInstantiateCtrl';
                    break;
                }*/
                case HOME_CONSTANTS.CONST_PREF: {
                    templateUrl = CRUD_TEMPLATE_URL.PREF.DELETE;
                    controller = 'DeleteContactPrefInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.GRP_MEMBERSHIP: {
                    templateUrl = CRUD_TEMPLATE_URL.GRP_MEMBERSHP.DELETE;
                    controller = 'DeleteGrpMembershpInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CHARACTERISTICS: {
                    templateUrl = CRUD_TEMPLATE_URL.CHARACTERISTICS.DELETE;
                    controller = 'DeleteCharacterInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.CONST_NAME: {
                    templateUrl = CRUD_TEMPLATE_URL.NAME.DELETE;
                    controller = 'DeleteNameInstantiateCtrl'
                    break;
                }
                case HOME_CONSTANTS.CONST_ORG_NAME: {
                    templateUrl = CRUD_TEMPLATE_URL.ORG_NAME.DELETE;
                    controller = 'DeleteOrgNameInstantiateCtrl';
                    break;
                }
                case HOME_CONSTANTS.AFFILIATOR: {
                    templateUrl = CRUD_TEMPLATE_URL.AFFILIATOR.DELETE;
                    controller = 'DeleteAffiliatorInstantiateCtrl';
                    break;

                }
                case HOME_CONSTANTS.EMAIL_DOMAINS: {
                    templateUrl = CRUD_TEMPLATE_URL.EMAIL_DOMAINS.DELETE;
                    controller = 'DeleteEmailDomainInstantiateCtrl';
                    break;

                }               
            }

            OpenModal($scope, $uibModal, $stateParams, templateUrl, controller, grid, row, '', $rootScope);
        },
        getAddCasePopup: function ($scope, $uibModal) {
            var templateUrl = BasePath + 'App/Constituent/Views/common/CreateCase.tpl.html';
           var controller = 'AddCaseCtrl';
           OpenCaseModal($scope, $uibModal, null, templateUrl, controller, null, null, 'lg', $rootScope);
        },
        getMergeCartComparePopup: function ($scope, $uibModal, rows, grid) {
            var templateUrl = BasePath + 'App/Constituent/Views/MergeCart.tpl.html';
            var controller = 'MergeCartCtrl';
            OpenModal($scope, $uibModal, null, templateUrl, controller, grid, rows, 'lg', $rootScope);
        },
        getUnmergeCartComparePopup: function ($scope, $uibModal, rows,grid) {
            var templateUrl = BasePath + 'App/Constituent/Views/UnmergeCart.tpl.html';
            var controller = 'UnmergeCartCtrl';
            OpenModal($scope, $uibModal, null, templateUrl, controller, grid, rows, 'lg', $rootScope);
        },
        getAdvanceCasePopup: function ($scope, $uibModal) {
            var templateUrl = BasePath + 'App/Constituent/Views/common/AdvanceCaseSearch.tpl.html';
            var controller = 'AdvanceCaseCtrl';
            OpenCaseModal($scope, $uibModal, null, templateUrl, controller, null, null, 'lg', $rootScope);
        },
        getQuickCasePopup: function ($scope, $uibModal) {
        var templateUrl = BasePath + 'App/Constituent/Views/common/QuickCaseSearch.tpl.html';
        var controller = 'QuickCaseCtrl';
        OpenCaseModal($scope, $uibModal, null, templateUrl, controller,null, null, 'lg', $rootScope);
    }
    }

}]);



function OpenModal($scope, $uibModal, $stateParams, templ, ctrl, grid, row, size, $rootScope) {
    var masterId = '';
    if ($stateParams!==null) {
        masterId = $stateParams.constituentId;
    }

    var params = {
        masterId: typeof $stateParams !== null ? masterId : null,
        row: row,
        grid: grid
      
    }
    var CssClass = '';
    if (size === 'lg') {
        CssClass = 'app-modal-window';
    }
    else if (size === 'md') {
        CssClass = 'app-modal-window-medium';
    }

    var ModalInstance = ModalInstance || $uibModal.open({
       // animation: $scope.animationsEnabled,
        templateUrl: templ,
        controller: ctrl,  // Logic in instantiated controller 
        windowClass: CssClass,
        backdrop: 'static',
        resolve: {
            params: function () {
                return params;
            }
        }
    });

    ModalInstance.result.then(function (result) {
        modalMessage($rootScope, result);
    })

}



function OpenCaseModal($scope, $uibModal, $stateParams, templ, ctrl, grid, row, size, $rootScope) {
    var masterId = '';
    if ($stateParams !== null) {
        masterId = $stateParams.constituentId;
    }

    var params = {
        masterId: typeof $stateParams !== null ? masterId : null,
        row: row,
        grid: grid

    }
    var CssClass = '';
    if (size === 'lg') {
        CssClass = 'app-modal-window';
    }

    var ModalInstance = ModalInstance || $uibModal.open({
       // animation: $scope.animationsEnabled,
        templateUrl: templ,
        controller: ctrl,  // Logic in instantiated controller 
        windowClass: CssClass,
        backdrop: 'static',
        resolve: {
            params: function () {
                return params;
            }
        }
    });

    ModalInstance.result.then(function (result) {

        if (angular.isUndefined(result.Output)) {
            setCaseNo($scope, result);
        }
        else {
            modalMessage($rootScope, result.Output);
        }

        
    });

}

function setCaseNo($scope, result) {
    if (typeof $scope.$parent.address !== 'undefined') {
        $scope.$parent.address.caseNo = result;
    }
    else if (typeof $scope.$parent.birth !== 'undefined') {
        $scope.$parent.birth.caseNo = result;
    }
    else if (typeof $scope.$parent.characteristics !== 'undefined') {
        $scope.$parent.characteristics.caseNo = result;
    }
    else if (typeof $scope.$parent.contactPref !== 'undefined') {
        $scope.$parent.contactPref.caseNo = result;
    }
    else if (typeof $scope.$parent.death !== 'undefined') {
        $scope.$parent.death.caseNo = result;
    }
    else if (typeof $scope.$parent.phone !== 'undefined') {
        $scope.$parent.phone.caseNo = result;
    }
    else if (typeof $scope.$parent.email !== 'undefined') {
        $scope.$parent.email.caseNo = result;
    }
    else if (typeof $scope.$parent.grpMembershp !== 'undefined') {
        $scope.$parent.grpMembershp.caseNo = result;
    }
    else if (typeof $scope.$parent.name !== 'undefined') {
        $scope.$parent.name.caseNo = result;
    }
    else if (typeof $scope.$parent.confirmCart != 'undefined') {
        $scope.confirmCart.caseNo = result;
    }
    else if (typeof $scope.$parent.unmerge != 'undefined') {
        $scope.unmerge.caseNo = result;
    }
    else {
        $scope.$parent.caseNo = result;
    }
    console.log($scope.$parent);
}

function OpenShowDetailsModal($scope, $uibModal, templ, ctrl, type, result,  $rootScope,title) {
   // console.log(title);
    var params = {
       title:title,
        type: type,
        result:result
    }
    var CssClass = 'app-modal-window';


    var ModalInstance = ModalInstance || $uibModal.open({
       // animation: $scope.animationsEnabled,
        templateUrl: templ,
        controller: ctrl,  // Logic in instantiated controller 
        windowClass: CssClass,
        backdrop: 'static',
        resolve: {
            params: function () {
                return params;
            }
        }
    });

    ModalInstance.result.then(function (result) {
        modalMessage($rootScope, result);
    })

}


newAccMod.factory('ConstUtilServices', ['$filter', function ($filter) {
    return {
        getStartDate: function () {
            return $filter('date')(new Date(), 'MM/dd/yyyy');
        }

    }


}]);

/************************* Services - Data *************************/
newAccMod.factory("ConstituentNAICSService", ['$http',
    function ($http) {
        //base Path
        var BasePath = $("base").first().attr("href");

        return {
            //Service method to perform search
           
            submitNAICSCodeUpdates: function (postData) {
                return $http.post(BasePath + "Test/SubmitNAICSCodeUpdates", postData, {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },
            submitEditNAICSCodes: function (postData) {
                return $http.post(BasePath + "Test/EditNAICSCode", postData, {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },            
            getMasterNAICSDetails: function (postData) {
                return $http.post(BasePath + "Test/getMasterNAICSDetails", postData, {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },           
            getNAICSCodesTreeGrid: function () {
                return $http.get(BasePath + "Test/GetNAICSCodesTreeGrid",
                    {
                        headers: {
                            "Content-Type": "Application/JSON",
                            "Accept": "Application/JSON"
                        }
                    }).success(function (result) {
                        return result;

                    }).error(function (result) {
                        return result;
                    });
            },                  
            getUserPermissions: function () {
                return $http.get(BasePath + "Home/GetUserPermissions",
                    {
                        headers: {
                            "Content-Type": "Application/JSON",
                            "Accept": "Application/JSON"
                        }
                    }).success(function (result) {
                        return result;

                    }).error(function (result) {
                        return result;
                    });
            }
        }

    }
]);

function modalMessage($rootScope, result) {

    $rootScope.FinalMessage = "";
    $rootScope.ReasonOrTransKey = "";
    $rootScope.ConfirmationMessage = "";
    //console.log(result);
    $rootScope.FinalMessage = result.finalMessage;
    $rootScope.ReasonOrTransKey = result.ReasonOrTransKey;
    $rootScope.ConfirmationMessage = result.ConfirmationMessage;
    $rootScope.CaseNo = '';
    if (!angular.isUndefined(result.CaseNo) || result.CaseNo != null) {
        $rootScope.CaseNo = result.CaseNo;
    }
        
    angular.element("#iConfirmationModal").modal({ backdrop: 'static', keyboard: false });
}

function messagePopup($rootScope, message, header) {

    $rootScope.message = "";
    $rootScope.header = "";

    $rootScope.message = message;
    $rootScope.header = header;
    angular.element("#iErrorModal").modal();
    //openErrorPopup(message, header, $uibModal);
}

function openErrorPopup(message, header, $uibModal) {
    var CssClass = 'app-modal-window';

    var params = {
        "message": message,
        "header": header
    }
    var ModalInstance = ModalInstance || $uibModal.open({
        templateUrl: BasePath + 'App/Constituent/Views/common/iError.tpl.html',
        controller: 'ErrorInstantiateCtrl',  // Logic in instantiated controller 
        backdrop: 'static',
        resolve: {
            params: function () {
                return params;
            }
        }
    });
}


function messagePopupClose() {
    angular.element("#iErrorModal").modal('hide');
}