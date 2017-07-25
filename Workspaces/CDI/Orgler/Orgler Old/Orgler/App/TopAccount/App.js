//Module and route configs corresponding to the Top Accounts Tab
BasePath = $("base").first().attr("href");

var topAccModule = angular.module('topAccountModule', ['ui.router', 'angularUtils.directives.uiBreadcrumbs', 'ui.bootstrap', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid.pagination', 'ui.grid.moveColumns', 'ui.grid.resizeColumns'])
.config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

    $urlRouterProvider.otherwise("/topaccount/search");

    $stateProvider
    //Home
    .state('topaccount', {
        url: '/topaccount',
        views:
            {
                'header':
                    {
                        templateUrl: BasePath + 'App/Shared/Views/headerContent.tpl.html'
                    },
                'content':
                    {
                        templateUrl: BasePath + 'App/TopAccount/Views/Home.tpl.html'
                    }
            },
        data:
            {
                displayName: "Top Account"
            }

    })
    .state('topaccount.search', {
        url: '/search',
        views:
            {
                'header@':
                    {
                        templateUrl: BasePath + 'App/Shared/Views/headerContent.tpl.html'
                    },
                'content@':
                    {
                        templateUrl: BasePath + 'App/TopAccount/Views/Search.tpl.html',
                        controller: 'topAccountController'
                    }
            },
        data:
            {
                displayName: "Top Account"
            }

    });

    $locationProvider.html5Mode(true);


});