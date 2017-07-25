angular.module('upload').factory('ListUploadServices', ['$http', '$uibModal', 'uiGridConstants', '$q', 'ErrorService',
    function ($http, $uibModal, uiGridConstants, $q, ErrorService) {
        var listUploadSearchData = [];
        var listUploadSearchParams = {};
        var listUploadTransactionDetails = {};
        var userPermissions = {};
    return {
        //added for getting user permissions
        getPermissions: function () {
            return $http.get(BasePath + "Home/GetUserPermissions", {
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            }).then(function (result) {
                return result;
            },
            function (error) {
                return $q.reject(error);
            });
        },
        getListUploadSearchResults: function (postParams) {
            return $http.post(BasePath + "UploadNative/listUploadSearch", postParams,
                {
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    }
                }
                ).then(function (result) { return result; }, function (error) { return $q.reject(error); });
        },

        getDropDownValues: function () {
            return $http.get(BasePath + 'Home/getGroupNames', {
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            }).then(function (results) {
                return results;
            }, function (error) {
                return $q.reject(error);
            });
        },
        //search results
        setListUploadSearchData: function (result) {
            listUploadSearchData = result;
        },
        getListUploadSearchData: function () {
            return listUploadSearchData;
        },       
        clearListUploadSearchData: function () {
            listUploadSearchData = [];
        },
        //search params stored for exporting 
        setListUploadSearchParams: function (result) {
            listUploadSearchParams = result;
        },
        getListUploadSearchParams: function () {
            return listUploadSearchParams;
        },
        clearListUploadSearchParams: function () {
            listUploadSearchParams = [];
        },

        setListUploadTransData : function(result){
            listUploadTransactionDetails = result;
        },
        getListUploadTransData : function(){
            return listUploadTransactionDetails;
        },
        clearListUploadTransData: function () {
            listUploadTransactionDetails = {};
        },
        //storeUserPermissions
        setUserPermisions:function(permissions){
            userPermissions = permissions;
        },
        getUserPermissions:function(){
            return userPermissions;
        },

        getGridOptions: function (colDef) {

            var gridOptions = {
                enableRowSelection: false,
                enableRowHeaderSelection: false,
                enableFiltering: false,
                enableSelectAll: false,

                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                //rowTemplate: rowtpl,
                paginationPageSize: 50,
                paginationPageSizes: [10 ,15, 20, 25, 30, 35, 40, 45, 50],
                enablePaginationControls: false,
                enableVerticalScrollbar: 0,
                enableHorizontalScrollbar: 0,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: colDef,
                enableColumnResizing: true,
                enableColumnMoving: true

            };
            gridOptions.data = '';
            return gridOptions;
        },
        getSearchResultsColumnDef: function () {
            var transKeyTemplate =
                         '<a class="text-center wordwrap" ng-href="#" ng-click="grid.appScope.listUpload.getTransactionDetails(row)">{{COL_FIELD}}</a>';
            var transGroupTemplate = '<div class="wordwrap">{{row.entity.grp_cd}}:{{row.entity.grp_info}}</div>'
            return [
                {
                    field: 'trans_key', displayName: 'Transaction Key', enableCellEdit: false, width: "*", minWidth: 90, maxWidth: 9000,
                    cellTemplate: transKeyTemplate, cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                        if (grid.columns[0]) {
                            return 'first-col-style';
                        }
                    }
                },
                { field: 'trans_stat', displayName: 'Transaction Status', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                { field: 'grp_cd', displayName: 'Group', cellTemplate: transGroupTemplate, width: "*", minWidth: 200, maxWidth: 9000 },
                { field: 'user_id', displayName: 'User Name', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 150, maxWidth: 9000 },
                { field: 'upld_typ', displayName: 'Upload Type', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>' },
                { field: 'trans_create_ts', displayName: 'Create Timestamp', cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 200, maxWidth: 9000 },

            ]
        },
        getTransColumnDef: function ($scope) {
            var masterIdTemplate =
                         '<a class="text-center wordwrap" ng-href="#" ng-show="grid.appScope.userPermisssions" ng-click="$event.preventDefault();grid.appScope.listUpload.getConstituentDetails(row)">{{COL_FIELD}}</a> <span class="text-center wordwrap" ng-hide="grid.appScope.userPermisssions"  ng-show="grid.appScope.userPermisssions" >{{COL_FIELD}}</span>';
          

            var GRID_HEADER_TEMPLATE = '<div ng-class="{ \'sortable\': sortable }" tooltip-placement="top-left" tooltip-append-to-body="true" uib-tooltip="{{col.displayName}}">' +
                                '<div class="ui-grid-cell-contents " col-index="renderIndex" title="TOOLTIP">' +
                                    '<span class="ui-grid-header-cell-label headerWrap-listUpload">{{ col.displayName CUSTOM_FILTERS }}</span>' +
                                    '<span ui-grid-visible="col.sort.direction" ng-class="{ \'ui-grid-icon-up-dir\': col.sort.direction == asc, \'ui-grid-icon-down-dir\': col.sort.direction == desc, \'ui-grid-icon-blank\': !col.sort.direction }">&nbsp</span>' +
                                '</div>' +
                                '<div class="ui-grid-column-menu-button" ng-if="grid.options.enableColumnMenus && !col.isRowHeader  && col.colDef.enableColumnMenu !== false" ng-click="toggleMenu($event)" ng-class="{\'ui-grid-column-menu-button-last-col\': isLastCol}">' +
                                    '<i class="ui-grid-icon-angle-down">&nbsp;</i>' +
                                '</div>' +
                                '<div ui-grid-filter></div>' +
                            '</div>';
            var GRID_FILTER_TEMPLATE = '<div >' +
                                    '<div class="ui-grid-cell-contents ">' +
                                        '&nbsp;User Actions' +
                                    '</div>' +
                                    '<div ng-click="grid.appScope.listUpload.toggleFiltering(grid)" ng-if="grid.options.enableFiltering"><span class="" style="cursor:pointer;font-size:1.5em;margin-left:1%;">x</span>  </div>' +
                                '</div>';
            return [

            {
                field: 'User Actions', cellTemplate: 'App/Upload/Views/common/gridButton.tpl.html',visible:$scope.userPermisssions,
                displayName: '', headerCellTemplate: GRID_FILTER_TEMPLATE, width: "*", minWidth: 75, maxWidth: 150
            },
          
             {
                 field: 'name', width: "*", displayName: "Name", minWidth: 120, maxWidth: 900, headerCellTemplate: GRID_HEADER_TEMPLATE,
                 menuItems: this.customGridFilter($scope),
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },
                 cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
             },
             
             
              {
                  field: 'addr_line_1', width: "*", displayName: "Address", minWidth: 340, maxWidth: 9000, headerCellTemplate: GRID_HEADER_TEMPLATE,
                  menuItems: this.customGridFilter($scope),
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{row.entity.addr_line_1}} {{row.entity.addr_line_2}} {{row.entity.city}} {{row.entity.state_cd}} {{row.entity.zip}}</div>'
              },
               {
                   field: 'email_address', width: "*", displayName: "Email Address", minWidth: 120, maxWidth: 900, headerCellTemplate: GRID_HEADER_TEMPLATE,
                   menuItems: this.customGridFilter($scope),
                   filter: {
                       condition: uiGridConstants.filter.STARTS_WITH
                   },
                   cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
               },
             
              {
                  field: 'phone_number', width: "*", displayName: "Phone Number", minWidth: 120, maxWidth: 200, headerCellTemplate: GRID_HEADER_TEMPLATE,
                  menuItems: this.customGridFilter($scope),
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
              },
              {
                  field: 'trans_key', width: "*", displayName: "Transaction Key", minWidth: 120, maxWidth: 900, headerCellTemplate: GRID_HEADER_TEMPLATE,
                  menuItems: this.customGridFilter($scope),
                  filter: {
                      condition: uiGridConstants.filter.STARTS_WITH
                  },
                  cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',visible:false
              },
            {
                field: 'constituent_id', width: "*", headerCellTemplate: GRID_HEADER_TEMPLATE ,displayName: "Master ID",  minWidth: 120, maxWidth: 320,cellTemplate:masterIdTemplate,
                cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                    if (grid.columns[0]) {
                        return '';
                    }
                },
                menuItems: this.customGridFilter($scope),
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }
                
            }
                
            ]
        },

        getTransactionDetails: function (postTransactionSearchParams) {
            return $http.get(BasePath + "TransactionNative/gettransemailupload/"+ postTransactionSearchParams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).then(function (result) {
                //  console.log(result);
                return result.data;
            }, function (error) {
                //if (error == TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT) {
                //    //messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE, "Error: Timed Out");
                //}
                //else if (error == TRANSACTION_CRUD_CONSTANTS.DB_ERROR) {
                //    //messageTransPopup($rootScope, TRANSACTION_CRUD_CONSTANTS.DB_ERROR_MESSAGE, "Error: Timed Out");
                //}
                return $q.reject(error);
            });
        },
        getListUploadSearchExportData: function (postParams) {
            return $http.post(BasePath + "UploadNative/exportListUploadSearch/", postParams, {
                headers: {
                    "Content-type": 'application/json'

                },
                "responseType": "arraybuffer",
            }).success(function (data) {
                if (data.byteLength > 0) {
                    var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                    //  var blob = new Blob([data], { type: type });
                    saveAs(blob, "ListUploadSearchResults.xlsx");
                }
                else {
                    angular.element("#iErrorModal").modal('hide');
                    angular.element("#accessDeniedModal").modal();
                }
            }).error(function (result) {

                console.log(result);
                //var decodedString = String.fromCharCode.apply(null, new Uint8Array(result));
              //  var obj = JSON.parse(decodedString);
                ErrorService.exportMessagePopup(result);
               // return obj;
            })
        },
        getListUploadTransExportData: function (id) {
            return $http.get(BasePath + "UploadNative/getTransExportAllUpload/" + id, {
                headers: {
                    "Content-type": 'application/json'

                },
                "responseType": "arraybuffer",
            }).success(function (data) {
                if (data.byteLength > 0) {
                    var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                    //  var blob = new Blob([data], { type: type });
                    saveAs(blob, "ListUploadTransData.xlsx");
                }
                else {
                    angular.element("#iErrorModal").modal('hide');
                    angular.element("#accessDeniedModal").modal();
                }
            }).error(function (result) {

                console.log(result);
                //var decodedString = String.fromCharCode.apply(null, new Uint8Array(result));
                //  var obj = JSON.parse(decodedString);
                ErrorService.exportMessagePopup(result);
                // return obj;
            })
        },
       customGridFilter:function(scope) {
            return [
                {
                    title: 'Filter',
                    icon: 'ui-grid-icon-filter',
                action: function ($event) {
                    //this method is declared in ConstMultiDetailsController
                    // if (!angular.isUndefined(type))
                    scope.listUpload.toggleFiltering();
                    // else
                    //  $rootScope.toggleHomeFilter();
                },

            }];
        }

    }
}]);

