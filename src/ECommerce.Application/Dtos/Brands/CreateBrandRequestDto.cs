namespace ECommerce.Application.Dtos.Brands;

public record CreateBrandRequestDto(
    string Name,
    string EnglishName,
    string Description,
    string LogoImageUrl,
    bool IsActive
);
