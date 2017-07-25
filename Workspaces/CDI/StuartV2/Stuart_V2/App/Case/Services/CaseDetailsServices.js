
var allGrids = {
    CaseInfo: null,
    CaseDetails: null,
    CaseLocInfo: null,
    CaseNotes: null,
    CaseTransaction: null
}


angular.module('case').factory('CaseMultiGridService', ['uiGridConstants', 'CONSTANTS', '$rootScope', '$window', function (uiGridConstants, CONSTANTS, $rootScope, $window) {

    var gridColumns = new CaseGridColumns();
    var uiScrollNever = uiGridConstants.scrollbars.NEVER;
    var uiScrollAlways = uiGridConstants.scrollbars.ALWAYS;
    //passing rootscope for toggling filters
    var columnDefs = gridColumns.getGridColumns(uiGridConstants, $rootScope);

    var gridOptions = {
        caseInfoGridOption: null,
        caseDetailsGridOption: null,
        caseNotesGridOption: null,
        CaseTransGridOption: null,
        caseLocInfoGridOption: null
    };


    return {

        getGridOptions: function (uiGridConstants, type) {

            switch (type) {
                case CONSTANTS.CASE_INFO: {
                    allGrids.caseInfoGrid = typeof null ? new CaseGrid(columnDefs.caseInfoColDef) : allGrids.caseInfoGrid;
                    gridOptions.caseInfoGridOption = allGrids.caseInfoGrid.getGridOption(uiScrollNever);
                    return gridOptions.caseInfoGridOption;
                    break;
                };
                case CONSTANTS.CASE_DETAILS: {
                    allGrids.caseDetailsGrid = typeof null ? new CaseGrid(columnDefs.caseDetailsColDef) : allGrids.caseDetailsGrid;
                    gridOptions.caseDetailsGridOption = allGrids.caseDetailsGrid.getGridOption(uiScrollNever);
                    return gridOptions.caseDetailsGridOption; break;
                };
                case CONSTANTS.CASE_LOCINFO: {
                    allGrids.caseLocInfoGrid = typeof null ? new CaseGrid(columnDefs.caseLocInfoColDef) : allGrids.caseLocInfoGrid;
                    gridOptions.caseLocInfoGridOption = allGrids.caseLocInfoGrid.getGridOption(uiScrollNever);
                    /*added by srini*/
                    gridOptions.caseLocInfoGridOption.columnDefs = this.removeUserAction(gridOptions.caseLocInfoGridOption.columnDefs);
                    return gridOptions.caseLocInfoGridOption; break;
                };
                case CONSTANTS.CASE_NOTES: {
                    allGrids.caseNotesGrid = typeof null ? new CaseGrid(columnDefs.caseNotesColDef) : allGrids.caseNotesGrid;
                    gridOptions.caseNotesGridOption = allGrids.caseNotesGrid.getGridOption(uiScrollNever);
                    /*added by srini*/
                    gridOptions.caseNotesGridOption.columnDefs = this.removeUserAction(gridOptions.caseNotesGridOption.columnDefs);
                    return gridOptions.caseNotesGridOption; break;
                };
                case CONSTANTS.CASE_TRANSACTION: {
                    allGrids.caseTransGrid = typeof null ? new CaseGrid(columnDefs.caseTransColDef) : allGrids.caseTransGrid;
                    gridOptions.CaseTransGridOption = allGrids.caseTransGrid.getGridOption(uiScrollNever)
                    return gridOptions.CaseTransGridOption; break;
                };
            }
        },

        getMultiGridLayout: function (gridOptions, uiGridConstants, result, type) {
            switch (type) {
                case CONSTANTS.CASE_INFO: {
                    return allGrids.caseInfoGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CASE_DETAILS: {
                    return allGrids.caseDetailsGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CASE_LOCINFO: {
                    return allGrids.caseLocInfoGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CASE_NOTES: {
                    return allGrids.caseNotesGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
                case CONSTANTS.CASE_TRANSACTION: {
                    return allGrids.caseTransGrid.getGridLayout(gridOptions, result, type, uiScrollNever); break;
                };
            }
        },
        /** added by srini **/
        //used in this service only to remove userAction column
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
                if (permissions.case_tb_access == "R") {
                    return true;
                }
            }
            return false;
        },
        /** added by srini **/
        getToggleDetails: function ($scope, visible, type) {


            var toggleContents = {
                CaseInfo: ['toggleCaseInfoDetails', 'toggleCaseInfoHeader', 'toggleCaseInfoLoader', 'liCaseInfo'],
                CaseDetails: ['toggleCaseDetails', 'toggleCaseDetailsHeader', 'toggleCaseDetailsLoader', 'liCaseDetails'],
                CaseLocInfo: ['toggleCaseLocInfoDetails', 'toggleCaseLocInfoHeader', 'toggleCaseLocInfoLoader', 'liCaseLocInfo'],
                CaseNotes: ['toggleCaseNotesDetails', 'toggleCaseNotesHeader', 'toggleCaseNotesLoader', 'liCaseNotes'],
                CaseTransaction: ['toggleCaseTransactionDetails', 'toggleCaseTransDetailsHeader', 'toggleCaseTransDetailsLoader', 'liCaseTransaction']
            };

            var toggle = {
                getToggleType: function (toggleType) {
                    if (visible) {
                        $scope[toggleType[0]] = { "display": "block" };
                        $scope[toggleType[1]] = { "display": "block" };
                        $scope[toggleType[2]] = { "display": "none" };
                        $scope[toggleType[3]] = "";
                    }
                    else {
                        $scope[toggleType[0]] = { "display": "none" };
                        $scope[toggleType[1]] = { "display": "none" };
                        $scope[toggleType[2]] = { "display": "none" };
                        $scope[toggleType[3]] = "";
                    }
                }
            };

            switch (type) {
                case CONSTANTS.CASE_INFO: { return toggle.getToggleType(toggleContents.CaseInfo); break; };
                case CONSTANTS.CASE_DETAILS: { return toggle.getToggleType(toggleContents.CaseDetails); break; };
                case CONSTANTS.CASE_LOCINFO: { return toggle.getToggleType(toggleContents.CaseLocInfo); break; };
                case CONSTANTS.CASE_NOTES: { return toggle.getToggleType(toggleContents.CaseNotes); break; };
                case CONSTANTS.CASE_TRANSACTION: { return toggle.getToggleType(toggleContents.CaseTransaction); break; };
            }
        }
    }
}]);


//Service to Get Details only
angular.module('case').factory('CaseApiService', ['$http', 'CONSTANTS', function ($http, CONSTANTS) {
    var urls = {};
   // urls[CONSTANTS.CASE_INFO] = BasePath + 'CaseNative/getcaseinfo/';
    urls[CONSTANTS.CASE_DETAILS] = BasePath + 'CaseNative/GetCasePreferenceDetails/';
    urls[CONSTANTS.CASE_LOCINFO] = BasePath + 'CaseNative/getCaseLocatorDetails/';
    urls[CONSTANTS.CASE_NOTES] = BasePath + 'CaseNative/GetCaseNotesDetails/';
    urls[CONSTANTS.CASE_TRANSACTION] = BasePath + 'CaseNative/getCaseTransDetails/';
   // console.log("Base path: " + BasePath);
    var getApiData = function (param, type) {
       // console.log("into the case api service");
        var url = urls[type];
       // console.log(param);
       // console.log(type);
        return $http.get(url + param + "", {
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }).success(function (result) {
           // console.log("Case Details");
           // console.log(result);
            if (result == null || result == undefined) {
                return "";
            }
            return result;
        }).error(function (result) {
           // console.log(result);
            return result;
        });
    };


    return {
        getApiDetails: function (param, type) {
            return getApiData(param, type);
        }



    }

}]);



var CASE_CRUD_CONSTANTS = {
    EDIT_SUCCESS_MESSAGE: 'The record was successfully edited',
    DELETE_SUCCESS_MESSAGE: 'The Record was successfully deleted',
    SUCCESS_MESSAGE: 'The record was successfully inserted!',
    FAILURE_MESSAGE: 'The record was not inserted.',
    EDIT_FAILURE_MESSAGE: 'The record was not edited',
    DELETE_FAILURE_MESSAGE: 'The record was not deleted',
    ACCESS_DENIED_MESSAGE: 'Logged in user does not have appropriate permission.',
    SUCCESS_REASON: 'Transaction Key: ',
    FAIULRE_REASON: 'It looks like there is a similar record in the database. Please review.',
    SUCCESS_CONFIRM: 'Success!',
    FAILURE_CONFIRM: 'Failed!',
    ACCESS_DENIED_CONFIRM: 'Access Denied!',
    ACCESS_DENIED: 'LoginDenied',
    SOURCE_SYS: "STRX",
    ROW_CODE: 'I',
    DUPLICATE_MSG: 'Duplicate record found.',
    PROCEDURE: {
        SUCCESS: 'Success',
        DUPLICATE: 'duplicate',
        NOT_PRESENT: 'The original record is not present.'
    },
    CASEINFO: {
        EDIT: "EditCase",
        DELETE: "DeleteCase",
        DELETE_FAILURE_REASON: 'Case cannot be deleted since there are transactions associated with the case.',
    },
    CASELOCINFO: {
        ADD: "AddCaseLocInfo",
        EDIT: "EditCaseLocInfo",
        DELETE: "DeleteCaseLocInfo"
    },
    CASENOTES: {
        ADD: "AddCaseNotes",
        EDIT: "EditCaseNotes",
        DELETE: "DeleteCaseNotes",
        DUPLICATE_NOTE_REASON: "This note already exists for the present case. Provide a different note."
    },
    ACCESS_DENIED_CONFIRM: 'Access Denied!',
    ACCESS_DENIED: 'LoginDenied',
    SERVICE_TIMEOUT: 'Timed out',
    SERVICE_TIMEOUT_CONFIRM: 'Error: Timed Out',
    SERVICE_TIMEOUT_MESSAGE: 'The service/database timed out. Please try again after some time.',
    DB_ERROR: "Database Error",
    DB_ERROR_CONFIRM: "Error: Database Error",
    DB_ERROR_MESSAGE: "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org)."

};
