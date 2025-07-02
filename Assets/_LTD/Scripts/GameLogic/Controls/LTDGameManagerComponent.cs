using LTD.GameLogic.BaseMono;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDGameManagerComponent : LTDBaseMono
    {
        void Start()
        {
            CoreManager.GameManager.InstantiatePlayer();
            CoreManager.GameManager.InstantiateBoss();
        }
    }
}