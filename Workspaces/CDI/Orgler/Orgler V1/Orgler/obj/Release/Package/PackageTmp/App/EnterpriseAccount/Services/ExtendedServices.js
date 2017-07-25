angular.module('enterpriseAccountModule').constant('EXTENDED_FUNC', {

    "ERROR_HEADER": "Failed!",
    "INTERNAL_SERVER_ERROR":"Internal Server Error",
    "PLEASE_PROVIDE": "Please provide the necessary inputs",
    "DB_ERROR": "Database Error",
    "DB_ERROR_HEADER": "Error: Database Error",
    "DB_ERROR_MESSAGE": "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",
    "ACCESS_DENIED_MESSAGE": 'Logged in user does not have appropriate permission.',
    "ACCESS_DENIED_CONFIRM": 'Access Denied!',
    "ACCESS_DENIED": 'LoginDenied',
    "SERVICE_TIMEOUT": 'Timed out',
    "SERVICE_TIMEOUT_CONFIRM": 'Error: Timed Out',
    "SERVICE_TIMEOUT_MESSAGE": 'The service/database timed out. Please try again after some time.',
    'ADD': 'add',
    'EDIT': 'edit',
    'DELETE': 'delete',
    'SHOW_ALL_RECORDS': 'Show All Records',
    'HIDE_RECORDS': 'Hide Inactive',
    "TRANSACTION_SUCCESS_HEADER": "Success!",
    "TRANSACTION_SUCCESS_EDIT_MESSAGE": "The record was successfully edited.",
    "TRANSACTION_SUCCESS_DELETE_MESSAGE": "The record was successfully deleted.",
    "TRANSACTION_SUCCESS_ADD_MESSAGE": "The record was successfully added.",

    "TRANSACTION_DUPLICATE_MESSAGE": "Please choose different parameters",
    "TRANSACTION_DUPLICATE_HEADER": "Duplicate Records",
    "HEADERS": {
        "Content-Type": "application/json",
        "Accept": "application/json"

    },
    'ROW_CODE': 'I',
    'DEFAULT_NOTE': 'Request from Constituent - Data Correction',
    'NOTES': [
        "Request from Constituent - Data Correction", "Request from Constituent - Change of Information", "Request from Chapter - Data Correction",
        "Request from Chapter - Change of Information", "Assessment for master constituent emails"
    ],
    'CHARACTERISTICS': 'characteristics',
    "GET_CHARACTERISTICS_URL": BasePath + "EnterpriseAccountService/GetEnterpriseCharacteristics/",
    "GET_ALL_CHARACTERISTICS_URL": BasePath + "EnterpriseAccountService/GetAllEnterpriseCharacteristics/",
    "ADD_CHARACTERISTICS_URL": BasePath + "EnterpriseAccountService/AddCharacteristics",
    "EDIT_CHARACTERISTICS_URL": BasePath + "EnterpriseAccountService/EditCharacteristics",
    "DELETE_CHARACTERISTICS_URL": BasePath + "EnterpriseAccountService/DeleteCharacteristics",


    "ADD_CHARACTERISTICS": "add_characteristics",
    "EDIT_CHARACTERISTICS": "edit_characteristics",
    "DELETE_CHARACTERISTICS": "delete_characteristics",
    

});


angular.module('enterpriseAccountModule').factory('ExtendedStoreData', [function () {
    var ExtendedStoreData = {};
    // this is for Characteristics
    /*Start of Characteristics */
    ExtendedStoreData.CharacteristicsList = [];
    ExtendedStoreData.CharacteristicsFullList = [];

    ExtendedStoreData.addCharacteristicsList = function (fullList, list) {
        ExtendedStoreData.CharacteristicsList = list;
        ExtendedStoreData.CharacteristicsFullList = fullList;
    };

    ExtendedStoreData.getCharacteristicsList = function () {
        return ExtendedStoreData.CharacteristicsList;
    };

    ExtendedStoreData.getCharacteristicsFullList = function () {
        return ExtendedStoreData.CharacteristicsFullList;
    };

    /* End of Characteristics code*/

   

    ExtendedStoreData.cleanData = function () {
        ExtendedStoreData.CharacteristicsList = [];
        ExtendedStoreData.CharacteristicsFullList = [];
       
    }



    return ExtendedStoreData;

}]);


