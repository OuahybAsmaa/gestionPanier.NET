using Microsoft.EntityFrameworkCore;
using projetPanier.Data;
using projetPanier.Services;

var builder = WebApplication.CreateBuilder(args);

// 1?? Ajouter la connexion à la base de données
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2?? Ajouter Razor Pages
builder.Services.AddRazorPages();

// Permet d'accéder au HttpContext (cookies)
builder.Services.AddHttpContextAccessor();

// Enregistrer ton service panier
builder.Services.AddScoped<PanierService>();


var app = builder.Build();

// 4?? Configurer le pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
