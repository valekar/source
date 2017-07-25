angular.module('upload').factory('CommonUploadService', ['$rootScope', function ($rootScope) {
    return {
        clearFileInput: function () {
            var result = document.getElementsByClassName("file-input-label");         //angular.element("input[type='file']").val(null);
            angular.element(result).text('');
            angular.element("input[type='file']").val(null);
        },
        getUploadValidationGridLayout: function (gridOptions, uiGridConstants, results) {
            gridOptions.data = '';
            gridOptions.data.length = 0;
            gridOptions.data = results;

            return gridOptions;
        },
        messageUploadPopup: function (message, header) {
            $rootScope.uploadMessage = message;
            $rootScope.uploadHeader = header;
            angular.element("#iUploadErrorModal").modal();
        }
    }

}]);


function OpenUploadModal($scope, $uibModal, $stateParams, templ, ctrl, grid, row, size, $rootScope, $state, $uibModalStack, uiGridConstants, UploadDataServices) {

    var CssClass = '';
    if (size === 'lg') {
        CssClass = 'app-modal-window';
    }

    var ModalInstance = ModalInstance || $uibModal.open({
        animation: $scope.animationsEnabled,
        templateUrl: templ,
        controller: ctrl,  // Logic in instantiated controller 
        windowClass: CssClass,
        backdrop: 'static', /*  this prevent user interaction with the background  */
        keyboard: false
    });

    ModalInstance.result.then(function (result) {
        console.log("Modal Instance Result " + result);
        console.log("State Param before");
        console.log($state);
        modalMessage($rootScope, result, $state, $uibModalStack, UploadDataServices);
    })
}


var myApp;
myApp = myApp || (function () {
    var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="basicModal" aria-hidden="true" tabindex="-1"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h3>Processing...</h3></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar" style="width: 100%;"><span class="sr-only">60% Complete</span></div></div></div></div></div></div></div></div>');
    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal('show');
        },
        hidePleaseWait: function () {
            pleaseWaitDiv.modal('hide');
        },
    };
})();





