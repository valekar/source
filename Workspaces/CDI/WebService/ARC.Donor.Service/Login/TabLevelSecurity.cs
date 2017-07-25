using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Login
{
    public class UserTabLevelSecurity
    {
        public void addUserTabLevelSecurity(ARC.Donor.Business.Login.UserTabLevelSecurity userTabLevelSecurity)
        {
            Mapper.CreateMap<Business.Login.UserTabLevelSecurity, Data.Entities.Login.UserTabLevelSecurity>();
            var Input = Mapper.Map<Business.Login.UserTabLevelSecurity, Data.Entities.Login.UserTabLevelSecurity>(userTabLevelSecurity);
            Data.Login.UserTabLevelSecurity cm = new ARC.Donor.Data.Login.UserTabLevelSecurity();

            var AcctLst = cm.addUserTabLevelSecurity(Input);
        }
    }
}
