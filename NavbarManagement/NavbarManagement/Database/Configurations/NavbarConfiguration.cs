using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NavbarManagement.Database.Models;

namespace NavbarManagement.Database.Configurations
{
    public class NavbarConfiguration : IEntityTypeConfiguration<Navbar>
    {
        public void Configure(EntityTypeBuilder<Navbar> builder)
        {
            builder
                .ToTable("Navbars");
        }
    }
}