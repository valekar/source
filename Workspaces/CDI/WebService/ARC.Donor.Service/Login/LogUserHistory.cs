using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Login
{
    public class LogUserHistory
    {
        public void insertLoginHistory(ARC.Donor.Business.Login.LoginHistoryInput LoginHistoryInput)
        {
            Mapper.CreateMap<Business.Login.LoginHistoryInput, Data.Entities.Login.LoginHistoryInput>();
            var Input = Mapper.Map<Business.Login.LoginHistoryInput, Data.Entities.Login.LoginHistoryInput>(LoginHistoryInput);
            Data.Login.LogUserHistory cm = new ARC.Donor.Data.Login.LogUserHistory();

            var AcctLst = cm.insertLoginHistory(Input);
        }
    }
}
