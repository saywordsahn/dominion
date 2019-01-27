using System;
namespace DominionWeb.Game.Log
{
    public class LogItem
    {
        public GameLogType LogType { get; set; }
        public bool Private { get; set; }
    }
}
