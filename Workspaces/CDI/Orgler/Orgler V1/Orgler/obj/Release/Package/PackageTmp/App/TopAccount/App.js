//BasePath = $("base").first().attr("href");
var topAccMod = angular.module('topAccountModule', ['common','ui.router', 'angularUtils.directives.uiBreadcrumbs', 'ui.bootstrap', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid.pagination', 'ui.grid.moveColumns', 'ui.grid.resizeColumns', 'ui.bootstrap.dateparser', 'ui.grid.treeView', 'ui.grid.selection', 'ngSanitize', 'ui.select'])
    .config(function ($stateProvider, $urlRouterProvider, $locationProvider, $localStorageProvider) {
       $localStorageProvider.setKeyPrefix('');
       $localStorageProvider.setKeyPrefix('topAccMod-');
        $urlRouterProvider.otherwise("/topaccount/search");

        $stateProvider
        //Home
        .state('topaccount', {
            url: '/topaccount',
            views: {
                'headerContent':
                    {
                        templateUrl: BasePath + 'App/Shared/Views/headerContent.tpl.html'
                    },
                'pageContent':
                    {
                        templateUrl: BasePath + 'App/TopAccount/Views/Home.tpl.html'
                    }
            },
            data: {
                displayName: 'Top Orgs'
            }
        })
            //Search top orgs
        .state('topaccount.search', {
            url: '/search',
            views: {
                'headerContent@':
                    {
                        templateUrl: BasePath + 'App/Shared/Views/headerContent.tpl.html'
                    },
                'pageContent@':
                    {
                        templateUrl: BasePath + 'App/TopAccount/Views/Search.tpl.html',
                        controller: 'topAccountSearchController'
                    }
            },
            data: {
                displayName: 'Search'
            }
        })
         //Search Results
        .state('topaccount.search.result', {
            url: '/result',
            views: {
                'headerContent@':
                    {
                        templateUrl: BasePath + 'App/Shared/Views/headerContent.tpl.html'
                    },
                'pageContent@':
                    {
                        templateUrl: BasePath + 'App/TopAccount/Views/SearchResults.tpl.html',
                        controller: 'topAccountSearchResultController'
                    }
            },
            data: {
                displayName: 'Search Results'
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