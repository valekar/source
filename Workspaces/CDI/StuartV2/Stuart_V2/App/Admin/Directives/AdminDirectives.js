angular.module('admin').directive('scrolldown', [function () {
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




angular.module('admin').directive('customUiGridHeader', [function () {
    return {
        link: function (scope, element, attr) {
            console.log("Heelo");
            var top = angular.element('#divMenu').offset().top - parseFloat(angular.element('#divMenu').css('marginTop').replace(/auto/, 100)) + 300;//why 300 becuase that is what is set in the _layout.cshtml page
           
            $(window).scroll(function (event) {
                // what the y position of the scroll is
                var y = angular.element(this).scrollTop();


                if (y >= top) {
                    if (element.find(".ui-grid-top-panel").length != 0) {
                                              
                        element.find(".ui-grid-top-panel").addClass('fixed');
                        element.find(".ui-grid-menu-button").addClass('fixed');
                        element.find(".ui-grid-column-menu").addClass('fixed');
                    }


                } else {
                    element.find(".ui-grid-top-panel").removeClass('fixed');
                    element.find(".ui-grid-menu-button").removeClass('fixed');
                    element.find(".ui-grid-column-menu").removeClass('fixed');
                    
                }

            });
        }
    }
    

}]);