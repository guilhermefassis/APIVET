using System.Threading.Tasks;
using VetApi.Context;
using VetApi.Repository.Interfaces;

namespace VetApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IAnimalsRepository _AnimalsRepository;
        private IAddressRepository _AddressRepository;
        private ITutorRepository _TutorRepository;
        private IVeterinarianRepository _VeterinarianRepository;
        private IQueryRepository _QueryRepository;
        private IDataRepository _DataRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IAnimalsRepository AnimalsRepository 
        {
            get 
            {
                return _AnimalsRepository = _AnimalsRepository ?? new AnimalsRepository(_context); 
            }
        }

        public IAddressRepository AddressRepository
        {
            get 
            {
                return _AddressRepository = _AddressRepository ?? new AddressRepository(_context);
            }
        }

        public ITutorRepository TutorRepository 
        {
            get 
            {
                return _TutorRepository = _TutorRepository ?? new TutorRepository(_context);
            }
        }

        public IVeterinarianRepository VeterinarianRepository
        {
            get
            {
                return _VeterinarianRepository = _VeterinarianRepository ?? new VeterinarianRepository(_context);
            }
        }

        public IQueryRepository QueryRepository 
        {
            get 
            {
                return _QueryRepository =  _QueryRepository ?? new QueryRepository(_context);
            }
        }

        public IDataRepository DataRepository
        {
            get
            {
                return _DataRepository = _DataRepository ?? new DataRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}