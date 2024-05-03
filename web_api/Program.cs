using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using SignalManagerAppWebApi.Data;
using SignalManagerAppWebApi.Models;

namespace SignalManagerAppWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddSingleton<ISignalsDataAccessor>(provider => new SignalsJsonDataAccessor("test_data/test_signals.json"));

            // create configuration with user secrets
            IConfiguration configuration = new ConfigurationBuilder()
                                            .AddUserSecrets<Program>()
                                            .Build();

            // add signals data accesor to DI (with mongo)
            builder.Services.AddScoped<ISignalsDataAccessor>(provider =>
            {
                var connectionString = configuration.GetConnectionString("MongoDB");
                var databaseName = configuration["MongoDB:DatabaseName"];
                var collectionName = configuration["MongoDB:SignalsCollectionName"];

                return new SignalsMongoDataAccessor(connectionString, databaseName, collectionName);
            });

            // add buy and sell orders data accesssors to DI (with mongo)
            builder.Services.AddScoped<IOrdersDataAccessor<BuyOrder>>(provider =>
            {
                var connectionString = configuration.GetConnectionString("MongoDB");
                var databaseName = configuration["MongoDB:DatabaseName"];
                var collectionName = configuration["MongoDB:BuyOrdersCollectionName"];

                return new OrdersMongoDataAccessor<BuyOrder>(connectionString, databaseName, collectionName);
            });

            builder.Services.AddScoped<IOrdersDataAccessor<SellOrder>>(provider =>
            {
                var connectionString = configuration.GetConnectionString("MongoDB");
                var databaseName = configuration["MongoDB:DatabaseName"];
                var collectionName = configuration["MongoDB:SellOrdersCollectionName"];

                return new OrdersMongoDataAccessor<SellOrder>(connectionString, databaseName, collectionName);
            });

            var app = builder.Build();

            app.UseCors("AllowAll");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}