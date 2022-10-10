using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;

namespace Mv.Models
{
    public class AdminModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]

        public string password { get; set; }
    }
    public partial class HomeModel : DbContext
    {
        public HomeModel()
            : base("name=HomeModel")
        {
        }

        public virtual DbSet<slider> sliders { get; set; }
        public virtual DbSet<webtbluser> webtblusers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<slider>()
                .Property(e => e.slidername)
                .IsUnicode(false);

            modelBuilder.Entity<slider>()
                .Property(e => e.sliderimage)
                .IsUnicode(false);

            modelBuilder.Entity<slider>()
                .Property(e => e.redirecturl)
                .IsUnicode(false);

            modelBuilder.Entity<webtbluser>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<webtbluser>()
                .Property(e => e.lastname)
                .IsUnicode(false);

            modelBuilder.Entity<webtbluser>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<webtbluser>()
                .Property(e => e.userpassword)
                .IsUnicode(false);
        }
    }
}
