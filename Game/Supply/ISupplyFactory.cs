using Microsoft.AspNetCore.Mvc;

namespace DominionWeb.Game.Supply
{
    public interface ISupplyFactory
    {
        Supply Create();
    }
}