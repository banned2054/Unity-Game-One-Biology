using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class AudioMgr : MonoBehaviour
    {
        private static AudioMgr    _instance;
        private static AudioSource _audio;

        public  Slider      audioSlider;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            if (_instance == this) return;
            Destroy(gameObject);
        }

        private void Update()
        {
            _audioSource.volume = audioSlider.value;
        }

        public void Play_audio() => GetComponent<AudioSource>().Play();
    }
}
