using eShopSolution.Data.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PosWebQLBH.Application.Catalog.Categories;
using PosWebQLBH.Application.Catalog.Products;
using PosWebQLBH.Application.Catalog.Units;
using PosWebQLBH.Application.Common;
using PosWebQLBH.Application.Partner.Customers;
using PosWebQLBH.Application.Partner.Suppliers;
using PosWebQLBH.Application.System.Languages;
using PosWebQLBH.Application.System.Roles;
using PosWebQLBH.Application.System.Users;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.WebApp
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
            //connection csdl
            //var connectString = Configuration.GetConnectionString(SystemConstants.MainConnectionString);
            //services.AddDbContext<DbQLBHContext>(item => item.UseSqlServer(connectString));

            services.AddDbContext<DbQLBHContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<DbQLBHContext>()
                .AddDefaultTokenProviders();

            //Declare DI -- product
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IProductService, ProductService>();

            //Declare DI -- Category
            services.AddTransient<ICategoryService, CategoryService>();

            //DI -- Unit
            services.AddTransient<IUnitService, UnitService>();

            //Declare DI -- customer
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<ICustomerService, CustomerService>();

            //Declare DI -- supplier
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<ISupplierService, SupplierService>();

            //Declare DI -- category
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<ICategoryService, CategoryService>();

            //Declare DI -- unit
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IUnitService, UnitService>();

            // Declare DI --> login
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILanguageService, LanguageService>();

            //DI -- Role
            services.AddTransient<IRoleService, RoleService>();

            //services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            //services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

            //Tạo xác thực bắt lỗi
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

            services.AddSwaggerGen(c =>
            {
                //add title swagger vào
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Pos Web QLBH ", Version = "v1" });

                //add Authorize vào header của swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });

            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication(); //--> ktra xác thực login

            app.UseRouting();

            app.UseAuthorization();

            //add swagger vào
            app.UseSwagger();
            // swagger UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger PosWeb V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}