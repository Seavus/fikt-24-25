namespace BudgetManager.Infrastructure.Extensions
{
    public static class SmtpServiceExtensions
    {
        private static FluentEmailServicesBuilder AddFluentEmailDefault(this IServiceCollection services)
        {
            return services.AddFluentEmail("no-reply@budgetmanager.com");
        }

        private static IServiceCollection AddSmtpSender(this IServiceCollection services)
        {
            services.AddFluentEmailDefault()
                    .AddSmtpSender("localhost", 25);

            return services;
        }

        public static IServiceCollection AddSmtpMail(this IServiceCollection services)
        {
            return services.AddSmtpSender();
        }
    }
}