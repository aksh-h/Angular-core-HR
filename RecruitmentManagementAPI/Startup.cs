using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RecruitmentManagementAPI.Controllers;
using RecruitmentManagementDataAccess.Models;

using RecruitmentManagementProvider.IProvider;
using RecruitmentManagementProvider.Provider;

namespace RecruitmentManagementAPI
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
            services.AddCors(c => c.AddPolicy("AllowOriginAny", o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddTransient<ILogin, Login>();
            services.AddTransient<IMasters, Masters>();
            services.AddTransient<IRequisition, Requisition>();
            services.AddTransient<IApplicant, Applicant>();
            services.AddTransient<IEmployee, Employee>();
            services.AddTransient<IRequisitionStaffing, RequisitionStaffing>();
            services.AddTransient<IApplicantStaffing, ApplicantStaffing>();
            services.AddControllers();



            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSecretKey").Value);

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




            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);




            //services.AddDbContext<RecruitmentManagementContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:RecruitmentManagementDB"]), ServiceLifetime.Scoped);
            //services.AddDbContext<RecruitmentManagementContext>(c => c.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddScoped<ActionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowOriginAny");

            app.UseHttpsRedirection();

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
