namespace DonorWebservice.ClientValidation
{ using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QueryTimeLogger")]
    public partial class QueryTimeLogger
    {
        [Key]
        public Int64 Id { get; set; }

        [Column("UserName")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column("Action")]
        [StringLength(150)]
        public string Action { get; set; }

        [Column("Query")]
        //[StringLength(3000)]
        public string Query { get; set; }

        [Column("StartTime")]
        public DateTime StartTime { get; set; }

        [Column("EndTime")]
        public DateTime? EndTime { get; set; }
    }
    
}