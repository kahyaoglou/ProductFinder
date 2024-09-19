using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProductFinder.Business.Abstract;
using ProductFinder.Business.Concrete;
using ProductFinder.DataAccess.Abstract;
using ProductFinder.DataAccess.Concrete;
using ProductFinder.Entities;
using System.Text;
using ProductFinder.Business.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
        };
    });

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddControllers(); //Controller kullanmak içingerekli.

builder.Services.AddEndpointsApiExplorer(); //Swagger'ýn API'mizdeki tüm endpoint'leri otomatik olarak keþfetmesini saðlar. 

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "ProductFinder API",
        Description = "ProductFinder API for managing products"
    });

    // JWT Bearer token için Swagger ayarlarý
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// FluentValidation'ý projeye ekliyoruz
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddSingleton<IValidator<Product>, ProductValidator>();

builder.Services.AddSingleton<IProductService, ProductManager>();
//Senden cotr'da IProductService istiyorsam bana ProductManager ver.
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
//Senden cotr'da IProductRepositroy istiyorsam bana ProductRepository ver.

builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = (doc =>
    {
        doc.Info.Title = "ProductFinder API";
        doc.Info.Version = "1.0.12";
    });
});
//Swagger dokümantasyonu eklemek için kullanýlan NSwag kütüphanesine ait bir yapýlandýrmadýr.

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || !app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseSwagger(); //uygulamanýzýn API dokümantasyonunu JSON formatýnda sunan bir endpoint oluþturur.
                      //Bu JSON dokümaný, Swagger UI gibi araçlar tarafýndan API'nizi görselleþtirmek ve test etmek için kullanýlýr.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List); // Tüm endpointleri default olarak listeler.
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseOpenApi(); //Swagger için gerekli.
app.UseSwaggerUi(); //Swagger için gerekli.

app.UseCors("AllowAll");

app.UseEndpoints(endpoints => { endpoints.MapControllers(); }); //Controller kullanmak için gerekli

app.MapRazorPages();

app.Run();
