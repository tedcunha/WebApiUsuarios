using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApiUsuarios.Business.Implementations;
using WebApiUsuarios.Business.Interface;
using WebApiUsuarios.Model.Context;
using WebApiUsuarios.Repositorio.Generico;
using WebApiUsuarios.Repositorio.Generico.Interface;

namespace WebApiUsuarios
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region "Conectando com o Banco"
            // Conectando com o Banco
            var connectioString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connectioString));
            #endregion

            services.AddControllers();

            #region "INJEÇÃO DE DEPENDENCIA"
            // Business
            services.AddScoped<IUsuariosBusiness, UsuariosBusinessImpl>();

            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
