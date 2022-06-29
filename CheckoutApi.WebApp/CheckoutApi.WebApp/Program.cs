using Checkout.Api.BussinessLogic.Startup;
using CheckoutApi.DataAccess.Startup;
using CheckoutApi.Shared.Settings;

var _apiName = "QualysoftAPI.WebApp";
var _apiVersion = "v1";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointHandlers();
builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.Configure<CheckoutApiSettings>(builder.Configuration.GetSection("CheckoutSetting"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_apiName} {_apiVersion}");
    c.RoutePrefix = string.Empty;
});

app.Run();