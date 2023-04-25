using EcommerceAPI.Application.Repositories.InvoiceFile;
using EcommerceAPI.Persistence.Contexts;

namespace EcommerceAPI.Persistence.Repositories.InvoiceFile
{
    public class InvoiceFileWriteRepository : WriteRepository<Domain.Entities.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(EcommerceAPIDbContext context) : base(context) { }
    }
}
