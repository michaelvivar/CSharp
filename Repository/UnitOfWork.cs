using System;
using ConsoleApplication1.Repositories;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private Model1Container _context;
        private Repository<tbl_person> userRepository;

        public UnitOfWork()
        {
            this._context = new Model1Container();
        }

        public Repository<tbl_person> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new Repository<tbl_person>(this._context);
                return this.userRepository;
            }
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
