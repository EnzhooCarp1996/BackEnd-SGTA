using BackEndSGTA.Middleware;

namespace BackEndSGTA.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SGTA API v1");
            });
        }
        
        //app.UseHttpsRedirection();
        app.UseCors("DevCors");
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
