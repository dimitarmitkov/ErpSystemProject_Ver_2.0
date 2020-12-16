namespace ErpSystem.WebApp
{
    using Microsoft.AspNetCore.Builder;

    using Microsoft.AspNetCore.Hosting;

    using Microsoft.EntityFrameworkCore;

    using Microsoft.Extensions.Configuration;

    using Microsoft.Extensions.DependencyInjection;

    using Microsoft.Extensions.Hosting;

    using ErpSystem.Data;

    using ErpSystem.Services.Services;

    using AutoMapper;

    using ErpSystem.Services;

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
            services.AddDbContext<ErpSystemDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
            // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //   .AddEntityFrameworkStores<ErpSystemDbContext>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddMemoryCache();
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "CacheRecords";
            });

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                options.Cookie.IsEssential = true; // for essentioal cookies needed to operate
            });

            services.AddAutoMapper(m => m.AddProfile<AutoMapping>(), typeof(Startup));
            services.AddSingleton(this.Configuration);

            //Database
            services.AddDbContext<ErpSystemDbContext>();

            //Application services
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IDeliveriesService, DeliveriesService>();
            services.AddTransient<IWarehouseSpace, WarehouseSpace>();
            services.AddTransient<ISuppliersService, SuppliersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseExceptionHandler("/Errors/Error404");
            }
            else
            {
                app.UseExceptionHandler("/Errors/Error404");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession(); // this is to use session setting form services.AddSession
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "areaRoute",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
