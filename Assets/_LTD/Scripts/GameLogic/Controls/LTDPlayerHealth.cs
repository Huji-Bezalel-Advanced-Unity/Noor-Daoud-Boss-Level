using LTD.GameLogic.Controls;
using Unity.VisualScripting.FullSerializer;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDPlayerHealth
    {
        private int _health = 20;
        
        private void Awake()
        {
            LTDEvents.DecreasePlayerHealth += Decrease;
        }
        private void OnDestroy()
        {
            LTDEvents.DecreasePlayerHealth -= Decrease;
        }
        private void Decrease()
        {
            if (_health == 0)
            {
                LTDEvents.PlayerDies.Invoke();
            }
            _health -= 1;
        }
    }
}