using LTD.GameLogic.BaseMono;

namespace LTD.GameLogic.Managers
{
    /// <summary>
    /// Initializes the core gameplay systems at the start of the scene.
    /// This includes spawning the player and boss, and starting background music.
    /// </summary>
    public class LTDGameManagerComponent : LTDBaseMono
    {
        /// <summary>
        /// Unity Start method — called before the first frame update.
        /// Sets up the player, boss, and background music for the level.
        /// </summary>
        private void Start()
        {
            CoreManager.GameManager.InstantiatePlayer();
            CoreManager.GameManager.InstantiateBoss();

            LTDAudioManager.Instance.PlayMusic(LTDAudioManager.AudioClipType.GameMusic);
        }

    }
}