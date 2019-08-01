using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StockApi.DAL;
using StockApi.DAL.Entities;
using StockApi.DAL.TranslationTables;

namespace StockApi
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connectionString = Configuration["connectionStrings:libraryDBConnectionString"];
            services.AddDbContext<StockContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IStockRepository, StockRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(
                options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Stockart, StockArtDto>()
                   .ForMember(dest => dest.productId, map => map.MapFrom(src => src.Id))
                   .ForMember(dest => dest.productName, map => map.MapFrom(src => src.MakeName))
                   .ForMember(dest => dest.productCode, map => map.MapFrom(src => src.Code))
                   .ForMember(dest => dest.releaseDate, map => map.MapFrom(src => src.ReleaseDate))
                   .ForMember(dest => dest.starRating, map => map.MapFrom(src => src.ScoreRating))
                   .ForMember(dest => dest.price, map => map.MapFrom(src => src.StockPrice))
                    .ForMember(dest => dest.imageUrl, map => map.MapFrom(src => src.ImageUrl))
                     .ForMember(dest => dest.description, map => map.MapFrom(src => src.Description));

                cfg.CreateMap<StockartForCreationDto, Stockart>();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
