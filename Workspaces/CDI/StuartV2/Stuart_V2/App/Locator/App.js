var locatorModule = angular.module('locator', ['common', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid.resizeColumns', 'ui.grid.exporter', 'ui.grid.moveColumns', 'angularUtils.directives.uiBreadcrumbs',
    'ui.bootstrap', 'ui.grid.pagination'])

.config(function ($urlRouterProvider, $stateProvider, $locationProvider, $localStorageProvider) {
    $localStorageProvider.setKeyPrefix('');
    $localStorageProvider.setKeyPrefix('Locator-');

   $urlRouterProvider.otherwise("/locator");
    $stateProvider.state('locator', {
        url: '/locator',
        views: {
            'header': {
                templateUrl: BasePath + 'App/Locator/Views/header.html',
                controller: 'LocatorheaderCtrl'
            },
            'content': {//Written in an absolute syntax
                templateUrl: BasePath + 'App/Locator/Views/content.html'
            }
        },
        data: {
            displayName: "Locator"
        }
    })
    .state('locator.email', {
        url: "/email",
        views: {
            'content@': {//Written in an absolute syntax
                templateUrl: BasePath + "App/Locator/Views/LocatorEmailTemplate.html",
                controller: 'locatorEmailController'
            },
        },
        data: {
            displayName: "Locator Email"
        }
    })    
    .state('locator.email.results', {
         url: "/results?type&masterID",
         views: {                                           
             'content@': {
                 templateUrl: BasePath + "App/Locator/Views/LocatorEmailSearchResultsTemplate.html",
                 controller: 'LocatorEmailSearchResultsController'
             }
         },
         data: {
             displayName: "Search Results"
         }
     })
    .state('locator.email.results.multi', {
        url: "/multi/:locator_addr_key",
        views: {
            'content@': {
                templateUrl: BasePath + "App/Locator/Views/multi/LocatorEmailDetailsTemplate.html",
                controller: 'LocatorEmailDetailsController'
            }
        },
        data: {
            displayName: "Multi"
        }
    })


    .state('locator.address', {
        url: "/address",
        views: {
            'content@': {//Written in an absolute syntax
                templateUrl: BasePath + "App/Locator/Views/LocatorAddressTemplate.html",
                controller: 'LocatorAddressController'
            },
        },
        data: {
            displayName: "Locator Address "
        }

    })
 .state('locator.address.results', {
     url: "/results?type&masterID",
     views: {
         'content@': {
             templateUrl: BasePath + "App/Locator/Views/LocatorAddressSearchResultsTemplate.html",
             controller: 'LocatorAddressSearchResultsController'
         }
     },
     data: {
         displayName: "Search Results"
     }
 })
    .state('locator.address.results.multi', {
        url: "/multi/:locator_addr_key",
        views: {
            'content@': {
                templateUrl: BasePath + "App/Locator/Views/multi/LocatorAddressDetailsTemplate.html",
                controller: 'LocatorAddressDetailsController'
            }
        },
        data: {
            displayName: "Multi"
        }
    })


    .state('locator.domain', {
        url: "/domain",
        views: {
            'content@': {//Written in an absolute syntax
                templateUrl: BasePath + "App/Locator/Views/LocatorEmailDomainCorrectionTemplate.html",
                controller: 'locatorDomainController'
            },
        },
        data: {
            displayName: "Locator Domain "
        }

    })
    .state('locator.domain.results', {
        url: "/results?type&masterID",
        views: {                                           
            'content@': {
                templateUrl: BasePath + "App/Locator/Views/LocatorDomainSearchResultsTemplate.html",
                controller: 'LocatorDomainSearchResultsController'
            }
        },
        data: {
            displayName: "Search Results"
        }
    })   

    $locationProvider.html5Mode(true);
});

// this is used for storing name and master id and then show the same in the header 
locatorModule.controller('LocatorheaderCtrl', ['$localStorage', '$scope', '$rootScope', '$window',
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

        $scope.$watch(function () {
            return $localStorage.locator_addr_key;
        }, function (newMasterId, oldMasterId) {
            if (!angular.isUndefined(newMasterId)) {
                $scope.LocEmailId = $localStorage.LocEmailId;
                $scope.locator_addr_key = $localStorage.locator_addr_key;
                // $scope.display = 'block';
                $scope.display = 'none';
            }
            else {
                $scope.display = 'none';
            }
        });



        if ($localStorage.locator_addr_key) {

            $scope.LocEmailId = $localStorage.LocEmailId;
            $scope.locator_addr_key = $localStorage.locator_addr_key;
            //$scope.display = 'block';
            $scope.display = 'none';
           
        }
        else
            $scope.display = 'none';
    }]);
angular.module('locator').directive('scrolldown', [function () {
    return {
        link: function (scope, element, attr) {
            scope.$watch('isDisplayed', function (newValue, oldValue) {
                var viewport = element.find('.ui-grid-render-container');

                ['touchstart', 'touchmove', 'touchend', 'keydown', 'wheel', 'mousewheel', 'DomMouseScroll', 'MozMousePixelScroll'].forEach(function (eventName) {
                    viewport.unbind(eventName);
                });
            });
        },
    };
}]);

angular.module('locator').controller('LocatorEmailTemplateCtrl', ['$scope', function ($scope) {

    $scope.baseUrl = BasePath;
   
}]);


