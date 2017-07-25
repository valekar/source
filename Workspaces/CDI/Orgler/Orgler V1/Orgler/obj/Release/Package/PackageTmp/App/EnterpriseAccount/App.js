BasePath = $("base").first().attr("href");

var enterpriseAccMod = angular.module('enterpriseAccountModule', ['common', 'rzModule', 'ui.router', 'angularUtils.directives.uiBreadcrumbs', 'ui.bootstrap', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid.pagination', 'ui.grid.moveColumns', 'ui.grid.exporter', 'ui.grid.resizeColumns', 'ui.bootstrap.dateparser', 'ui.grid.treeView', 'ui.grid.selection', 'ngSanitize', 'ui.select', 'ui.grid.grouping', 'ui.grid.draggable-rows'])
    .config(function ($stateProvider, $urlRouterProvider, $locationProvider) {

        $urlRouterProvider.otherwise("/enterpriseaccount/search");

        $stateProvider
        //Home
        .state('enterpriseaccount', {
            url: '/enterpriseaccount',
            views: {
                'headerContent':
                    {
                        templateUrl: BasePath + 'App/EnterpriseAccount/Views/header.html'
                    },
                'pageContent':
                    {
                        templateUrl: BasePath + 'App/EnterpriseAccount/Views/content.html'
                    }
            },
            data: {
                displayName: 'Enterprise Orgs'
            }
        })
            //Search enterprise orgs
        .state('enterpriseaccount.search', {
            url: '/search',
            views: {
                'headerContent@':
                    {
                        templateUrl: BasePath + 'App/EnterpriseAccount/Views/header.html'
                    },
                'pageContent@':
                    {
                        templateUrl: BasePath + 'App/EnterpriseAccount/Views/EnterpriseAccountMainTemplate.html',
                        controller: 'enterPriseAccountSearchController'
                    }
            },
            data: {
                displayName: 'Search'
            }
        })
             //Search Results
            .state('enterpriseaccount.search.result', {
                url: "/result",
                views: {
                    'headerContent@':
                    {
                        templateUrl: BasePath + 'App/EnterpriseAccount/Views/header.html'
                    },
                    'pageContent@': {
                        templateUrl: BasePath + "App/EnterpriseAccount/Views/EnterpriseAccountSearchResults.html",
                        controller: 'enterPriseAccountSearchResultController'
                    }
                },
                data: {
                    displayName: "Search Results"

                }
            })
        //Details screen
        .state('enterpriseaccount.search.result.multi', {
            url: '/multi/:ent_org_id',
            views: {
                'headerContent@':
                    {
                        templateUrl: BasePath + 'App/EnterpriseAccount/Views/detailsHeader.html',
                        controller: 'enterpriseOrgDetailsHeaderCtrl'
                    },
                'pageContent@':
                    {
                        templateUrl: BasePath + 'App/EnterpriseAccount/Views/EnterpriseOrgDetails.tpl.html',
                        controller: 'enterpriseOrgDetailsCtrl'
                    }
            },
            data:
            {
                displayName: "Details"
            }
        });

        $locationProvider.html5Mode(true);
    });

enterpriseAccMod.controller('EnterpriseAccountTemplateCtrl', ['$scope', function ($scope) {
    $scope.baseUrl = BasePath;
    $scope.searchTemplateUrl = BasePath + 'App/EnterpriseAccount/Views/EnterpriseAccountSearch.html';
    $scope.createTemplateUrl = BasePath + 'App/EnterpriseAccount/Views/EnterpriseAccountCreate.html';
}]);

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