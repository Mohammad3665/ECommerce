using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using ECommerce.Domain.IRepositories.Persistence.Application.Article;
using ECommerce.Domain.IRepositories.Persistence.Application.Invoice;
using ECommerce.Domain.IRepositories.Persistence.Application.Role;
using ECommerce.Domain.IRepositories.Persistence.Application.Slide;
using ECommerce.Domain.IRepositories.Persistence.Identity;
using ECommerce.Domain.IRepositories.Persistence.Order;
using ECommerce.Domain.IRepositories.Persistence.Product;
using ECommerce.Infrastructure.Repositories.Persistence.Application.Article;
using ECommerce.Infrastructure.Repositories.Persistence.Application.Invoice;
using ECommerce.Infrastructure.Repositories.Persistence.Application.Role;
using ECommerce.Infrastructure.Repositories.Persistence.Application.Slide;
using ECommerce.Infrastructure.Repositories.Persistence.Identity;
using ECommerce.Infrastructure.Repositories.Persistence.Order;
using ECommerce.Infrastructure.Repositories.Persistence.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Infrastructure.Repositories.Common.UnitOfWork;

/// <summary>
/// The real super boss that implements the IUnitOfWork blueprint, handling repositories and magic spells (transactions).
/// </summary>
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    #region Repositories

    #region Product

    // Product repository
    private IProductRepository? _productRepository;
    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(context);

    // Product image repository
    private IProductImageRepository? _productImageRepository;
    public IProductImageRepository ProductImageRepository => _productImageRepository ??= new ProductImageRepository(context);

    // Product specification repository
    private IProductSpecificationRepository? _productSpecificationRepository;
    public IProductSpecificationRepository ProductSpecificationRepository => _productSpecificationRepository ??= new ProductSpecificationRepository(context);

    // Comment repository
    private ICommentRepository? _commentRepository;
    public ICommentRepository CommentRepository => _commentRepository ??= new CommentRepository(context);

    // Category repository
    private ICategoryRepository? _categoryRepository;
    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(context);

    // Brand repository
    private IBrandRepository? _brandRepository;
    public IBrandRepository BrandRepository => _brandRepository ??= new BrandRepository(context);

    #endregion

    #region Order

    // Order repository
    private IOrderRepository? _orderRepository;
    public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(context);

    // Order item repository
    private IOrderItemRepository? _orderItemRepository;
    public IOrderItemRepository OrderItemRepository => _orderItemRepository ??= new OrderItemRepository(context);

    // Order history repository
    private IOrderHistoryRepository? _orderHistoryRepository;
    public IOrderHistoryRepository OrderHistoryRepository => _orderHistoryRepository ??= new OrderHistoryRepository(context);

    // Order payment repository
    private IOrderPaymentRepository? _orderPaymentRepository;
    public IOrderPaymentRepository OrderPaymentRepository => _orderPaymentRepository ??= new OrderPaymentRepository(context);

    // Order shipping repository
    private IOrderShippingRepository? _orderShippingRepository;
    public IOrderShippingRepository OrderShippingRepository => _orderShippingRepository ??= new OrderShippingRepository(context);

    // Coupon repository
    private ICouponRepository? _couponRepository;
    public ICouponRepository CouponRepository => _couponRepository ??= new CouponRepository(context);

    #endregion

    #region Identity

    // User repository
    private IUserRepository? _userRepository;
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(context);

    // User role repository
    private IUserRoleRepository? _userRoleRepository;
    public IUserRoleRepository UserRoleRepository => _userRoleRepository ??= new UserRoleRepository(context);

    #endregion

    #region Application

    // Article repository
    private IArticleRepository? _articleRepository;
    public IArticleRepository ArticleRepository => _articleRepository ??= new ArticleRepository(context);

    // Article category repository
    private IArticleCategoryRepository? _articleCategoryRepository;
    public IArticleCategoryRepository ArticleCategoryRepository => _articleCategoryRepository ??= new ArticleCategoryRepository(context);

    // Invoice repository
    private IInvoiceRepository? _invoiceRepository;
    public IInvoiceRepository InvoiceRepository => _invoiceRepository ??= new InvoiceRepository(context);

    // Role repository
    private IRoleRepository? _roleRepository;
    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(context);

    // Permission repository
    private IPerimssionRepository? _perimssionRepository;
    public IPerimssionRepository PerimssionRepository => _perimssionRepository ??= new PermissionRepository(context);

    // Role permission repository
    private IRolePermissionRepository? _rolePermissionRepository;
    public IRolePermissionRepository RolePermissionRepository => _rolePermissionRepository ??= new RolePermissionRepository(context);

    // Silde repository
    private ISlideRepository? _slideRepository;
    public ISlideRepository SlideRepository => _slideRepository ??= new SlideRepository(context);

    #endregion

    #endregion

    #region Save

    public async Task<Result> SaveAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException ex)
        {
            var error = new Error(
                "Database.SaveError",
                $"Failed to save changes to database.{ex.Message}",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }
        catch (OperationCanceledException)
        {
            var error = new Error(
                "Database.OperationCanceled",
                $"Operation canceled.",
                ErrorType.Canceled
            );
            return Result.Failure(error);
        }
        catch (Exception ex)
        {
            var error = new Error(
                "Database.UnexpectedError",
                $"An unexpected error occurred: {ex.Message}",
                ErrorType.Unexpected
            );

            return Result.Failure(error);
        }
    }

    #endregion

    #region Transaction

    Task<Result> IUnitOfWork.SaveAsync(CancellationToken cancellationToken)
    {
        return SaveAsync(cancellationToken);
    }
    Task<IDbContextTransaction> IUnitOfWork.BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return BeginTransactionAsync(cancellationToken);
    }

    Task<Result> IUnitOfWork.CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        return CommitTransactionAsync(transaction, cancellationToken);
    }

    Task<Result> IUnitOfWork.RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        return RollbackTransactionAsync(transaction, cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task<Result> CommitTransactionAsync(IDbContextTransaction? transaction, CancellationToken cancellationToken = default)
    {
        if (transaction is null)
        {
            var error = new Error(
                "Transaction.Null",
                "Transaction cannot be null.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        try
        {
            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            var error = new Error(
                "Database.OperationCanceled",
                $"Operation canceled.",
                ErrorType.Canceled
            );
            return Result.Failure(error);
        }
        catch (Exception ex)
        {
            var error = new Error(
                "Database.UnexpectedError",
                $"An unexpected error occurred: {ex.Message}",
                ErrorType.Unexpected
            );

            return Result.Failure(error);
        }
    }

    public async Task<Result> RollbackTransactionAsync(IDbContextTransaction? transaction, CancellationToken cancellationToken = default)
    {
        if (transaction is null)
        {
            var error = new Error(
                "Transaction.Null",
                "Transaction cannot be null.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        try
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            var error = new Error(
                "Database.OperationCanceled",
                $"Rollback canceled.",
                ErrorType.Canceled
            );
            return Result.Failure(error);
        }
        catch (Exception ex)
        {
            var error = new Error(
                "Database.UnexpectedError",
                $"An unexpected error occurred: {ex.Message}",
                ErrorType.Unexpected
            );

            return Result.Failure(error);
        }

    }

    #endregion

    #region Dispose

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }

    #endregion
}
