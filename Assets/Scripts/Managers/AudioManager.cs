using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum AudioList {
        ActivateRoot,
        DeadRabbit,
        PickFruite,
        PickNutrients,
        ShootImpact,
        Spawn,
        ThrowFruite,
        Water
    }

    [SerializeField] private List<AudioScriptable> _audioList;
    
    [Header("Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    [Header("Mixer")]

    [SerializeField] private AudioMixer _mixer;
    private bool _isFading = false;

    private static AudioManager _instance;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioList audioItem, bool randomSound = false)
    {
        AudioScriptable audioScript = _audioList[(int) audioItem];
        if (!audioScript) return;

        _soundSource.volume = Random.Range(audioScript.volume.x, audioScript.volume.y);
        _soundSource.pitch = Random.Range(audioScript.pitch.x, audioScript.pitch.y);

        if (randomSound)
            _soundSource.PlayOneShot(audioScript.GetRandom());
        else
            _soundSource.PlayOneShot(audioScript.Get(0));
    }

    public void CreateSoundAndPlay(AudioList audioItem)
    {
        AudioScriptable audioScript = _audioList[(int) audioItem];
        if (!audioScript) return;

        GameObject go = new GameObject();
        AudioSource source = go.AddComponent<AudioSource>();

        source.clip = audioScript.Get(0);
        source.Play();

        Destroy(go, source.clip.length / source.pitch);
    }

    public void PlayMusic(AudioList audioItem, bool randomSound = true, int soundIndex = -1)
    {
        if (_isFading) return;

        AudioScriptable audioScript = _audioList[(int) audioItem];
        if (!audioScript || !audioScript.isMusic) return;

        if (randomSound)
            _musicSource.clip = audioScript.GetRandom();
        else
            _musicSource.clip = audioScript.Get(soundIndex);

        _musicSource.volume = Random.Range(audioScript.volume.x, audioScript.volume.y);
        _musicSource.pitch = Random.Range(audioScript.pitch.x, audioScript.pitch.y);

        _musicSource.Play();
    }

    public void FadeBetweenMusic(AudioList musicClip)
    {
        _isFading = true;
        StartCoroutine("FadeOut", musicClip);
    }

    private IEnumerator FadeOut(AudioList musicClip)
    {
        _isFading = false;

        float musicVolume;
        _mixer.GetFloat("MusicVolume", out musicVolume);

        while(musicVolume > -80) {
            
            _mixer.SetFloat("MusicVolume", musicVolume -= 2f);
            yield return new WaitForSeconds(0.1f);
        }

        PlayMusic(musicClip);
        StartCoroutine("FadeIn");
    }

    
    private IEnumerator FadeIn()
    {
        float musicVolume;
        _mixer.GetFloat("MusicVolume", out musicVolume);

        while(musicVolume < -20) {
            _mixer.SetFloat("MusicVolume", musicVolume += 2f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private void OnDestroy()
    {
        if (_instance != null) _instance = null;
    }
    
    public static AudioManager GetInstance
    { 
        get { return _instance; }
    }
}
