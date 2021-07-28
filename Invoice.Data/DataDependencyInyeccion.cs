using AutoMapper;
using Invoice.Core;
using Invoice.Core.Services;
using Invoice.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Data
{
   public static class DataDependencyInyeccion
    {
        public static void AddDataDependency(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>((dbContextBuilder) =>
            {
                dbContextBuilder.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddMappers();

            #region Repository
            services.AddScoped<IRepository<Unit>, BaseRepository<Unit>>();
            services.AddScoped<IRepository<Bill>, BaseRepository<Bill>>();
            services.AddScoped<IRepository<BillDetail>, BaseRepository<BillDetail>>();
            services.AddScoped<IRepository<Supplier>, BaseRepository<Supplier>>();
            services.AddScoped<IRepository<Customer>, BaseRepository<Customer>>();
            services.AddScoped<IRepository<InvoiceHeader>, BaseRepository<InvoiceHeader>>();
            services.AddScoped<IRepository<InvoiceDetail>, BaseRepository<InvoiceDetail>>();
            services.AddScoped<IRepository<Item>, BaseRepository<Item>>();
            #endregion

            #region Service
            services.AddScoped<BaseService<Unit>, UnitService>();
            services.AddScoped<BaseService<Supplier>, SupplierService>();
            services.AddScoped<BaseService<Customer>, CustomerService>();
            services.AddScoped<BaseService<Item>, ItemService>();
            services.AddScoped<BaseService<InvoiceHeader>, InvoiceService>();
            services.AddScoped<BaseService<Bill>, BillService>();
            #endregion
        }

        public static void AddMappers(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfiles>());
            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);
            services.AddScoped<IMapperExtension, MapperExtension>();
        }
    }
}
