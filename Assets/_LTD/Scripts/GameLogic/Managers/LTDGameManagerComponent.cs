using LTD.Core.BaseMono;

namespace LTD.Core.Managers.AudioManager
{
    public class LTDGameManagerComponent : LTDBaseMono
    {
        private void Start()
        {
            CoreManager.GameManager.InstantiatePlayer();
            CoreManager.GameManager.InstantiateBoss();
            LTDAudioManager.Instance.PlayMusic(LTDAudioManager.AudioClipType.GameMusic);
        }
    }
}