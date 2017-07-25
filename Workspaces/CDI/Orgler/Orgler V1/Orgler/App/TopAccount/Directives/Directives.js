
//Directives for Top Accounts Tab
topAccMod.filter('topaccplaceholderfunc', function ($rootScope) {
    return function (input) {
        var ResultsBindingData = "";

        if (input.los != null)
            ResultsBindingData += "LOB: " + input.los + ", ";
        if (input.rfm_scr != null)
            ResultsBindingData += "RFM Score: " + input.rfm_scr + ", ";
        if (input.listNaicsCodes != null)
            ResultsBindingData += "NAICS Segments: " + input.listNaicsCodes + ", ";
        if (input.naicsStatus != null)
            ResultsBindingData += "NAICS Stewarding Status: " + input.naicsStatus + ", ";       
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
topAccMod.filter('placeholderfuncNAICS', function ($rootScope) {
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
topAccMod.directive('ngFiles', ['$parse', function ($parse) {

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
topAccMod.directive('validfile', function validFile() {

    var validFormats = ['xls', 'xlsx'];
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            ctrl.$validators.validFile = function () {
                elem.on('change', function () {
                    var value = elem.val(),
                        ext = value.substring(value.lastIndexOf('.') + 1).toLowerCase();

                    return validFormats.indexOf(ext) !== -1;
                });
            };
        }
    };
});