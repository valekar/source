HOME_CONSTANTS = {
    BEST_SMRY: 'BestSmry',
    CONST_NAME: 'ConstName',
    //CONST_ORG_NAME: 'ConstOrgName',
    CONST_ADDRESS: 'ConstAddress',
    CONST_EMAIL: 'ConstEmail',
    CONST_PHONE: 'ConstPhone',
    CONST_EXT_BRIDGE: 'ConstExtBridge',
    CONST_BIRTH: 'ConstBirth',
    CONST_DEATH: 'ConstDeath',
    CONST_PREF: 'ConstPref',
    CHARACTERISTICS: 'Character',
    GRP_MEMBERSHIP: 'GrpMembership',
    TRANS_HISTORY: "TransHistory",
    ANON_INFO: 'AnonInfo',
    MASTER_ATTEMPT: 'MasterAttempt',
    INTERNAL_BRIDGE: 'InternalBridge',
    MERGE_HISTORY: 'MergeHistory',
    CONST_ORG_NAME: "ConstOrgName",
    AFFILIATOR: "Affiliator",
    SUMMARY_VIEW: "SummaryView",
    MASTER_DETAIL: "MasterDetail",
    ADV_CASE_SEARCH: "AdvCaseSearch",
    QUICK_CASE_SEARCH: "QuickCaseSearch",
    SHOW_DETAIL: "ShowDetails",
    SHOW_CONST_BIRTH: "showConstBirth",
    SHOW_CONST_DEATH: "showConstDeath",
    SHOW_CONST_EMAIL: "showConstEmail",
    SHOW_CONST_NAME: "showConstName",
    SHOW_CONST_PHONE: "showConstPhone",
    SHOW_CONT_PREF: "showContPref",
    SHOW_INTERNAL_BRIDGE: "showInternalBridge",
    SHOW_CHARACTERISTICS: "showCharacteristics",
    SHOW_CONST_EXT_BRIDGE: "showConstExtBridge",
    SHOW_CONST_ORG_NAME: "showConstOrgName",
    SHOW_CONST_ADDRESS: "showAddressName",
    OLD_MASTER_IDS: "oldMasterids",
    PRIVATE: "private",
    EXPORT_SEARCH: "exportSearchResult",
    CONST_PREF_VAL: 'ConstPrefVal',
    CHAPTER_SYSTEM: 'ChapterSys',
    SOURCE_SYSTEM_TYPE:"SourceSystemType"
};

var SIDE_PANEL_URL = BasePath + 'App/Constituent/Views/ConstSidePanel.html';

HOME_REDIRECT_URL = BasePath + "constituent/search";


var CONST_SHOW = "Show All Records";
var CONST_HIDE = "Hide Inactive";

angular.module('constituent').controller('constituentSummaryViewController', ['$scope', '$stateParams', 'uiGridConstants', '$window',
    'constituentServices', '$log', '$localStorage', 'constituentDataServices', 'constMultiDataService', 'constituentApiService', 'constMultiGridService', '$rootScope',
    function ($scope, $stateParams, uiGridConstants, $window, constituentServices, $log, $localStorage, constituentDataServices,
        constMultiDataService, constituentApiService, constMultiGridService, $rootScope) {


            var initialize = function () {
                $scope.ConstSidePanel = SIDE_PANEL_URL;
                $scope.toggleDetails = { "display": "none" };
                $scope.toggleHeader = { "display": "none" };
                $scope.pleaseWait = { "display": "none" };
                $scope.liSmryView = "ChkSelected";
                // var columnDefs = constituentServices.getConstDetailsPhoneColumns();

                // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
                var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.SUMMARY_VIEW,10);
                $scope.gridOptions = options;
                $scope.gridOptions.onRegisterApi = function (grid) {
                    $scope.gridApi = grid;
                }

            };
            initialize();

            var params = $stateParams.constituentId;
            $scope.masterId = params;
            if (params != "") {

                $scope.pleaseWait = { "display": "block" };
                var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.SUMMARY_VIEW, params, $scope.gridOptions,
                  uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
                $scope.gridOptions = finalResults.gridOp;

            } else {
                $window.location.href = HOME_REDIRECT_URL;
            }


            $rootScope.toggleFiltering = function () {
                $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
                // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
                $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
            }
            $scope.pageChanged = function (page) {
                $scope.gridApi.pagination.seek(page);
            };


        

    }]);

