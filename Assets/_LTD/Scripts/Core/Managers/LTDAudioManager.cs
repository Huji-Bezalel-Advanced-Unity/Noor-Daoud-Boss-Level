using LTD.Core.BaseMono;
using UnityEngine;
using System.Collections.Generic;

namespace LTD.Core.Managers.AudioManager
{
    public class LTDAudioManager : LTDBaseMono
    {
        public static LTDAudioManager Instance;

        public enum AudioClipType
        {
            MenuMusic,
            GameMusic,
            SpellCast,
            DevilsHurt,
            Lock,
            PowerUp
        }

        [System.Serializable]
        public struct AudioClipEntry
        {
            public AudioClipType type;
            public AudioClip clip;
        }

        [Header("Audio Clips")]
        [SerializeField] private AudioClipEntry[] audioClipEntries;

        private Dictionary<AudioClipType, AudioClip> audioClips;

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

                LoadAudioClips();

                PlayMusic(AudioClipType.MenuMusic);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void LoadAudioClips()
        {
            audioClips = new Dictionary<AudioClipType, AudioClip>();
            foreach (var entry in audioClipEntries)
            {
                if (!audioClips.ContainsKey(entry.type))
                {
                    audioClips.Add(entry.type, entry.clip);
                }
            }
        }

        public void PlayMusic(AudioClipType type)
        {
            if (audioClips.TryGetValue(type, out AudioClip clip) && musicSource != null)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }

        public void PlaySFX(AudioClipType type)
        {
            if (audioClips.TryGetValue(type, out AudioClip clip) && sfxSource != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }
    }
}
