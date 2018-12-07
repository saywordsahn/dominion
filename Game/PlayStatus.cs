namespace DominionWeb.Game
{
    public enum PlayStatus
    {
        //TODO: refactor, Responder and ActionRequestResponder are confusing and unclear
        GameStart,
        WaitForTurn,
        BuyPhase,
        ActionPhase,
        Attacker,
        Responder,
        ActionRequestResponder
    }
}