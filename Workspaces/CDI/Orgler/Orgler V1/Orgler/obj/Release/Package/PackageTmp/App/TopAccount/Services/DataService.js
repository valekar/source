/************************* Services - Data *************************/
topAccMod.factory("TopAccountDataService", [
    function () {
        //Variables to store data
        var TopAccountSearchInput = {};
        var TopAccountSearchResults = {};
        var uploadNaicsSuggetionsFormData = {};

        //Functions to set and get data
        return {
            setTopAccountSearchInput: function(data)
            {
                TopAccountSearchInput = data;
            },
            getTopAccountSearchInput: function () {
                return TopAccountSearchInput;
            },
            setTopAccountSearchResults: function (data) {
                TopAccountSearchResults = data;
            },
            getTopAccountSearchResults: function () {
                return TopAccountSearchResults;
            },            
            setUploadNaicsSuggetionsFormData: function (result) {
                uploadNaicsSuggetionsFormData = result;
            },
            getUploadNaicsSuggetionsFormData: function () {
                return uploadNaicsSuggetionsFormData;
            },
        }
}]);