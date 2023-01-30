using Basket.API.Repositories;
using Basket.API.Services;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddGrpc();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(client => client.Address = new Uri(builder.Configuration.GetSection("Grpc:ServerUrl").Value));
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddTransient<IBasketRepository, BasketRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();
Console.WriteLine("Running...");
app.Run();