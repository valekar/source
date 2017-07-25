using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Constituents
{
    public class PreferredComChannel
    {
        public IList<Entities.Constituents.PreferredComChannel> getPreferredComChannel(int NoOfRecs, int PageNum, string id)
        {
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteSqlQuery<Entities.Constituents.PreferredComChannel>(SQL.Constituents.PreferredComChannel.getPreferredComChannelSQL(NoOfRecs, PageNum, id)).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.PreferredComChannelOutput> addPreferredComChannel(Entities.Constituents.PreferredComChannelInput preferredComChannelInput)
        {
            string requestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.PreferredComChannel.addPreferredComChannelSQL(preferredComChannelInput, requestType, out strSPQuery, out listParam);
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.PreferredComChannelOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

        public IList<Entities.Constituents.PreferredComChannelOutput> updatePreferredComChannel(Entities.Constituents.PreferredComChannelInput preferredComChannelInput)
        {
            string requestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.PreferredComChannel.updatePreferredComChannelSQL(preferredComChannelInput, requestType, out strSPQuery, out listParam);
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.PreferredComChannelOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }


        public IList<Entities.Constituents.PreferredComChannelOutput> inactivatePreferredComChannel(Entities.Constituents.PreferredComChannelInput preferredComChannelInput)
        {
            string requestType = "Insert";
            string strSPQuery = string.Empty;
            List<object> listParam = new List<object>();
            SQL.Constituents.PreferredComChannel.inactivatePreferredComChannelSQL(preferredComChannelInput, requestType, out strSPQuery, out listParam);
            Repository rep = new Repository();
            var AcctLst = rep.ExecuteStoredProcedure<Entities.Constituents.PreferredComChannelOutput>(strSPQuery, listParam).ToList();
            return AcctLst;
        }

    }
}
