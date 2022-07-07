using Api.Logs.Extensions;
using Api.Utils;
using Application.Utils;
using Application.Utils.Interfaces;
using CheatCodesUI2022.Server;
using Microsoft.AspNetCore.ResponseCompression;
using Persistance;
using Persistance.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Add builder.Services to the container.
builder.Services.AddSingleton<IScopeInformation, ScopeInformation>();
var connectionString = builder.Configuration.GetConnectionString("CheatCodesDatabase");//using secrets.json
var con = new DatabaseSetting(connectionString);
builder.Services.AddSingleton(con);
var queriesConnectionString = new QueriesConnectionString(connectionString);
builder.Services.AddSingleton(queriesConnectionString);
// builder.Services.AddTransient<IDbTransaction, DbTransaction>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICategoryQueryRepository, CategoryQueryRepository>();
builder.Services.AddTransient<ICategoryCommandRepository, CategoryCommandRepository>();
builder.Services.AddSingleton<Messages>();

builder.Services.AddHandlers();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
