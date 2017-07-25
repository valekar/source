using System.Web;
using System.Web.Optimization;

namespace Orgler
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/ui-grid.min.js"));

            bundles.Add(new ScriptBundle("~/App/account.js").Include(
                     //"~/App/Constituent/App.js",

                     "~/App/AccountMonitoring/Directives/ConstituentDirectives.js",
                     "~/App/AccountMonitoring/Services/ConstituentServices.js",
                    "~/App/AccountMonitoring/Services/ConstituentMultiService.js",
                     "~/App/AccountMonitoring/Services/ConstituentHelper.js",
                     "~/App/AccountMonitoring/Services/CRUDService.js",

                     "~/App/AccountMonitoring/Controllers/ConstituentController.js",
                     "~/App/AccountMonitoring/Controllers/ConstSearchResultsController.js",
                     "~/App/AccountMonitoring/Controllers/ConstituentDetailsController.js",
                     "~/App/AccountMonitoring/Controllers/ConstMultiDetailsController.js",
                     "~/App/AccountMonitoring/Controllers/ConstHomeDetailsController.js",

                     "~/App/AccountMonitoring/Controllers/ConstCartController.js",
                     "~/App/AccountMonitoring/Controllers/ConstMergeController.js",
                     "~/App/AccountMonitoring/Controllers/ConstUnmergeController.js",

                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/ErrorInstantiateCtrl.js",

                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/NameInstantiateControllers.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/PhoneInstantiateCtrl.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/EmailInstantiateCtrl.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/DeathInstantiateCtrl.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/BirthInstantiateCtrl.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/AddressInstantiateControllers.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/ContactPrefInstantiateCtrl.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/GroupInstantiateCtrl.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/CharacterInstantiateCtrl.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/CaseCtrl.js",
                     "~/App/AccountMonitoring/Controllers/InstantiatedControllers/AffiliatorCtrl.js"
                    ));

            bundles.Add(new ScriptBundle("~/App/transaction.js").Include(

                    "~/App/Transaction/Directives/TransactionDirectives.js",

                    "~/App/Transaction/Services/TransactionServices.js",
                    "~/App/Transaction/Services/TransactionMultiServices.js",
                    "~/App/Transaction/Services/CaseService/CaseServices.js",
                    "~/App/Transaction/Services/TransactionHelper.js",

                    "~/App/Transaction/Controllers/TransactionSearchController.js",
                    "~/App/Transaction/Controllers/TransactionSearchController.js",
                    "~/App/Transaction/Controllers/TransactionSearchResultsController.js",

                    "~/App/Transaction/Controllers/TransactionMultiDetailsController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsBirthController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsPersonNameController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsOrgNameController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsAddressController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsAffiliatorHierarchyController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsAffiliatorTagsController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsAffiliatorController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsCharacteristicsController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsContactPreferenceController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsEmailController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsEmailOnlyUploadController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsMergeController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsGroupMembershipUploadController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsOrgTransformsController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsPhoneController.js",

                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsDeathController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/TransactionDetailsUnmergeController.js",
                    "~/App/Transaction/Controllers/InstantiatedControllers/AssociateCaseCtrl.js"

                    ));

            bundles.Add(new ScriptBundle("~/App/case.js").Include(
                "~/App/Case/Services/CaseHelper.js",
                "~/App/Case/Services/CaseServices.js",

                "~/App/Case/Services/CaseDetailsServices.js",
                "~/App/Case/Controllers/CaseDetailsController.js",
                "~/App/Case/Services/CaseCRUDService.js",


                "~/App/Case/Controllers/CaseSearchController.js",
                "~/App/Case/Controllers/CaseSearchResultsController.js",
                "~/App/Case/Controllers/CaseCreateController.js",
                "~/App/Case/Controllers/Instantiated Controllers/CaseInfoInstantiatedController.js",
                "~/App/Case/Controllers/Instantiated Controllers/CaseLocInfoInstantiatedController.js",
                "~/App/Case/Controllers/Instantiated Controllers/CaseNotedInstantiatedController.js"
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
