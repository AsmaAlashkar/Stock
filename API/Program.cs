using API.Extensions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.CategoryRepo;
using Repository.Identity;
using Repository.MainWearHouse;
using Repository.Service;
using Repository.SubWearHouse;
using Repository.VMainWearhouseItem;
using Repository.VWearhouseWithSubHierarchy;
using Standard.Entities;
using Standard.Mapping.AddressDtoProf;
using Standard.Mapping.CategoryDtoProf;
using Standard.Mapping.mainwearhouseProf;
using Standard.Mapping.SubwearhouseProf;
using Standard.Mapping.ViewWearHItemProf;
using Standard.Mapping.ViewWearhouseWithSubHierarchyProf;


var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<StockContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));


builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));


// Repositories
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 
builder.Services.AddScoped<IMWHRepository, MWHRepository>();
builder.Services.AddScoped<ISWHRepository, SWHRepository>();
builder.Services.AddScoped<IVWHIRepository, VWHIRepository>();
builder.Services.AddScoped<IVWHIWHRepository,VWHIWHRepository >();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();




//Mapping
builder.Services.AddAutoMapper(

    typeof(MainWearhouseProfile),
    typeof(SubWearhouseProfile),
    typeof(ViewWearHItemProfile),
    typeof(ViewWearhouseWithSubHierarchyProfile),
    typeof(AddressProf),
    typeof(CategoryProfile)

    );


builder.Services.AddEndpointsApiExplorer();
// Add controller services
builder.Services.AddControllers();

// Add role-related services if needed (e.g., Identity services)
builder.Services.AddIdentityServices(_config);

// Add AutoMapper services if needed
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options =>
{
    // Add file upload support

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock", Version = "v1" });
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "JWT Auth Bearer Schema",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition("Bearer", securitySchema);
    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    };
    options.AddSecurityRequirement(securityRequirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHsts();

app.UseCors(x => x
    //.WithOrigins("http://localhost:4200")
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map controller routes
    /*endpoints.MapFallbackToController("Index", "Fallback");*/ // Map fallback route to Fallback controller's Index action
});

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    // Add roles
    //var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
    //await IdentityServiceExtensions.AddRoles(roleManager);
}

// Run the app
app.Run();
