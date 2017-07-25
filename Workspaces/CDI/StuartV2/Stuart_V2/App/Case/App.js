angular.module('case', ['common', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid.resizeColumns', 'ui.grid.exporter', 'ui.grid.moveColumns', 'angularUtils.directives.uiBreadcrumbs',
    'ui.bootstrap', 'ui.grid.pagination'])
.config(function ($stateProvider, $urlRouterProvider, $locationProvider, $localStorageProvider) {
    $localStorageProvider.setKeyPrefix('');
    $localStorageProvider.setKeyPrefix('Case-');

    $urlRouterProvider.otherwise("/case/search");
    $stateProvider.state('case', {
        url: '/case',
        views: {
            'header': {
                templateUrl: BasePath + 'App/Case/Views/header.html',
                controller: 'CaseheaderCtrl'
            },
            'content': {
                templateUrl: BasePath + 'App/Case/Views/content.html'
            }
        },
        data: {
            displayName: 'Case'
        }
    })
    .state('case.search', {
        url: "/search",
        views: {
            'content@': {
                //templateUrl: BasePath + "App/Case/Views/CaseSearchTemplate.html",
                templateUrl: BasePath + "App/Case/Views/CaseMainTemplate.html",
                controller: 'CaseSearchController'
            }
        },
        data: {
            displayName: "Search"
        }
    }).state('case.search.results', {
        url: "/results?type&masterID",
        views: {
            'content@': {
                templateUrl: BasePath + "App/Case/Views/CaseSearchResultsTemplate.html",
                controller: 'CaseSearchResultsController'
            }
        },
        data: {
            displayName: "Search Results"

        }
    }).state('case.search.results.multi', {
        url: "/multi/:case_key",
        views: {
            'content@': {
                templateUrl: BasePath + "App/case/Views/multi/CaseMultiDetailsTemplate.html",
                controller: 'CaseMultiDetailsController'
            }
        },
        data: {
            displayName: "Multi"
        }
        // added for optimizatiom
    });
    $locationProvider.html5Mode(true);
});



// this is used for storing name and master id and then show the same in the header 
angular.module('case').controller('CaseheaderCtrl', ['$localStorage', '$scope', '$rootScope', '$window', 'CaseDataServices',
    function ($localStorage, $scope, $rootScope, $window, CaseDataServices) {
        $scope.pleaseWait = false;
        $rootScope.headerCaseKey;
        $rootScope.headerCaseName;
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
            return $localStorage.case_key;
        }, function (newMasterId, oldMasterId) {
            if (!angular.isUndefined(newMasterId)) {
                $scope.case_nm = $localStorage.case_nm;
                $scope.case_key = $localStorage.case_key;
                // $scope.display = 'block';
                $scope.display = 'none';
            }
            else {
                $scope.display = 'none';
            }
        });
       


        if ($localStorage.case_key) {

            $scope.case_nm = $localStorage.case_nm;
            $scope.case_key = $localStorage.case_key;
            //$scope.display = 'block';
            $scope.display = 'none';
           // HeaderOption = CaseDataServices.getHeaderCtrlDisplayStatus();
        }
        else
            $scope.display = 'none';


           
    }]);



angular.module('case').directive('scrolldown', [function () {
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

angular.module('case').controller('CaseTemplateCtrl', ['$scope', function ($scope) {

    $scope.baseUrl = BasePath;

    $scope.searchTemplateUrl = BasePath + 'App/Case/Views/CaseSearchTemplate.html';
    $scope.createTemplateUrl = BasePath + 'App/Case/Views/CaseCreateTemplate.html';
}]);