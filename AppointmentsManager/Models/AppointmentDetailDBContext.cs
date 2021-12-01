using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsManager.Models
{
    public class AppointmentDetailDBContext : DbContext
    {
        public AppointmentDetailDBContext(DbContextOptions<AppointmentDetailDBContext> options) : base(options)
        {
        }
        
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }
    }
}
