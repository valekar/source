BasePath = $("base").first().attr("href");
//var commonModule = angular.module('common', ['ngRoute', 'ngResource', 'ui.bootstrap']);
var commonModule = angular.module('common', ['ui.router', 'ngResource', 'ui.bootstrap', 'angular.backtop', 'ngStorage']).config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('redirectInjector');
    //initialize get if not there
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    // because previous version of code introduced browser-related errors
    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';
}]);
var mainModule = angular.module('main', ['common']).config(['$locationProvider', '$localStorageProvider', function ($locationProvide, $localStorageProvider) {
    // $locationProvider.html5Mode(true);
    $localStorageProvider.setKeyPrefix('');
    $localStorageProvider.setKeyPrefix('Main-');
}]);
//this redirect injector is used when request timeouts and redirects to logincontroller, 
//then logincontroller responds with header redirect, so that this can be used to redirect to loginPage
commonModule.factory('redirectInjector', ['$window', function ($window) {
    var errorInjector = {
        response: function (response) {
            //console.log(response.headers("Accept"));
            if (response.headers("redirect")) {
                $window.location.href = BasePath;
                return;
            }

            return response;
        }
    };
    return errorInjector;
}]);


mainModule.controller('UserPermissionCtrl', ['$scope', 'mainService', function ($scope, mainService) {
    if (mainService.getLocalUserPermissions() == null || angular.isUndefined(mainService.getLocalUserPermissions())) {
        mainService.getUserPermissions().then(function (result) {
            if (result.data)
            {
                mainService.setLocalUserPermissions(result.data);
            }        
        });
    }
}]);

mainModule.controller('quickSearchController', ['$scope', '$window', 'mainService', '$location', '$localStorage','$rootScope','$timeout',
    function ($scope, $window, mainService, $location, $localStorage, $rootScope, $timeout) {
        $scope.type = "masterid";
       
        $scope.pleaseWait = { "display": "none" };
        
    $scope.quickSearch = function () {
        console.log($scope.quickSearchInput);
        if ($scope.quickSearchInput == "" || angular.isUndefined($scope.quickSearchInput)) {
            mainErrorPopup($rootScope, "Please enter quick search parameters", "Alert");
            return;
        }

        if ($scope.masterId == "") {
            $window.location.href = BasePath + "/constituent/search";
        }
        else {
            var quickSearchParams = {};
            switch ($scope.type) {
                case "masterid":
                    var reg = /^\d+$/;
                    if (reg.test($scope.quickSearchInput)) {
                        quickSearchParams = {
                            "masterId": $scope.quickSearchInput
                        };
                        break;
                    }
                    else {
                        quickSearchErrors("Please enter only numbers");
                        return;
                    }
                   
                case "address":
                    quickSearchParams = {
                        "addressLine": $scope.quickSearchInput
                    }
                    break;
                case "city":
                    quickSearchParams = {
                        "city": $scope.quickSearchInput
                    }
                    break;

                case "state":
                    quickSearchParams = {
                        "state": $scope.quickSearchInput
                    }
                    break;

                case "zip":
                    var reg = /^\d+$/;
                    if (reg.test($scope.quickSearchInput)) {
                        quickSearchParams = {
                            "zip": $scope.quickSearchInput
                        }
                        break;
                    }
                    else {
                        quickSearchErrors("Please enter only numbers");
                        return;
                    }                  
                case "phone":
                    var reg = /^\d+$/;
                    if (reg.test($scope.quickSearchInput)) {
                        quickSearchParams = {
                            "phone": $scope.quickSearchInput
                        }
                        break;
                    }
                    else {
                        quickSearchErrors("Please enter only numbers");
                        return;
                    }
       
                case "email":
                    var reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                    if (reg.test($scope.quickSearchInput)) {
                        quickSearchParams = {
                            "emailAddress": $scope.quickSearchInput
                        }
                        break;
                    }
                    else {
                        quickSearchErrors("Please enter correct email id format");
                        return;
                    }
            }
            $scope.pleaseWait = { "display": "block" };
            quickSearchParams["type"] = "IN";
            mainService.setQuickSearchParams(quickSearchParams);
            // $location.path("/constituent/search/results").replace();
            //$window.location.href = BasePath + "constituent/search";
            var newPostParams = { "listAccountSearchInputModel": [] };
            newPostParams["listAccountSearchInputModel"].push(quickSearchParams);
            mainService.getConstituentAdvSearchResults(newPostParams).then(function (_result) {
                //cleat all multi tab data , need to do for normal tab too

                mainService.setSearchResutlsData(_result);
                // this is for export 
                $localStorage.quickSearchParams = newPostParams;
                mainService.setAdvSearchParams(newPostParams);
                var _result = mainService.getSearchResultsData();
                //  console.log(_result);
                if (_result.length > 0) {
                    $scope.pleaseWait = { "display": "none" };
                    $localStorage.SearchResultsData = _result;
                    //$location.url("/constituent/search/results");
                    //$location.path("/constituent/search/results").replace();
                    $window.location.href = BasePath + "constituent/search/results";

                }
                else {
                    //alert("no data");
                    $scope.pleaseWait = { "display": "none" };
                    $scope.quickSearchConstNoResults = "No Results found!";
                    $timeout(function () {
                        $scope.quickSearchConstNoResults = "";

                    }, 3000);
                    
                   
                }


            }, function (error) {
                console.log(error);
                // just to be sure that noe localstorage data exists
                delete $localStorage.quickSearchParams;
                delete $localStorage.SearchResultsData;
                $scope.quickSearchInput = "";
                $scope.pleaseWait = { "display": "none" };
               // $scope.ConstNoResults = "No Results found!";
                constituentSearchErrorPopup($rootScope, error)
            });
        }
    }


    function quickSearchErrors(message) {
        $scope.quickSearchConstNoResults = message;
        $timeout(function () {
            $scope.quickSearchConstNoResults = "";
        }, 2000);
        
    }


}]);

