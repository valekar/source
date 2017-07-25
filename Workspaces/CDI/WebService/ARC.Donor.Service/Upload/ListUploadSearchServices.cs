using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ARC.Donor.Service.Upload
{
    public class ListUploadSearchServices
    {
        public IList<ARC.Donor.Business.Upload.ListUploadOutput> getListUploadSearchResults(ARC.Donor.Business.Upload.ListOfListUploadSearchInput listInput)
        {
            Mapper.CreateMap<ARC.Donor.Business.Upload.ListUploadSearch, ARC.Donor.Data.Entities.Upload.ListUploadSearch>();
            Mapper.CreateMap<ARC.Donor.Business.Upload.ListOfListUploadSearchInput, ARC.Donor.Data.Entities.Upload.ListOfListUploadSearchInput>();
            Mapper.CreateMap<Data.Entities.Upload.ListUploadOutput, ARC.Donor.Business.Upload.ListUploadOutput>();

            var Input = Mapper.Map<ARC.Donor.Business.Upload.ListOfListUploadSearchInput, ARC.Donor.Data.Entities.Upload.ListOfListUploadSearchInput>(listInput);


            Data.Upload.ListUpload lisUpload= new Data.Upload.ListUpload();
            var searchResults = lisUpload.getListUploadDetails(Input);
            var result = Mapper.Map<IList<Data.Entities.Upload.ListUploadOutput>, IList<ARC.Donor.Business.Upload.ListUploadOutput>>(searchResults);
            return result;
            
        }
    }
}
