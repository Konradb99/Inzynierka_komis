using CarsNeuralApplication.Services;
using CarsNeuralInfrastructure;
using CarsNeuralInfrastructure.Repositories;
using CarsNeuralInfrastructure.Validators;
using CarsNeuralKNN.Algorithm;
using CarsNeuralKNN.Algorithms;
using CarsNeuralKNN.Encoders;
using CarsNeuralKNN.Metrics;
using CarsNeuralNetwork.Encoders;
using CarsNeuralNetwork.Services;
using CarsNeuralNetworkAccurancyTester.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<ICarsRepository, CarsRepository>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddTransient<IDataRepository, DataRepository>();
builder.Services.AddScoped<IKNNService, KNNService>();
builder.Services.AddScoped<IDataEncoder, DataEncoder>();
builder.Services.AddScoped<INeuralNetworkDataEncoder, NeuralNetworkDataEncoder>();
builder.Services.AddScoped<INeuralNetworkService, NeuralNetworkService>();
builder.Services.AddScoped<IMetrics, Metrics>();
builder.Services.AddScoped<ICarValidator, CarValidator>();
builder.Services.AddScoped<IVotingService, VotingService>();
builder.Services.AddScoped<IFiltersRepository, FiltersRepository>();
builder.Services.AddScoped<IDataNormalizer, DataNormalizer>();
builder.Services.AddScoped<IDataClassifier, DataClassifier>();
builder.Services.AddScoped<IAccurancyTesterService, AccurancyTesterService>();

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    builder.Services.AddDbContext<CarsNeuralDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringProd"));
    });
}
else
{
    builder.Services.AddDbContext<CarsNeuralDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
    });
}

builder.Services.BuildServiceProvider().GetService<CarsNeuralDbContext>().Database.Migrate();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }