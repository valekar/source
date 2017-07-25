angular.module('transaction').directive('ngFiles', ['$parse', function ($parse) {

    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on('change', function (event) {
            onChange(scope, { $files: event.target.files });
        });
    };

    return {
        link: fn_link
    }
}]);


angular.module('transaction').filter('transformationConditionType', [function () {
    return function (code){
        switch (code)
        {
            case 'C': return 'Contains'; break;
            case 'D': return 'Does not contain'; break;
            case 'E': return 'Exact match'; break;
            case 'F': return 'Starts with'; break;
        }
        return '';
    }
}]);

angular.module('transaction').directive('ngFiles', ['$parse', function ($parse) {

    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on('change', function (event) {
            onChange(scope, { $files: event.target.files });
        });
    };

    return {
        link: fn_link
    }
}]);