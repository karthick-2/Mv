using Mv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mv.Repository;
namespace Mv.Unitofwork
{
    public class UnitOfwork
    {
        private HomeModel hm = new HomeModel();
        private GenericRepository<webtbluser> userrep;
        public GenericRepository<webtbluser> userrepo { get { if (this.userrep == null) { this.userrep = new GenericRepository<webtbluser>(hm); } return this.userrep; } }
        private GenericRepository<slider> sliderep;
        public GenericRepository<slider> sliderepo { get { if (this.sliderep == null) { this.sliderep = new GenericRepository<slider>(hm); } return this.sliderep; } }
        public void save()
        {
            hm.SaveChanges();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}