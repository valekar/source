angular.module('transaction', ['common', 'ui.grid', 'ui.grid.selection', 'ui.grid.resizeColumns', 'ui.grid.moveColumns', 'ui.grid.autoResize', 'angularUtils.directives.uiBreadcrumbs',
    'ui.bootstrap', 'ui.grid.pagination','ui.grid.exporter'])
.config(function ($urlRouterProvider, $stateProvider, $locationProvider, $localStorageProvider) {
    $localStorageProvider.setKeyPrefix('');
    $localStorageProvider.setKeyPrefix('Trans-');

    $urlRouterProvider.otherwise("/transaction/search");
    $stateProvider.state('transaction', {
        url: '/transaction',
        views: {
            'header': {
                templateUrl: BasePath + 'App/Transaction/Views/header.html',
                controller: 'TransactionheaderCtrl'
            },
            'content': {//Written in an absolute syntax
                templateUrl: BasePath + 'App/Transaction/Views/content.html'
            }
        },
        data: {
            displayName: "Transaction"
        }
    })
    .state('transaction.search', {
        url: "/search",
        views: {
            'content@': {//Written in an absolute syntax
                templateUrl: BasePath + "App/Transaction/Views/TransactionSearchTemplate.html",
                controller: 'TransactionSearchController'
            },
        },
        data: {
            displayName: "Search "
        }
    })
    .state('transaction.search.results', {
        url: "/results?type&masterId",
        views: {
            'content@': {//Written in an absolute syntax
                templateUrl: BasePath + "App/Transaction/Views/TransactionSearchResultsTemplate.html",
                controller: 'TransactionSearchResultsController'
            },
        },
        data: {
            displayName: "Search Results"
        }
    }).state('transaction.search.results.multi', {
        url: "/multi/:trans_key",
        views: {
            'content@': {
                templateUrl: BasePath + "App/transaction/Views/multi/TransactionMultiDetailsTemplate.html",
                controller: 'TransactionMultiDetailsController'
            }
        },
        data: {
            displayName: "Search Details"
        }
        // added for optimizatiom
    });
    $locationProvider.html5Mode(true);
});