angular.module('constituent').controller('constituentDetailsController', ['$scope', '$stateParams', 'uiGridConstants', '$window',
    'constituentServices', '$log', '$localStorage', 'constituentDataServices', 'constMultiDataService', 'constituentApiService', 'constMultiGridService', '$rootScope',
    function ($scope, $stateParams, uiGridConstants, $window, constituentServices, $log, $localStorage, constituentDataServices,
        constMultiDataService, constituentApiService, constMultiGridService, $rootScope) {

        var initailize = function () {
            $scope.ConstSidePanel = SIDE_PANEL_URL;
            $scope.liARCBest = "ChkSelected";
            $scope.toggleConstDetails = { "display": "none" };
            $scope.toggleHeader = false;
            $scope.pleaseWait = { "display": "none" };

            //var columnDefs = constituentServices.getConstDetailsNamesColumns();

            var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.BEST_SMRY,10);
            $scope.gridOptions = options;
            $scope.gridOptions.onRegisterApi = function (grid) {
                $scope.gridApi = grid;
            }
            // CallAddNamePopup($scope, $modal, $log);

            //pagination
            $scope.currentConstAddress = 1;
        }

        initailize();

        //logic part
        var params = $stateParams.constituentId;
        $scope.masterId = params;
        $scope.BaseURL = BasePath;

        if (params != "") {
            var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.BEST_SMRY, params, $scope.gridOptions,
            uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
            $scope.gridOptions = finalResults.gridOp;
        } else {
            $window.location.href = HOME_REDIRECT_URL;
        }

        // function for toggling the filters
        // $scope.toggleFiltering = function () {
        ////   $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;          
        // $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
        // }

        $rootScope.toggleFiltering = function (type) {
            $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
            $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
        }

        $scope.pageChanged = function (page) {
            $scope.gridApi.pagination.seek(page);
        };

    }]);


/*********************constituentDetailsNameController****************/

angular.module('constituent').controller('constituentDetailsNameController', ['$scope', 'constituentServices', 'uiGridConstants', '$uibModal',
    '$stateParams', '$localStorage', 'constituentDataServices', 'constMultiDataService', '$window', 'constituentApiService', '$rootScope', 'constMultiGridService', 'constituentCRUDoperations',
function ($scope, constituentServices, uiGridConstants, $uibModal, $stateParams, $localStorage, constituentDataServices,
    constMultiDataService, $window, constituentApiService, $rootScope, constMultiGridService, constituentCRUDoperations) {
    // $scope.masterId = "";
    var initialize = function () {
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.toggleConstDetails = { "display": "none" };
        $scope.liName = "ChkSelected";
        $scope.toggleHeader = false;
        $scope.pleaseWait = { "display": "none" };
        $scope.BaseURL = BasePath;
        // var columnDefs = constituentServices.getConstDetailsSummaryColumns(uiGridConstants, $scope);
        //var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_NAME,10);
        $scope.gridOptions = options;
       // $scope.gridOptions.
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    //get the consId from URL;
    var params = $stateParams.constituentId;

    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_NAME, params, $scope.gridOptions,
           uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        constituentDataServices.setConstNameGrid(finalResults.gridOp);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }
    //end of if param condition

    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }

    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_NAME);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_NAME);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }

    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }



    $scope.showName = {
        showAllButtonName: CONST_SHOW
    };

    $scope.showAllrecords = function () {
        switch ($scope.showName.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showName.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CONST_NAME, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showName.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CONST_NAME, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }

}]);


/*********************constituentDetailsOrgNameController****************/

