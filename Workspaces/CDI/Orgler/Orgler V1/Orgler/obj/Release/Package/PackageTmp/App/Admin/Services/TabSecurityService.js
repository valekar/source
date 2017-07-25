//service for getting http results
adminMod.factory('TabSecurityApiServices', ['TABSECURITY_CONTANTS', '$q', '$http', function (TABSECURITY_CONTANTS, $q, $http) {
    var saveSearchParams = [];
    return {
        insertTabSecurity: function (params) {
            return $http.post(BasePath + TABSECURITY_CONTANTS.REST_INSERT_TAB_SECURITY_URL, params, TABSECURITY_CONTANTS.HTTP_HEADERS).then(function (result) {
                return result.data;
            }, function (error) {
                return $q.reject(error);
            });
        },
        editTabSecurity: function (params) {
            return $http.post(BasePath + TABSECURITY_CONTANTS.REST_EDIT_TAB_SECURITY_URL, params, TABSECURITY_CONTANTS.HTTP_HEADERS).then(function (result) {
                return result.data;
            }, function (error) {
                return $q.reject(error);
            });
        },
        deleteTabSecurity: function (params) {
            return $http.post(BasePath + TABSECURITY_CONTANTS.REST_DELETE_TAB_SECURITY_URL, params, TABSECURITY_CONTANTS.HTTP_HEADERS).then(function (result) {
                return result.data;
            }, function (error) {
                return $q.reject(error);
            });
        },
        getTabSecurityGetResults: function (id) {
            return $http.get(BasePath + TABSECURITY_CONTANTS.REST_GET_TAB_SECURITY_URL + "/" + id, TABSECURITY_CONTANTS.HTTP_HEADERS).then(function (result) {
                console.log(result);
                return result.data;
            }, function (error) {
                return $q.reject(error);
            });
        },
        getUpdatedTabSecurityGetResults: function (id) {
            return $http.get(BasePath + TABSECURITY_CONTANTS.REST_UPDATED_GET_TAB_SECURITY_URL + "/" + id, TABSECURITY_CONTANTS.HTTP_HEADERS).then(function (result) {
                console.log(result);
                return result.data;
            }, function (error) {
                return $q.reject(error);
            });
        },
        getReadAccessValues: function () {
            return [
                           { id: 'N', value: 'No Access' },
                           { id: 'R', value: 'Read' }
               
            ]
        },
        getWriteAccessValues: function () {
            return [
                           { id: 'N', value: 'No Access' },
                           { id: 'R', value: 'Read' },
                           { id: 'RW', value: 'Read Write' }
            ]
        },
        getGroupNames: function () {
            return [
                 { id: '', value: '' },
                { id: 'Orgler', value: 'Orgler' },
                { id: 'Orgler Writer', value: 'Orgler Writer' },
                { id: 'Orgler Admin', value: 'Orgler Admin' }

            ]
        }
    }

}]);


