using CRUD_PRAC.Data;
using CRUD_PRAC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

//cors 
builder.Services.AddCors(options => {

    options.AddDefaultPolicy(builder =>
    {
        var f_url = configuration.GetValue<string>("front_end_url");
        builder.WithOrigins().AllowAnyMethod().AllowAnyHeader();
    });
});
// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => 
options.UseSqlServer(connectionString));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "poc_apis",
        Version = "v1"
    });
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAvailablityService, AvailablityService>();
builder.Services.AddScoped<IStripePayService, StripePayService>();
object value = builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();
StripeConfiguration.ApiKey = "sk_test_51KUSexH97OQctAcHwgEDd46ge4T69VcUXANs39P63tYcjkEnMBe8qyzqVCiJoZxsjOoClCTfMiCLThqIUr5jgPcE00KP4Y9Fqx"; // "sk_test_tR3PYbcVNZZ796tH88S4VQ2u";
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