angular.module('constituent').controller('constituentOrgNameDetailsController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constituentApiService', 'constMultiDataService', '$window', 'constMultiGridService', '$rootScope', 'constituentCRUDoperations', '$uibModal',
function ($scope, constituentServices, uiGridConstants, $stateParams, constituentApiService, constMultiDataService, $window,
    constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal) {

    var initialize = function () {
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.liOrgName = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsPhoneColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_ORG_NAME,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {

        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_ORG_NAME, params, $scope.gridOptions,
          uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };


    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_ORG_NAME);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_ORG_NAME);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }

    $scope.showOrgName = {
        showAllButtonName: CONST_SHOW
    };

    $scope.showAllrecords = function () {
        switch ($scope.showOrgName.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showOrgName.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CONST_ORG_NAME, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showOrgName.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CONST_ORG_NAME, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }

}]);



/*********************constituentDetailsAddressController****************/

angular.module('constituent').controller('constituentDetailsAddressController',
    ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams', 'constituentApiService', 'constMultiDataService', '$window',
        'constMultiGridService', '$rootScope','$uibModal','constituentCRUDoperations',
function ($scope, constituentServices, uiGridConstants, $stateParams, constituentApiService, constMultiDataService, $window,
    constMultiGridService, $rootScope, $uibModal, constituentCRUDoperations) {
    var initialize = function () {
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.liAddress = "ChkSelected";
        //pagination
        $scope.currentPage = 1;
        // var columnDefs = constituentServices.getConstDetailsAddressColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_ADDRESS,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_ADDRESS, params, $scope.gridOptions,
            uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService,$rootScope);
        $scope.gridOptions = finalResults.gridOp;
    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        //   constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        //  console.log(page);
        $scope.gridApi.pagination.seek(page);
    };


    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_ADDRESS);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_ADDRESS);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }
    $scope.showAddress = {
        showAllButtonName: CONST_SHOW
    };

    $scope.showAllrecords = function () {
        switch ($scope.showAddress.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showAddress.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CONST_ADDRESS, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems= result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showAddress.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CONST_ADDRESS, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }


}]);


/*********************constituentDetailsBirthController****************/

angular.module('constituent').controller('constituentDetailsBirthController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams', 'constituentApiService',
    'constMultiDataService', '$window', 'constMultiGridService', '$rootScope','constituentCRUDoperations', '$uibModal',
function ($scope, constituentServices, uiGridConstants, $stateParams, constituentApiService, constMultiDataService, $window,
    constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal) {
    var initialize = function () {
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.liBirth = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsBirthColumns();

        //  var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_BIRTH,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_BIRTH, params, $scope.gridOptions,
          uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;


    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        //  constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_BIRTH);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_BIRTH);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }

    $scope.showBirth = {
        showAllButtonName: CONST_SHOW
    };

    $scope.showAllrecords = function () {
        switch ($scope.showBirth.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showBirth.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CONST_BIRTH, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showBirth.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CONST_BIRTH, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }


}]);


/*********************constituentDetailsPhoneController****************/

angular.module('constituent').controller('constituentDetailsPhoneController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams', '$location',
    'constituentApiService', 'constMultiDataService', '$window', 'constituentDataServices', 'constMultiGridService', '$rootScope', 'constituentCRUDoperations', '$uibModal', '$localStorage','$sessionStorage',
function ($scope, constituentServices, uiGridConstants, $stateParams, $location, constituentApiService, constMultiDataService, $window, constituentDataServices,
    constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal, $localStorage,$sessionStorage) {

    var initialize = function () {
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.liPhone = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsPhoneColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_PHONE,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {

        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_PHONE, params, $scope.gridOptions,
          uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };


    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_PHONE);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_PHONE);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }

    $scope.showPhone = {
        showAllButtonName: CONST_SHOW
    };

    $scope.showAllrecords = function () {
        switch ($scope.showPhone.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showPhone.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CONST_PHONE, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showPhone.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CONST_PHONE, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }


    $scope.phoneResearchOneRecord = function (row, grid) {
        console.log(row);
        var NAVIGATE_URL = BasePath + "App/constituent/search"
        var postParams = {
            "phone": row.cnst_phn_num,
            "type": $sessionStorage.type
        }
        constituentDataServices.setPhoneSearchParams(postParams);
        $location.url(NAVIGATE_URL);
    }

    $scope.phoneResearchAllRecords = function () {
        var NAVIGATE_URL = BasePath + "App/constituent/search"
        var results = constMultiDataService.getData(HOME_CONSTANTS.CONST_PHONE);

        console.log(results);
        if (results.length > 0) {
            for (var i = 0; i < results.length; i++) {
                var postParams = {
                    "phone": results[i].cnst_phn_num,
                    "type": $sessionStorage.type
                }
                constituentDataServices.setPhoneSearchParams(postParams);
            }
            $location.url(NAVIGATE_URL);
        } else {
            messagePopup($rootScope, "No Records found to search", "Alert");
        }
        
    }

}]);


