// Cart Controller //
angular.module('constituent').controller('constCartController', ['$scope', 'constituentServices', 'constituentDataServices', '$localStorage', 'constRecordType',
    'uiGridConstants', '$window', '$http', '$localStorage', 'constituentCRUDapiService', 'constClearDataService', '$location', 'constituentCRUDoperations',
    '$uibModal', '$rootScope', '$sessionStorage', 'MessageService',
function ($scope, constituentServices, constituentDataServices, $localStorage, constRecordType, uiGridConstants, $window, $http, $localStorage, constituentCRUDapiService,
    constClearDataService, $location, constituentCRUDoperations, $uibModal, $rootScope, $sessionStorage, MessageService) {
    //  var SEARCH_URL = BasePath + "constituent/search";
    var MERGE = 'Merge';
    var UNMERGE = 'Unmerge';
    var SEARCH_RESULTS_URL = "/constituent/search/results";
    var REDIRECT_URL = "/constituent/search/results/multi/";
    var inialize = function () {

        // console.log($localStorage);
        // $scope.record_type = $localStorage.type;

        $scope.toggleCartGrid = { "display": "none" };
        $scope.toggleNoRecords = { "display": "none" };

        var columnDefs = constituentServices.getCartColumnDef();
        $scope.cartGridOptions = constituentServices.getCartGridOptions(uiGridConstants, columnDefs);


        $scope.cartGridOptions.onRegisterApi = function (grid) {
            $scope.gridApi = grid;
            $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                // var msg = 'row selected ' + row.isSelected;
                rows = $scope.gridApi.selection.getSelectedRows();
                //add the rows to be merged into MergeRows variable
                $scope.cart.mergeRows = rows;
            });
        };

        var listener = $scope.$watch(function () {
            return $scope.cartGridOptions.data.length;
        }, function (newLength, oldLength) {
            if (newLength > 0) {
                $scope.totalCartItems = $scope.cartGridOptions.data.length;
                //unbind watch after use
                listener();
            }
        });

        //get the merge cart data and set merge as default section to shown
        constituentServices.getCartResults().success(function (datas) {
            constituentDataServices.setCartData(datas);
            var cartItems = constituentDataServices.getCartData();
            if (cartItems.length <= 0) {
                $scope.toggleNoRecords = { "display": "block" };
                $scope.pleaseWait = { "display": "none" };
            }
            else {
                $scope.toggleCartGrid = { "display": "block" };
                $scope.totalCartItems = cartItems.length;
                $scope.currentPage = 1;
                $scope.cartGridOptions = constituentServices.getCartGridLayout($scope.cartGridOptions, uiGridConstants, datas);
                $scope.pleaseWait = { "display": "none" };
            }

        }).error(function (result) {
            errorPopup(result);
        });

        //get the unmerge cart data
        constituentServices.getUnmergeCartResults().success(function (result) {
            constituentDataServices.setUnmergeCartData(result);

        }).error(function (result) {
            errorPopup(result);
        });

    }
    inialize();


    $scope.removeMergeRow = function (row) {
        var index = row.rowIndex;
        //console.log(JSON.stringify(row.IndexString));
        constituentServices.removeMergeFromCart(row).success(function (result) {
            $scope.cartGridOptions.data.splice(index, 1);
            $scope.cart.mergeRows = [];
        }).error(function (result) {
            errorPopup(result);
        });
    };

    $scope.removeUnmergeRow = function (row) {
        console.log(row);
        var index = row.rowIndex;
        //the method accepts array;
        var arr = [row];
        var newData = $.grep($scope.cartGridOptions.data, function (e) {
            return e.IndexString != row.IndexString;
        });

       // console.log(newData);
        constituentServices.removeUnmergeFromCart(arr).success(function (result) {
            $scope.cartGridOptions.data = '';
            $scope.cartGridOptions.data.length = 0;
            $scope.cartGridOptions.data = newData;
            constituentDataServices.setUnmergeCartData(newData);
            $scope.cart.mergeRows = [];
        }).error(function (result) {
            errorPopup(result);
        });
    };

    $scope.pageChanged = function (page) {
        $scope.gridApi.pagination.seek(page);
    };


    //Clear Carts 
    $scope.ClearCart = function (type) {

        //  alert("alert");
        if (type === MERGE) {
            constituentServices.clearMergeCart().success(function (result) {
                if (result == "1") {
                    $scope.cartGridOptions.data = [];
                    //$scope.ConfirmationMessage = CART.CLEARED;
                    //$scope.ReasonOrTransKey = CART.CLEAR_REASON;
                    //   $scope.RedirectToSearch();
                    //$("#CartClearConfirmationModal").modal();

                    var params = {
                        ConfirmationMessage : CART.CLEARED,
                        ReasonOrTransKey: CART.CLEAR_REASON
                    }

                    MessageService.getPopup(params).then(function (result) {
                        if(result.redirect)
                            $location.url(SEARCH_RESULTS_URL);
                    });


                }
            }).error(function (result) {
                errorPopup(result);
            });
        }
        else if (type === UNMERGE) {
            constituentServices.clearUnmergeCart().success(function (result) {
                if (result == "1") {
                    $scope.cartGridOptions.data = [];
                    //$scope.ConfirmationMessage = CART.CLEARED;
                    //$scope.ReasonOrTransKey = CART.CLEAR_REASON;
                    //  $scope.RedirectToSearch();
                    // $("#CartClearConfirmationModal").modal();

                    var params = {
                        ConfirmationMessage: CART.CLEARED,
                        ReasonOrTransKey: CART.CLEAR_REASON
                    }

                    MessageService.getPopup(params).then(function (result) {
                        if (result.redirect)
                            $location.url(SEARCH_RESULTS_URL);
                    });

                }
            }).error(function (result) {
                errorPopup(result);
            });;
        }

    };
    // drop downs
    $scope.cart = {
        dropDowns: [MERGE, UNMERGE],
        selected: {
            dropDown: MERGE
        },
        pleaseWait: false

    };


    $scope.RedirectToSearch = function () {
        $("#CartClearConfirmationModal").modal('hide');
        $('.modal-backdrop').hide();
        $location.url(SEARCH_RESULTS_URL);
    }

    //triggered when you click on constituent_id(masterId) in cart page
    $scope.setGlobalValues = function (row) {
        delete $sessionStorage.masterId;
        delete $sessionStorage.name;
        delete $sessionStorage.type;
        $sessionStorage.masterId = row.entity.constituent_id;
        $sessionStorage.name = row.entity.name;
        $sessionStorage.type = row.entity.constituent_type;
        constClearDataService.clearMultiData();
        $scope.pleaseWait = { "display": "block" };
        $location.url(REDIRECT_URL + row.entity.constituent_id + "");

    };

    //used when you select one of the dropdowns in cart section
    $scope.cart.setCart = function () {
        if ($scope.cart.selected.dropDown == MERGE) {
            var columnDefs = constituentServices.getCartColumnDef();
            // $scope.cartGridOptions.data = [];               
            var result = constituentDataServices.getCartData();

            if (result.length <= 0) {
                $scope.totalCartItems = 0;
                $scope.toggleNoRecords = { "display": "block" };
                $scope.toggleCartGrid = { "display": "none" };
            }
            else {

                $scope.totalCartItems = result.length;
                $scope.currentPage = 1;
                $scope.cartGridOptions = constituentServices.getCartGridOptions(uiGridConstants, columnDefs);
                $scope.cartGridOptions = constituentServices.getCartGridLayout($scope.cartGridOptions, uiGridConstants, result);
                $scope.toggleCartGrid = { "display": "block" };
                $scope.toggleNoRecords = { "display": "none" };
            }

        }
        else if ($scope.cart.selected.dropDown == UNMERGE) {
            var columnDefs = constituentServices.getUnmergeColumnDef(uiGridConstants);
            // $scope.cartGridOptions.data = [];
            var _previousUnmergeResults = constituentDataServices.getUnmergeCartData();
            if (_previousUnmergeResults.length <= 0) {
                $scope.totalCartItems = 0;
                $scope.toggleNoRecords = { "display": "block" };
                $scope.toggleCartGrid = { "display": "none" };
            }
            else {

                $scope.totalCartItems = _previousUnmergeResults.length;
                $scope.currentPage = 1;
                $scope.cartGridOptions = constituentServices.getCartGridOptions(uiGridConstants, columnDefs);
                $scope.cartGridOptions = constituentServices.getCartGridLayout($scope.cartGridOptions, uiGridConstants, _previousUnmergeResults);
                $scope.toggleCartGrid = { "display": "block" };
                $scope.toggleNoRecords = { "display": "none" };
            }
        }
    };


    // this compare is for merge/unmerge section, triggered when clicked on compare in cart section
    $scope.compare = function (type) {


        if (type === MERGE) {
            var masterIds = [];
            var constituent_types = [];
            var validate = true;

            if (angular.isUndefined($scope.cart.mergeRows) || $scope.cart.mergeRows.length <= 0 || $scope.cart.mergeRows.length <= 1) {
                messagePopup($rootScope, "Please select at least 2 records", "Alert");
                return;
            }


            $scope.cart.pleaseWait = true;
            angular.forEach($scope.cart.mergeRows, function (v, k) {
                if(validate){
                    masterIds.push(v.constituent_id);
                    if (constituent_types.length <= 0) {
                        constituent_types.push(v.constituent_type);
                    }
                    else {
                        var type = constituent_types.pop();
                        //check if the constituent type of all selected records for comparing are same 
                        if (type === v.constituent_type) {
                            constituent_types.push(type);
                        
                        }
                        else {
                            validate = false;
                        }
                    }
               // console.log(v);
                }                
            });


            if (validate) {
                var postParams = { // postParams = Object {ConstituentType: "OR", MasterId: Array[2]}
                    "ConstituentType": constituent_types.pop(),
                    "MasterId": masterIds
                }

              
                constituentCRUDapiService.getCRUDOperationResult(postParams, CRUD_CONSTANTS.MERGE_COMPARE).success(function (result) {
                    $scope.cart.pleaseWait = false;
                    var sendResult = {
                        data: result,
                        rows: $scope.cart.mergeRows
                    }
                    constituentCRUDoperations.getMergeCartComparePopup($scope, $uibModal, sendResult, $scope.cartGridOptions);
                }).error(function (result) {
                    $scope.cart.pleaseWait = false;
                    errorPopup(result);
                });
            }
            else {
                $scope.cart.pleaseWait = false;
                messagePopup($rootScope, "Please select either Individual or Organization records for merging", "Alert");
                return;
            }
            
        }
        else if (type === UNMERGE) {
            // console.log($scope.cart.mergeRows);
            var constituent_types = [];
            var validate = true;

            if (angular.isUndefined($scope.cart.mergeRows) || $scope.cart.mergeRows.length <= 0) {
                messagePopup($rootScope, "Please select at least 1 record", "Alert");
                return;
            }

            angular.forEach($scope.cart.mergeRows, function (v, k) {
                if (validate) {

                    if (constituent_types.length <= 0) {
                        constituent_types.push(v.constituent_type);
                    }
                    else {
                        var type = constituent_types.pop();
                        //check if the constituent type of all selected records for comparing are same 
                        if (type === v.constituent_type) {
                            constituent_types.push(type);

                        }
                        else {
                            validate = false;
                        }
                    }
                    // console.log(v);
                }
            });
            if (validate) {
                constituentCRUDapiService.getCRUDOperationResult($scope.cart.mergeRows, CRUD_CONSTANTS.UNMERGE_COMPARE).success(function (result) {
                    $scope.cart.pleaseWait = false;
                    var sendResult = {
                        data: result,
                        rows: $scope.cart.mergeRows
                    }
                    constituentCRUDoperations.getUnmergeCartComparePopup($scope, $uibModal, sendResult, $scope.cartGridOptions);

                    // console.log(result);
                });
            }
            else {
                $scope.cart.pleaseWait = false;
                messagePopup($rootScope, "Please select either Individual or Organization records for unmerging", "Alert");
                return;
            }
            
        }



    }



    function errorPopup(result) {
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



angular.module('constituent').controller("MessagePopupCtrl", ['$scope', '$state', '$uibModal', '$uibModalInstance', '$uibModalStack', 'params', 
function ($scope, $state, $uibModal, $uibModalInstance, $uibModalStack,params) {


    console.log(params)


   

    var initialize = function () {

        var reasonOrtranskey = "";
        if(typeof params.ReasonOrTransKey != 'undefined'){
            reasonOrtranskey = params.ReasonOrTransKey;
        }


        $scope.popup = {
            ConfirmationMessage: params.ConfirmationMessage,
            FinalMessage: params.FinalMessage,
            ReasonOrTransKey: reasonOrtranskey
        }
    }

    initialize();


    $scope.popup.RedirectToSearch = function () {
        var result = {
            redirect : true
        }
        $uibModalInstance.close(result);
        
    }

}]);

