namespace ECommerce.Application.Dtos.Brands;

public record GetBrandResponseDto(
    string Name,
    string EnglishName,
    string Slug,
    string LogoImageUrl
);
