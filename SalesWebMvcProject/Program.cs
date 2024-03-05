using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvcProject.Data;
using SalesWebMvcProject.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization; 

namespace SalesWebMvcProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("SalesWebMvcContext");
            builder.Services.AddDbContext<SalesWebMvcProjectContext>(Options => Options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Injeção de dependência para os serviços
            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentService>();

            // Configure localization
            var supportedCultures = new[]
             {
             new CultureInfo("en-US"),
             new CultureInfo("pt-BR")
             };

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"), // Defina a cultura padrão
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            builder.Services.AddControllersWithViews()
                .AddViewLocalization(); // Adicione esta linha para habilitar a localização nas visualizações

            // Construa o aplicativo
            var app = builder.Build();

            // Adicione middleware de localização
            app.UseRequestLocalization(localizationOptions);

            // Configure o pipeline de solicitação HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // O valor padrão de HSTS é de 30 dias. Você pode querer alterar isso para cenários de produção, veja https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }


    }
}
