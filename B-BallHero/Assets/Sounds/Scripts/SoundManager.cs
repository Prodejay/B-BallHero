using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BBallHero.Gameplay.Sound
{
    [RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private SoundEffect[] _soundList;

        private AudioSource _audioSource;

        public static SoundManager instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundEffect(SoundType sound, float volume = 1f)
        {
            AudioClip[] clip = instance._soundList[(int)sound].Sounds;
            AudioClip randomClip = clip[UnityEngine.Random.Range(0, clip.Length)];
            instance._audioSource.PlayOneShot(randomClip, volume);         
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            string[] names = Enum.GetNames(typeof(SoundType));
            Array.Resize(ref _soundList, names.Length);
            for (int i = 0; i < _soundList.Length; i++)
            {
                _soundList[i].name = names[i];
            }
        }
#endif
    }

    [Serializable]
    public struct SoundEffect
    {
        [SerializeField]
        private AudioClip[] _sounds;
        public AudioClip[] Sounds { get => _sounds; }
        [HideInInspector]
        public string name;
    }

    public enum SoundType
    {
        DRIBBLE,
        BACKBOARD,
        MLGHORN

    }

}
