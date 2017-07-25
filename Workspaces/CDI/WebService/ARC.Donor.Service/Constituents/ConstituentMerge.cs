using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Constituents
{
    public class ConstituentMerge
    {
        public IList<ARC.Donor.Business.Constituents.CompareOutput> getCompareData(ARC.Donor.Business.Constituents.MasterDetailsInput MasterDetailsInput)
        {
            Mapper.CreateMap<Business.Constituents.MasterDetailsInput, Data.Entities.Constituents.MasterDetailsInput>();
            var Input = Mapper.Map<Business.Constituents.MasterDetailsInput, Data.Entities.Constituents.MasterDetailsInput>(MasterDetailsInput);
            Data.Constituents.ConstituentMerge cm = new Data.Constituents.ConstituentMerge();

            var AcctLst = cm.getCompareData(Input);
            Mapper.CreateMap<Data.Entities.Constituents.CompareOutput, Business.Constituents.CompareOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.CompareOutput>, IList<Business.Constituents.CompareOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.MergeSPOutput> mergeMasters(ARC.Donor.Business.Constituents.MergeInput MergeInput)
        {
            Mapper.CreateMap<Business.Constituents.MergeInput, Data.Entities.Constituents.MergeInput>();
            var Input = Mapper.Map<Business.Constituents.MergeInput, Data.Entities.Constituents.MergeInput>(MergeInput);
            Data.Constituents.ConstituentMerge cm = new Data.Constituents.ConstituentMerge();

            var AcctLst = cm.mergeMasters(Input);
            Mapper.CreateMap<Data.Entities.Constituents.MergeSPOutput, Business.Constituents.MergeSPOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MergeSPOutput>, IList<Business.Constituents.MergeSPOutput>>(AcctLst);
            return result;
        }

        public IList<ARC.Donor.Business.Constituents.MergeSPOutput> submitMergeConflicts(ARC.Donor.Business.Constituents.MergeConflictInput MergeInput)
        {
            Mapper.CreateMap<Business.Constituents.MergeConflictInput, Data.Entities.Constituents.MergeConflictInput>();
            var Input = Mapper.Map<Business.Constituents.MergeConflictInput, Data.Entities.Constituents.MergeConflictInput>(MergeInput);
            Data.Constituents.ConstituentMerge cm = new Data.Constituents.ConstituentMerge();

            var AcctLst = cm.submitMergeConflicts(Input);
            Mapper.CreateMap<Data.Entities.Constituents.MergeSPOutput, Business.Constituents.MergeSPOutput>();
            var result = Mapper.Map<IList<Data.Entities.Constituents.MergeSPOutput>, IList<Business.Constituents.MergeSPOutput>>(AcctLst);
            return result;
        }
    }
}
