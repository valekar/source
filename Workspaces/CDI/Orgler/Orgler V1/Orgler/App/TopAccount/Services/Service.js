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

/************************* Services - Data *************************/
topAccMod.factory("TopAccountService", ['$http',
    function($http)
    {
        //base Path
        var BasePath = $("base").first().attr("href");

        return {
            //Service method to perform search
            getTopAccountSearchResults: function(postData)
            {
                return $http.post(BasePath + "TopAccountService/Search", postData, {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function(result){
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },
            //method to perform NAICS Code Updates
            submitNAICSCodeUpdates: function (postData)
            {
                return $http.post(BasePath + "TopAccountService/SubmitNAICSCodeUpdates", postData, {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },
            //method to perform NAICS Code Edits
            submitEditNAICSCodes: function (postData) {
                return $http.post(BasePath + "TopAccountService/EditNAICSCode", postData, {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },
            //method to confirm account details
            confirmAccount: function (postData) {
                return $http.post(BasePath + "TopAccountService/ConfirmAccount", postData, {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },
            //method to get master NAICS details
            getMasterNAICSDetails: function (postData) {
                return $http.post(BasePath + "TopAccountService/getMasterNAICSDetails", postData, {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },
            //method to get Potential Merges Details
            getPotentialMergesDetails: function (postData) {
                return $http.post(BasePath + "TopAccountService/getPotentialMergesDetails", postData, {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },            
            //Method to download NAICS Suggestions 
            getDownloadNAICSSuggestionsData: function (postData,expType) {
                return $http.post(BasePath + "TopAccountService/DownloadNAICSSuggestionsExcel/", postData, {
                    headers: {
                        "Content-type": 'application/json'

                    },
                    "responseType": "arraybuffer",
                }).success(function (data) {
                    if (data.byteLength > 0) {
                        var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });                      
                        var today = new Date();
                        var dd = today.getDate();
                        var mm = today.getMonth() + 1;
                        var yyyy = today.getFullYear();
                        if (dd < 10) {
                            dd = '0' + dd
                        }
                        if (mm < 10) {
                            mm = '0' + mm
                        }
                        var today = dd + '/' + mm + '/' + yyyy;

                        saveAs(blob, expType + "_NAICSSuggestions_Template_" + today + ".xlsx");
                    }                    
                }).error(function (result) {
                    return result;
                })


            },
            //Service method to fetch the details for address, phone and name
            getStuartDetails: function (postData, type) {
                var strURL = "";
                if (type == "name")
                    strURL = "TopAccountService/GetConstituentOrgName/";
                else if (type == "address")
                    strURL = "TopAccountService/GetConstituentAddress/";
                else if (type == "phone")
                    strURL = "TopAccountService/GetConstituentPhone/";

                return $http.post(BasePath + strURL , postData , {
                    headers:
                        {
                            "Content-Type": "application/json",
                            "Accept": "application/json"
                        }
                })
                .success(function (result) {
                    return result;
                })
                .error(function (result) {
                    return result;
                })
            },
            //Service method to log Recent Search
            logRecentSearch: function (SearchParams) {
                return $http.post(BasePath + "TopAccountService/InsertTopAccountRecentSearch", SearchParams, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                }).success(function (result) {
                    return result;

                }).error(function (result) {
                    return result;
                });
            },
            //Service method to get Recent Search
            getRecentSearches: function () {
                return $http.get(BasePath + "TopAccountService/GetTopAccountRecentSearches",
                    {
                        headers: {
                            "Content-Type": "Application/JSON",
                            "Accept": "Application/JSON"
                        }
                    }).success(function (result) {
                        return result;

                    }).error(function (result) {
                        return result;
                    });
            },           
            //Service method to get Naics Codes
            getNAICSCodes: function () {
            return $http.get(BasePath + "TopAccountService/GetNAICSCodes",
                {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                }).success(function (result) {
                    return result;

                }).error(function (result) {
                    return result;
                });
            },
            //Service method to get Naics Codes Level2 and 3
            getNAICSCodesLevel2and3: function () {
                return $http.get(BasePath + "TopAccountService/GetNAICSCodesLevel2and3",
                {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                }).success(function (result) {
                    return result;

                }).error(function (result) {
                    return result;
                });
            },
            getUserPermissions: function () {
                return $http.get(BasePath + "Home/GetUserPermissions",
                    {
                        headers: {
                            "Content-Type": "Application/JSON",
                            "Accept": "Application/JSON"
                        }
                    }).success(function (result) {
                        return result;

                    }).error(function (result) {
                        return result;
                    });
            },
  logRecentNAICSUpdate: function (SearchParams) {
      return $http.post(BasePath + "TopAccountService/InsertTopOrgsRecentNAICS", SearchParams, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                }).success(function (result) {
                    return result;

                }).error(function (result) {
                    return result;
                });
            },
            getRecentNAICSUpdate: function () {
                return $http.get(BasePath + "TopAccountService/GetTopOrgsRecentNAICSUpdate",
                    {
                        headers: {
                            "Content-Type": "Application/JSON",
                            "Accept": "Application/JSON"
                        }
                    }).success(function (result) {
                        return result;

                    }).error(function (result) {
                        return result;
                    });
            }
        }

    }
]);

//Service to Upload the naics suggestion files
topAccMod.service('FileUploadService', ['$http', function ($http) {
    this.uploadFileToUrl = function (file, uploadUrl) {
        var fd = new FormData();
        fd.append('file', file);

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })

        .success(function () {
        })

        .error(function () {
        });
    }
}]);