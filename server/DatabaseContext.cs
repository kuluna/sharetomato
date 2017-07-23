using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sharetomato
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
