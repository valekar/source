
//Directives for New Accounts Tab
newAccMod.filter('placeholderfunc', function ($rootScope) {
    return function (input) {
        var ResultsBindingData = "";

        if (input.los != null)
            ResultsBindingData += "LOB: " + input.los + ", ";
        if (input.createdDateFrom != null)
            ResultsBindingData += "Account Creation Date - From: " + input.createdDateFrom + ", ";
        if (input.createdDateTo != null)
            ResultsBindingData += "Account Creation Date - To: " + input.createdDateTo + ", ";
        if (input.naicsStatus != null)
            ResultsBindingData += "NAICS Stewarding Status: " + input.naicsStatus + ", ";
        if (input.masteringType != null)
            ResultsBindingData += "Mastering Type: " + input.masteringType + ", ";
        if (input.enterpriseOrgAssociation != null)
            ResultsBindingData += "Enterprise Org Type: " + input.enterpriseOrgAssociation + ", ";

        ResultsBindingData += ";"
        if (ResultsBindingData != "")
            return ResultsBindingData.slice(0, -3);
        else
            return ResultsBindingData;

    }
});

//Directives for New Accounts Tab
newAccMod.filter('placeholderfuncNAICS', function ($rootScope) {
    return function (input) {
        var ResultsBindingData = "";

        if (input.naics_codes != null)
            ResultsBindingData += "NAICS Code: " + input.naics_codes + ", ";
        if (input.naics_titles != null)
            ResultsBindingData += "NAICS Title: " + input.naics_titles + ", ";
        //if (input.naics_status != null)
        //    ResultsBindingData += "naics_status: " + input.naics_status + ", ";       

        ResultsBindingData += ";"
        if (ResultsBindingData != "")
            return ResultsBindingData.slice(0, -3);
        else
            return ResultsBindingData;

    }
});
//Upload files event handler
newAccMod.directive('ngFiles', ['$parse', function ($parse) {

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
newAccMod.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;
            
            element.bind('change', function(){
                scope.$apply(function(){
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);