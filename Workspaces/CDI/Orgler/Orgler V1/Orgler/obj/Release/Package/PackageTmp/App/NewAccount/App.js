//BasePath = $("base").first().attr("href");
var newAccMod = angular.module('newAccountModule', ['common', 'ui.router', 'angularUtils.directives.uiBreadcrumbs', 'ui.bootstrap', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid.pagination', 'ui.grid.moveColumns', 'ui.grid.resizeColumns', 'daterangepicker', 'ui.grid.treeView', 'ui.grid.selection', 'ngSanitize', 'ui.select'])
    .config(function ($stateProvider, $urlRouterProvider, $locationProvider, $localStorageProvider) {
        // added this for browser tabs segregation
       $localStorageProvider.setKeyPrefix('');
       $localStorageProvider.setKeyPrefix('newAccMod-');
        $urlRouterProvider.otherwise("/newaccount/search");
        //$urlRouterProvider.otherwise("/newaccount/search/results/cart");

        $stateProvider
        //Home
        .state('newaccount', {
            url: '/newaccount',
            views: {
                'headerContent':
                    {
                        templateUrl: BasePath + 'App/Shared/Views/headerContent.tpl.html'
                    },
                'pageContent':
                    {
                        templateUrl: BasePath + 'App/NewAccount/Views/Home.tpl.html'
                    }
            },
            data: {
                displayName: 'New Account'
            }
        })
        //Search
        .state('newaccount.search', {
            url: '/search',
            views: {
                'headerContent@':
                    {
                        templateUrl: BasePath + 'App/Shared/Views/headerContent.tpl.html'
                    },
                'pageContent@':
                    {
                        templateUrl: BasePath + 'App/NewAccount/Views/Search.tpl.html',
                        controller: 'newAccountSearchController'
                    }
            },
            data: {
                displayName: 'Search'
            }
        })
        //Search Results
        .state('newaccount.search.result', {
            url: '/result',
            views: {
                'headerContent@':
                    {
                        templateUrl: BasePath + 'App/Shared/Views/headerContent.tpl.html'
                    },
                'pageContent@':
                    {
                        templateUrl: BasePath + 'App/NewAccount/Views/SearchResults.tpl.html',
                        controller: 'newAccountSearchResultController'
                    }
            },
            data: {
                displayName: 'Search Results'
            }
        }).state('newaccount.search.results.cart', {
            url: "/cart",
            views: {
                'content@': {
                    templateUrl: BasePath + "App/Shared/Views/ViewCartTemplate.html",
                    controller: 'constCartController'
                }
            },
            data: {
                displayName: "Cart"
            }
        })
        ;

        $locationProvider.html5Mode(true);
    });

    var myApp;
myApp = myApp || (function () {
    //var pleaseWaitDiv = $('<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');
    var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="basicModal" aria-hidden="true" tabindex="-1"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h3>Processing...</h3></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar" style="width: 100%;"><span class="sr-only">60% Complete</span></div></div></div></div></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal('show');
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        },
    };
})();