angular.module('enterpriseAccountModule').factory('ExtendedServices', ['$window', function ($window) {
    return {

        getGridOptions: function (colDef) {
            var transaction = "Transaction";
            var softDeletedMessage = "Inactive/Soft-Deleted records cannot be edited.";
            var smallSoftDeleted = "deleted";
            var rowtpl = '<div ng-class="{\'greyClass\':(row.entity.transNotes.length > \'0\' && (row.entity.transNotes == \'' + softDeletedMessage + '\' || row.entity.transNotes.indexOf(\'' + smallSoftDeleted + '\')>0)  ) || (row.entity.row_stat_cd && row.entity.row_stat_cd ==\'L\'),maroonClass:row.entity.transNotes.length > \'0\' && (row.entity.transNotes.indexOf(\'' + transaction + '\')==0) }"><div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }" ui-grid-cell></div></div>';
            var new_col = this.removeUserAction(colDef);
            //var page_size = angular.isUndefined(pagesize) ? 10 : pagesize;
            var options = function () {
                return {
                    enableRowSelection: false,
                    enableRowHeaderSelection: false,
                    enableFiltering: false,
                    enableSelectAll: false,
                    selectionRowHeaderWidth: 35,
                    rowHeight: 43,
                    rowTemplate: rowtpl,
                    paginationPageSize: 10,
                    paginationPageSizes: [5, 10, 15, 20, 25, 30, 35, 40, 45, 50],
                    enablePaginationControls: false,
                    enableVerticalScrollbar: 0,
                    enableHorizontalScrollbar: 1,
                    enableGridMenu: true,
                    showGridFooter: false,
                    columnDefs: new_col,
                    enableColumnResizing: true,
                    enableColumnMoving: true,
                    data: ""
                }
            }

            return options();
        },
        refreshGridData: function (gridOptions, res) {
            gridOptions.data = '';
            gridOptions.data.length = 0;
            gridOptions.data = res;
            //gridOptions.rowTemplate = rowtpl;
            return gridOptions;
        },
        removeUserAction: function (columnDefs) {
            if (this.getTabDenyPermission()) {
                columnDefs.splice(columnDefs.length - 1, 1);
            }

            return columnDefs;
        },
        getUserAllPermission: function () {
            if (!angular.isUndefined($window.localStorage['Main-userPermissions'])) {
                if (typeof $window.localStorage['Main-userPermissions'] == 'string')
                    return JSON.parse($window.localStorage['Main-userPermissions']);
                else
                    return JSON.parse($window.localStorage['Main-userPermissions']);
                //console.log(permissions);               

            }
            return null;
        },

        getTabDenyPermission: function () {
            var permissions = this.getUserAllPermission();
            if (permissions != null) {
                if (permissions.constituent_tb_access == "R") {
                    return true;
                }
            }
            return false;
        },
    }
}]);

angular.module('enterpriseAccountModule').factory('CustomHttp', ['$http', 'EXTENDED_FUNC', "$templateCache", function ($http, EXTENDED_FUNC, $templateCache) {
    return {
        httpGet: function (url, enableCache) {
            if (enableCache) {
                return $http({ method: 'GET', url: url, headers: EXTENDED_FUNC.HEADERS, cache: $templateCache });
            } else {
                return $http({ method: 'GET', url: url, headers: EXTENDED_FUNC.HEADERS });
            }
        },
        httpPost: function (url, enableCache, postParams) {
            if (enableCache) {
                return $http({ method: 'POST', url: url, headers: EXTENDED_FUNC.HEADERS, data: postParams, cache: $templateCache });
            } else {
                return $http({ method: 'POST', url: url, headers: EXTENDED_FUNC.HEADERS, data: postParams });
            }
        }
    }


}]);

