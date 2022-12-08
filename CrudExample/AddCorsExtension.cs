namespace CrudExample
{
    public static class AddCorsExtension
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration, string corsName = "CorsPolicySpecificHosts")
        {
            var allowedHots = configuration.GetValue<string>("AllowedHosts");
            if (allowedHots.Trim().Length > 1)
            {
                //Cors for especific hots
                services.AddCors(options =>
                {
                    options.AddPolicy(corsName, builder =>
                    {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins(allowedHots.Split(','))
                        .SetIsOriginAllowed(x => true)
                        .Build();
                    });
                });
            }
            else
            {
                //Cors enabled for all hosts
                services.AddCors(options =>
                {
                    options.AddPolicy(corsName, builder =>
                    {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed(x => true)
                        .Build();
                    });
                });
            }

            return services;
        }
    }
}
