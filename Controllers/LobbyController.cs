using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using DominionWeb;
using DominionWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace SignalRAuthenticationSample.Controllers
{
    [Route("api/[controller]")]
    public class LobbyController : Controller
    {
        private readonly DominionContext _context;

        public LobbyController(DominionContext context){
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Lobby> GetLobbies()
        {
            return _context.Lobby.Include(x => x.LobbyUser);
        }
    }
}