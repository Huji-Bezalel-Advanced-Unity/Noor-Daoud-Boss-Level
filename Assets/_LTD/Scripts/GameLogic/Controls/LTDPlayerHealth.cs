using LTD.Core.Managers;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDPlayerHealth
    {
        private int _health = 20;
        
        private void Awake()
        {
            Events.DecreaseHealth += Decrease;
        }
        private void OnDestroy()
        {
            Events.DecreaseHealth -= Decrease;
        }
        private void Decrease()
        {
            if (_health == 0)
            {
                Events.Die.Invoke();
            }
            _health -= 1;
        }
    }
}