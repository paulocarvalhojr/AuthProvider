using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using PC.Core.Auth.Configuration;
using System.Text;

namespace PC.Core.Commerce
{
	public class Startup
    {
		private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityConfiguration.SECRET_KEY));

		public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
			services.AddOptions();
			services.AddMvc(config =>
			{
				var policy = new AuthorizationPolicyBuilder()
								 .RequireAuthenticatedUser()
								 .Build();
				config.Filters.Add(new AuthorizeFilter(policy));
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("DisneyUser", policy => policy.RequireClaim(SecurityConfiguration.CLAIM_PERMISSION, "IAmDisney"));
				options.AddPolicy("DisneyBoss", policy => policy.RequireClaim(SecurityConfiguration.CLAIM_PERMISSION, "IAmBoss"));
			});
		}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			var jwtAppSettingOptions = Configuration.GetSection(nameof(IssuerOptions));
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = jwtAppSettingOptions[nameof(IssuerOptions.Issuer)],

				ValidateAudience = true,
				ValidAudience = jwtAppSettingOptions[nameof(IssuerOptions.Audience)],

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = _signingKey,

				RequireExpirationTime = true,
				ValidateLifetime = true,

				ClockSkew = TimeSpan.Zero
			};

			app.UseJwtBearerAuthentication(new JwtBearerOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				TokenValidationParameters = tokenValidationParameters
			});

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
		}
    }
}
