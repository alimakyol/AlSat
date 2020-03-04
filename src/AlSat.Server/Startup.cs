using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using AlSat.Server.DAL;
using AlSat.Server.Filters;
using AlSat.Server.Helpers;
using AlSat.Server.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;

using Swashbuckle.AspNetCore.SwaggerGen;

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
			services.AddMvcCore();

			services
				.AddDbContext<LogDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:LogDbCS"]))
				.AddDbContext<LocalizationDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:LocalizationDbCS"]))
				.AddDbContext<MainDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:MainDbCS"]))
				;

			//services
			//	.AddIdentity<User, Role>()
			//	.AddEntityFrameworkStores<MainDbContext>()
			//	.AddDefaultTokenProviders()
			//	;

			services.AddScoped<IUserService, UserService>();

			//services.Configure<IdentityOptions>(options =>
			//{
			//	// Default Lockout settings.
			//	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			//	options.Lockout.MaxFailedAccessAttempts = 5;
			//	options.Lockout.AllowedForNewUsers = true;

			//	// Default Password settings.
			//	options.Password.RequireDigit = true;
			//	options.Password.RequireLowercase = true;
			//	options.Password.RequireNonAlphanumeric = true;
			//	options.Password.RequireUppercase = true;
			//	options.Password.RequiredLength = 6;
			//	options.Password.RequiredUniqueChars = 1;

			//	// Default signin settings
			//	options.SignIn.RequireConfirmedEmail = true;
			//	options.SignIn.RequireConfirmedPhoneNumber = false;

			//	// User settings.
			//	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
			//	options.User.RequireUniqueEmail = true;
			//});

			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

				options.LoginPath = "/Account/Login";
				options.AccessDeniedPath = "/Account/AccessDenied";
				options.SlidingExpiration = true;
			});

			// configure strongly typed settings objects
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// configure jwt authentication
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
				options.Events = new JwtBearerEvents
				{
					OnTokenValidated = context =>
					{
						var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

						if (!userService.IsTokenValid(((JwtSecurityToken)context.SecurityToken).RawData))
							context.Fail("User is not recognized.");

						return Task.CompletedTask;
					}
				};
			});

			services.AddControllers(options =>
			{
				options.Filters.Add(typeof(ApiLoggingActionFilter));
			});

			services.AddApiVersioning(
					options =>
					{
						// reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
						options.ReportApiVersions = true;
					});
			services.AddVersionedApiExplorer(
					options =>
					{
						// add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
						// note: the specified format code will format the version as "'v'major[.minor][-status]"
						options.GroupNameFormat = "'v'VVV";

						// note: this option is only necessary when versioning by url segment. the SubstitutionFormat
						// can also be used to control the format of the API version in route templates
						options.SubstituteApiVersionInUrl = true;
					});

			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
			services.AddSwaggerGen(
					options =>
					{
						// add a custom operation filter which sets default values
						options.OperationFilter<SwaggerDefaultValues>();

						// integrate xml comments
						//options.IncludeXmlComments(XmlCommentsFilePath);
					});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(builder => builder.MapControllers());
			app.UseSwagger();
			app.UseSwaggerUI(
					options =>
					{
						// build a swagger endpoint for each discovered API version
						foreach (var description in provider.ApiVersionDescriptions)
						{
							string str = $"/swagger/{description.GroupName}/swagger.json";
							string sr2 = description.GroupName.ToUpperInvariant();

							options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
						}
					});
		}

		static string XmlCommentsFilePath
		{
			get
			{
				var basePath = PlatformServices.Default.Application.ApplicationBasePath;
				var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
				return Path.Combine(basePath, fileName);
			}
		}
	}
}
