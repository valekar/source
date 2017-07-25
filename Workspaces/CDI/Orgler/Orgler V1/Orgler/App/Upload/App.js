//Base Path URL
BaseUrl = $("base").first().attr("href");

var upldModule = angular.module('uploadModule', ['common', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'angularUtils.directives.uiBreadcrumbs', 'ui.bootstrap', 'ui.grid.pagination'])
    .config(function ($locationProvider, $urlRouterProvider, $stateProvider) {
        $urlRouterProvider.otherwise('/upload/eosiupload');

        $stateProvider
        .state('upload',
            {
                url: '/upload',
                views: {
                    'headerContent': {
                        templateUrl: BasePath + 'App/Upload/Views/header.tpl.html',
                        controller: 'uploadHeaderCtrl'
                    },
                    'pageContent': {
                        templateUrl: BasePath + 'App/Upload/Views/homeContent.tpl.html'
                    }
                },
                data: {
                    displayName: 'Upload'
                }
            }
        )
        .state('upload.eosiupload',
            {
                url: '/eosiupload',
                views: {
                    'headerContent@': {
                        templateUrl: BasePath + 'App/Upload/Views/header.tpl.html',
                        controller: 'uploadHeaderCtrl'
                    },
                    'pageContent@': {
                        templateUrl: BasePath + 'App/Upload/Views/EOSIUploadTemplate.tpl.html',
                        controller: 'EOSIUploadController'
                    }
                },
                data: {
                    displayName: 'EOSI Site'
                }
            }
        )
        .state('upload.affiliationupload',
            {
                url: '/affiliationupload',
                views: {
                    'headerContent@': {
                        templateUrl: BasePath + 'App/Upload/Views/header.tpl.html',
                        controller: 'uploadHeaderCtrl'
                    },
                    'pageContent@': {
                        templateUrl: BasePath + 'App/Upload/Views/AffiliationUploadTemplate.tpl.html',
                        controller: 'AffiliationUploadController'
                    }
                },
                data: {
                    displayName: 'Affiliation'
                }
            }
        )
        .state('upload.eoupload',
            {
                url: '/eoupload',
                views: {
                    'headerContent@': {
                        templateUrl: BasePath + 'App/Upload/Views/header.tpl.html',
                        controller: 'uploadHeaderCtrl'
                    },
                    'pageContent@': {
                        templateUrl: BasePath + 'App/Upload/Views/EOUploadTemplate.tpl.html',
                        controller: 'EOUploadController'
                    }
                },
                data: {
                    displayName: 'Enterprise Orgs'
                }
            }
        );

        $locationProvider.html5Mode(true);
    })
;

// this is used for storing name and master id and then show the same in the header 
upldModule.controller('uploadHeaderCtrl', ['$localStorage', '$scope', '$rootScope', '$window',
    function ($localStorage, $scope, $rootScope, $window) {
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
    }]);

