upldModule.factory('uploadDataServices', [function () {
    var loggedInUserName = null;
    var formData = null;
    var uploadResult = {};

    return {
        setUserName: function (data) {
            loggedInUserName = data;
        },
        getUserName: function () {
            return loggedInUserName;
        },
        setFormData: function (data) {
            formData = data;
        },
        getFormData: function () {
            return formData;
        },
        setuploadResult: function (data) {
            uploadResult = data;
        },
        getuploadResult: function () {
            return uploadResult;
        },
        clearFileInput: function () {
            var result = document.getElementsByClassName("file-input-label");
            angular.element(result).text('');
            angular.element("input[type='file']").val(null);
        }
    }
}]);