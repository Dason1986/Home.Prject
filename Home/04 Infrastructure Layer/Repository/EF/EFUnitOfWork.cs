using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DbContext _dbconntext;

        public EFUnitOfWork(DbContext dbconntext)
        {
            if (dbconntext == null) throw new ArgumentNullException("dbconntext");
            _dbconntext = dbconntext;
        }

 

        #region IQueryableUnitOfWork

        public void Commit()
        {

            _dbconntext.SaveChanges();


        }

     

        public void RollbackChanges()
        {
            _dbconntext.ChangeTracker.Entries().ToList().ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #endregion IQueryableUnitOfWork
    }
}
