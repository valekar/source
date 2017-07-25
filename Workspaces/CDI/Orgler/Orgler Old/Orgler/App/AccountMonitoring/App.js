angular.module('constituent', ['common', 'ui.grid', 'ui.grid.selection', 'ui.grid.resizeColumns', 'ui.grid.exporter', 'ui.grid.moveColumns', 'ui.grid.autoResize', 'angularUtils.directives.uiBreadcrumbs',
    'ui.bootstrap', 'ui.grid.pagination', 'ngFileUpload'])
    .config(function ($stateProvider, $urlRouterProvider, $locationProvider, $localStorageProvider) {
        // added this for browser tabs segregation
        $localStorageProvider.setKeyPrefix('');
        $localStorageProvider.setKeyPrefix('AccountMonitoring-');

        $urlRouterProvider.otherwise("/account/search");
        $stateProvider.state('account', {
            url: '/account',
            views: {
                //'header': {
                //    templateUrl: BasePath + 'App/AccountMonitoring/Views/header.html',
                //    controller:'headerCtrl'
                //},
                'content': {
                    //templateUrl: BasePath + 'App/AccountMonitoring/Views/content.html',
                    templateUrl: BasePath + "App/AccountMonitoring/Views/ConstituentSearchTemplate.html",
                    controller: 'constituentSearchController'
                }
            },
            data: {
                displayName: "New Accounts"
           }
            
        }).state('account.search', {
            url: "/search",
            views: {
                'content@': {
                    templateUrl: BasePath + "App/AccountMonitoring/Views/ConstituentSearchTemplate.html",
                    controller: 'constituentSearchController'
                }
            },
            data: {
                displayName: "Search"
            }
        }).state('account.search.results', {
            url: "/results?type&masterId",
            views: {
                'content@': {
                    templateUrl: BasePath + "App/AccountMonitoring/Views/constituentSearchResultsTemplate.html",
                    controller: 'constituentSearchResultsController'
                }
            },
            data: {
                displayName: "Search Results"

            }
        }).state('constituent.search.results.cart', {
            url: "/cart",
            views: {
                'content@': {
                    templateUrl: BasePath + "App/AccountMonitoring/Views/ViewCartTemplate.html",
                    controller: 'constCartController'
                }
            },
            data: {
                displayName: "Cart"
            }
        }).state('constituent.search.results.multi', {
            url: "/multi/:constituentId",
            views: {
                'content@': {
                    templateUrl: BasePath + "App/AccountMonitoring/Views/multi/ConstMultiDetailsTemplate.html",
                    controller: 'constMultiDetialsController'
                }
            },
            data: {
                displayName: "Multi"
            }
            // added for optimizatiom
        }).state('constituent.search.results.home', {
                url: "/home/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/home/ConstHomeDetailsTemplate.html",
                        controller: 'constituentHomeDetailsController'
                    }
                },
                data: {
                    displayName: "Home"

                }

            }).state('constituent.search.results.summary', {
                url: "/summary/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsBestSmryTemplate.html",
                        controller: 'constituentDetailsController'
                    }
                },
                data: {

                    displayName: "Arc Summary"
                }
            }).state('constituent.search.results.names', {
                url:  "/names/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsNameTemplate.html",
                        controller: 'constituentDetailsNameController'
                    }
                },
                data: {
                    displayName: "Names"
                }

            }).state('constituent.search.results.address', {
                url: "/address/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsAddressTemplate.html",
                        controller: 'constituentDetailsAddressController'
                    }
                },
                data: {
                    displayName: "Address"
                }
            }).state('constituent.search.results.birth', {
                url: "/birth/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsBirthTemplate.html",
                        controller: 'constituentDetailsBirthController'
                    }
                },
                data: {
                    displayName: "Birth"
                }
            }).state('constituent.search.results.phone', {
                url: "/phone/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsPhoneTemplate.html",
                        controller: 'constituentDetailsPhoneController'
                    }
                },
                data: {
                    displayName: "phone"
                }
            }).state('constituent.search.results.email', {
                url:"/email/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsEmailTemplate.html",
                        controller: 'constituentDetailsEmailController'
                    }
                },
                data: {
                    displayName: "Email"
                }
            }).state('constituent.search.results.externalbridge', {
                url: "/externalbridge/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsExternalBridgeTemplate.html",
                        controller: 'constituentDetailsExternalBridgeController'
                    }
                },
                data: {
                    displayName: "External Bridge"
                }
            }).state('constituent.search.results.death', {
                url: "/death/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsDeathTemplate.html",
                        controller: 'constituentDetailsDeathController'
                    }
                },
                data: {
                    displayName: "Death"
                }
            }).state('constituent.search.results.contactpreference', {
                url: "/contactpreference/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsContactPreferenceTemplate.html",
                        controller: 'constituentDetailsContactPreferenceController'
                    }
                },
                data: {
                    displayName: "ContactPreference"
                }
            }).state('constituent.search.results.characteristics', {
                url: "/characteristics/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsCharacteristicsTemplate.html",
                        controller: 'constituentDetailsCharacteristicsController'
                    }
                },
                data: {
                    displayName: "Charateristics"
                }
            }).state('constituent.search.results.groupmemberships', {
                url: "/groupmemberships/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsGroupMembershipsTemplate.html",
                        controller: 'constituentDetailsGroupMembershipsController'
                    }
                },
                data: {
                    displayName: "Group Memberships"
                }
            }).state('constituent.search.results.transactionhistory', {
                url: "/transactionhistory/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsTransactionHistoryTemplate.html",
                        controller: 'constituentDetailsTransactionHistoryController'
                    }
                },
                data: {
                    displayName: "Transaction History"
                }
            }).state('constituent.search.results.anonymousinformation', {
                url: "/anonymousinformation/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsAnonymousInformationTemplate.html",
                        controller: 'constituentDetailsAnonymousInformationController'
                    }
                },
                data: {
                    displayName: "AnonymousInformation"
                }
            }).state('constituent.search.results.masteringattempts', {
                url: "/masteringattempts/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsMasteringAttemptsTemplate.html",
                        controller: 'constituentDetailsMasteringAttemptsController'
                    }
                },
                data: {
                    displayName: "Mastering Attempts"
                }
            }).state('constituent.search.results.internalbridge', {
                url: "/internalbridge/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsInternalBridgeTemplate.html",
                        controller: 'constituentDetailsInternalBridgeController'
                    }
                },
                data: {
                    displayName: "Internal Bridge"
                }
            }).state('constituent.search.results.mergehistory', {
                url: "/mergehistory/:constituentId",
                views: {
                    'content@': {
                        templateUrl: BasePath + "App/AccountMonitoring/Views/ConstDetailsMergeHistoryTemplate.html",
                        controller: 'constituentDetailsMergeHistoryController'
                    }
                },
                data: {
                    displayName: "Merge History"
                }
            });
        $locationProvider.html5Mode(true).hashPrefix('!');
    }


    );

