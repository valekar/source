using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARC.Donor.Business.Case
{
    public class CompositeCreateCaseInput
    {
        public CreateCaseInput CreateCaseInput { get; set; }
        public SaveCaseSearchInput SaveCaseSearchInput { get; set; }
    }
}
