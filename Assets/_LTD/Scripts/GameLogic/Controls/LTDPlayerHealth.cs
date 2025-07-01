using LTD.Core.Managers;
using Unity.VisualScripting.FullSerializer;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDPlayerHealth
    {
        private int _health = 20;
        
        private void Awake()
        {
            Events.DecreasePlayerHealth += Decrease;
        }
        private void OnDestroy()
        {
            Events.DecreasePlayerHealth -= Decrease;
        }
        private void Decrease()
        {
            if (_health == 0)
            {
                Events.PlayerDies.Invoke();
            }
            _health -= 1;
        }
    }
}