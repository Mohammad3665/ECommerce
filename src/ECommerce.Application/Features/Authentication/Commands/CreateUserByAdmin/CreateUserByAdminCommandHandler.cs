using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.CreateUserByAdmin;

public class CreateUserByAdminCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService, IEmailService emailService, ICurrentUserService currentUserService) : IRequestHandler<CreateUserByAdminCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserByAdminCommand request, CancellationToken cancellationToken)
    {
        var emailTaken = await unitOfWork.UserRepository.GetAsync(
            expression: u => u.Email == request.Email,
            cancellationToken: cancellationToken
        );

        if (emailTaken is not null)
        {
            var error = new Error(
                "Auth.EmailTaken",
                "This email aleardy exists.", 
                ErrorType.Conflict
            );
            return Result<Guid>.Failure(error);
        }

        var roleName = request.Role;
        var role = await unitOfWork.RoleRepository.GetAsync(
            expression: r => r.Name == roleName,
            cancellationToken: cancellationToken
        );
        if (role is null)
        {
            var error = new Error(
                "Auth.NotFound",
                $"{roleName} role not found in system", 
                ErrorType.NotFound
            );
            return Result<Guid>.Failure(error);
        }
        var rawPassword = !string.IsNullOrWhiteSpace(request.Password) 
            ? request.Password 
            : Guid.NewGuid().ToString("N")[..8] + "A1!";

        var user = request.Adapt<User>();
        user.Id = Guid.NewGuid();
        user.PasswordHash = passwordService.Hash(rawPassword);

        user.UserRoles = new List<UserRole>
        {
            new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                AssignedAt = DateTime.UtcNow,
                AssignedByUserId = currentUserService.UserId
            }
        };

        user.IsEmailConfirmed = true;

        await unitOfWork.UserRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
        var loginLink = "http://localhost:5000/Api/V1/Auth/Login";
        var emailBody = $@"
            <div dir='rtl' style='font-family: Tahoma, Arial; text-align: right;'>
                <h3>سلام {request.FullName} عزیز،</h3>
                <p>حساب کاربری شما در فروشگاه توسط مدیریت ایجاد و فعال‌سازی شده است.</p>
                <p>شما می‌توانید با مشخصات زیر وارد حساب خود شوید:</p>
                <table style='border: 1px solid #ccc; padding: 10px; background-color: #f9f9f9;'>
                    <tr>
                        <td><strong>نام کاربری (ایمیل):</strong></td>
                        <td>{request.Email}</td>
                    </tr>
                    <tr>
                        <td><strong>رمز عبور موقت:</strong></td>
                        <td><code style='background:#eee; padding:2px 5px;'>{rawPassword}</code></td>
                    </tr>
                </table>
                <p style='color: red;'>لطفاً بلافاصله پس از اولین ورود، رمز عبور خود را از قسمت پروفایل تغییر دهید.</p>
                <br/>
                <a href='{loginLink}' style='padding: 10px 20px; background-color: #28a745; color: white; text-decoration: none; border-radius: 5px;'>ورود به سایت</a>
            </div>
        ";

        await emailService.SendEmailAsync(
            request.Email, 
            "اطلاعات حساب کاربری شما", 
            emailBody);

        return Result<Guid>.Success(user.Id);
    }

}