angular.module('enterpriseAccountModule').factory('ExtendedHttpServices', ['EXTENDED_FUNC', 'CustomHttp', '$q', function (EXTENDED_FUNC, CustomHttp, $q) {

    return {
        getCharacteristics: function (id, enableCache) {
            var url = EXTENDED_FUNC.GET_CHARACTERISTICS_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        getAllCharacteristics: function (id, enableCache) {
            var url = EXTENDED_FUNC.GET_ALL_CHARACTERISTICS_URL + "" + id;
            return CustomHttp.httpGet(url, enableCache).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        },
        postAddChracteristics: function (postParams, enableCache) {
            var url = EXTENDED_FUNC.ADD_CHARACTERISTICS_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error);});
        },
        postEditChracteristics: function (postParams, enableCache) {
            var url = EXTENDED_FUNC.EDIT_CHARACTERISTICS_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error);});
        },
        postDeleteChracteristics: function (postParams, enableCache) {
            var url = EXTENDED_FUNC.DELETE_CHARACTERISTICS_URL;
            return CustomHttp.httpPost(url, enableCache, postParams).then(function (result) { return result.data; }, function (error) { return $q.reject(error); });
        }
    }
}]);


angular.module('enterpriseAccountModule').factory('CharacteristicsModal', ['EXTENDED_FUNC', 'ModalService', "$q", function (EXTENDED_FUNC, ModalService, $q) {
    return {

        getModal: function (type, results) {
            var deferred = $q.defer();
            if (type == EXTENDED_FUNC.ADD_CHARACTERISTICS) {
                var templUrl = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Characteristics/Add.tpl.html";
                var ctrl = "AddCharacteristicsCtrl";
                var size = 'sm';
            }
            else if (type == EXTENDED_FUNC.EDIT_CHARACTERISTICS) {
                var templUrl = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Characteristics/Edit.tpl.html";
                var ctrl = "EditCharacteristicsCtrl";
                var size = 'sm';
            }
            else if (type == EXTENDED_FUNC.DELETE_CHARACTERISTICS) {
                var templUrl = BasePath + "App/EnterpriseAccount/Views/DetailSectionCRUDTemplates/Characteristics/Delete.tpl.html";
                var ctrl = "DeleteCharacteristicsCtrl";
                var size = 'sm';
            }

            ModalService.OpenModal(templUrl, ctrl, size, results).then(function (response) {
                deferred.resolve(response);
            });

            return deferred.promise;
        }

    }
}]);

angular.module('enterpriseAccountModule').factory('ConstUtilServices', ['$filter', function ($filter) {
    return {
        getStartDate: function () {
            return $filter('date')(new Date(), 'MM/dd/yyyy');
        }

    }


}]);