angular.module('upload').filter("column_date_filter", ['$filter', function ($filter) {
    return function (value) {
        if (value != null)
            return $filter('date')(new Date(value), 'MM/dd/yyyy h:MM:ss a');
        else
            return null;
    }
}]);

angular.module('upload').constant('UPLOAD_CONSTANTS', {
    'ACCESS_DENIED_MESSAGE': 'Logged in user does not have appropriate permission.',
    'ACCESS_DENIED_CONFIRM': 'Access Denied!',
    'ACCESS_DENIED': 'LoginDenied',
    'SERVICE_TIMEOUT_HEADER':'Error:Timed Out',
    'SERVICE_TIMEOUT': 'Timed out',
    'SERVICE_TIMEOUT_CONFIRM': 'Error: Timed Out',
    'SERVICE_TIMEOUT_MESSAGE': 'The service/database timed out. Please try again after some time.',
    'DB_ERROR': "Database Error",
    'DB_ERROR_HEADER': "Error: Database Error",
    'DB_ERROR_MESSAGE': "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",
    'EMPTY_RESULTS_HEADER': "No Results",
    'EMPTY_RESUTLS_MESSAGE':"No Results Found! There are records associated with search parameters"

});

angular.module('upload').factory('ErrorService', ['$rootScope', 'UPLOAD_CONSTANTS', function ($rootScope, UPLOAD_CONSTANTS) {
    return {
        messagePopup: function (error) {
            var message = "";
            var header = "";
            if (error.data == UPLOAD_CONSTANTS.SERVICE_TIMEOUT) {
                message = UPLOAD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE;
                header = UPLOAD_CONSTANTS.SERVICE_TIMEOUT_HEADER;
            }
            else if (error.data == UPLOAD_CONSTANTS.DB_ERROR) {
                message = UPLOAD_CONSTANTS.DB_ERROR_MESSAGE;
                header = UPLOAD_CONSTANTS.DB_ERROR_HEADER;
           
            }
            else if (error.data == UPLOAD_CONSTANTS.ACCESS_DENIED) {
             
                message = UPLOAD_CONSTANTS.ACCESS_DENIED_MESSAGE;
                header = UPLOAD_CONSTANTS.ACCESS_DENIED_CONFIRM;
            }
            else {
                message = "No Results found!";
                header = "No Results";
            }


        $rootScope.message = "";
        $rootScope.header = "";
        $rootScope.message = message;
        $rootScope.header = header;
        angular.element("#iErrorModal").modal();
    
        },
        exportMessagePopup: function (obj) {
            if (obj.ArrayBuffer == UPLOAD_CONSTANTS.ACCESS_DENIED) {
                message = UPLOAD_CONSTANTS.ACCESS_DENIED_MESSAGE;
                header = UPLOAD_CONSTANTS.ACCESS_DENIED_CONFIRM;
            }
            else if (obj.ArrayBuffer == UPLOAD_CONSTANTS.SERVICE_TIMEOUT) {
                message = UPLOAD_CONSTANTS.SERVICE_TIMEOUT_MESSAGE;
                header = UPLOAD_CONSTANTS.SERVICE_TIMEOUT_HEADER;
            }
            else if (obj.ArrayBuffer == UPLOAD_CONSTANTS.DB_ERROR) {
                message = UPLOAD_CONSTANTS.DB_ERROR_MESSAGE;
                header = UPLOAD_CONSTANTS.DB_ERROR_HEADER;

            }
            else  {
                message = "No Results found!";
                header = "No Results";
            }

            $rootScope.message = "";
            $rootScope.header = "";
            $rootScope.message = message;
            $rootScope.header = header;
            angular.element("#iErrorModal").modal();
        }
    }
       
}]);

