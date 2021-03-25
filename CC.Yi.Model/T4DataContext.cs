using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CC.Yi.Model
{
    public partial class DataContext :IdentityDbContext
    {
        public DbSet<student> student { get; set; }
    }
}