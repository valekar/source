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
newAccMod.factory("NewAccountService", ['$http',
    function($http)
    {
        //base Path
        var BasePath = $("base").first().attr("href");

        return {
            //Service method to perform search
            getNewAccountSearchResults: function(postData)
            {
                return $http.post(BasePath + "NewAccountService/Search", postData, {
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
            submitNAICSCodeUpdates: function (postData)
            {
                return $http.post(BasePath + "NewAccountService/SubmitNAICSCodeUpdates", postData, {
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
            submitEditNAICSCodes: function (postData) {
                return $http.post(BasePath + "NewAccountService/EditNAICSCode", postData, {
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
            confirmAccount: function (postData) {
                return $http.post(BasePath + "NewAccountService/ConfirmAccount", postData, {
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
            getMasterNAICSDetails: function (postData) {
                return $http.post(BasePath + "NewAccountService/getMasterNAICSDetails", postData, {
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
            getPotentialMergesDetails: function (postData) {
                return $http.post(BasePath + "NewAccountService/getPotentialMergesDetails", postData, {
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
            
            getDownloadNAICSSuggestionsData: function (postData,expType) {
                return $http.post(BasePath + "NewAccountService/DownloadNAICSSuggestionsExcel/", postData, {
                    headers: {
                        "Content-type": 'application/json'

                    },
                    "responseType": "arraybuffer",
                }).success(function (data) {
                    if (data.byteLength > 0) {
                        var blob = new Blob([data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
                        //  var blob = new Blob([data], { type: type });                      
                        var today = new Date();
                        var dd = today.getDate();
                        var mm = today.getMonth()+1;
                        var yyyy = today.getFullYear();
                        if(dd<10){
                            dd='0'+dd
                        } 
                        if(mm<10){
                            mm='0'+mm
                        } 
                        var today = dd+'/'+mm+'/'+yyyy; 
                        
                        saveAs(blob, expType + "_NAICSSuggestions_Template_" + today + ".xlsx");
                    }
                else{alert("No data found!")}
                }).error(function (result) {
                    return result;
                })


            },
            //Service method to fetch the details for address, phone and name
            getStuartDetails: function (postData, type) {
                var strURL = "";
                if (type == "name")
                    strURL = "NewAccountService/GetConstituentOrgName/";
                else if (type == "address")
                    strURL = "NewAccountService/GetConstituentAddress/";
                else if (type == "phone")
                    strURL = "NewAccountService/GetConstituentPhone/";

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
            logRecentSearch: function (SearchParams) {
                return $http.post(BasePath + "NewAccountService/InsertNewAccountRecentSearch", SearchParams, {
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
            getRecentSearches: function () {
                return $http.get(BasePath + "NewAccountService/GetNewAccountRecentSearches",
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
            getNaicsSuggetions: function () {
                return $http.get(BasePath + "NewAccountService/GetNewAccountRecentSearches",
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
            getNAICSCodes: function () {
                return $http.get(BasePath + "NewAccountService/GetNAICSCodes",
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
                return $http.get(BasePath + "NewAccountService/GetNAICSCodesLevel2and3",
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
                return $http.post(BasePath + "NewAccountService/InsertNewAccountRecentNAICS", SearchParams, {
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
                return $http.get(BasePath + "NewAccountService/GetNewAccountRecentNAICSUpdate",
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
newAccMod.service('FileUploadService', ['$http', function ($http) {
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