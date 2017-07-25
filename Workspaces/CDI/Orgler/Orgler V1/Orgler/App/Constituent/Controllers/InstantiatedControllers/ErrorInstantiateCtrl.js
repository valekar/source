angular.module('constituent').controller('ErrorInstantiateCtrl', ['$scope', '$uibModalInstance', 'params', function ($scope, $uibModalInstance, params) {

    $scope.error = {
        header: params.header,
        message:params.message
    };

    $scope.close = function () {
        $uibModalInstance.dismiss();
    }

}]);