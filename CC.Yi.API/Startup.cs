
using Autofac;
using Autofac.Extras.DynamicProxy;
using CC.Yi.BLL;
using CC.Yi.Common.Cache;
using CC.Yi.Common.Castle;
using CC.Yi.DAL;
using CC.Yi.IBLL;
using CC.Yi.IDAL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CC.Yi.API", Version = "v1" });
            });
            string connection = Configuration["ConnectionStringBySQL"];
            string connection2 = Configuration["ConnectionStringByMySQL"];
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connection, b => b.MigrationsAssembly("CC.Yi.API"));//设置数据库
            });
            //依赖注入转交给Autofac
            //services.AddScoped(typeof(IBaseDal<>), typeof(BaseDal<>));
            //services.AddScoped(typeof(IstudentBll), typeof(studentBll));
            services.AddSingleton(typeof(ICacheWriter), new RedisCacheService(new Microsoft.Extensions.Caching.Redis.RedisCacheOptions()
            {
                Configuration = Configuration.GetSection("Cache.ConnectionString").Value,
                InstanceName = Configuration.GetSection("Cache.InstanceName").Value
            }));
        }

        //动态 面向AOP思想的依赖注入 Autofac
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(CustomAutofacAop));
            builder.RegisterGeneric(typeof(BaseDal<>)).As(typeof(IBaseDal<>)) ;
            builder.RegisterType<studentBll>().As<IstudentBll>().EnableInterfaceInterceptors();//表示注入前后要执行Castle
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CC.Yi.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
