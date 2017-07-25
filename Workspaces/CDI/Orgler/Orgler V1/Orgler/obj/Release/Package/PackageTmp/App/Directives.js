
angular.module('common').directive('scrolldown', [function () {
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