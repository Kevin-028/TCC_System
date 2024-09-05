using TCC_System_Domain.Core;

namespace TCC_System_Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private TCC_Context _context;

        public UnitOfWork(TCC_Context context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Commit(string user)
        {
            _context.SaveChanges(user);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}
