adminMod.controller("TabSecurityEditCtrl", ['$scope', '$rootScope', 'params', '$location', '$uibModalInstance', 'TabSecurityApiServices', 'ErrorService','TabSecurityGridServices',
    function ($scope, $rootScope, params, $location, $uibModalInstance, TabSecurityApiServices, ErrorService, TabSecurityGridServices) {
      
        console.log(params);
        var row = {};
        var initialize = function () {
            row = params.row;
            $scope.tabSecurityEdit = {

                usr_nm: row.usr_nm,
                grp_nm: row.grp_nm,
                email_address:row.email_address,

                newaccount_tb_access_id: row.newaccount_tb_access,
                newaccount_tb_access_value: accessValue(row.newaccount_tb_access),

                topaccount_tb_access_id: row.topaccount_tb_access,
                topaccount_tb_access_value: accessValue(row.topaccount_tb_access),

                enterprise_orgs_tb_access_id: row.enterprise_orgs_tb_access,
                enterprise_orgs_tb_access_value: accessValue(row.enterprise_orgs_tb_access),

                constituent_tb_access_id: row.constituent_tb_access,
                constituent_tb_access_value: accessValue(row.constituent_tb_access),

                transaction_tb_access_id: row.transaction_tb_access,
                transaction_tb_access_value: accessValue(row.transaction_tb_access),

                upload_eosi_tb_access_id: row.upload_eosi_tb_access,
                upload_eosi_tb_access_value: accessValue(row.upload_eosi_tb_access),

                upload_affil_tb_access_id: row.upload_affil_tb_access,
                upload_affil_tb_access_value: accessValue(row.upload_affil_tb_access),

                upload_eo_tb_access_id: row.upload_eo_tb_access,
                upload_eo_tb_access_value: accessValue(row.upload_eo_tb_access),

                admin_tb_access_id: row.admin_tb_access,
                admin_tb_access_value: accessValue(row.admin_tb_access),             

                has_merge_unmerge_access_id:row.has_merge_unmerge_access,
                is_approver_id: row.is_approver,

                readAccess: TabSecurityApiServices.getReadAccessValues(),
                writeAccess: TabSecurityApiServices.getWriteAccessValues(),
                accessDropDown: []
            }
        

            if ($scope.tabSecurityEdit.grp_nm == 'Orgler') {
            $scope.tabSecurityEdit.accessDropDown = $scope.tabSecurityEdit.readAccess;
        }
        else {
            $scope.tabSecurityEdit.accessDropDown = $scope.tabSecurityEdit.writeAccess;
        }
    }

    initialize();

    $scope.tabSecurityEdit.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }


    $scope.tabSecurityEdit.submit = function () {
        myApp.showPleaseWait();
        if ($scope.myForm.$valid) {

            var postParams = {
                usr_nm:  $scope.tabSecurityEdit.usr_nm,
                grp_nm : $scope.tabSecurityEdit.grp_nm,
                email_address : $scope.tabSecurityEdit.email_address,
                telephone_number: $scope.tabSecurityEdit.telephone_number,
                newaccount_tb_access: $scope.tabSecurityEdit.newaccount_tb_access_id,
                topaccount_tb_access: $scope.tabSecurityEdit.topaccount_tb_access_id,
                enterprise_orgs_tb_access: $scope.tabSecurityEdit.enterprise_orgs_tb_access_id,
                constituent_tb_access : $scope.tabSecurityEdit.constituent_tb_access_id,
                transaction_tb_access :  $scope.tabSecurityEdit.transaction_tb_access_id,              
                admin_tb_access : $scope.tabSecurityEdit.admin_tb_access_id, 
                has_merge_unmerge_access : $scope.tabSecurityEdit.has_merge_unmerge_access_id,
                is_approver : $scope.tabSecurityEdit.is_approver_id,
                help_tb_access: row.help_tb_access,
                upload_eosi_tb_access: $scope.tabSecurityEdit.upload_eosi_tb_access_id,
                upload_affil_tb_access: $scope.tabSecurityEdit.upload_affil_tb_access_id,
                upload_eo_tb_access: $scope.tabSecurityEdit.upload_eo_tb_access_id
            }

            TabSecurityApiServices.editTabSecurity(postParams).then(function (result) {
                myApp.hidePleaseWait();
                //console.log(result);                  
                var result = {
                    message : "Request has been processed successfully",
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
                    message : "Unable to process the request"
                }
                ErrorService.messagePopup(result);
            });

        }
    }  

    function accessValue(tabAccess) {
        if(tabAccess == 'N'){
            return 'No Access';
        }
        else if(tabAccess == 'R'){
            return 'Read';
        }
        else if(tabAccess == 'RW'){
            return 'Read Write';
        }
    }

    }]);

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