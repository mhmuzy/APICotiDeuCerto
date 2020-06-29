using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Projeto.Repository.Contracts;
using Projeto.Repository.Repositories;

namespace Projeto.Services
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Injeção de Dependência

            //capturar a string de conexão do arquivo appsettings.json
            var connectionString = Configuration.GetConnectionString("Aula16");

            //mapear as interfaces e classe criadas no repositorio
            services.AddTransient<ISetorRepository, SetorRepository>
                (map => new SetorRepository(connectionString));

            services.AddTransient<IFuncaoRepository, FuncaoRepository>
                (map => new FuncaoRepository(connectionString));

            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>
                (map => new FuncionarioRepository(connectionString));

            #endregion

            #region Configuração do Swagger

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Sistema de Controle de Funcionários",
                        Description = "API REST para integração com serviços de funcionário",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "COTI Informática",
                            Url = new Uri("http://www.cotiinformatica.com.br/"),
                            Email = "contato@cotiinformatica.com.br"
                        }
                    });
                }
                );

            #endregion

            #region CORS

            services.AddCors(c => c.AddPolicy("DefaultPolicy",
                builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            #region Configuração do Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto API");
            });

            #endregion

            #region CORS

            app.UseCors("DefaultPolicy");

            #endregion
        }
    }
}
