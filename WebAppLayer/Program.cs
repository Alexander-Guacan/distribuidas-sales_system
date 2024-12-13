using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

// Configuración para manejar sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(2); // Duración de la sesión
    options.Cookie.HttpOnly = true;                // Solo accesible desde HTTP
    options.Cookie.IsEssential = true;            // Necesario para el correcto funcionamiento
});

// Configuración de la autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Account/Login";  // Ruta para redirigir al login
        options.AccessDeniedPath = "/Account/AccessDenied"; // Ruta en caso de acceso denegado
    });

var app = builder.Build();

// Configuración del pipeline de solicitudes
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Redirección a HTTPS
app.UseHttpsRedirection();

// Servir archivos estáticos
app.UseStaticFiles();

// Middleware para encabezados de seguridad
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self';");

    context.Response.Headers.Append("X-Content-Type-Options", "nosniff"); // Protección contra MIME sniffing
    context.Response.Headers.Append("X-Frame-Options", "DENY");          // Protección contra clickjacking
    context.Response.Headers.Append("Referrer-Policy", "no-referrer");   // Controla el encabezado Referer
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block"); // Protección contra XSS en navegadores más antiguos

    await next();
});

// Middleware para evitar caché en rutas protegidas
app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        context.Response.Headers["Pragma"] = "no-cache";
        context.Response.Headers["Expires"] = "0";
    }
    await next();
});
// Configuración del routing
app.UseRouting();

// Middleware para sesiones
app.UseSession();

// Middleware para autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Configuración de las rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecutar la aplicación
app.Run();