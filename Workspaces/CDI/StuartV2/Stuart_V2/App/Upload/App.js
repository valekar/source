var UploadModule = angular.module('upload', ['common', 'ui.grid', 'ui.grid.selection', 'ui.grid.autoResize', 'angularUtils.directives.uiBreadcrumbs', 
    'ui.bootstrap', 'ui.grid.pagination'])
.config(function ($urlRouterProvider, $stateProvider, $locationProvider, $localStorageProvider) {

    $urlRouterProvider.otherwise("/upload/groupmembershipupload");
    $stateProvider.state('upload', {
        url: '/upload',
        views: {
            'header': {
                templateUrl: BasePath + 'App/Upload/Views/header.html',
                controller: 'UploadheaderCtrl'
            },
            'content': {//Written in an absolute syntax
                templateUrl: BasePath + 'App/Upload/Views/content.html'
            }
        },
        data: {
            displayName: "Upload"
        }
    })
    .state('upload.groupmembershipreferencetable', {
        url: "/groupmembershipreferencetable",
        views: {
            'content@': {//Written in an absolute syntax
                templateUrl: BasePath + "App/Upload/Views/GroupMembershipReferenceTable.html",
                controller: 'GroupMembershipReferenceController'
            },
        },
        data: {
            displayName: "Group Membership Reference Data Table"
        }
    })
    .state('upload.listUpload', {
        url: "/listUpload",
        views: {
            'content@': {//Written in an absolute syntax
                templateUrl: BasePath + "App/Upload/Views/ListUploadSearch.tpl.html",
                controller: 'ListUploadSearchCtrl'
            },
        },
        data: {
            displayName: "List Upload Search"
        }
        
    })
         .state('upload.listUpload.results', {
             url: "/results",
             views: {
                 'content@': {//Written in an absolute syntax
                     templateUrl: BasePath + "App/Upload/Views/ListUploadSearchResults.tpl.html",
                     controller: 'ListUploadSearchResultsCtrl'
                 },
             },
             data: {
                 displayName: "List Upload Search Results"
             }

         })
        .state('upload.listUpload.results.details', {
            url: "/details",
            views: {
                'content@': {//Written in an absolute syntax
                    templateUrl: BasePath + "App/Upload/Views/ListUploadTransDetails.tpl.html",
                    controller: 'ListUploadTransDetailsCtrl'
                },
            },
            data: {
                displayName: "List Upload Transaction Details"
            }

        })
        .state('upload.emailonlyupload', {
            url: "/emailonlyupload",
            views: {
                'content@': {//Written in an absolute syntax
                    templateUrl: BasePath + "App/Upload/Views/EmailOnlyUpload.html",
                    controller: 'EmailOnlyUploadController'
                },
            },
            data: {
                displayName: "Email Only Upload"
            }
        })
        .state('upload.groupmembershipupload', {
            url: "/groupmembershipupload",
            views: {
                'content@': {//Written in an absolute syntax
                    templateUrl: BasePath + "App/Upload/Views/GroupMembershipUpload.html",
                    controller: 'GroupMembershipUploadController'
                },
            },
            data: {
                displayName: "Group Membership Upload"
            }
        })
       .state('upload.nameandemailupload', {
           url: "/nameandemailupload",
        views: {
            'content@': {//Written in an absolute syntax
                templateUrl: BasePath + "App/Upload/Views/NameAndEmailUpload.html",
                controller: 'NameAndEmailUploadController'
            },
        },
        data: {
            displayName: "Name and Email Upload"
        }
       })
        .state('upload.dncUpload', {
            url: "/dncUpload",
            views: {
                'content@': {//Written in an absolute syntax
                    templateUrl: BasePath + "App/Upload/Views/dnc/DncUpload.tpl.html",
                    controller: 'DNCUploadCtrl'
                },
            },
            data: {
                displayName: "DNC Upload"
            }
        })
        .state('upload.msgPrefUpload', {
            url: "/msgPrefUpload",
            views: {
                'content@': {//Written in an absolute syntax
                    templateUrl: BasePath + "App/Upload/Views/msgPref/MsgPref.tpl.html",
                    controller: 'MsgPrefUploadCtrl'
                },
            },
            data: {
                displayName: "Message Preference Upload"
            }
        });

        $locationProvider.html5Mode(true);
});

// this is used for storing name and master id and then show the same in the header 
angular.module('upload').controller('UploadheaderCtrl', ['$localStorage', '$scope', '$rootScope', '$window',
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