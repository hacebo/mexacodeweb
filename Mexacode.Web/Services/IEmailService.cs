using Mexacode.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mexacode.Web.Services
{
    public interface IEmailService
    {
        Task SendMailAsync(EmailModel email);
    }
}
