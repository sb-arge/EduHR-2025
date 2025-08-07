using EduHR.Api.Middlewares;
using EduHR.Application;
using EduHR.Domain.Entities;
using EduHR.Infrastructure;
using EduHR.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Adım: Servislerin Bağımlılık Enjeksiyonu Konteynerine Eklenmesi (DI) ---

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddIdentity<User, Role>(options =>
{
    // Parola Ayarları
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;

    // Kullanıcı Ayarları
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// --- 2. Adım: Kimlik Doğrulama ve Yetkilendirme Yapılandırması ---

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured in appsettings.json.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization(options => { /* ... Politikalar buraya eklenecek ... */ });

// --- Diğer Servisler ---

builder.Services.AddControllers();

// API Versiyonlama servisini DI konteynerine ekler.
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true; // Versiyon belirtilmezse varsayılanı kullan
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0); // Varsayılan versiyon v1.0
    options.ReportApiVersions = true; // Yanıt başlıklarında desteklenen versiyonları bildir
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Swagger'da "v1" gibi gruplama sağlar
    options.SubstituteApiVersionInUrl = true; // URL'de {version:apiVersion} parametresini değiştirir
});


// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Swagger UI'da JWT ile yetkilendirme yapabilmek için kilit/authorize butonu ekler
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Lütfen 'Bearer {token}' formatında JWT token'ınızı girin.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});


// --- Uygulamayı İnşa Etme ---
var app = builder.Build();


// --- HTTP İstek Pipeline'ının Yapılandırılması ---

// 1. Geliştirme ortamı için Swagger'ı etkinleştir.
// Bu, üretim ortamında API dokümantasyonunuzun görünmesini engeller.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. Özel Hata Yakalama Middleware'i.
// Pipeline'da en başa yakın olmalıdır ki, sonraki adımlarda oluşabilecek tüm hataları yakalayabilsin.
app.UseMiddleware<ExceptionHandlerMiddleware>();

// 3. HTTPS Yönlendirmesi.
// Gelen HTTP isteklerini otomatik olarak HTTPS'e yönlendirir.
app.UseHttpsRedirection();

// 4. Yönlendirme (Routing).
// Gelen isteğin hangi endpoint ile eşleştiğini belirler.
app.UseRouting();

// 5. Kimlik Doğrulama (Authentication).
// Gelen istekteki JWT token'ı doğrular ve kullanıcının kimliğini (HttpContext.User) belirler.
// YETKİLENDİRMEDEN ÖNCE ÇAĞRILMALIDIR!
app.UseAuthentication();

// 6. Yetkilendirme (Authorization).
// Kimliği belirlenmiş olan kullanıcının, eşleşen endpoint'e erişim izni olup olmadığını kontrol eder.
app.UseAuthorization();

// 7. Özel Middleware'lerimiz.
// Bu adımlar, kullanıcı kimliği ve yetkisi belirlendikten sonra çalışmalıdır.
app.UseMiddleware<TenantResolverMiddleware>();
app.UseMiddleware<CultureMiddleware>();

// 8. Controller'ları Eşleştirme.
// Son olarak, isteği ilgili Controller'daki Action metoduna yönlendirir.
app.MapControllers();

app.Run();