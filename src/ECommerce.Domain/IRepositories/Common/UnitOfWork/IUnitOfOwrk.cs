using ECommerce.Domain.IRepositories.Common.Comment;
using ECommerce.Domain.IRepositories.Persistence.Application.Article;
using ECommerce.Domain.IRepositories.Persistence.Application.Invoice;
using ECommerce.Domain.IRepositories.Persistence.Application.Role;
using ECommerce.Domain.IRepositories.Persistence.Application.Slide;
using ECommerce.Domain.IRepositories.Persistence.Identity;
using ECommerce.Domain.IRepositories.Persistence.Order;
using ECommerce.Domain.IRepositories.Persistence.Product;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Domain.IRepositories.Common.UnitOfWork;

/// <summary>
/// Unit of Work pattern for coordinating multiple repositories and database transactions.
/// </summary>
/// <remarks>
/// Provides a unified interface for all repositories and ensures transactional consistency.
/// All changes are committed together or rolled back entirely.
/// </remarks>
public interface IUnitOfWork : IAsyncDisposable
{
    #region Repositories

    /// <summary>
    /// Repository for working with products.
    /// </summary>
    IProductRepository ProductRepository { get; }

    /// <summary>
    /// Repository for working with product images.
    /// </summary>
    IProductImageRepository ProductImageRepository { get; }

    /// <summary>
    /// Repository for working with product specifications.
    /// </summary>
    IProductSpecificationRepository ProductSpecificationRepository { get; }

    /// <summary>
    /// Repository for working with comments.
    /// </summary>
    ICommentRepository CommentRepository { get; }

    /// <summary>
    /// Repository for working with categories.
    /// </summary>
    ICategoryRepository CategoryRepository { get; }

    /// <summary>
    /// Repository for working with brands.
    /// </summary>
    IBrandRepository BrandRepository { get; }

    /// <summary>
    /// Repository for working with orders.
    /// </summary>
    IOrderRepository OrderRepository { get; }

    /// <summary>
    /// Repository for working with order items.
    /// </summary>
    IOrderItemRepository OrderItemRepository { get; }

    /// <summary>
    /// Repository for working with order histories.
    /// </summary>
    IOrderHistoryRepository OrderHistoryRepository { get; }

    /// <summary>
    /// Repository for working with order payments.
    /// </summary>
    IOrderPaymentRepository OrderPaymentRepository { get; }

    /// <summary>
    /// Repository for working with order shippings.
    /// </summary>
    IOrderShippingRepository OrderShippingRepository { get; }

    /// <summary>
    /// Repository for working with coupons.
    /// </summary>
    ICouponRepository CouponRepository { get; }

    /// <summary>
    /// Repository for working with users.
    /// </summary>
    IUserRepository UserRepository { get; }

    /// <summary>
    /// Repository for working with user roles.
    /// </summary>
    IUserRoleRepository UserRoleRepository { get; }

    /// <summary>
    /// Repository for working with articles.
    /// </summary>
    IArticleRepository ArticleRepository { get; }

    /// <summary>
    /// Repository for working with article categories.
    /// </summary>
    IArticleCategoryRepository ArticleCategoryRepository { get; }

    /// <summary>
    /// Repository for working with invoices.
    /// </summary>
    IInvoiceRepository InvoiceRepository { get; }

    /// <summary>
    /// Repository for working with roles.
    /// </summary>
    IRoleRepository RoleRepository { get; }

    /// <summary>
    /// Repository for working with permissions.
    /// </summary>
    IPerimssionRepository PerimssionRepository { get; }

    /// <summary>
    /// Repository for working with slides.
    /// </summary>
    ISlideRepository SlideRepository { get; }

    #endregion

    #region Transactions

    /// <summary>
    /// Saves all changes without a transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Success result or failure with error details.</returns>
    Task<Result> SaveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A transaction object for commit/rollback operations.</returns>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finishes the spell successfully (commit), so all changes stay.
    /// </summary>
    /// <param name="transaction">The spell handler.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Success or failure.</returns>
    Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);

    /// <summary>
    /// Undoes the spell (rollback) if something goes wrong, so no changes happen.
    /// </summary>
    /// <param name="transaction">The spell handler.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Success or failure.</returns>
    Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
    #endregion
}