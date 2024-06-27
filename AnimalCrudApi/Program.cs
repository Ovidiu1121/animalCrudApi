


using AnimalCrudApi.Animals.Repository;
using AnimalCrudApi.Animals.Repository.interfaces;
using AnimalCrudApi.Animals.Service;
using AnimalCrudApi.Animals.Service.Interfaces;
using AnimalCrudApi.Data;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);

        var app = builder.Build();

        Configure(app);

        using (var scope = app.Services.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("Default")!,
                new MySqlServerVersion(new Version(8, 0, 21))));

        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddMySql5()
                .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
        builder.Services.AddScoped<IAnimalCommandService, AnimalCommandService>();
        builder.Services.AddScoped<IAnimalQueryService, AnimalQueryService>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    private static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}

// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
//
// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseMySql(builder.Configuration.GetConnectionString("Default")!,
//         new MySqlServerVersion(new Version(8, 0, 21))));
//
// builder.Services.AddFluentMigratorCore()
//     .ConfigureRunner(rb => rb
//         .AddMySql5()
//         .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
//         .ScanIn(typeof(Program).Assembly).For.Migrations())
//     .AddLogging(lb => lb.AddFluentMigratorConsole());
//
// builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
// builder.Services.AddScoped<IAnimalCommandService, AnimalCommandService>();
// builder.Services.AddScoped<IAnimalQueryService, AnimalQueryService>();
//
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
//
// app.UseAuthorization();
//
// app.MapControllers();
//
// using (var scope = app.Services.CreateScope())
// {
//     var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
//     runner.MigrateUp();
// }
//
// app.Run();
