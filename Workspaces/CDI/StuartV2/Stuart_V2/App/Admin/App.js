angular.module('admin', ['common', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'angularUtils.directives.uiBreadcrumbs',
    'ui.bootstrap', 'ui.grid.pagination'])
.config(function ($urlRouterProvider, $stateProvider,$locationProvider) {
     $urlRouterProvider.otherwise("/admin");
    $stateProvider.state('admin', {
        url: '/admin',
        views: {

            'maincontent@': {//Written in an absolute syntax
                templateUrl: BasePath + 'App/Admin/Views/TabSecurity.tpl.html',
                controller: 'TabSecurityCtrl'
            }
        },
        data: {
            displayName: "Admin"
        }
    });

    $locationProvider.html5Mode(true);
});