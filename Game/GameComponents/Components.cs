namespace DominionWeb.Game.GameComponents
{
    public class Components
    {
        public SpoilsPile Spoils { get; set; }
                
        public Components()
        {
            Spoils = new SpoilsPile(15);
        }
    }
}