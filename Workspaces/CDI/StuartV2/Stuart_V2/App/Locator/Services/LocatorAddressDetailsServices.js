



//Service to Get Details only
angular.module('locator').factory('LocatorAddressApiServices', ['$http', 'CONSTANTS', function ($http, CONSTANTS) {
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
    LocatorAddressINFO: {
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