/*********************constituentDetailsEmailController****************/

angular.module('constituent').controller('constituentDetailsEmailController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope','constituentCRUDoperations', '$uibModal',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window,
    constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal) {
    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liEmail = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsEmailColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_EMAIL,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_EMAIL, params, $scope.gridOptions,
          uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };


    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_EMAIL);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_EMAIL);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }

    $scope.showEmail = {
        showAllButtonName: CONST_SHOW
    };

    $scope.showAllrecords = function () {
        switch ($scope.showEmail.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showEmail.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CONST_EMAIL, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showEmail.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CONST_EMAIL, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }

}]);

/*********************constituentDetailsExternalBridgeController****************/

angular.module('constituent').controller('constituentDetailsExternalBridgeController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope','constituentDataServices',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window, constMultiGridService, $rootScope, constituentDataServices) {

    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liExternalBridge = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsExternalBridgeColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_EXT_BRIDGE,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
            $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            });
        }


        $scope.unmerge = {
            rows: []
        };
    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_EXT_BRIDGE, params, $scope.gridOptions,
         uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;


    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };


    //add to unmerge cart!!
    $scope.unmerge.addTocart = function () {
        $scope.unmerge.rows = $scope.gridApi.selection.getSelectedRows();
        console.log($scope.unmerge.rows);
        angular.forEach($scope.unmerge.rows, function (k, v) {
            {
                if (k.$$hashKey != null) {
                    k.IndexString = k.$$hashKey;
                    delete k.$$hashKey;
                }
            }
            //added new column for type of the record
            k["constituent_type"] = $scope.constituent_type;
        });
        //console.log($scope.unmerge.rows);
        if ($scope.unmerge.rows.length > 0) {
            constituentServices.addUnmergeToCart(JSON.stringify($scope.unmerge.rows)).success(function (result) {
                $rootScope.ConfirmationMessage = CART.CONFIRMATION_MESSAGE;
                $rootScope.FinalMessage = '';
                $rootScope.ReasonOrTransKey = CART.REASON;
                angular.element("#iConfirmationModal").modal();
                constituentServices.getCartResults().success(function (result) {
                    var carItemsNumber = result.length;
                    $rootScope.CartItemsNumber = carItemsNumber;

                    constituentServices.getUnmergeCartResults().success(function (result) {
                        constituentDataServices.setUnmergeCartData(result);
                        $rootScope.CartItemsNumber = $rootScope.CartItemsNumber + result.length;
                        if ($rootScope.CartItemsNumber > 0) {

                            $rootScope.CartVisibility = true;
                        }
                        else {

                            $rootScope.CartVisibility = false;
                        }
                    }).error(function (result) {
                        errorPopupMessages(result);

                    });

                }).error(function (result) {
                    errorPopupMessages(result);

                });

               
            }).error(function (result) {
                errorPopupMessages(result);

            });
        }
        else {
            messagePopup($rootScope, CART.SELECT_MESSAGE, "Alert");
        }
        

       

    }

    $scope.showExternal = {
        showAllButtonName: CONST_SHOW
    };

    $scope.showAllrecords = function () {
        switch ($scope.showExternal.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showExternal.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CONST_EXT_BRIDGE, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showExternal.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CONST_EXT_BRIDGE, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }


    function errorPopupMessages(result) {

        if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
            messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
        }
        else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
            messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
        }
        else if (result == CRUD_CONSTANTS.DB_ERROR) {
            messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

        }
    }

    $scope.showMergeUnmerge = true;
    var mergeUnmergePermission = constMultiGridService.getMergeUnmergePermission();
    if (!mergeUnmergePermission) {
        //this is a common varaible for all sections
        $scope.showMergeUnmerge = false;
    }

}]);

