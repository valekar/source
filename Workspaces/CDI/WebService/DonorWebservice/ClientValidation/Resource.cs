namespace DonorWebservice.ClientValidation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Resource
    {
        public Guid ClientID { get; set; }

        [Key]
        [StringLength(500)]
        public string ResourceName { get; set; }

        public bool? Active { get; set; }
    }
}
