using LTD.GameLogic.BaseMono;
using UnityEngine;

namespace LTD.GameLogic.Controls
{
    public class LTDAudioManager : LTDBaseMono
    {

        public static LTDAudioManager Instance;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip menuMusic;
        [SerializeField] private AudioClip gameMusic;
        
      //  [SerializeField] private AudioClip spellCastSFX;
     //   [SerializeField] private AudioClip playerHurtSFX;
       // [SerializeField] private AudioClip playerDeathSFX;
       
   //    [SerializeField] private AudioClip smallDevilsSoundSFX;
      /// [SerializeField] private AudioClip smallDevilsDeathSFX;
       
      // [SerializeField] private AudioClip devilsHurtSFX;
       //[SerializeField] private AudioClip devilsShootSFX;
      // [SerializeField] private AudioClip lockSFX;
       

        private AudioSource musicSource;
        private AudioSource sfxSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                musicSource = gameObject.AddComponent<AudioSource>();
                sfxSource = gameObject.AddComponent<AudioSource>();

                musicSource.loop = true;
                musicSource.volume = 0.5f;
                PlayMenuMusic();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayMenuMusic() => PlayMusic(menuMusic);
        public void PlayGameMusic() => PlayMusic(gameMusic);

        private void PlayMusic(AudioClip clip)
        {
            if (clip == null) return;
            musicSource.clip = clip;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

    }
}