/*********************constituentDetailsDeathController****************/

angular.module('constituent').controller('constituentDetailsDeathController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope','constituentCRUDoperations', '$uibModal',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService,
    constituentApiService, $window, constMultiGridService, $rootScope,constituentCRUDoperations, $uibModal) {

    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liDeath = "ChkSelected";
        //var columnDefs = constituentServices.getConstDetailsDeathColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_DEATH,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_DEATH, params, $scope.gridOptions,
         uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };


    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_DEATH);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_DEATH);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }


    $scope.showDeath = {
        showAllButtonName: CONST_SHOW
    };

    $scope.showAllrecords = function () {
        console.log($scope.showDeath.showAllButtonName);
        switch ($scope.showDeath.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showDeath.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CONST_DEATH, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.death.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CONST_DEATH, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }

}]);


/*********************constituentDetailsContactPreferenceController****************/

angular.module('constituent').controller('constituentDetailsContactPreferenceController', ['$scope', 'constituentServices', 'uiGridConstants',
    '$stateParams', 'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope','constituentCRUDoperations', '$uibModal',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService,
    $window, constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal) {
    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liConactPreference = "ChkSelected";
        //  var columnDefs = constituentServices.getConstDetailsContactPreferenceColumns();
        //
        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_PREF,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        //console.log(params);
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_PREF, params, $scope.gridOptions,
          uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        //  constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };



    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_PREF);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CONST_PREF);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }


    $scope.showContactPref = {
        showAllButtonName: CONST_SHOW
    };

    $scope.showAllrecords = function () {
        switch ($scope.showContactPref.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showContactPref.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CONST_PREF, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showContactPref.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CONST_PREF, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }

}]);


/*********************constituentDetailsCharacteristicsController****************/

angular.module('constituent').controller('constituentDetailsCharacteristicsController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window, constMultiGridService, $rootScope) {
    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liCharacteristics = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsCharacteristicsColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CHARACTERISTICS,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        // console.log(params);
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CHARACTERISTICS, params, $scope.gridOptions,
        uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        //  constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };


    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CHARACTERISTICS);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.CHARACTERISTICS);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }

    $scope.showCharacteristics = {
        showAllButtonName: CONST_SHOW
    };
    $scope.showAllrecords = function () {
        switch ($scope.showCharacteristics.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showCharacteristics.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.CHARACTERISTICS, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showCharacteristics.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.CHARACTERISTICS, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }

}]);

/*********************constituentDetailsGroupMembershipsController****************/

angular.module('constituent').controller('constituentDetailsGroupMembershipsController', ['$scope', 'constituentServices', 'uiGridConstants',
    '$stateParams', 'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope','constituentCRUDoperations', '$uibModal',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, 
    constituentApiService, $window, constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal) {
    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liGroupMemberships = "ChkSelected";
        //  var columnDefs = constituentServices.getConstDetailsGroupMembershipsColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.GRP_MEMBERSHIP,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.GRP_MEMBERSHIP, params, $scope.gridOptions,
         uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        //   constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };




    $scope.commonEditGridRow = function (row, grid) {
        commonEditGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.GRP_MEMBERSHIP);
    }

    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.GRP_MEMBERSHIP);

    }

    //added by srini for tab level security
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }

    $scope.showGrpMembership = {
        showAllButtonName: CONST_SHOW
    };
    $scope.showAllrecords = function () {
        switch ($scope.showGrpMembership.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showGrpMembership.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.GRP_MEMBERSHIP, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showGrpMembership.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.GRP_MEMBERSHIP, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }

}]);


/*********************constituentDetailsTransactionHistoryController****************/

angular.module('constituent').controller('constituentDetailsTransactionHistoryController', ['$scope', 'constituentServices', 'uiGridConstants',
    '$stateParams', 'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window, constMultiGridService, $rootScope) {
    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liTransactionHistory = "ChkSelected";
        //  var columnDefs = constituentServices.getConstDetailsTransactionHistoryColumns();

        //var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.TRANS_HISTORY,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.TRANS_HISTORY, params, $scope.gridOptions,
        uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

}]);



