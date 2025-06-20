using Demo.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DAL.presistance.Data.Configurations.Employees
{
    class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FirstName).HasColumnType("varchar(50)").IsRequired();

            builder.Property(e => e.LastName).HasColumnType("varchar(50)").IsRequired();
                        
            builder.Property(e => e.Salary).HasColumnType("decimal(8,3)");

            builder.HasOne(e => e.Manager)
            .WithMany()
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}