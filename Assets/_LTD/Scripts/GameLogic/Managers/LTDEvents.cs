using System;
using _LTD.Scripts.GameLogic;

namespace LTD.Core.Managers
{
    public class LTDEvents
    {
        public static  Action SafeZone;
        public static  Action RedZone;
        public static Action<int> DecreasePlayerHealth;
        public static Action IncreasePlayerHealth;
        
        public static Action PlayerDies;
        public static Action DecreaseDevilHealth;
        public static Action DevilDies;
        public static Action PlayerShoot;
        public static Action IncreasePlayerSpeed;
        
        
    }
}