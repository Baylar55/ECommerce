using EcommerceAPI.Application.Repositories.File;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.File
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(EcommerceAPIDbContext context) : base(context) { }
    }
}
