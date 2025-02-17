namespace BudgetManager.Infrastructure;
    
sealed public class JwtOptions
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
}