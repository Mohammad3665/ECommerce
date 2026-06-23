using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;
using Mapster;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.Entities.Application.Role;

namespace ECommerce.Application.Features.Authentication.Commands.Register;

public class RegisterUserCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService, IEmailService emailService, ICodeGeneratorService codeGenerator)
    : IRequestHandler<RegisterCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailTaken = await unitOfWork.UserRepository.GetAsync(
            expression: u => u.Email == request.Email,
            cancellationToken: cancellationToken
        );

        if (emailTaken is not null)
        {
            var error = new Error(
                "Auth.EmailTaken",
                "ایمیل قبلا ثبت شده است.",
                ErrorType.Conflict
            );
            return Result<Guid>.Failure(error);
        }

        var roleName = request.Role ?? "Customer";
        var role = await unitOfWork.RoleRepository.GetAsync(
            expression: r => r.Name == roleName,
            cancellationToken: cancellationToken
        );
        if (role is null)
        {
            var error = new Error(
                "Auth.NotFound",
                $"نقش {roleName} در سیستم یافت نشد.",
                ErrorType.NotFound
            );
            return Result<Guid>.Failure(error);
        }
        var user = request.Adapt<User>();
        user.Id = Guid.NewGuid();
        user.PasswordHash = passwordService.Hash(request.Password);
        user.UserRoles = new List<UserRole>
        {
            new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                AssignedAt = DateTime.UtcNow,
                AssignedByUserId = null
            }
        };
        user.IsActive = false;
        user.IsEmailConfirmed = false;
        user.SecurityCode = null;
        user.SecurityCodeExpiry = null;
        user.SecurityCode = codeGenerator.Generate();
        user.SecurityCodeExpiry = DateTime.UtcNow.AddHours(1);

        await unitOfWork.UserRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);

        var emailBody = $@"
            <div dir='rtl' style='font-family: Tahoma, Arial; text-align: right; line-height: 1.6;'>
                <h3>سلام {request.FullName} عزیز،</h3>
                <p>به فروشگاه ما خوش آمدید! جهت فعال‌سازی حساب کاربری و تایید ایمیل خود، لطفاً از کد تایید زیر استفاده کنید:</p>
                <div style='text-align: center; margin: 25px 0;'>
                    <span style='background-color: #f8f9fa; border: 2px dashed #007bff; padding: 10px 30px; font-size: 26px; font-weight: bold; letter-spacing: 5px; color: #007bff; border-radius: 5px;'>
                        {user.SecurityCode}
                    </span>
                </div>
                <p style='color: #dc3545; font-size: 13px;'>⚠️ این کد به مدت <strong>۱ ساعت</strong> معتبر است.</p>
            </div>
        ";

        await emailService.SendEmailAsync(
            request.Email,
            "کد تایید ثبت‌نام در فروشگاه",
            emailBody);

        return Result<Guid>.Success(user.Id);
    }

}