/*********************constituentDetailsAffiliatorController****************/

angular.module('constituent').controller('constituentDetailsAffiliatorController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constituentApiService', 'constMultiDataService', '$window', 'constMultiGridService', '$rootScope', 'constituentCRUDoperations', '$uibModal',
function ($scope, constituentServices, uiGridConstants, $stateParams, constituentApiService, constMultiDataService, $window,
    constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal) {

    var initialize = function () {
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.liAffiliator = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsPhoneColumns();
        $scope.currentPage = 1;
        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.AFFILIATOR,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {

        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.AFFILIATOR, params, $scope.gridOptions,
          uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };



    $scope.commonDeleteGridRow = function (row, grid) {
        commonDeleteGridRow($scope, row, grid, constituentCRUDoperations, $uibModal, $stateParams, $scope.gridOptions, HOME_CONSTANTS.AFFILIATOR);

    }
    $scope.commonShowAddGridRow = true;
    var userTabPermission = constMultiGridService.getTabDenyPermission();
    if (userTabPermission) {
        //this is a common varaible for all sections
        $scope.commonShowAddGridRow = false;
    }
    $scope.commonAddGridRow = function (type) {
        commonAddGridRow($scope, null, constituentCRUDoperations, $uibModal, $stateParams, type, $scope.gridOptions);
    }

}]);



/*********************constituentDetailsPhoneController****************/

angular.module('constituent').controller('constituentMasterDetailController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constituentApiService', 'constMultiDataService', '$window', 'constMultiGridService', '$rootScope', 'constituentCRUDoperations', '$uibModal',
function ($scope, constituentServices, uiGridConstants, $stateParams, constituentApiService, constMultiDataService, $window,
    constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal) {

    var initialize = function () {
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.liMasterDetail = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsPhoneColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.MASTER_DETAIL,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {

        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.MASTER_DETAIL, params, $scope.gridOptions,
          uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

    

}]);

/*********************constituentDetailsAnonymousInformationController****************/

angular.module('constituent').controller('constituentDetailsAnonymousInformationController', ['$scope', 'constituentServices', 'uiGridConstants',
    '$stateParams', 'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window, constMultiGridService, $rootScope) {
    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liAnonymousInfo = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsAnonymousInformationColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.ANON_INFO,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.ANON_INFO, params, $scope.gridOptions,
        uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        //  constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

}]);

/*********************constituentDetailsMasteringAttemptsController****************/

angular.module('constituent').controller('constituentDetailsMasteringAttemptsController', ['$scope', 'constituentServices', 'uiGridConstants',
    '$stateParams', 'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window, constMultiGridService, $rootScope) {
    var initialize = function () {
        $scope.toggleConstMasteringAttemptsDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liMasteringAttempts = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsMasteringAttemptsColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.MASTER_ATTEMPT,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.MASTER_ATTEMPT, params, $scope.gridOptions,
        uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

}]);

/*********************constituentDetailsInternalBridgeController****************/

angular.module('constituent').controller('constituentDetailsInternalBridgeController', ['$scope', 'constituentServices', 'uiGridConstants',
    '$stateParams', 'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window, constMultiGridService, $rootScope) {

    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liInternalBridge = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsInternalBridgeColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.INTERNAL_BRIDGE,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.INTERNAL_BRIDGE, params, $scope.gridOptions,
        uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        //  constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

    $scope.showInternalBridge = {
        showAllButtonName: CONST_SHOW
    };
    $scope.showAllrecords = function () {
        switch ($scope.showInternalBridge.showAllButtonName) {
            case CONST_HIDE: {
                $scope.showInternalBridge.showAllButtonName = CONST_SHOW;
                var result = setHomeGridData(HOME_CONSTANTS.INTERNAL_BRIDGE, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
            case CONST_SHOW: {
                $scope.showInternalBridge.showAllButtonName = CONST_HIDE;
                var result = setFullHomeGridData(HOME_CONSTANTS.INTERNAL_BRIDGE, $scope.gridOptions, uiGridConstants, constMultiDataService, constMultiGridService)
                //console.log(result);
                $scope.gridOptions = result.gridOp;
                $scope.totalItems = result.itemCount;
                break;
            }
        }
    }

}]);

/*********************constituentDetailsMergeHistoryController****************/

angular.module('constituent').controller('constituentDetailsMergeHistoryController', ['$scope', 'constituentServices', 'uiGridConstants',
    '$stateParams', 'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window, constMultiGridService, $rootScope) {

    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
        $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liMergeHistory = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsMergeHistoryColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.MERGE_HISTORY,10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
        }

    };
    initialize();

    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.MERGE_HISTORY, params, $scope.gridOptions,
        uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }


    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        //  constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };

}]);


