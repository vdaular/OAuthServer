using IdentityServer.LdapExtension.Extensions;
using IdentityServer.LdapExtension.UserModel;
using Microsoft.EntityFrameworkCore;
using OAuth2Server.Extensions.Identity;
using OAuth2Server.Models.Config.Auth;
using OAuth2Server.Services.Cache;
using OAuth2Server.Services.Profile;
using System.Reflection;
using OAuth2Server.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using IdentityServer8.Configuration;

var builder = WebApplication.CreateBuilder(args);

const string CORS_POLICY = "AllowSpecificOrigin";

// Cargar archivo de configuración y enlazarlo al modelo AppSettings
AppSettings appSettings = new();
builder.Configuration.Bind(appSettings);

// Configuración de controladores y vistas, necesarias para las páginas de Login, Logout y Consent
builder.Services.AddControllersWithViews();

// Agregar soporte para sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Configuración global de los AppSettings
AppSettingsProvider.Global = appSettings.Global;
AppSettingsProvider.AllowedCrossDomains = appSettings.AllowedCrossDomains;

// Agregar soporte para caché en memoria y caché distribuido con Redis
builder.Services.AddMemoryCache();  // Caché local en memoria
//builder.Services.AddSingleton<ICacheService, RedisService>();  // Servicio de caché basado en Redis
builder.Services.AddDistributedMemoryCache();  // Caché distribuido en memoria

// Obtener la cadena de conexión a la base de datos Oracle desde los archivos de configuración
var connectionString = await CryptoHelper.CryptoHelper.DecryptAsync(builder.Configuration.GetConnectionString("Oracle"));
var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;  // Ensamblado para migraciones

// Configuración de IdentityServer
var identityServer = builder.Services.AddIdentityServer(options =>
{
    // Definir la URI del emisor, que es la URL del servidor de identidad a través de API Gateway
    options.IssuerUri = "https://idmadmin.gire.com:3100";

    // Configurar eventos para rastrear errores, información, fallos y éxitos
    //options.Events.RaiseErrorEvents = true;
    //options.Events.RaiseInformationEvents = true;
    //options.Events.RaiseFailureEvents = true;
    //options.Events.RaiseSuccessEvents = true;

    // Configurar la caché del documento de descubrimiento de IdentityServer
    options.Discovery.ResponseCacheInterval = 60;  // Cachear la respuesta por 60 segundos
});

// Configuración de la base de datos para almacenar la configuración de IdentityServer
// Add-Migration InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
identityServer.AddConfigurationStore(options =>
{
    options.ConfigureDbContext = builder =>
        builder.UseOracle(connectionString, db =>
            db.MigrationsAssembly(migrationsAssembly)  // Definir el ensamblado para las migraciones
            .MigrationsHistoryTable("OAUTH2_MIGRATIONHISTORY", "SEGURIDAD_INFORMATICA") // Tabla para el historial de migraciones
            .UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19));  // Compatibilidad con versiones anteriores de Oracle (Ej.: casos de tipos booleanos)
    options.DefaultSchema = "SEGURIDAD_INFORMATICA";  // Definir el esquema de la base de datos

    options.ApiResource.Name = "OAUTH2_API_RESOURCES";  // Nombre de la tabla para los recursos de la API
    options.ApiResourceClaim.Name = "OAUTH2_API_RESOURCE_CLAIMS";  // Nombre de la tabla para las reclamaciones de los recursos de la API
    options.ApiResourceProperty.Name = "OAUTH2_API_RESOURCE_PROPERTIES";  // Nombre de la tabla para las propiedades de los recursos de la API
    options.ApiResourceScope.Name = "OAUTH2_API_RESOURCE_SCOPES";  // Nombre de la tabla para los alcances de los recursos de la API
    options.ApiResourceSecret.Name = "OAUTH2_API_RESOURCE_SECRETS";  // Nombre de la tabla para los secretos de los recursos de la API
    options.ApiScopeClaim.Name = "OAUTH2_API_SCOPE_CLAIMS";  // Nombre de la tabla para las reclamaciones de los alcances de la API
    options.ApiScopeProperty.Name = "OAUTH2_API_SCOPE_PROPERTIES";  // Nombre de la tabla para las propiedades de los alcances de la API
    options.ApiScope.Name = "OAUTH2_API_SCOPES";  // Nombre de la tabla para los alcances de la API
    options.ClientClaim.Name = "OAUTH2_CLIENT_CLAIMS";  // Nombre de la tabla para las reclamaciones de los clientes de la API
    options.ClientCorsOrigin.Name = "OAUTH2_CLIENT_CORS_ORIGINS";  // Nombre de la tabla para los orígenes CORS de los clientes de la API
    options.ClientGrantType.Name = "OAUTH2_CLIENT_GRANT_TYPES";  // Nombre de la tabla para los tipos de concesión de los clientes de la API
    options.ClientIdPRestriction.Name = "OAUTH2_CLIENT_IDP_RESTRICTIONS";  // Nombre de la tabla para las restricciones de proveedores de identidad de los clientes de la API
    options.ClientPostLogoutRedirectUri.Name = "OAUTH2_CLIENT_POST_LOGOUT_REDIRECT_URIS";  // Nombre de la tabla para las URI de redirección de cierre de sesión de los clientes de la API
    options.ClientProperty.Name = "OAUTH2_CLIENT_PROPERTIES";  // Nombre de la tabla para las propiedades de los clientes de la API
    options.ClientRedirectUri.Name = "OAUTH2_CLIENT_REDIRECT_URIS";  // Nombre de la tabla para las URI de redirección de los clientes de la API
    options.Client.Name = "OAUTH2_CLIENTS";  // Nombre de la tabla para los clientes de la API
    options.ClientScopes.Name = "OAUTH2_CLIENT_SCOPES";  // Nombre de la tabla para los alcances de los clientes de la API
    options.ClientSecret.Name = "OAUTH2_CLIENT_SECRETS";  // Nombre de la tabla para los secretos de los clientes de la API
    options.IdentityResource.Name = "OAUTH2_IDENTITY_RESOURCES";  // Nombre de la tabla para los recursos de identidad
    options.IdentityResourceProperty.Name = "OAUTH2_IDENTITY_RESOURCE_PROPERTIES";  // Nombre de la tabla para las propiedades de los recursos de identidad
    options.IdentityResourceClaim.Name = "OAUTH2_IDENTITY_RESOURCE_CLAIMS";  // Nombre de la tabla para las reclamaciones de los recursos de identidad
});

