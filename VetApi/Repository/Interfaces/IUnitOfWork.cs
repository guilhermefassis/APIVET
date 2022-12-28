using System.Threading.Tasks;

namespace VetApi.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IAnimalsRepository AnimalsRepository { get; }
        IAddressRepository AddressRepository { get; } 
        IVeterinarianRepository VeterinarianRepository { get; }
        ITutorRepository TutorRepository { get; }
        IQueryRepository QueryRepository { get; }
        IDataRepository DataRepository { get; }
        Task Commit();
    }
}