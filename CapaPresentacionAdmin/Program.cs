using CapaDatos.Interfaces;
using CapaDatos.Servicio;
using CapaNegocio;
using CapaPresentacionAdmin.Recursos;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddRazorPages().AddNewtonsoftJson();

//Ropositorios
builder.Services.AddTransient<IRepositorioUsuarios, RepositorioUsuarios>();
builder.Services.AddTransient<IRepositorioCategorias, RepositorioCategorias>();
builder.Services.AddTransient<IRepositorioMarcas, RepositorioMarcas>();

//Capa Negocio
builder.Services.AddTransient<ICapaNegocioUsuarios, CapaNegocioUsuarios>();
builder.Services.AddTransient<ICapaNegocioMarcas, CapaNegocioMarcas>();
builder.Services.AddTransient<ICapaNegocioCategorias, CapaNegocioCategorias>();

//Recursos
builder.Services.AddTransient<IClaveEncriptacion, ClaveEncriptacion>();
builder.Services.AddTransient<IEnviarCorreoUsuarios, EnviarCorreoUsuarios>();
builder.Services.AddTransient<IGenerarClaveUsuario, GenerarClaveUsuario>();




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

app.Run();
