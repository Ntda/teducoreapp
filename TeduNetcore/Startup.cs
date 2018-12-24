using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using TeduNetcore.Application.Implementations;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Data.EF;
using TeduNetcore.Data.EF.Repositories;
using TeduNetcore.Data.Entities;
using TeduNetcore.Data.IRepositories;
using TeduNetcore.Helper;
using TeduNetcore.Infrastructure.Intarfaces;
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
                opt.Password.RequiredLength = 7;
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
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();
            services.AddMvc().AddJsonOptions(opt =>
            {
                IContractResolver resolver = opt.SerializerSettings.ContractResolver;
                if (resolver != null)
                {
                    DefaultContractResolver res = resolver as DefaultContractResolver;
                    res.NamingStrategy = null;
                }
            });

            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IFunctionRepository, FunctionRepository>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IProductTagRepository, ProductTagRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/tedu-{Date}.txt");
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
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
            });

        }
    }
}
