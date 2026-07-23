namespace ECommerce.Application.Dtos.Brands;

public record EditBrandRequestDto(
    string Name,
    string EnglishName,
    string Description,
    bool IsActive
);
