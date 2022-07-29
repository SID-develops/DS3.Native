using DS3.Native.Hoster.Resources;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DS3.Native.Hoster.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddVirtualFileServer();

        services.AddJwt<JwtHandler>();

        services.AddCorsAccessor();

        services.AddControllers()
                .AddInjectWithUnifyResult();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        //app.UseHttpsRedirection();

        app.UseRouting();

        app.UseDefaultFiles(new DefaultFilesOptions
        {
            DefaultFileNames = new string[] { "index.html" },
            FileProvider = EmbeddedResource.GetFileProvider()
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = EmbeddedResource.GetFileProvider()
        });

        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseInject("swagger");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}