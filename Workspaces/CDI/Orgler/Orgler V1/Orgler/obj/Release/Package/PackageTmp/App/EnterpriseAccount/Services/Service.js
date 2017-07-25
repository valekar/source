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
enterpriseAccMod.factory("EnterpriseAccountService", ['$http',
    function ($http) {
        //base Path
        var BasePath = $("base").first().attr("href");
        
        return {
            //Service method to perform search
            getEnterpriseAccountSearchResults: function (postData) {
                return $http.post(BasePath + "EnterpriseAccountService/Search", postData, {
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
            //method to perform Create Enterprise Account.
            submitEnterpriseAccountCreate: function (postData) {
                return $http.post(BasePath + "EnterpriseAccountService/Create", postData, {
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
            //method to perform Edit Enterprise Account.
            submitEnterpriseAccountEdit: function (postData) {
                return $http.post(BasePath + "EnterpriseAccountService/EditEnterpriseOrg", postData, {
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
            //method to perform Delete Enterprise Orgs.
            submitEnterpriseOrgsDelete: function (postData) {
                return $http.post(BasePath + "EnterpriseAccountService/DeleteEnterpriseOrg", postData, {
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
            //method to perform Get basic Enterprise Account.
            getEnterpriseAccountBasicDetails: function (postData) {
                return $http.post(BasePath + "EnterpriseAccountService/Edit", postData, {
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
            //method to perform Get basic Enterprise Account.
            logEnterpriseAccountRecentSearch: function (postData) {
                return $http.post(BasePath + "EnterpriseAccountService/InsertEnterpriseOrgRecentSearch", postData, {
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
            //method to perform Get basic Enterprise Account.
            getEnterpriseAccountRecentSearch: function () {
                return $http.get(BasePath + "EnterpriseAccountService/GetEnterpriseOrgsRecentSearches", {
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
            //Service method to get Naics Codes Level2 and 3
            getNAICSCodesLevel2and3: function () {
                return $http.get(BasePath + "EnterpriseAccountService/GetNAICSCodesLevel2and3",
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
            //Service method to get Rank provider
            getFourtuneRank: function () {
                return $http.get(BasePath + "EnterpriseAccountService/GetFortuneRankProvider",
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
            //Service method to get SourceSystem
            getSourceSystem: function () {
                return $http.get(BasePath + "Home/getSourceSystemType",
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
            //Service method to get ChapterSystem
            getChapterSystem: function () {
                return $http.get(BasePath + "EnterpriseAccountService/GetChapterSystemData",
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
            //Service method to get ChapterSystem
            getTags: function () {
                return $http.get(BasePath + "EnterpriseAccountService/GetTagsData",
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

enterpriseAccMod.factory("StoreData", [function () {

    var StoreData = {};

    StoreData.naicsCounter = "";

    StoreData.setNaicsCounter = function (counter) {
        StoreData.naicsCounter = counter;
    }

    StoreData.getNaicsCounter = function () {
        return StoreData.naicsCounter;
    }

    StoreData.clearNaicsCounter = function () {
        StoreData.naicsCounter = "";
    }

    return StoreData;

}]);