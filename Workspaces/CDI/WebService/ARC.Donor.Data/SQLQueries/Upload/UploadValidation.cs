using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.SQL.Upload
{
    public class UploadValidation
    {
        public static string getMasterIdValidationSQL(List<string> _masterIds)
        {
            var _processedMasterIds = string.Join(",", _masterIds);

            return string.Format(strMasterIdValidationQuery, string.Join(",", _processedMasterIds));
        }

        static readonly string strMasterIdValidationQuery = @" SELECT DISTINCT cnst_mstr_id from 
            arc_mdm_vws.bzal_cnst_mstr where row_stat_cd <> 'L' and cnst_mstr_id in ({0})";


        public static string getChapterCodeValidationSQL(List<string> _chapterCodes)
        {
            var _inputChapterCodes = string.Join(",", _chapterCodes);
            string replaced = "'" + _inputChapterCodes.Replace(",", "','") + "'";
            return string.Format(strChapterCodeValidationQuery, string.Join(",", replaced));
        }

        static readonly string strChapterCodeValidationQuery = @" SEL DISTINCT chpt_cd, appl_src_cd from 
            dw_stuart_vws.cdc_data_src_tracking where chpt_cd in ({0})";

        public static string getGroupCodeValidationSQL(List<string> _groupCodes)
        {
            var _inputGroupCodes = string.Join(",", _groupCodes);
            string replaced = "'" + _inputGroupCodes.Replace(",", "','") + "'";
            return string.Format(strGroupCodeValidationQuery, string.Join(",", replaced));
        }

        static string strGroupCodeValidationQuery = @" SEL DISTINCT grp_cd, grp_nm FROM 
            dw_stuart_vws.bz_grp_ref where grp_cd in ({0}) ";

        public static string getNkecodeValidationSQL(List<string> _chapterCodes)
        {
            var _inputNkecodes = string.Join(",", _chapterCodes);
            string replaced = "'" + _inputNkecodes.Replace(",", "','") + "'";
            return string.Format(strNkecodeValidationQuery, replaced);
        }

        static string strNkecodeValidationQuery = @" SELECT DISTINCT nk_ecode FROM 
            dw_stuart_vws.dim_unit where nk_ecode in ({0}); ";
    }
}
