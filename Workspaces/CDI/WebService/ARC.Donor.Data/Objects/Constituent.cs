using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Data.Objects
{
    public class Constituent: Interfaces.IConstituent
    {

        private IUnitOfWork uOfwork;
        private IRepository<ARC.Donor.Data.Entity.stwrd_dnr_prfle> profileRep;
        private IRepository<ARC.Donor.Data.ComplexEntity.ConsSearchResults> srchResultsRep;
        public Constituent(IUnitOfWork uOfwork)
        {
            this.uOfwork = uOfwork;
            this.profileRep = this.uOfwork.Repository<ARC.Donor.Data.Entity.stwrd_dnr_prfle>();
            this.srchResultsRep = this.uOfwork.Repository<ARC.Donor.Data.ComplexEntity.ConsSearchResults>();
        }       
       

    }
}