//service for grid
adminMod.factory('TabSecurityGridServices', ['uiGridConstants', '$rootScope', function (uiGridConstants, $rootScope) {
    return {
        //enter your column def here.Remove the below sample and write your own column defs
        getColumnDef: function () {
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

            return [
                    {
                        field: 'usr_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'User Name',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 130, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'grp_nm', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Group Name',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 80, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                    },
                    {
                        field: 'email_address', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Email Address',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 180, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'telephone_number', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Telephone Number',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 120, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'newaccount_tb_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'New Account Tab Access',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'topaccount_tb_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Top Orgs Tab Access',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'enterprise_orgs_tb_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Enterprise Orgs Tab Access',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'constituent_tb_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Constituent Tab Access',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'transaction_tb_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Transaction Tab Access',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },                    
                    {
                        field: 'admin_tb_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Admin Tab Access',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'upload_eosi_tb_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'EO Site Upload Tab Access',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'upload_affil_tb_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Affiliation Upload Tab Access',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'upload_eo_tb_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'EO Upload Tab Access',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'has_merge_unmerge_access', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Merge/Unmerge Access?',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 70, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                    {
                        field: 'is_approver', headerCellTemplate: GRID_HEADER_TEMPLATE, displayName: 'Is Approver?',
                        cellTemplate: '<div class="wordwrap">{{COL_FIELD}}</div>', width: "*", minWidth: 60, maxWidth: 300,
                        filter: {
                            condition: uiGridConstants.filter.STARTS_WITH
                        },
                        menuItems: this.customGridFilter()
                    },
                     {
                         field: 'Action', cellTemplate: 'App/Admin/Views/gridDropDown.tpl.html',  displayName: 'User Action',
                         width: "*", minWidth: 120, maxWidth: 9000, headerCellTemplate: '<div style="margin-top:4.5%;">User Actions</div>'
                     }
            ]
        },

        getGridOptions: function (columnDefs, enableSelection) {
            return {
                enableRowSelection: true,
                enableRowHeaderSelection: enableSelection,
                enableFiltering: false,
                enableSelectAll: false,
                selectionRowHeaderWidth: 35,
                rowHeight: 43,
                paginationPageSize: 50,
                enablePagination: true,
                paginationPageSizes: [10, 15, 20, 25, 30, 35, 40, 45, 50],
                enablePaginationControls: false,
                enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
                enableHorizontalScrollbar: uiGridConstants.scrollbars.ALWAYS,
                enableGridMenu: true,
                showGridFooter: false,
                columnDefs: columnDefs,
                enableColumnResizing: true,
                enableColumnMoving: true,
               
                data: ''
            };
        },
        //don't forget to enter your preferred column def name below
        getGridLayout: function (gridOptions, result) {
            gridOptions.data = result;
            //uncomment below section if you want a dynamic row height increase based on the column values
            /*var max = 0;
            for (var i = 0; i < result.length; i++) {
                if (result[i].'<enter your column name>'.length > max) {
                    max = result[i].source_system_count.length;
                }
            }
            var lengthOfSoruceCount = max;
            if (lengthOfSoruceCount > (gridOptions.rowHeight + lengthOfSoruceCount/3)) {
                gridOptions.rowHeight = lengthOfSoruceCount / 2 + 35;
            }
            angular.element(document.getElementsByClassName('grid')[0]).css('height', '0px');*/
            return gridOptions;
        },
        customGridFilter: function () {
            return [
                {
                    title: 'Filter',
                    icon: 'ui-grid-icon-filter',
                    action: function ($event) {
                        $rootScope.toggleSearchFilter();

                    },

                }];
        }
    }
}]);


adminMod.constant('TABSECURITY_CONTANTS', {
    'HTTP_HEADERS': {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        }
    },
    'REST_POST_URL': '<Please enter your search rest method>',

    'NO_RESULTS': "No Results found!",
    'ONLY_DIGITS_ERROR': "Please enter only numbers for MasterId field",
    'PROVIDE_ATLEAST_ONE': "Please provide at least one input criteria before search",
    'ACCESS_DENIED_MESSAGE': 'Logged in user does not have appropriate permission.',
    'ACCESS_DENIED_CONFIRM': 'Access Denied!',
    'ACCESS_DENIED': 'LoginDenied',
    'SERVICE_TIMEOUT_HEADER': 'Error:Timed Out',
    'SERVICE_TIMEOUT': 'Timed out',
    'SERVICE_TIMEOUT_CONFIRM': 'Error: Timed Out',
    'SERVICE_TIMEOUT_MESSAGE': 'The service/database timed out. Please try again after some time.',
    'DB_ERROR': "Database Error",
    'DB_ERROR_HEADER': "Error: Database Error",
    'DB_ERROR_MESSAGE': "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",
    'INTERNAL_SERVER_ERROR': "Internal Server Error",
    'CACHE_CLEARED_ERROR': "LocalStorage seems just to be cleared out! Please proceed with the regular search",
     
    'TAB_SECURITY_EDIT':'edit',
    'TAB_SECURITY_DELETE': 'delete',
    'TAB_SECURITY_INSERT':'insert',

    // rest URLs for admin tabsecurity 
    'REST_GET_TAB_SECURITY_URL': 'AdminService/GetTabLevelSecurity',
    'REST_EDIT_TAB_SECURITY_URL': 'AdminService/EditTabLevelSecurity',
    'REST_INSERT_TAB_SECURITY_URL': 'AdminService/InsertTabLevelSecurity',
    'REST_DELETE_TAB_SECURITY_URL': 'AdminService/DeleteTabLevelSecurity',
    'REST_UPDATED_GET_TAB_SECURITY_URL': 'AdminService/GetUpdatedTabLevelSecurity',

});