// Configuración de la base de datos para almacenar los tokens y grants persistentes de IdentityServer
// Add-Migration InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
identityServer.AddOperationalStore(options =>
{
    options.ConfigureDbContext = builder =>
        builder.UseOracle(connectionString, db =>
            db.MigrationsAssembly(migrationsAssembly)
            .MigrationsHistoryTable("OAUTH2_MIGRATIONHISTORY", "SEGURIDAD_INFORMATICA"));
    options.DefaultSchema = "SEGURIDAD_INFORMATICA";
    options.EnableTokenCleanup = true;  // Habilitar limpieza automática de tokens
    options.TokenCleanupInterval = 3600;  // Intervalo de limpieza de tokens cada 3600 segundos (1 hora)

    options.PersistedGrants.Name = "OAUTH2_PERSISTED_GRANTS";
    options.DeviceFlowCodes.Name = "OAUTH2_DEVICE_FLOW_CODES";
});

// Configuración para soporte de usuarios LDAP (Active Directory)
//identityServer.AddLdapUsers<ActiveDirectoryAppUser>(builder.Configuration.GetSection("ActiveDirectory"), UserStore.Redis);

// Servicio para perfiles de usuario
//identityServer.AddProfileService<ProfileService>();

// Configuración de las credenciales de firmado de tokens
//if (builder.Environment.IsDevelopment())
//{
//    identityServer.AddDeveloperSigningCredential();  // Usar credenciales de desarrollo en modo desarrollo
//}
//else
//{
//    identityServer.AddSigningCredentialByRedis(appSettings);  // En producción, usar credenciales desde Redis
//}
identityServer.AddDeveloperSigningCredential();  // Usar credenciales de desarrollo en modo desarrollo


//var aadApp = builder.Configuration.GetSection("AadApp");
//builder.Services.AddOidcStateDataFormatterCache("AADandMicrosoft");

//builder.Services.AddAuthentication()
//.AddOpenIdConnect("AADandMicrosoft", "Microsoft", options =>
//{
//    //  https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration
//    options.ClientId = aadApp["ClientId"];
//    options.ClientSecret = aadApp["ClientSecret"];
//    options.Authority = aadApp["AuthorityUrl"];

//    options.SignInScheme = "idsrv.external";
//    options.RemoteAuthenticationTimeout = TimeSpan.FromSeconds(20);
//    options.ResponseType = "code";
//    options.Scope.Add("profile");
//    options.Scope.Add("email");
//    options.Scope.Add("openid");
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false, // multi tenant => means all tenants can use this
//        NameClaimType = "email",
//    };
//    options.ReturnUrlParameter = "https://localhost:3100/signin-oidc";
//    options.Prompt = "select_account"; // login, consent select_account
//    options.GetClaimsFromUserInfoEndpoint = true;
//    options.MapInboundClaims = true;
//    options.ClaimActions.MapAll();
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        CORS_POLICY,
        builder => builder.WithOrigins(AppSettingsProvider.AllowedCrossDomains)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

var app = builder.Build();

// Configurar manejo de excepciones en modo desarrollo
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseCors(CORS_POLICY);

// Middleware para archivos estáticos
app.UseDefaultFiles();
app.UseStaticFiles();

// Middleware de enrutamiento
app.UseRouting();

// Middleware personalizado para establecer el origen y las rutas al usar un API Gateway
//app.Use(async (ctx, next) =>
//{
//    // Configurar el origen de IdentityServer para los contextos de la solicitud
//    ctx.SetIdentityServerOrigin("https://localhost:3000/oauth");
//    ctx.Request.Scheme = "https";  // Forzar HTTPS en las solicitudes
//    ctx.Request.Host = new HostString("localhost:3000");  // Configurar el host
//    ctx.Request.PathBase = new PathString("/oauth");  // Base de las rutas para OAuth

//    await next();  // Continuar con el siguiente middleware
//});

// Configurar el middleware de IdentityServer
app.UseIdentityServer();

// Habilitar soporte para sesiones
app.UseSession();

// Redireccionar HTTP a HTTPS
app.UseHttpsRedirection();

// Habilitar el middleware de autorización
app.UseAuthorization();

// Mapear la ruta predeterminada de los controladores
app.MapDefaultControllerRoute();

// Inicializar la base de datos al inicio de la aplicación
DatabaseHelpers.InitializeDatabase(app);

app.MapGet("/", () => "OAuth 2.0 Server");

app.Run();