function constituentSearchErrorPopup($rootScope, error) {
    var result = error.data;
    if (result == 'LoginDenied') {
        mainErrorPopup($rootScope, 'Logged in user does not have appropriate permission.', "Error: Access Denied");
    }
    else if (result == 'Timed out') {
        mainErrorPopup($rootScope, 'The service/database timed out. Please try again after some time.', "Error: Timed Out");
    }
    else if (result == "Database Error") {
        mainErrorPopup($rootScope, error.statusText, "Alert");
    }
}

function mainErrorPopup($rootScope, message, header) {

    $rootScope.message = "";
    $rootScope.header = "";

    $rootScope.message = message;
    $rootScope.header = header;
    angular.element("#iMainErrorModal").modal();
   // angular.element("#iMainErrorModal").modal('hide');
}

commonModule.factory('mainService', ['$localStorage', '$q', '$http', '$window', function ($localStorage, $q, $http, $window) {
    //var quickSearchParams = {};
    //  locInfoResearchData = {};
    var searchResutlsData;
    var advSearchParams;
    return {
        //prefix is Main-
        setQuickSearchParams: function (params) {
            // $localStorage.quickSearchParams = params;
            $http.post(BasePath + "Home/WriteQuickSearchResults", params, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            });
        },
        getQuickSearchParams: function () {
            //return $localStorage.quickSearchParams;

            return $http.get(BasePath + "Home/GetCaseRecentSearches",
                {
                    headers: {
                        "Content-Type": "Application/JSON",
                        "Accept": "Application/JSON"
                    }
                }).success(function (result) {
                    if (result == "" || result == null) {
                        console.log("Failed to get Case Recent Searches");
                    }
                    else {
                        console.log(result);
                        return result;
                    }
                }).error(function (result) {
                    console.log(result);
                    return result;
                })
        },
        //prefix is Constituent- so no need to append anything
        setLocInfoResearchData: function (result, param, row, MergeConflictFlag) {
            if (MergeConflictFlag) {
                $localStorage.case = {
                    caseKey: param,
                    groupID: row.ref_id,
                    locInfoResearchData: result,
                    mergeConflictFlag: MergeConflictFlag
                }
                //  locInfoResearchData = $localStorage.case;

            }
            else {
                $localStorage.case = {
                    caseKey: param,
                    locInfoResearchData: result,
                    mergeConflictFlag: MergeConflictFlag

                }
                //  locInfoResearchData = $localStorage.case;
            }


        },
        //Prefix is Case-
        getLocInfoResearchData: function () {
            if ($window.localStorage["Case-case"]) {
                return JSON.parse($window.localStorage["Case-case"]);
            }
            else {
                return;
            }

        },
        clearLocInfoResearchData: function () {
            delete $window.localStorage.removeItem("Case-case");
        },
        getUsername: function () {
            var deferObject;
            var promise = $http.get(BasePath + "Home/getUserName", {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }),
            deferObject = deferObject || $q.defer();
            promise.then(function (answer) {
                var username;
                switch (answer) {
                    case '':
                        username = "";
                        break;
                    case 'Username not found':
                        username = "No username";
                        break;
                    case 'Error':
                        username = "Error";
                        break;
                    default:
                        username = answer;
                        break;
                }
                deferObject.resolve(username);
            }, function (reason) {
                deferObject.reject(reason);
            });
            return deferObject.promise;
        },
        getConstituentAdvSearchResults: function (postConstituentparams) {
            return $http.post(BasePath + "Test/advsearch", postConstituentparams, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).then(function (result) {
                $http.post(BasePath + "Home/WriteConstituentRecentSearches", postConstituentparams, {
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    }
                });
                // console.log(result);
                return result.data;
            });
        },

        setLastTransSearchResult: function (result) {
            $localStorage.lastTransSearchResultData = result;
        },

        getLastTransSearchResult: function () {
            return $localStorage.lastTransSearchResultData;
        },

        setLastCaseSearchResult: function (result) {
            $localStorage.lastCaseSearchResultData = result;
        },

        getLastCaseSearchResult: function () {
            return $localStorage.lastCaseSearchResultData;
        },

        setLastConstSearchResult: function (result) {
            $localStorage.lastConstSearchResultData = result;
        },

        getLastConstSearchResult: function () {
            return $localStorage.lastConstSearchResultData;
        },

        setSearchResutlsData: function (result) {
            searchResutlsData = result;
        },
        getSearchResultsData: function () {
            return searchResutlsData;
        },
        setAdvSearchParams: function (result) {
            advSearchParams = result;
        },
        getAdvSearchParams: function (result) {
            return advSearchParams;
        },
        getUserPermissions: function () {
            return $http.get(BasePath + "Home/GetUserPermissions", {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                }
            }).then(function (result) {
                return result;

            }, function (result) {
                console.log(result);
                console.log("Error");
            });
        },
        setLocalUserPermissions: function (myData) {
            $localStorage.userPermissions = myData;
        },
        getLocalUserPermissions : function(){
            return $localStorage.userPermissions;
        }

    }
}]);


