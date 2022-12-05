using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NavbarManagement.Database.Models;

namespace NavbarManagement.Database.Configurations
{
    public class SubNavbarConfiguration : IEntityTypeConfiguration<SubNavbar>
    {
        public void Configure(EntityTypeBuilder<SubNavbar> builder)
        {
            builder
               .HasOne(sn => sn.Navbar)
               .WithMany(n => n.SubNavbars)
               .HasForeignKey(sn => sn.NavbarId);
        }
    }
}