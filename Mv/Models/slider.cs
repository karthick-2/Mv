namespace Mv.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("slider")]
    public partial class slider
    {
        public int id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string slidername { get; set; }

        [StringLength(50)]
        public string sliderimage { get; set; }

        public int? imageorder { get; set; }

        public bool isactive { get; set; }

        public bool? isdelete { get; set; }

        public DateTime? created { get; set; }

        public DateTime? Modified { get; set; }

        [StringLength(50)]
        public string redirecturl { get; set; }
    }
}
