using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARC.Donor.Data.Entity
{
    //[Table("stwrd_dnr_prfle")]
    public class stwrd_dnr_prfle
    {
       // [Key]
        public string constituent_id {get;set;}
        public string cnst_dsp_id {get;set;}
        public string  name {get;set;} 
        public string first_name {get;set;} 
        public string  last_name {get;set;} 
        public string  constituent_type {get;set;}
        public string  phone_number {get;set;}
        public string  email_address {get;set;} 
        public string  addr_line_1 {get;set;} 
        public string  addr_line_2 {get;set;} 
        public string  city {get;set;} 
        public string  state_cd {get;set;}
        public string  zip {get;set;} 
    }
     

}
