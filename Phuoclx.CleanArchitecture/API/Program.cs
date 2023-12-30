var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<APIContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option => {
    option.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<APIContext>()
    .AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenApplication at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddMediatR(typeof(MediatREntryPoint).Assembly);

// Add Identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//                .AddEntityFrameworkStores<DriverLicenseLearningSupportContext>();

//builder.Services.AddDbContext<DriverLicenseLearningSupportContext>((sp, options) => {
//    options.UseSqlServer(connectionString);
//});

// Add Infrastructure Config
builder.Services.AddInfrastructureServices(configuration);

// Add AutoMapper
var mapperConfig = new MapperConfiguration(mc => {
    mc.AddProfile(new MappingExtensions());
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());

// Configure AppSettings
builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

// Add Authentication
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new()
    {
        //ValidIssuer = configuration["AppSettings:Issuer"],
        //ValidAudience = configuration["AppSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Key"]!)),

        // auto provide token 
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidateLifetime = true,
        // sign in token 
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Add Authorization
//builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.AdminUserPolicyName, p =>
        p.RequireClaim(IdentityData.AdminUserClaimName,
                       Roles.Administrator));
    options.AddPolicy(IdentityData.ManagerUserPolicyName, m =>
        m.RequireClaim(IdentityData.ManagerUserClaimName, 
                       Roles.Manager));
});

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IRoleManagerService, RoleManagerService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// initialise Database
app.InitialiserDatabaseAsync().Wait();

app.UseHttpsRedirection();

// Add authentication middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
