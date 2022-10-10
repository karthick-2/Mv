namespace Mv.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("webtbluser")]
    public partial class webtbluser
    {
        public int id { get; set; }

        [StringLength(50)]
        public string firstname { get; set; }

        [StringLength(50)]
        public string lastname { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(50)]
        public string userpassword { get; set; }

        public bool? isactive { get; set; }

        public bool? isdelete { get; set; }

        public DateTime? created { get; set; }

        public DateTime? modified { get; set; }
    }
}
