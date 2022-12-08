using Microsoft.Extensions.Options;

namespace CrudExample
{
    /// <summary>
    /// Secure ASP.NET Core Web API using API Key Authentication
    /// http://codingsonata.com/secure-asp-net-core-web-api-using-api-key-authentication/
    /// Created Date: Oct 08, 2021
    /// Created By: Elvin.Landeta
    /// </summary>
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationOptions _applicationOptions;
        private readonly ILogger<ApiKeyMiddleware> _logger;
        private const string APIKEYNAME = "ApiKey";
        private const string CLIENTKEYNAME = "ClientKey";
        public ApiKeyMiddleware(RequestDelegate next, IOptions<ApplicationOptions> applicationOptionsAccessor, ILogger<ApiKeyMiddleware> logger)
        {
            _next = next;
            _applicationOptions = applicationOptionsAccessor.Value;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var message = "";
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                message = "Api Key was not provided. (Using ApiKeyMiddleware) ";
                _logger.LogWarning(message);
                await context.Response.WriteAsync(message)
                    .ConfigureAwait(false);
                return;
            }

            var apiKey = _applicationOptions.ApiKey;
            //var apiKey = appSettings.GetValue<string>(APIKEYNAME) ?? "";

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                message = "Unauthorized client. (Using ApiKeyMiddleware)";
                await context.Response.WriteAsync(message)
                    .ConfigureAwait(false);
                _logger.LogWarning(message);

                return;
            }

            await _next(context);
        }
    }
}
