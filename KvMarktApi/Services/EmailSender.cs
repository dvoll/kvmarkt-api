using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }


    public class UserService : IUserService
    {

        // private readonly ApplicationDbContext _context;

        public UserService(
            // ApplicationDbContext context
        ) {
            // _context = context;
        }

        private Contributor contributor;
        public Contributor Contributor
        {
            get { return contributor;}
            set { contributor = value;}
        }
        
        private ApplicationUser user;
        public ApplicationUser User
        {
            get { return user;}
            set { 
                // contributor = _context.Set<Contributor>().Where(x => x.Email == value.Email).FirstOrDefault();
                user = value;
            }
        }
        
    }

    public interface IUserService {
        Contributor Contributor { get; set; }
        ApplicationUser User { get; set; }
    }
}
