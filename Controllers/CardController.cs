using DominionWeb.Game.Cards;
using Microsoft.AspNetCore.Mvc;

namespace DominionWeb.Controllers
{
    [Route("api/[controller]")]
    public class CardController : Controller
    {
        [HttpGet("[action]")]
        public int[] CardCosts()
        {
            //return CardFactory.GetCardCostArray();
            return new int[] {1, 2, 3};
        }

    }
}
