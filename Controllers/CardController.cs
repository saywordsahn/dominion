using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DominionWeb.Game.Cards;
using DominionWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DominionWeb.Controllers
{
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        [HttpGet("[action]")]
        public int[] CardCosts()
        {
            return CardFactory.GetCardCostArray();
        }

    }
}
