using Microsoft.EntityFrameworkCore;
using VaultContracts.BusinessLogicContracts;
using VaultContracts.StoragesContracts;
using VaultDatabase;
using VaultDatabase.Implements;
using VaultBusinessLogic.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IAccountStorage, AccountStorage>();
builder.Services.AddTransient<ITransactionStorage, TransactionStorage>();

builder.Services.AddTransient<IAccountLogic, AccountLogic>();
builder.Services.AddTransient<ITransactionLogic, TransactionLogic>();

builder.Services.AddDbContext<VaultContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
