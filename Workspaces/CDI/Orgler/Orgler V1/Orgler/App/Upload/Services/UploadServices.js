/************************* Constants *************************/
var GEN_CONSTANTS = {
    ACCESS_DENIED: 'LoginDenied',
    ACCESS_DENIED_CONFIRM: 'Error: Access Denied!',
    ACCESS_DENIED_MESSAGE: 'Logged in user does not have appropriate permission.',
    DB_ERROR: "Database Error",
    DB_ERROR_CONFIRM: "Error: Database Error",
    DB_ERROR_MESSAGE: "A database error occurred. Please try again later and if it persists, contact the Stuart Administrator (StuartAdmin@redcross.org).",
    TIMEOUT_ERROR: "Timed out",
    TIMEOUT_ERROR_CONFIRM: "Error: Timed Out",
    TIMEOUT_ERROR_MESSAGE: "The service/database timed out. Please try again after some time."
};

var SEARCH_CONSTANTS = {
    MESSAGE_TEXT: {
        INVALID_SEARCH: 'Please provide atleast one of the search criteria inputs!',
        NO_SEARCH_RESULTS: 'No Results Found!',
        TECHNICAL_ERROR: 'Due to high usage at this time, we are experiencing a delayed response. For a better experience, please try back in an hour.'
    },
    MESSAGE_TEXT_COLOUR: {
        INVALID_SEARCH: 'Red',
        NO_SEARCH_RESULTS: 'Red',
        TECHNICAL_ERROR: 'Red'
    }
};

var ACTION_CONSTANTS = {
    MESSAGE_TEXT: {
        SUCCESS_MSG: 'Transaction was processed successfully. Transaction ID is ',
        FAILURE_MSG: 'An unexpected error occurred while processing your request. Please try again in an hour.'
    }
};

var UPLD_CONSTANTS = {
    NO_FILE: 'No file',
    AFF_UPLD_FAIL_HEADER: "Affiliation File Upload Failed",
    NO_FILE_UPLD_MESSAGE: "No file has been uploaded.",
    ICON_ERROR: "Images/icon-error.png",
    INVALID_FORMAT: 'Invalid format',
    INCORRECT_TEMPLATE: "Incorrect template uploaded. Please review and re-upload.",
    SIMILAR_FILE: "Similar file already has been uploaded. Please review and re-upload.",
    NO_DATE_FILE: "The uploaded file contains no data. Please update the values and re-upload.",
    FILE_DUPLICATE: 'File duplicates',
    NOT_VALID: 'Not even one valid',
    EOSI_FAIL_HEADER: "EOSI File Upload Failed",
    EO_FAIL_HEADER: "EO File Upload Failed"
}


BasePath = $("base").first().attr("href");

upldModule.factory('uploadServices', ['$http',
	function ($http) {

		return {
			uploadAffiliationFile: function (formdata) {
				return $http.post(BasePath + 'UploadService/UploadAffiliationFiles/', formdata, {
					headers: {
						"Content-Type": undefined,
						"Accept": "application/json"
					}
				})
				.success(function (result) {
					return result;
				});
			},
			submitAffiliationFile: function (uploadData) {
			    return $http.post(BasePath + 'UploadService/SubmitAffiliationFiles/', uploadData, {
			        headers:
                        {
                            "Content-Type": undefined, //"application/json",
                            "Accept": "application/json"
                        }
			    })
				.success(function (result) {
				    return result;
				});
			},
			uploadEosiFile: function (formdata) {
			    return $http.post(BasePath + 'UploadService/UploadEosiFiles/', formdata, {
			        headers: {
			            "Content-Type": undefined,
			            "Accept": "application/json"
			        }
			    })
				.success(function (result) {
				    return result;
				});
			},
			submitEosiFile: function (uploadData) {
			    return $http.post(BasePath + 'UploadService/SubmitEosiFiles/', uploadData, {
			        headers:
                        {
                            "Content-Type": undefined,//"application/json",
                            "Accept": "application/json"
                        }
			    })
				.success(function (result) {
				    return result;
				});
			},
			uploadEoFile: function (formdata) {
			    return $http.post(BasePath + 'UploadService/UploadEoFiles/', formdata, {
			        headers: {
			            "Content-Type": undefined,
			            "Accept": "application/json"
			        }
			    })
				.success(function (result) {
				    return result;
				});
			},
			submitEoFile: function (uploadData) {
			    return $http.post(BasePath + 'UploadService/SubmitEoFiles/', uploadData, {
			        headers:
                        {
                            "Content-Type": undefined,//"application/json",
                            "Accept": "application/json"
                        }
			    })
				.success(function (result) {
				    return result;
				});
			}
		}

}]);