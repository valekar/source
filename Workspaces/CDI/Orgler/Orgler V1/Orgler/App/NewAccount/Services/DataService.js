/************************* Services - Data *************************/
newAccMod.factory("NewAccountDataService", [
    function () {
        //Variables to store data
        var NewAccountSearchInput = {};
        var NewAccountSearchResults = {};
        var uploadNaicsSuggetionsFormData = {};

        //Functions to set and get data
        return {
            setNewAccountSearchInput: function(data)
            {
                NewAccountSearchInput = data;
            },
            getNewAccountSearchInput: function () {
                return NewAccountSearchInput;
            },
            setNewAccountSearchResults: function (data) {
                NewAccountSearchResults = data;
            },
            getNewAccountSearchResults: function () {
                return NewAccountSearchResults;
            },            
            setUploadNaicsSuggetionsFormData: function (result) {
                uploadNaicsSuggetionsFormData = result;
            },
            getUploadNaicsSuggetionsFormData: function () {
                return uploadNaicsSuggetionsFormData;
            },
        }
}]);