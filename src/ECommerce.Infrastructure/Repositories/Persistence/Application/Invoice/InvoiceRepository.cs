using ECommerce.Domain.IRepositories.Persistence.Application.Invoice;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Invoice;

public class InvoiceRepository(ApplicationDbContext context)
    : BaseRepository<long, Domain.Entities.Application.Invoice.Invoice>(context), IInvoiceRepository;