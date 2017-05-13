using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Mexacode.Web.Models;
using Mexacode.Web.Services;

namespace Mexacode.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/ContactUs")]
    public class ContactUsController : Controller
    {
        private IEmailService _emailService;
        public ContactUsController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs([FromBody]EmailModel userInfo)
        {
            try
            {
                await _emailService.SendMailAsync(userInfo);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrio un error al enviar el correo por favor intente mas tarde.");
            }
            return Ok();
        }
    }
}


