using LTD.Core.BaseMono;

namespace LTD.Core.Managers
{
    public class LTDGameManagerComponent : LTDBaseMono
    {
        private void Start()
        {
            CoreManager.GameManager.InstantiatePlayer();
            CoreManager.GameManager.InstantiateBoss();
            LTDAudioManager.Instance.PlayGameMusic();
        }
    }
}