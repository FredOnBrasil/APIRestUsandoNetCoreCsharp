using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Curso.api
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
            // onde se customiza a resposta em caso de erro de preenchimento dos dados passados como request para a aplica��o:
            // services.AddControllers(); --- antes da customiza��o
            // ---depois da customiza��o abaixo:
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Adicionamos a permiss�o/configura��o para o swagger acessar o arquivo xml de documenta��o do c�digo
            services.AddSwaggerGen(c =>
            {
                var xmlFile =
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; //pega o nome do arquivo e o caminho com uma tecnica de reflection
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //configura��es para uso do jwt:
            var secret = Encoding.ASCII.GetBytes(Configuration.GetSection("jwtConfigurations:Secret")
                    .Value); //l� a chave configurada em appsettings.json
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //configura��o
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //configura��o
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidIssuer = false.ToString(),
                    ValidAudience = false.ToString()
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // subindo o middleware do swagger
            app.UseSwagger();

            // configurando a rota do swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEB API curso");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
