using BigEcommerce.Producer.Sales.Presentation.Extensions;
using BigEcommerce.Producer.Sales.Presentation.Middlewares;
using BigEcommerce.Producer.Sales.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.AddSwaggerWithKestrel();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomValidation();
builder.Services.AddCustomCors(builder.Environment);
builder.Services.AddFluentValidation();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddMediatRConfiguration();
builder.Services.AddRebusConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();
app.UseBigEcommerceExceptionHandling();
app.UseBigEcommercePipeline();
app.UseCustomCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerDocumentation();
app.MapControllers();
SalesDbContextSeed.Seed(app.Services);
app.Run();

//http://localhost:5100/swagger/