//error service for handling errors
adminMod.factory('ErrorService', ['$rootScope', 'TABSECURITY_CONTANTS', function ($rootScope, TABSECURITY_CONTANTS) {
    return {
        messagePopup: function (error) {
            var message = "";
            var header = "";
            if (error.data == TABSECURITY_CONTANTS.SERVICE_TIMEOUT) {
                message = TABSECURITY_CONTANTS.SERVICE_TIMEOUT_MESSAGE;
                header = TABSECURITY_CONTANTS.SERVICE_TIMEOUT_HEADER;
            }
            else if (error.data == TABSECURITY_CONTANTS.DB_ERROR) {
                message = TABSECURITY_CONTANTS.DB_ERROR_MESSAGE;
                header = TABSECURITY_CONTANTS.DB_ERROR_HEADER;

            }
            else if (error.data == TABSECURITY_CONTANTS.ACCESS_DENIED) {

                message = TABSECURITY_CONTANTS.ACCESS_DENIED_MESSAGE;
                header = TABSECURITY_CONTANTS.ACCESS_DENIED_CONFIRM;
            }
            else {
                $rootScope.header = error.header;
                $rootScope.message = error.message;
            }

            $rootScope.message = "";
            $rootScope.header = "";
            $rootScope.message = message;
            $rootScope.header = header;
            angular.element("#iErrorModal").modal();

        },
        modalMessage: function (result) {
            $rootScope.message = result.message;
            $rootScope.header = result.header;
            angular.element("#iErrorModal").modal();
        }
      
    }

}]);


adminMod.factory('ModalService', ['$rootScope', '$uibModal', 'TABSECURITY_CONTANTS', 'ErrorService', '$q',
    function ($rootScope, $uibModal, TABSECURITY_CONTANTS, ErrorService,$q) {
        
    return {
        openModal: function (row, grid, type) {
            var defer = $q.defer();
            var ctrl, templ = '';

            if (type == TABSECURITY_CONTANTS.TAB_SECURITY_EDIT) {
                templ = BasePath + 'App/Admin/Views/TabSecurityEdit.tpl.html';
                ctrl = 'TabSecurityEditCtrl'
            }
            else if (type == TABSECURITY_CONTANTS.TAB_SECURITY_DELETE) {
                templ = BasePath + 'App/Admin/Views/TabSecurityDelete.tpl.html';
                ctrl = 'TabSecurityDeleteCtrl';
            }
            else if (type == TABSECURITY_CONTANTS.TAB_SECURITY_INSERT) {
                templ = BasePath + 'App/Admin/Views/TabSecurityInsert.tpl.html';
                ctrl = 'TabSecurityInsertCtrl';
            }

            var params = {
                row: row,
                grid: grid

            }
            var ModalInstance = ModalInstance || $uibModal.open({
                // animation: $scope.animationsEnabled,
                templateUrl: templ,
                controller: ctrl,  // Logic in instantiated controller 
                //windowClass: CssClass,
                backdrop: 'static',
                resolve: {
                    params: function () {
                        return params;
                    }
                }
            });

            ModalInstance.result.then(function (result) {
                ErrorService.modalMessage(result);
                defer.resolve(result.header);
                 
            })

            return defer.promise;
        },
      

    }
}]);



