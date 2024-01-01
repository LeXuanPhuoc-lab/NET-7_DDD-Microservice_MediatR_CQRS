using Application.Common.Extensions;
using Infrastructure.Repository.Cached;

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

// Add Mediator
builder.Services.AddMediatR(typeof(MediatREntryPoint));
// Add IPipelineBehaviour for Mediator
// AddTranisent Run every time request
// Register with <,> : For any <TRequest,TResponse>
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
// Register for all validator in an assembly
builder.Services.AddValidatorsFromAssembly(typeof(ValidatorConfigurer).Assembly);

//builder.Services.AddMediatR(cfg => {
//    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
//    //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
//    //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
//    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
//    //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
//});



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

builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
// Scrutor
builder.Services.Decorate<IIdentityRepository, CachedIdentityRepository>();
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

// Add Middleware exception for to pipeline will catch exception/log/re-excute
// Add FluentValidation Exception Handler
app.UseFluentValidationExceptionHandler();

// This only use for handle exception without use LanguageExt.Commond.Result 
//app.UseFluentValidationExceptionHandler();

app.UseHttpsRedirection();


// Add authentication middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