angular.module('enterpriseAccountModule').factory('dropDownService', ['$http', 'EXTENDED_FUNC', '$q', function ($http, EXTENDED_FUNC, $q) {

    var URL = {};
    URL[EXTENDED_FUNC.CHARACTERISTICS] = "Home/getCharacteristicsType";
    
    return {
        getDropDown: function (type) {
            return $http.get(URL[type], {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).then(function (result) {

                return result.data;
            }, function (error) { return $q.reject(error) });
        }
    }

}]);


angular.module('enterpriseAccountModule').factory('ModalService', ['$uibModal', '$q', function ($uibModal, $q) {
    return {
        OpenModal: function (templ, ctrl, size, params) {
            var deferred = $q.defer();
            var CssClass = '';
            if (size != 'sm') {
                CssClass = 'app-modal-window';
            }

            var ModalInstance = ModalInstance || $uibModal.open({
                animation: true,
                templateUrl: templ,
                controller: ctrl,  // Logic in instantiated controller 
                windowClass: CssClass,
                backdrop: 'static', /*  this prevent user interaction with the background  */
                keyboard: false,
                resolve: {
                    params: function () {
                        return params;
                    }
                }
            });

            ModalInstance.result.then(function (result) {
                deferred.resolve(result);
            });


            return deferred.promise;
        }
    }

}]);



angular.module('enterpriseAccountModule').factory('ShowDetailsModal', ['ModalService', "$q", function (ModalService, $q) {
    return function (results) {
        var deferred = $q.defer();

        var templUrl = BasePath + "App/EnterpriseAccount/Views/common/ShowDetails.tpl.html";
        var ctrl = "ShowExtendedCtrl";
        var size = 'lg';


        ModalService.OpenModal(templUrl, ctrl, size, results).then(function (response) {
            deferred.resolve(response);
        });

        return deferred.promise;
    }


}]);

angular.module('enterpriseAccountModule').factory('ExtendedColumnServices', ["$q", "EXTENDED_FUNC", "uiGridConstants", "$rootScope", "StoreData",
    function ($q, EXTENDED_FUNC, uiGridConstants, $rootScope, StoreData) {
        var GRID_HEADER_TEMPLATE = '<div ng-class="{ \'sortable\': sortable }" tooltip-placement="top-left" tooltip-append-to-body="true" uib-tooltip="{{col.displayName}}">' +
                                '<div class="ui-grid-cell-contents " col-index="renderIndex" title="TOOLTIP">' +
                                    '<span class="ui-grid-header-cell-label ">{{ col.displayName CUSTOM_FILTERS }}</span>' +
                                    '<span ui-grid-visible="col.sort.direction" ng-class="{ \'ui-grid-icon-up-dir\': col.sort.direction == asc, \'ui-grid-icon-down-dir\': col.sort.direction == desc, \'ui-grid-icon-blank\': !col.sort.direction }">&nbsp</span>' +
                                '</div>' +
                                '<div class="ui-grid-column-menu-button" ng-if="grid.options.enableColumnMenus && !col.isRowHeader  && col.colDef.enableColumnMenu !== false" ng-click="toggleMenu($event)" ng-class="{\'ui-grid-column-menu-button-last-col\': isLastCol}">' +
                                    '<i class="ui-grid-icon-angle-down">&nbsp;</i>' +
                                '</div>' +
                                '<div ui-grid-filter></div>' +
                            '</div>';
        var GRID_FILTER_TEMPLATE = '<div >' +
                              '<div class="ui-grid-cell-contents ">' +
                                  '&nbsp;' +
                              '</div>' +
                              '<div ng-click="grid.appScope.toggleFiltering(grid)" ng-if="grid.options.enableFiltering"><span class="glyphicon glyphicon-remove-circle" style="font-size:1.5em;margin-left:1%;"></span> </div>' +
                          '</div>';
        
        return {
            getCharacteristicsColumns: function () {
                return [{ field: 'dummy', displayName: 'Icon', minWidth: 40, maxWidth: 40, cellTemplate: 'App/EnterpriseAccount/Views/common/iconGrid.tpl.html' },
              //  { field: 'cnst_mstr_id', displayName: 'Master Id', enableCellEdit: false },
                {
                    field: 'cnst_chrctrstc_typ_cd', menuItems: this.customGridFilter(EXTENDED_FUNC.CHARACTERISTICS),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Characteristic Type Code', width: "*", minWidth: 150, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
                {
                    field: 'cnst_chrctrstc_val', menuItems: this.customGridFilter(EXTENDED_FUNC.CHARACTERISTICS),
                    headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Charateristic Value', width: "*", minWidth: 120, maxWidth: 9000,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },
                    cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>'
                },
               

                {
                    field: 'cnst_chrctrstc_strt_dt', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                    cellFilter: 'column_date_filter'

                },
                {
                    field: 'cnst_chrctrstc_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                    displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: true,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                    cellFilter: 'column_date_filter'

                },
                {
                    field: 'row_stat_cd', width: "*", minWidth: 60, maxWidth: 9000,
                    displayName: "Row Code", headerCellTemplate: GRID_HEADER_TEMPLATE, visible: false,
                    filter: {
                        condition: uiGridConstants.filter.STARTS_WITH
                    },

                },

                {
                    field: 'Action', cellTemplate: 'App/EnterpriseAccount/Views/common/gridDropDown.tpl.html', displayName: 'User Actions', headerCellTemplate: '<div>User Actions</div>',
                    width: "*", minWidth: 325, maxWidth: 9000//, headerCellTemplate: GRID_FILTER_TEMPLATE
                }];
            },
            getAllCharacteristicsColumns: function () {
                return [
             {
                 field: 'cnst_mstr_id', width: "*", minWidth: 100, maxWidth: 9000,
                 displayName: "Master Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
             
            {
                field: 'cnst_chrctrstc_seq', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Case Seq", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_chrctrstc_typ_cd', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Type Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_chrctrstc_val', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "Value", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'cnst_chrctrstc_strt_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Start Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },
            {
                field: 'cnst_chrctrstc_end_dt', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "End Date", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

                cellFilter: 'column_date_filter'

            },
             {
                 field: 'appl_src_cd', width: "*", minWidth: 100, maxWidth: 9000,
                 displayName: "Appl Source Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                 filter: {
                     condition: uiGridConstants.filter.STARTS_WITH
                 },

             },
            {
                field: 'dw_srcsys_trans_ts', width: "*", minWidth: 180, maxWidth: 9000,
                displayName: "Dw Timestamp", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                }, cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>',

                cellFilter: 'column_date_timestamp_filter'
            },
           
            {
                field: 'load_id', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Load Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'row_stat_cd', width: "*", minWidth: 140, maxWidth: 9000,
                displayName: "Row Stat Code", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'trans_key', width: "*", minWidth: 100, maxWidth: 9000,
                displayName: "Trans Key", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },

            },
            {
                field: 'user_id', width: "*", minWidth: 120, maxWidth: 9000,
                displayName: "User Id", headerCellTemplate: GRID_HEADER_TEMPLATE,
                filter: {
                    condition: uiGridConstants.filter.STARTS_WITH
                },visible:false

            }
               
                ]
            },
            customGridFilter: function (type) {
                return [
                    {
                        title: 'Filter',
                        icon: 'ui-grid-icon-filter',
                        action: function ($event) {
                            // called in their respective controllers, not the efficient way of doing but it does the job
                            if (type == EXTENDED_CONSTANTS.CHARACTERISTICS) {
                                $rootScope.toggleCharacteristicsSearchFilter();
                            }

                        },

                    }];
            },
        }


    }]);




angular.module('enterpriseAccountModule').factory('HandleResults', ['Message', 'EXTENDED_FUNC', function (Message, EXTENDED_FUNC) {

    var func = function (result, type) {

        if (result == EXTENDED_FUNC.PLEASE_PROVIDE) {
            Message.open(EXTENDED_FUNC.ERROR_HEADER, result);
        }
            // because below three are coming from tab level security
        else if (result.data == EXTENDED_FUNC.ACCESS_DENIED) {
            Message.open(EXTENDED_FUNC.ACCESS_DENIED_CONFIRM, EXTENDED_FUNC.ACCESS_DENIED_MESSAGE);
        }
        else if (result.data == EXTENDED_FUNC.SERVICE_TIMEOUT) {
            Message.open(EXTENDED_FUNC.SERVICE_TIMEOUT_CONFIRM,EXTENDED_FUNC.SERVICE_TIMEOUT_MESSAGE);
        }
        else if (result.data == EXTENDED_FUNC.DB_ERROR) {
            Message.open( EXTENDED_FUNC.SERVICE_TIMEOUT_CONFIRM,EXTENDED_FUNC.DB_ERROR_MESSAGE);

        }
        else if (result.statusText == EXTENDED_FUNC.INTERNAL_SERVER_ERROR) {
            //console.log(error);
            Message.open(EXTENDED_FUNC.DB_ERROR_HEADER, EXTENDED_FUNC.DB_ERROR_MESSAGE);
        }
        else if (!angular.isUndefined(result[0].o_outputMessage)) {
            if (result[0].o_transaction_key == null) {
                Message.open(EXTENDED_FUNC.TRANSACTION_DUPLICATE_HEADER, EXTENDED_FUNC.TRANSACTION_DUPLICATE_MESSAGE, result[0].o_transaction_key);
                return false;
            }
            else {
                if (type == EXTENDED_FUNC.ADD) {
                    message = EXTENDED_FUNC.TRANSACTION_SUCCESS_ADD_MESSAGE;
                } else if (type == EXTENDED_FUNC.EDIT) {
                    message = EXTENDED_FUNC.TRANSACTION_SUCCESS_EDIT_MESSAGE;
                } else if (type == EXTENDED_FUNC.DELETE) {
                    message = EXTENDED_FUNC.TRANSACTION_SUCCESS_DELETE_MESSAGE;
                }

                Message.open(EXTENDED_FUNC.TRANSACTION_SUCCESS_HEADER, message, result[0].o_transaction_key);
                return true;
            }
            //return true;
        }


        return false;
    }


    return func;

}]);


angular.module('common').directive('scrolldown', [function () {
    return {
        link: function (scope, element, attr) {
            scope.$watch('isDisplayed', function (newValue, oldValue) {
                var viewport = element.find('.ui-grid-render-container');

                ['touchstart', 'touchmove', 'touchend', 'keydown', 'wheel', 'mousewheel', 'DomMouseScroll', 'MozMousePixelScroll'].forEach(function (eventName) {
                    viewport.unbind(eventName);
                });
            });
        },
    };
}]);

