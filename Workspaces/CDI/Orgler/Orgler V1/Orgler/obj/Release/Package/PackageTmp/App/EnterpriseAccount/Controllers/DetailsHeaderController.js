BasePath = $("base").first().attr("href");

//Details Controller
enterpriseAccMod.controller("enterpriseOrgDetailsHeaderCtrl", ['$scope', '$stateParams',
    function ($scope, $stateParams) {

        //Get the selected enterprise org id from the url
        $scope.selectedEntOrg = $stateParams.ent_org_id;
        $scope.selectedEntOrgNm = sessionStorage.ent_org_name;
    }]);