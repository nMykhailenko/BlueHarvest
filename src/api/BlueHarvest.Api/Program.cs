using BlueHarvest.Modules.Transactions.Api;
using BlueHarvest.Modules.Users.Api;
using BlueHarvest.Shared.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSharedInfrastructure();
builder.Services.AddUsersModule();
builder.Services.AddTransactionsModule();

var app = builder.Build();

app.UseSharedInfrastructure();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseUsersModule();
app.UseTransactionsModule();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", 
        context => context.Response.WriteAsync("BlueHarvest API"));
});

app.Run();