adminMod.controller("TabSecurityDeleteCtrl", ['$scope', '$rootScope', 'params', '$uibModalInstance', 'TabSecurityApiServices', 'ErrorService',
function ($scope,$rootScope, params, $uibModalInstance, TabSecurityApiServices, ErrorService) {
    var row = {};
        console.log(params);
        var initialize = function () {
            row = params.row;
            $scope.tabSecurityDelete = {

                usr_nm: row.usr_nm,
                grp_nm: row.grp_nm,
                email_address: row.email_address,


                admin_tb_access_id: row.admin_tb_access,
                admin_tb_access_value: accessValue(row.admin_tb_access),

                newaccount_tb_access_id: row.newaccount_tb_access,
                newaccount_tb_access_value: accessValue(row.newaccount_tb_access),

                topaccount_tb_access_id: row.topaccount_tb_access,
                topaccount_tb_access_value: accessValue(row.topaccount_tb_access),

                enterprise_orgs_tb_access_id: row.enterprise_orgs_tb_access,
                enterprise_orgs_tb_access_value: accessValue(row.enterprise_orgs_tb_access),

                constituent_tb_access_id: row.constituent_tb_access,
                constituent_tb_access_value: accessValue(row.constituent_tb_access),
                //domn_corctn_access_id: row.domn_corctn_access,
                has_merge_unmerge_access_id: row.has_merge_unmerge_access,
                is_approver_id: row.is_approver,

                //locator_tab_access_id:row.locator_tab_access,
                transaction_tb_access_id: row.transaction_tb_access,
                transaction_tb_access_value: accessValue(row.transaction_tb_access),

                upload_eosi_tb_access_id: row.upload_eosi_tb_access,
                upload_eosi_tb_access_value: accessValue(row.upload_eosi_tb_access),

                upload_affil_tb_access_id: row.upload_affil_tb_access,
                upload_affil_tb_access_value: accessValue(row.upload_affil_tb_access),

                upload_eo_tb_access_id: row.upload_eo_tb_access,
                upload_eo_tb_access_value: accessValue(row.upload_eo_tb_access)
            }
        }
        initialize();

        $scope.tabSecurityDelete.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }


        $scope.tabSecurityDelete.submit = function () {
            myApp.showPleaseWait();
            if ($scope.myForm.$valid) {

                var postParams = {
                    usr_nm: $scope.tabSecurityDelete.usr_nm,
                    grp_nm: $scope.tabSecurityDelete.grp_nm,
                    email_address: $scope.tabSecurityDelete.email_address,
                    telephone_number: $scope.tabSecurityDelete.telephone_number,
                    newaccount_tb_access: $scope.tabSecurityDelete.newaccount_tb_access_id,
                    topaccount_tb_access: $scope.tabSecurityDelete.topaccount_tb_access_id,
                    enterprise_orgs_tb_access: $scope.tabSecurityDelete.enterprise_orgs_tb_access_id,
                    constituent_tb_access: $scope.tabSecurityDelete.constituent_tb_access_id,
                    transaction_tb_access: $scope.tabSecurityDelete.transaction_tb_access_id,                   
                    admin_tb_access: $scope.tabSecurityDelete.admin_tb_access_id,
                    domn_corctn_access: row.domn_corctn_access,
                    has_merge_unmerge_access: $scope.tabSecurityDelete.has_merge_unmerge_access_id,
                    is_approver: $scope.tabSecurityDelete.is_approver_id,                   
                    help_tb_access: row.help_tb_access,
                    upload_eosi_tb_access: $scope.tabSecurityDelete.upload_eosi_tb_access_id,
                    upload_affil_tb_access: $scope.tabSecurityDelete.upload_affil_tb_access_id,
                    upload_eo_tb_access: $scope.tabSecurityDelete.upload_eo_tb_access_id
                }

                TabSecurityApiServices.deleteTabSecurity(postParams).then(function (result) {
                    myApp.hidePleaseWait();
                    //console.log(result);
                    var result = {
                        message: "Request has been processed successfully",
                        header: result[0].o_transOutput
                    }
                    //$rootScope.inialize();
                    $uibModalInstance.close(result);

                },
                function (error) {
                    myApp.hidePleaseWait();
                    $uibModalInstance.dismiss('cancel');
                    var result = {
                        header: error.data.o_transOutput,
                        message: "Unable to process the request"
                    }
                    ErrorService.messagePopup(result);
                });

            }
        }



        function accessValue(tabAccess) {
            if (tabAccess == 'N') {
                return 'No Access';
            }
            else if (tabAccess == 'R') {
                return 'Read';
            }
            else if (tabAccess == 'RW') {
                return 'Read Write';
            }
        }
        var myApp;
myApp = myApp || (function () {
    //var pleaseWaitDiv = $('<div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false"><div class="modal-header"><h1>Processing...</h1></div><div class="modal-body"><div class="progress progress-striped active"><div class="bar" style="width: 100%;"></div></div></div></div>');
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

}]);

