angular.module('constituent', ['common', 'ui.grid', 'ui.grid.selection', 'ui.grid.resizeColumns', 'ui.grid.exporter', 'ui.grid.moveColumns', 'ui.grid.autoResize', 'angularUtils.directives.uiBreadcrumbs',
    'ui.bootstrap', 'ui.grid.pagination', 'ngFileUpload', 'ngSanitize', 'ui.select', 'ui.grid.treeView'])
    .config(function ($stateProvider, $urlRouterProvider, $locationProvider, $localStorageProvider) {
        // added this for browser tabs segregation
        $localStorageProvider.setKeyPrefix('');
        $localStorageProvider.setKeyPrefix('Organization-');

        $urlRouterProvider.otherwise("/constituent/search");
        $stateProvider.state('constituent', {
            url: '/constituent',
            views: {
                'header': {
                    templateUrl: BasePath + 'App/Constituent/Views/header.html',
                    controller: 'headerCtrl'
                },
                'content': {
                    templateUrl: BasePath + 'App/Constituent/Views/content.html',

                }
            },
            data: {
                displayName: "Organization"
            }

        }).state('constituent.search', {
            url: "/search",
            views: {
                'content@': {
                    templateUrl: BasePath + "App/Constituent/Views/ConstituentSearchTemplate.html",
                    controller: 'constituentSearchController'
                }
            },
            data: {
                displayName: "Search"
            }
        }).state('constituent.search.masterid', {
            url: "/masterid/:masterId",
            views: {
                'content@': {
                    templateUrl: BasePath + "App/Constituent/Views/constituentSearchResultsTemplate.html",
                    controller: 'quickSearchController1'
                }
            },
            data: {
                displayName: "Search Results"

            }
        }).state('constituent.search.results', {
            url: "/results?type&masterId",
            views: {
                'content@': {
                    templateUrl: BasePath + "App/Constituent/Views/constituentSearchResultsTemplate.html",
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
                    templateUrl: BasePath + "App/Constituent/Views/ViewCartTemplate.html",
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
                    templateUrl: BasePath + "App/Constituent/Views/multi/ConstMultiDetailsTemplate.html",
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
                    templateUrl: BasePath + "App/Constituent/Views/home/ConstHomeDetailsTemplate.html",
                    controller: 'constituentHomeDetailsController'
                }
            },
            data: {
                displayName: "Home"

            }

        })
        //CEM Chnages
        /* .state('constituent.search.results.cem', {
             url: "/cem/:constituentId",
             views: {
                 'content@': {
                     templateUrl: BasePath + "App/Constituent/Views/cem/ConstCemDetails.tpl.html",
                     controller: 'ConstCemCtrl'
                 }
             },
             data: {
                 displayName: "CEM"
             }
         });*/
        $locationProvider.html5Mode(true).hashPrefix('!');
    });

// this is used for storing name and master id and then show the same in the header 
angular.module('constituent').controller('headerCtrl', ['$localStorage', '$scope', '$rootScope', '$window', '$sessionStorage','constituentApiService',
    function ($localStorage, $scope, $rootScope, $window, $sessionStorage, constituentApiService) {
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
            $scope.param = newMasterId;
           
            if (!angular.isUndefined(newMasterId)) {
                constituentApiService.getApiDetails($scope.param, "ConstOrgName").success(function (result) {
                    //set the value in cache
                    $rootScope.constName = result[0].cnst_org_nm;
                    delete $sessionStorage.name;
                    $sessionStorage.name = $rootScope.constName;
                    $scope.constituent_name = $sessionStorage.name;
                    //initailize();

                })
               // $scope.constituent_name = $sessionStorage.name;
                $scope.constituent_masterId = $sessionStorage.masterId;
                $scope.display = 'block';


            }
            else {
                //$scope.constituent_name = $sessionStorage.name;
                $scope.display = 'none';
            }
        }

        );



        $rootScope.CaseSearchPath = BasePath + "App/Constituent/Views/common/Case.tpl.html";
        // console.log("BasePath :: " + $rootScope.BasePathURL);
        $rootScope.rootCaseLinkHtmlPath1 = BasePath + "App/Constituent/Views/common/CartLink.tpl.html"

    }]);


