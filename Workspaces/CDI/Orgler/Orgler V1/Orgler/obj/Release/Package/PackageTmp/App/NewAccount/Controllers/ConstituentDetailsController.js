HOME_CONSTANTS = {    
 // CONST_NAME: 'ConstName',
    CONST_ORG_NAME: 'ConstOrgName',
    CONST_ADDRESS: 'ConstAddress',
    CONST_EMAIL: 'ConstEmail',
    CONST_PHONE: 'ConstPhone',
    CONST_EXT_BRIDGE: 'ConstExtBridge',
    CONST_POTENTIALMERGE: 'PotentialMerge',
    CONST_POTENTIALUNMERGE: 'PotentialUnMerge'
};

//var SIDE_PANEL_URL = BasePath + 'App/Constituent/Views/ConstSidePanel.html';

HOME_REDIRECT_URL = BasePath + "newaccount/search/result";


var CONST_SHOW = "Show All Records";
var CONST_HIDE = "Hide Inactive";

newAccMod.controller('constituentOrgNameDetailsController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constituentApiService', 'constMultiDataService', '$window', 'constMultiGridService', '$rootScope', 'constituentCRUDoperations', '$uibModal','$uibModalInstance',
function ($scope, constituentServices, uiGridConstants, $stateParams, constituentApiService, constMultiDataService, $window,
    constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal, $uibModalInstance) {

    var initialize = function () {
       // $scope.ConstSidePanel = SIDE_PANEL_URL;
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
    $stateParams.constituentId = $rootScope.paramsacc;
    var params = $stateParams.constituentId;
    //var params = $rootScope.paramsacc;
    $scope.masterId = params;
    if (params != "") {

        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_ORG_NAME, params, $scope.gridOptions,
          uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;

    } else {
       $window.location.href = HOME_REDIRECT_URL;
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
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
        type = HOME_CONSTANTS.CONST_ORG_NAME;
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

newAccMod.controller('constituentDetailsAddressController',
    ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams', 'constituentApiService', 'constMultiDataService', '$window',
        'constMultiGridService', '$rootScope', '$uibModal', 'constituentCRUDoperations', '$uibModalInstance',
function ($scope, constituentServices, uiGridConstants, $stateParams, constituentApiService, constMultiDataService, $window,
    constMultiGridService, $rootScope, $uibModal, constituentCRUDoperations,$uibModalInstance) {
    var initialize = function () {
       // $scope.ConstSidePanel = SIDE_PANEL_URL;
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

    $stateParams.constituentId = $rootScope.paramsacc;
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

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

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
        type = HOME_CONSTANTS.CONST_ADDRESS;
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



/*********************constituentDetailsPhoneController****************/

newAccMod.controller('constituentDetailsPhoneController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams', '$location',
    'constituentApiService', 'constMultiDataService', '$window', 'constituentDataServices', 'constMultiGridService', '$rootScope', 'constituentCRUDoperations', '$uibModal', '$uibModalInstance',
function ($scope, constituentServices, uiGridConstants, $stateParams, $location, constituentApiService, constMultiDataService, $window, constituentDataServices,
    constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal, $uibModalInstance) {

    var initialize = function () {
        //$scope.ConstSidePanel = SIDE_PANEL_URL;
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

    $stateParams.constituentId = $rootScope.paramsacc;
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

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

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
        type = HOME_CONSTANTS.CONST_PHONE;
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


    //$scope.phoneResearchOneRecord = function (row, grid) {
    //    console.log(row);
    //    var NAVIGATE_URL = BasePath + "App/constituent/search"
    //    var postParams = {
    //        "phone": row.cnst_phn_num,
    //        "type": $sessionStorage.type
    //    }
    //    constituentDataServices.setPhoneSearchParams(postParams);
    //    $location.url(NAVIGATE_URL);
    //}

    //$scope.phoneResearchAllRecords = function () {
    //    var NAVIGATE_URL = BasePath + "App/constituent/search"
    //    var results = constMultiDataService.getData(HOME_CONSTANTS.CONST_PHONE);

    //    console.log(results);
    //    if (results.length > 0) {
    //        for (var i = 0; i < results.length; i++) {
    //            var postParams = {
    //                "phone": results[i].cnst_phn_num,
    //                "type": $sessionStorage.type
    //            }
    //            constituentDataServices.setPhoneSearchParams(postParams);
    //        }
    //        $location.url(NAVIGATE_URL);
    //    } else {
    //        messagePopup($rootScope, "No Records found to search", "Alert");
    //    }
        
    //}

}]);


/*********************constituentDetailsEmailController****************/

newAccMod.controller('constituentDetailsEmailController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope', 'constituentCRUDoperations', '$uibModal', '$uibModalInstance',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window,
    constMultiGridService, $rootScope, constituentCRUDoperations, $uibModal,$uibModalInstance) {
    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
       // $scope.ConstSidePanel = SIDE_PANEL_URL;
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

    $stateParams.constituentId = $rootScope.paramsacc;
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

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

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
        type = HOME_CONSTANTS.CONST_EMAIL;
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

/*********************constituentDetailsPotentialMergeController****************/

newAccMod.controller('constituentDetailsPotentialMergeController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope', 'constituentDataServices', '$uibModal', '$uibModalInstance',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window, constMultiGridService, $rootScope, constituentDataServices, $uibModal, $uibModalInstance) {

    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
       // $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liExternalBridge = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsExternalBridgeColumns();
        $scope.chkselct=true;
        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_POTENTIALMERGE, 10);
        $scope.gridOptions = options;
        $scope.gridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
            $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row) {
            });
        }


        $scope.merge = {
            rows: []
        };
    };
    initialize();

    $stateParams.constituentId = $rootScope.paramsacc;
    var params = $stateParams.constituentId;
    $scope.masterId = params;
    if (params != "") {
        $scope.pleaseWait = { "display": "block" };
        var finalResults = commonGridDataSetup($scope, HOME_CONSTANTS.CONST_POTENTIALMERGE, params, $scope.gridOptions,
         uiGridConstants, constituentApiService, constMultiDataService, constMultiGridService, $rootScope);
        $scope.gridOptions = finalResults.gridOp;


    } else {
        $window.location.href = HOME_REDIRECT_URL;
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $rootScope.toggleFiltering = function () {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        // constituentServices.setGridFilterCSS($scope.gridOptions.enableFiltering);
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.ALL);
    }
    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };
   
    $scope.AddtoCart = function () {
        // console.log(JSON.stringify(rows));
       
        $scope.rows = $scope.gridApi.selection.getSelectedRows();
        //var chkselctCheked = $('chkselctCheked').val();
       var chkselctCheked = $scope.chkselct;
        if (chkselctCheked == true)
        {
            $scope.rows.push({ pot_merge_mstr_id: $scope.masterId, pot_merge_mstr_nm: $rootScope.pot_merge.name, address: $rootScope.pot_merge.addr, dsp_id: $rootScope.pot_merge.lexis_nexis_id, email: $rootScope.pot_merge.email, phone: $rootScope.pot_merge.phone });

        }             
       
        console.log($scope.rows);
        angular.forEach($scope.rows, function (k, v) {
            {
                if (k.$$hashKey != null) {
                    k.IndexString = k.$$hashKey;
                    delete k.$$hashKey;
                }
            }
            //added new column for type of the record
            k["constituent_type"] = "OR";
            //k["original_masterId"] = $scope.masterId;
        });
        if ($scope.rows.length > 0) {
            constituentServices.addMergeToCartPotential($scope.rows).success(function (result) {
                $rootScope.FinalMessage = CART.CONFIRMATION_MESSAGE;
                $rootScope.ReasonOrTransKey = CART.REASON;

                var message = {
                    finalMessage: "",
                    ReasonOrTransKey: CART.REASON,
                    ConfirmationMessage: CART.CONFIRMATION_MESSAGE
                }
                //modalMessage($rootScope, message);
                MessagePopup($rootScope, "Success", message.ReasonOrTransKey);
                // $("#CartConfirmationModal").modal();
                // $scope.CartVisibility = { "display": "block" };

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
                        errorPopups(result);
                    });


                }).error(function (result) {
                    errorPopups(result);
                });


            }).error(function (result) {
                errorPopups(result);
            });
        }

    };
    function MessagePopup($rootScope, headerText, bodyText) {
        $rootScope.newAccountModalPopupHeaderText = headerText;
        $rootScope.newAccountModalPopupBodyText = bodyText;
        angular.element(newAccountMessageDialogBox).modal({ backdrop: "static" });
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
    $rootScope.rootCaseLinkHtmlPath1 = BasePath + "App/Shared/Views/common/CartLink.tpl.html";
}]);
newAccMod.controller('constituentDetailsPotentialUnMergeController', ['$scope', 'constituentServices', 'uiGridConstants', '$stateParams',
    'constMultiDataService', 'constituentApiService', '$window', 'constMultiGridService', '$rootScope', 'constituentDataServices', '$uibModal', '$uibModalInstance',
function ($scope, constituentServices, uiGridConstants, $stateParams, constMultiDataService, constituentApiService, $window, constMultiGridService, $rootScope, constituentDataServices, $uibModal, $uibModalInstance) {

    var initialize = function () {
        $scope.toggleDetails = { "display": "none" };
        $scope.toggleHeader = { "display": "none" };
        $scope.pleaseWait = { "display": "none" };
       // $scope.ConstSidePanel = SIDE_PANEL_URL;
        $scope.liExternalBridge = "ChkSelected";
        // var columnDefs = constituentServices.getConstDetailsExternalBridgeColumns();

        // var options = constituentServices.getGridOptions(uiGridConstants, columnDefs);
        var options = constMultiGridService.getGridOptions(uiGridConstants, HOME_CONSTANTS.CONST_EXT_BRIDGE, 10);
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

    $stateParams.constituentId = $rootScope.paramsacc;
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
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
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
            k["constituent_type"] = "OR";
        });
        //console.log($scope.unmerge.rows);
        if ($scope.unmerge.rows.length > 0) {
            constituentServices.addUnmergeToCart(JSON.stringify($scope.unmerge.rows)).success(function (result) {
                $rootScope.ConfirmationMessage = CART.CONFIRMATION_MESSAGE;
                $rootScope.FinalMessage = '';
                $rootScope.ReasonOrTransKey = CART.REASON;
                // angular.element("#iConfirmationModal").modal();
                MessagePopup($rootScope, "Success", $rootScope.ReasonOrTransKey);
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
    function MessagePopup($rootScope, headerText, bodyText) {
        $rootScope.newAccountModalPopupHeaderText = headerText;
        $rootScope.newAccountModalPopupBodyText = bodyText;
        angular.element(newAccountMessageDialogBox).modal({ backdrop: "static" });
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
    $rootScope.rootCaseLinkHtmlPath = BasePath + "App/Shared/Views/common/CartLink.tpl.html";
}]);
newAccMod.controller('cartLinkCtrl', ['constituentServices', '$rootScope', 'constituentDataServices', '$scope', '$location', '$window',
    function (constituentServices, $rootScope, constituentDataServices, $scope, $location, $window) {
        $rootScope.CartVisibility = false;
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
                errorPopups(result);
            });

        }).error(function (result) {
            errorPopups(result);
        });




        $scope.ViewCart = function () {
            //delete $sessionStorage.masterId;
            //delete $sessionStorage.name;
            //delete $sessionStorage.type;            
            // $location.url("/constituent/search/results/cart");
            window.open(BasePath + "constituent/search/results/cart");
           // $window.location.href = BasePath + "constituent/search/results/cart";
           
        }


        function errorPopups(result) {
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

    }]);
/*********************Common Grid Data Setup Function ****************/
function commonGridDataSetup($scope, constant, params, gridOptions, uiGridConstants,
    constituentApiService, constMultiDataService, constMultiGridService,$rootScope) {
    //var _previousData = constMultiDataService.getData(constant);
    //if (_previousData.length <= 0) {
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
            if(gridOptions.data.length > 0)
            {
                $scope.distinctRecordCount = gridOptions.data[0].distinct_count;
            }
           
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
   // }
    //else {
    //    //   gridOptions = constituentServices.getGridLayout(gridOptions, uiGridConstants, _previousData);
    //    gridOptions = constMultiGridService.getMultiGridLayout(gridOptions, uiGridConstants, _previousData, constant);
    //    $scope.toggleDetails = { "display": "block" };
    //    $scope.toggleHeader = { "display": "block" };
    //    $scope.pleaseWait = { "display": "none" };
    //    $scope.totalItems = gridOptions.data.length;

    //    $scope.distinctRecordCount = gridOptions.data[0].distinct_count;
    //}

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