// this is used for storing name and master id and then show the same in the header 
angular.module('constituent').controller('headerCtrl', ['$localStorage', '$scope', '$rootScope','$window','$sessionStorage',
    function ($localStorage, $scope, $rootScope, $window, $sessionStorage) {
        $scope.pleaseWait = false;

    $rootScope.$on('$stateChangeStart',
        function (event, toState, toParams, fromState, fromParams) {            
            $scope.pleaseWait = true;              
        });


    $rootScope.$on('$viewContentLoaded',
    function (event, toState, toParams, fromState, fromParams) {

       $scope.pleaseWait = false;
    })

    $scope.display = 'none';
    $scope.$watch(function () {
        return $sessionStorage.masterId;
    }, function (newMasterId, oldMasterId) {
        if (!angular.isUndefined(newMasterId)) {
            $scope.constituent_name = $sessionStorage.name;
            $scope.constituent_masterId = $sessionStorage.masterId;
            $scope.display = 'block';
            
        }
        else {
            $scope.display = 'none';
        }
    });



    $rootScope.CaseSearchPath = BasePath + "App/AccountMonitoring/Views/common/Case.tpl.html";
   // console.log("BasePath :: " + $rootScope.BasePathURL);
    $rootScope.rootCaseLinkHtmlPath = BasePath + "App/AccountMonitoring/Views/common/CartLink.tpl.html"

    }]);


