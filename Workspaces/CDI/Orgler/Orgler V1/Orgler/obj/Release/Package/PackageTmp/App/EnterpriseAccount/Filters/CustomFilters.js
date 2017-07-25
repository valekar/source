enterpriseAccMod.filter('standardizeScore', function () {
    return function (data) {
        var result = 0;
        if (data != null && data != '' && angular.isDefined)
            result = data;
        return result;
    }
});

enterpriseAccMod.filter('removeAgg', function () {
    return function (input) {
        if (input != null && input != '' && angular.isDefined(input) && input != " null" && input != "max: null")
            input = input.replace("max:", "");
        else
            input = '';
        return input;
    }
});

enterpriseAccMod.filter('removeAggHeader', function () {
    return function (input) {
        if (input != null && input != '' && angular.isDefined(input) && input != " null" && input != "max: null") {
            input = input.replace("max:", "");
            input = input.slice(0, -(input.length - input.indexOf('| (')));
        }
        else
            input = '';
        return input;
    }
});

enterpriseAccMod.filter('removeAggCustom', function () {
    return function (input,defaultValue) {
        if (input != null && input != '' && angular.isDefined(input) && input != " null" && input != "max: null")
            input = input.replace("max:", "");
        else
            input = defaultValue;
        return input;
    }
});

//used in constituent helper start date columns
enterpriseAccMod.filter('column_date_filter', ['$filter', function ($filter) {
    return function (value) {
        if (value != null) {
            var localDate = new Date(value);
            var localTime = localDate.getTime();
            var localOffset = localDate.getTimezoneOffset() * 60000;
            //return $filter('date')(new Date(localTime + localOffset), 'yyyy-MM-dd');
            return $filter('date')(localDate, 'yyyy-MM-dd');
        }
        else
            return null;
    }
}]);