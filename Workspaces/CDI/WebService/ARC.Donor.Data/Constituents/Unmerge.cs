using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARC.Donor.Data.Entities.Constituents;

namespace ARC.Donor.Data.Constituents
{
    public class Unmerge
    {
        /* Method to get the submit the unmerge requests
         * Input Parameters : UnmergeInput object
         * Output Parameter : Output variables from the Unmerge SP
         */
        public IList<UnmergeSPOutput> unmergeMasters(UnmergeInput UnmergeInput)
        {
            Repository rep = new Repository();
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();

            SQL.Constituents.Unmerge.getUnmergeParameters(UnmergeInput, out strSPQuery, out listParam);
            var UnmergeLst = rep.ExecuteStoredProcedure<Entities.Constituents.UnmergeSPOutput>(strSPQuery, listParam).ToList();
            return UnmergeLst;
        }
    }
}