/*********************Common Grid Data Setup Function ****************/
function commonGridDataSetup($scope, constant, params, gridOptions, uiGridConstants,
    constituentApiService, constMultiDataService, constMultiGridService,$rootScope) {
    var _previousData = constMultiDataService.getData(constant);
    if (_previousData.length <= 0) {
        constituentApiService.getApiDetails(params, constant).success(function (results) {
            constMultiDataService.setFullData(results, constant);
            var newResult = filterConstituentData(results);
            constMultiDataService.setData(newResult, constant);
            // gridOptions = constituentServices.getGridLayout(gridOptions, uiGridConstants, results);
            gridOptions = constMultiGridService.getMultiGridLayout(gridOptions, uiGridConstants, newResult, constant);
            $scope.toggleDetails = { "display": "block" };
            $scope.toggleHeader = { "display": "block" };
            $scope.pleaseWait = { "display": "none" };

            //pagination
            $scope.totalItems = gridOptions.data.length;
        }).error(function (result) {
            $scope.toggleDetails = { "display": "block" };
            $scope.toggleHeader = { "display": "block" };
            $scope.pleaseWait = { "display": "none" };
            if (result == CRUD_CONSTANTS.ACCESS_DENIED) {
                messagePopup($rootScope, CRUD_CONSTANTS.ACCESS_DENIED_MESSAGE, "Error: Access Denied");
            }
            else if (result == CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                messagePopup($rootScope, CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
            }
            else if (result == CRUD_CONSTANTS.DB_ERROR) {
                messagePopup($rootScope, CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");

            }
        });
    }
    else {
        //   gridOptions = constituentServices.getGridLayout(gridOptions, uiGridConstants, _previousData);
        gridOptions = constMultiGridService.getMultiGridLayout(gridOptions, uiGridConstants, _previousData, constant);
        $scope.toggleDetails = { "display": "block" };
        $scope.toggleHeader = { "display": "block" };
        $scope.pleaseWait = { "display": "none" };
        $scope.totalItems = gridOptions.data.length;
    }

    var finalResults = {
        gridOp: gridOptions,
        itemCount: constMultiDataService.getData(constant).length
    }

    return finalResults;

}


function setFullHomeGridData( constant ,gridOptions, uiGridConstants, constMultiDataService, constMultiGridService) {
    var _previousData = constMultiDataService.getFullData(constant);
    gridOptions = constMultiGridService.getMultiGridLayout(gridOptions, uiGridConstants, _previousData, constant);
    var finalResults = {
        gridOp: gridOptions,
        itemCount: _previousData.length
    }
    return finalResults;
}

function setHomeGridData( constant, gridOptions, uiGridConstants, constMultiDataService, constMultiGridService) {
    var _previousData = constMultiDataService.getData(constant);
    gridOptions = constMultiGridService.getMultiGridLayout(gridOptions, uiGridConstants, _previousData, constant);
    var finalResults = {
        gridOp: gridOptions,
        itemCount: _previousData.length
    }
    return finalResults;
}

var myApp;
myApp = myApp || (function () {
    //var pleaseWaitDiv = $('<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');
    var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="basicModal" aria-hidden="true" tabindex="-1"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h3>Processing...</h3></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar" style="width: 100%;"><span class="sr-only">60% Complete</span></div></div></div></div></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal('show');
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        },
    };
})();




