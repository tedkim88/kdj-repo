using Azure.Identity;
using DMIT2018Project.Components;
using DMIT2018Project.Components.Account;
using DMIT2018Project.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using POSystem;
using SalesSystem; //added
using System.Text;



var builder = WebApplication.CreateBuilder(args);

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.UTF8;


var keyVaultUri = builder.Configuration["KeyVaultSettings:VaultUri"];
if (!string.IsNullOrEmpty(keyVaultUri))
{
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
}
// ----------------------------------

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.UTF8;

// APIM  HttpClient
builder.Services.AddHttpClient("APIMClient", client =>
{
    // from appsettings.json
    client.BaseAddress = new Uri(builder.Configuration["APIMSettings:BaseAddress"]);

    // Key Vault Secret "APIMSubscriptionKey"   
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", builder.Configuration["APIMSubscriptionKey"]);

    client.DefaultRequestHeaders.Add("Accept", "application/json");
});






// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();




builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// added. code retrieves the eTools2023 connection string
//var connectionStringEtools = builder.Configuration.GetConnectionString("eTools2023");



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//  added: when local dev is needed, just change connectionString to connectionStringEtools
builder.Services.SalesDependencies(options =>
    options.UseSqlServer(connectionString));

//  added: when local dev is needed, just change connectionString to connectionStringEtools
builder.Services.PODependencies(options =>
options.UseSqlServer(connectionString));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
