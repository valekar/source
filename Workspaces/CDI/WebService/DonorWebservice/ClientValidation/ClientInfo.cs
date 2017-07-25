namespace DonorWebservice.ClientValidation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientInfo")]
    public partial class ClientInfo
    {
        [Key]
        public Guid ClientID { get; set; }

        [StringLength(1000)]
        public string ClientSecret { get; set; }

        [Column("Client Name")]
        [StringLength(200)]
        public string Client_Name { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool Active { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(20)]
        public string IPAddresss { get; set; }

        [StringLength(250)]
        public string DNSName { get; set; }

        public bool? IsIPRequired { get; set; }

        public bool? IsClientIDRequired { get; set; }
    }
}
