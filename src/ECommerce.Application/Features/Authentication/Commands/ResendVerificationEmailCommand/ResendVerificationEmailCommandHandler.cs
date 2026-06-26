using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.ResendVerificationEmailCommand;

public class ResendVerificationEmailCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService, ICodeGeneratorService codeGenerator) : IRequestHandler<ResendVerificationEmailCommand, Result>
{
    public async Task<Result> Handle(ResendVerificationEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetAsync(
            expression: u => u.Email == request.Email,
            cancellationToken: cancellationToken);

        if (user is null || !user.IsActive)
        {
            return Result.Success();
        }

        if (user.IsEmailConfirmed)
        {
            var error = new Error(
                "Auth.EmailAlreadyVerified",
                "این حساب کاربری قبلاً تایید شده است.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        user.SecurityCode = codeGenerator.Generate();
        user.SecurityCodeExpiry = DateTime.UtcNow.AddHours(1);

        unitOfWork.UserRepository.Update(user);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Auth.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        var emailBody = $@"
            <div dir='rtl' style='font-family: Tahoma, Arial; text-align: right; line-height: 1.6;'>
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

        return Result.Success();
    }
}
