namespace ECommerce.Application.Dtos.Brands;

public record GetBrandResponseDto(
    long Id,
    string Name,
    string EnglishName,
    string Slug,
    string LogoImageUrl,
    string Description,
    bool IsActive
);
