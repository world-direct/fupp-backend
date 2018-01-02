using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.NetCore.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Our Hackathon FUPP Application\n";
        }
    }
}
