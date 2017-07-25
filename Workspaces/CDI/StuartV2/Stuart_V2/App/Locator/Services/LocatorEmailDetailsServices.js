

angular.module('locator').factory('LocatorEmailMultiGridService', ['uiGridConstants', 'CONSTANTS', '$rootScope', '$window', function (uiGridConstants, CONSTANTS, $rootScope, $window) {

    var gridColumns = new CaseGridColumns();
    var uiScrollNever = uiGridConstants.scrollbars.NEVER;
    var uiScrollAlways = uiGridConstants.scrollbars.ALWAYS;
    //passing rootscope for toggling filters
    var columnDefs = gridColumns.getGridColumns(uiGridConstants, $rootScope);

   return {

        //used in this service only to remove userAction column
        removeUserAction: function (columnDefs) {
            if (this.getTabDenyPermission())
            {
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
       
       
    }
}]);



var LOCATOR_CRUD_CONSTANTS = {
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
    LOCATOREMAILINFO: {
        EDIT: "EditLocator",
        DELETE: "DeleteLocator",
        DELETE_FAILURE_REASON: 'Locator Email cannot be deleted since there are transactions associated with the Email.',
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
