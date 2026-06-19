using ECommerce.Application.Common.Interfaces.Services;

namespace ECommerce.Infrastructure.Common.Services;

public class CodeGeneratorService : ICodeGeneratorService
{
    public string Generate()
    {
        return Random.Shared.Next(100_000, 999_999).ToString();
    }
}
