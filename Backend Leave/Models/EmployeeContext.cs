using Microsoft.EntityFrameworkCore;

namespace Backend_Leave.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveApply> LeaveApplys { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