mainModule.directive('menuline', [function () {
    return {
        restrict: "A",
        link: function (scope, element, attr) {
            (function ($) {

                $.fn.menumaker = function (options) {

                    var cssmenu = $(this), settings = $.extend({
                        title: "Menu",
                        format: "dropdown",
                        sticky: false
                    }, options);

                    return this.each(function () {
                        cssmenu.prepend('<div id="menu-button">' + settings.title + '</div>');
                        $(this).find("#menu-button").on('click', function () {
                            $(this).toggleClass('menu-opened');
                            var mainmenu = $(this).next('ul');
                            if (mainmenu.hasClass('open')) {
                                mainmenu.hide().removeClass('open');
                            }
                            else {
                                mainmenu.show().addClass('open');
                                if (settings.format === "dropdown") {
                                    mainmenu.find('ul').show();
                                }
                            }
                        });

                        cssmenu.find('li ul').parent().addClass('has-sub');

                        multiTg = function () {
                            cssmenu.find(".has-sub").prepend('<span class="submenu-button"></span>');
                            cssmenu.find('.submenu-button').on('click', function () {
                                $(this).toggleClass('submenu-opened');
                                if ($(this).siblings('ul').hasClass('open')) {
                                    $(this).siblings('ul').removeClass('open').hide();
                                }
                                else {
                                    $(this).siblings('ul').addClass('open').show();
                                }
                            });
                        };

                        if (settings.format === 'multitoggle') multiTg();
                        else cssmenu.addClass('dropdown');

                        if (settings.sticky === true) cssmenu.css('position', 'fixed');

                        resizeFix = function () {
                            if ($(window).width() > 768) {
                                cssmenu.find('ul').show();
                            }

                            if ($(window).width() <= 768) {
                                cssmenu.find('ul').hide().removeClass('open');
                            }
                        };
                        resizeFix();
                        return $(window).on('resize', resizeFix);

                    });
                };
            })(jQuery);

            (function ($) {
                $(document).ready(function () {

                    $(document).ready(function () {
                        $("#cssmenu").menumaker({
                            title: "Menu",
                            format: "multitoggle"
                        });

                        $("#cssmenu").prepend("<div id='menu-line'></div>");

                        var foundActive = false, activeElement, linePosition = 0, menuLine = $("#cssmenu #menu-line"), lineWidth, defaultPosition, defaultWidth;

                        $("#cssmenu > ul > li").each(function () {
                            if ($(this).hasClass('active')) {
                                activeElement = $(this);
                                foundActive = true;
                            }
                        });

                        if (foundActive === false) {
                            activeElement = $("#cssmenu > ul > li").first();
                        }

                        defaultWidth = lineWidth = activeElement.width();

                        defaultPosition = linePosition = activeElement.position().left;

                        menuLine.css("width", lineWidth);
                        menuLine.css("left", linePosition);

                        $("#cssmenu > ul > li").hover(function () {
                            activeElement = $(this);
                            lineWidth = activeElement.width();
                            linePosition = activeElement.position().left;
                            menuLine.css("width", lineWidth);
                            menuLine.css("left", linePosition);
                        },
                        function () {
                            menuLine.css("left", defaultPosition);
                            menuLine.css("width", defaultWidth);
                        });

                    });


                });
            })(jQuery);

        }
    }

}]);