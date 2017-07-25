//BasePath = $("base").first().attr("href");
var adminMod = angular.module('adminModule', ['common', 'ui.router', 'angularUtils.directives.uiBreadcrumbs', 'ui.bootstrap', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid.pagination', 'ui.grid.moveColumns', 'ui.grid.exporter', 'ui.grid.resizeColumns', 'ui.bootstrap.dateparser', 'ui.grid.treeView', 'ui.grid.selection', 'ngSanitize', 'ui.select'])
    .config(function ($stateProvider, $urlRouterProvider, $locationProvider, $localStorageProvider) {
        $localStorageProvider.setKeyPrefix('');
        $localStorageProvider.setKeyPrefix('adminModule-');
        $urlRouterProvider.otherwise("/admin/searchresult");

        $stateProvider
        //Home
        .state('admin', {
            url: '/admin',
            views: {
                'headerContent':
                    {
                        templateUrl: BasePath + 'App/Admin/Views/header.html'
                    },
                'pageContent':
                    {
                        templateUrl: BasePath + 'App/Admin/Views/content.html'
                    }
            },
            data: {
                displayName: 'Admin'
            }
        })
          //Search Results
            .state('admin.searchresult', {
                url: "/searchresult",
                views: {
                    'pageContent@': {
                        templateUrl: BasePath + "App/Admin/Views/TabSecurity.tpl.html",
                        controller: 'TabSecurityCtrl'
                    }
                },
                data: {
                    displayName: "Tab Level Security"

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