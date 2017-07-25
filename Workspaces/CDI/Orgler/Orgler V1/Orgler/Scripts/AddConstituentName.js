angular.module('plunker', ['ui.bootstrap']);
var ModalDemoCtrl = function ($scope, $modal, $log) {


    $scope.lositems = ['1', '0'];
    $scope.sourcesystemitems = ['TAFS', 'SFFS', 'CDIM'];
    $scope.nametypeitems = ['PN', 'AN', 'LN'];
    $scope.animationsEnabled = true;

    $scope.status = {
        isopen: false
    };

    $scope.toggled = function (open) {
        $log.log('Dropdown is now: ', open);
    };

    $scope.toggleDropdown = function ($event) {

        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.isopen = !$scope.status.isopen;
    };

    $scope.appendToEl = angular.element(document.querySelector('#dropdown-long-content'));
    $scope.open = function () {

        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: ModalInstanceCtrl,
            resolve: {
                lositems: function () {
                    return $scope.lositems;

                },
                sourcesystemitems: function () {
                    return $scope.sourcesystemitems;

                },
                nametypeitems: function () {
                    return $scope.nametypeitems;
                }
            }
        });

        modalInstance.result.then(function (result) {
            $scope.selected = result;
        })

    };
};

var ModalInstanceCtrl = function ($scope, $modalInstance, lositems, sourcesystemitems, nametypeitems) {
    $scope.lositems = lositems;
    $scope.sourcesystemitems = sourcesystemitems;
    $scope.nametypeitems = nametypeitems;
    $scope.selected = {
        lositem: $scope.lositems[0],
        sourcesystemitem: $scope.sourcesystemitems[0],
        nametypeitem: $scope.nametypeitems[0]
    };


    $scope.submit = function () {

        if ($scope.myForm.$valid) {
            var postParams = {
                "masterid": $scope.masterid,
                "prefixname": $scope.prefixname,
                "firstname": $scope.firstname,
                "lastname": $scope.lastname,
                "middlename": $scope.middlename,
                "suffixname": $scope.suffixname,
                "nickname": $scope.nickname,
                "maidenname": $scope.maidenname,
                "BestLOS": $scope.selected.lositem,
                "SourceSystemCd": $scope.selected.sourcesystemitem,
                "NameTypeCd": $scope.selected.nametypeitem
            };
            //do something
            $.ajax({
                type: "POST",
                data: JSON.stringify(postParams),
                url: "/api/DataApi/PostAddConstPersonName",
                contentType: "application/json"
            });
            $modalInstance.close(postParams);
        }

    };

};
