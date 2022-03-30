using Microsoft.EntityFrameworkCore;
using SMSSenderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSenderManagement.Repository.SmsManagementContect
{
    public class SMSManagementContext:DbContext
    {
        public SMSManagementContext(DbContextOptions<SMSManagementContext> options):base(options)
        {

        }


        public DbSet<SmsSentHistoryWithotp> OtpAndIds { get; set; }
        public DbSet<MessagesInfo> MessagesTable { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SmsSentHistoryWithotp>().HasKey(x => x.Id);
            modelBuilder.Entity<MessagesInfo>().HasKey(x => x.Id);
        }

    }
}
