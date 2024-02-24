using BloggingPlatform;
using BloggingPlatform.Middleware;


// Open - Close --> Extension method
var builder = WebApplication.CreateBuilder(args).ConfigurationBinding();
var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();

app.Run();
