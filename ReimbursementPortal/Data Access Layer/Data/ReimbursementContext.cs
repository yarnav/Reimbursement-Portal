using Data_Access_Layer.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Data
{
    public class ReimbursementContext: DbContext
    {
        public ReimbursementContext()
        {
        }

        public ReimbursementContext(DbContextOptions<ReimbursementContext> options)
            :base(options)
        {

        }
        public DbSet<ReimbursementEntity>Reimbursement { get; set; }
        public DbSet<EmployeeEntity> Employee { get; set; }
    }
}
