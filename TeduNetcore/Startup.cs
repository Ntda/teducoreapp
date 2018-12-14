using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeduNetcore.Application.Implementations;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Data.EF;
using TeduNetcore.Data.EF.Repositories;
using TeduNetcore.Data.Entities;
using TeduNetcore.Data.IRepositories;
using TeduNetcore.Services;

namespace TeduNetcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                     sqlOptBuilder => sqlOptBuilder.MigrationsAssembly("TeduNetcore.Data.EF")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.Configure<IdentityOptions>(opt =>
            {
                // Password setting
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            });
            services.AddAutoMapper();
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(mapper => new Mapper(mapper.GetRequiredService<AutoMapper.IConfigurationProvider>(), mapper.GetService));
            services.AddTransient<UserManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<DbInitializer>();

            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name:"areaRoute",
                    template:"{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
