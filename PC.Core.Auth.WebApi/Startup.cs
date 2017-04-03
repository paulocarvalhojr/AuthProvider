using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PC.Core.Auth.WebApi.Interfaces;
using PC.Core.Auth.WebApi.Repositories;
using PC.Core.Auth.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using PC.Core.Auth.Configuration;

namespace PC.Core.Auth.WebApi
{
	public class Startup
    {
		private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityConfiguration.SECRET_KEY));
		public IConfigurationRoot Configuration { get; }

		public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
		
        public void ConfigureServices(IServiceCollection services)
        {
			//services.AddOptions();
			//services.AddMvc(config =>
			//{
			//	var policy = new AuthorizationPolicyBuilder()
			//					 .RequireAuthenticatedUser()
			//					 .Build();
			//	config.Filters.Add(new AuthorizeFilter(policy));
			//});

			//services.AddAuthorization(options =>
			//{
			//	options.AddPolicy("DisneyUser", policy => policy.RequireClaim("Permissions", "IAmDisney"));
			//	options.AddPolicy("DisneyBoss", policy => policy.RequireClaim("Permissions", "IAmBoss"));
			//});

			services.AddMvc();
			services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));

			//utilizado no userservice.
            var jwtAppSettingOptions = Configuration.GetSection(nameof(IssuerOptions));
            services.Configure<IssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(IssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(IssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			//var jwtAppSettingOptions = Configuration.GetSection(nameof(IssuerOptions));
			//var tokenValidationParameters = new TokenValidationParameters
			//{
			//	ValidateIssuer = true,
			//	ValidIssuer = jwtAppSettingOptions[nameof(IssuerOptions.Issuer)],

			//	ValidateAudience = true,
			//	ValidAudience = jwtAppSettingOptions[nameof(IssuerOptions.Audience)],

			//	ValidateIssuerSigningKey = true,
			//	IssuerSigningKey = _signingKey,

			//	RequireExpirationTime = true,
			//	ValidateLifetime = true,

			//	ClockSkew = TimeSpan.Zero
			//};

			//app.UseJwtBearerAuthentication(new JwtBearerOptions
			//{
			//	AutomaticAuthenticate = true,
			//	AutomaticChallenge = true,
			//	TokenValidationParameters = tokenValidationParameters
			//});

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
		}
    }
}
