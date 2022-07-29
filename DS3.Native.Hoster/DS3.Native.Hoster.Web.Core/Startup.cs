using Furion;
using Furion.VirtualFileServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

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

        var options = new DefaultFilesOptions
        {
            DefaultFileNames = new string[] { "index.html" },
            FileProvider = FS.GetEmbeddedFileProvider(Assembly.GetEntryAssembly())
        };

        app.UseDefaultFiles(options);
        app.UseStaticFiles();

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