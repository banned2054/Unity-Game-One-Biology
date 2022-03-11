using UnityEngine;
using UnityEngine.UI;

public class AudioMgr : MonoBehaviour
{
    private static AudioMgr _instance = null;
    private static AudioSource _audio;
    public Slider AudioSlider;

    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        if (_instance == this) return;
        Destroy(gameObject);
    }

    void Update()
    {
        _audio = GetComponent<AudioSource>();
        _audio.volume = AudioSlider.value;
    }

    public void Play_audio()
    {
        GetComponent<AudioSource>().Play();
    }
}
