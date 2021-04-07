using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using SchoolDiary.Domain.Data;
using SchoolDiary.Domain.Services;
using SchoolDiary.Helpers;
using SchoolDiary.Helpers.Interfaces;
using Microsoft.AspNetCore.CookiePolicy;
using System.Threading.Tasks;
using VueCliMiddleware;
using System.Text;

namespace SchoolDiary
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
            // Configure strongly typed settings objects.
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            // Adding project services.
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<ITeachersEditService, TeachersEditService>();
            services.AddTransient<IStudentsEditService, StudentsEditService>();
            services.AddTransient<IScheduleEditService, ScheduleEditService>();
            services.AddHttpContextAccessor();
            // Adding database context.
            services.AddDbContext<DataContext>(options => 
                options.UseSqlServer(Configuration
                    .GetConnectionString("DefaultConnection")));
            // Authentication settins.
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
            services.AddCors();
            services.AddControllers();
            services.AddSpaStaticFiles(options => options.RootPath = "ClientApp");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // For correctly working with vue front.
            // Allowing cross-domain queries!
            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "frontent_part_vue/";
                }
                else
                {
                    spa.Options.SourcePath = "dist";
                }
                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "start");
                }
            });
        }
    }
}
