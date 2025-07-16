using UnityEngine;
using System.Collections.Generic;
using LTD.GameLogic.BaseMono;

namespace LTD.GameLogic.Managers
{
    /// <summary>
    /// Manages background music and sound effects playback throughout the game.
    /// Implements a singleton pattern and supports audio clip lookup via enum keys.
    /// </summary>
    public class LTDAudioManager : LTDBaseMono
    {
        /// <summary>
        /// Singleton instance of the audio manager.
        /// </summary>
        public static LTDAudioManager Instance;


        #region Types & Structs

        /// <summary>
        /// Enum that defines all supported audio clip types.
        /// </summary>
        public enum AudioClipType
        {
            MenuMusic,
            GameMusic,
            SpellCast,
            DevilsHurt,
            Lock,
            PowerUp
        }

        /// <summary>
        /// Struct that pairs an audio clip with its corresponding AudioClipType enum.
        /// </summary>
        [System.Serializable]
        public struct AudioClipEntry
        {
            public AudioClipType type;
            public AudioClip clip;
        }

        #endregion

        #region Fields

        [Header("Audio Clips")]
        [SerializeField] private AudioClipEntry[] audioClipEntries;

        /// <summary>
        /// Dictionary mapping clip types to audio clips for fast lookup.
        /// </summary>
        private Dictionary<AudioClipType, AudioClip> audioClips;

        /// <summary>
        /// AudioSource used for playing background music.
        /// </summary>
        private AudioSource musicSource;

        /// <summary>
        /// AudioSource used for playing sound effects.
        /// </summary>
        private AudioSource sfxSource;

        #endregion

        #region Unity Methods

        /// <summary>
        /// Initializes the audio manager, sets up AudioSources, and loads all configured clips.
        /// </summary>
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

        #endregion

        #region Clip Loading

        /// <summary>
        /// Loads all audio clips from the serialized array into the dictionary for quick access.
        /// </summary>
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

        #endregion

        #region Playback Methods

        /// <summary>
        /// Plays a background music clip based on the provided AudioClipType.
        /// </summary>
        /// <param name="type">The type of audio clip to play.</param>
        public void PlayMusic(AudioClipType type)
        {
            if (audioClips.TryGetValue(type, out AudioClip clip) && musicSource != null)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }

        /// <summary>
        /// Plays a one-shot sound effect based on the provided AudioClipType.
        /// </summary>
        /// <param name="type">The type of audio clip to play.</param>
        public void PlaySFX(AudioClipType type)
        {
            if (audioClips.TryGetValue(type, out AudioClip clip) && sfxSource != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        #endregion
    }
}
