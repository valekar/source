angular.module('constituent').directive('modal', [function () {
    return {
        template: '<div class="modal fade">' +
            '<div class="modal-dialog">' +
              '<div class="modal-content">' +
                '<div class="modal-header">' +
                  '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
                  '<h4 class="modal-title">{{ title }}</h4>' +
                '</div>' +
                '<div class="modal-body" ng-transclude></div>' +
              '</div>' +
            '</div>' +
          '</div>',
        restrict: 'E',
        transclude: true,
        replace: true,
        scope: true,
        link: function postLink(scope, element, attrs) {
            scope.title = attrs.title;

            scope.$watch(attrs.visible, function (value) {
                if (value == true)
                    $(element).modal('show');
                else
                    $(element).modal('hide');
            });

            $(element).on('shown.bs.modal', function () {
                scope.$apply(function () {
                    scope.$parent[attrs.visible] = true;
                });
            });

            $(element).on('hidden.bs.modal', function () {
                scope.$apply(function () {
                    scope.$parent[attrs.visible] = false;
                });
            });
        }
    };
}]);

angular.module('constituent').directive('menuPreference', [function () {
    return {
        restrict: "A",
        transclude:true,
        templateUrl: BasePath + "App/Constituent/Views/multi/MenuPrefPopup.html",
        link: function (scope, element, attr) {
            var _element;
            var ele;
            element.on('click', function (event) {
                ele = element;
                _element = ele.parent().children(0).children(1).eq(1);
                _element.addClass("popover fade right in");
                _element.css("display", "block");

            });

            scope.closePreferences = function () {
                console.log(_element);
                _element.css("display", "none");
            };
           
        }
    }
}]);

angular.module('constituent').directive('ngFiles', ['$parse', function ($parse) {

    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on('change', function (event) {
            onChange(scope, { $files: event.target.files });
        });
    };

    return {
        link: fn_link
    }
} ])

angular.module('constituent').directive('custommenupref', [function () {
    return {
        link: function (scope, element, attr) {
            element.addClass("custom-popover");

        }
    }

}]);


angular.module('constituent').directive('scrolldown', [function () {
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