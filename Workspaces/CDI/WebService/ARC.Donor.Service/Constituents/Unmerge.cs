using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Constituents
{
    public class Unmerge
    {
        public IList<ARC.Donor.Business.Constituents.UnmergeSPOutput> unmerge(ARC.Donor.Business.Constituents.UnmergeInput UnmergeInput)
        {
            Mapper.CreateMap<Business.Constituents.UnMasterRequest, Data.Entities.Constituents.UnMasterRequest>();
            Mapper.CreateMap<Business.Constituents.UnmergeInput, Data.Entities.Constituents.UnmergeInput>();
            var Input = Mapper.Map<Business.Constituents.UnmergeInput, Data.Entities.Constituents.UnmergeInput>(UnmergeInput);
            Data.Constituents.Unmerge unmerge = new Data.Constituents.Unmerge();
            
            var AcctLst = unmerge.unmergeMasters(Input);
            Mapper.CreateMap<Data.Entities.Constituents.UnmergeSPOutput, Business.Constituents.UnmergeSPOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.UnmergeSPOutput>, IList<Business.Constituents.UnmergeSPOutput>>(AcctLst);
            return result;
        }
    }
}
