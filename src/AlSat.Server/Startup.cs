using System;
using System.Text;

using AlSat.Data.DAL;
using AlSat.Data.Models;
using AlSat.Server.Filters;
using AlSat.Server.Helpers;
using AlSat.Server.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AlSat.Server
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
			services
			.AddDbContext<MainDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:MainDbCS"]))
			.AddDbContext<LogDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:LogDbCS"]))
			;

			services
				.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<MainDbContext>()
				.AddDefaultTokenProviders()
				;

			services.AddScoped<IUserService, UserService>();

			services.Configure<IdentityOptions>(options =>
			{
				// Default Lockout settings.
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				// Default Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;

				// Default signin settings
				options.SignIn.RequireConfirmedEmail = true;
				options.SignIn.RequireConfirmedPhoneNumber = false;

				// User settings.
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
			});

			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

				options.LoginPath = "/api/Account/Login";
				options.AccessDeniedPath = "/api/Account/AccessDenied";
				options.SlidingExpiration = true;
			});

			services.AddControllers(options =>
			{
				options.Filters.Add(typeof(ApiLoggingActionFilter));
			});

			// configure strongly typed settings objects
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// configure jwt authentication
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1",
					new OpenApiInfo
					{
						Title = "AlSat API",
						Version = "v1",
						Description = "AlSat API documentation",
						TermsOfService = new Uri("https://alsat.com/terms"),
						Contact = new OpenApiContact
						{
							Name = "Alim Akyol",
							Email = string.Empty,
							Url = new Uri("https://twitter.com/AlimAkyol"),
						},
						License = new OpenApiLicense
						{
							Name = "Use under Proprietary license.",
							Url = new Uri("https://alsat.com/license"),
						}
					}
					);
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
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlSat API V1");
			});

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
