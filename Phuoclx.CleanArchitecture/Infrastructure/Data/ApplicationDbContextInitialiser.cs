namespace Infrastructure.Data
{
    public static class InitialiserExtension
    {
        public static async Task InitialiserDatabaseAsync(this WebApplication app)
        {
            // Create new IServiceScope that can be used to resolve scope service
            using (var scope = app.Services.CreateScope()) 
            {
                var initialiser =
                    scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

                await initialiser.InitiailiseAsync();

                await initialiser.SeedAsync();
            };
        }
    }

    public class ApplicationDbContextInitialiser
    {
        private readonly APIContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // logger 
        //ILogger<ApplicationDbContextInitialiser> logger;


        public ApplicationDbContextInitialiser(APIContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitiailiseAsync()
        {
            try
            {
                // Apply any pending migrations for the context to database
                // Will create if it does not exist
                await _context.Database.MigrateAsync();
            }catch(Exception ex)
            {
                //_logger.LogError(ex);
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }catch(Exception ex)
            {
                //_logger.LogError(ex);
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            try
            {
                // Default roles
                IdentityRole administratorRole = new(UserRoles.Administrator);
                List<IdentityRole> roles = new() 
                {
                    administratorRole,
                    new (UserRoles.Manager),
                    new (UserRoles.Mentor),
                    new (UserRoles.Member)
                };
                foreach(IdentityRole role in roles)
                {
                    if (_roleManager.Roles.All(x => x.Name != role.Name))
                    {
                        // add new role if not exist
                        await _roleManager.CreateAsync(role);
                    }
                }

                // Default users
                var administrator = new ApplicationUser
                {
                    UserName = "administrator@localhost",
                    Email = "administrator@localhost"
                };

                if(_userManager.Users.All(x => x.UserName != administrator.UserName))
                {
                    await _userManager.CreateAsync(administrator, "@Admin123");
                    if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                    {
                        await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
                    }
                }


                // Default data
                // Seed, if necessary
                //if (!_context.TodoLists.Any())
                //{
                //    _context.TodoLists.Add(new TodoList
                //    {
                //        Title = "Todo List",
                //        Items =
                //{
                //    new TodoItem { Title = "Make a todo list 📃" },
                //    new TodoItem { Title = "Check off the first item ✅" },
                //    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                //    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                //}
                //    });

                //await _context.SaveChangesAsync();

                }catch(Exception ex)
            {
                //_logger.LogError(ex);
                throw;
            }
        }
    }
}
