using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get { return _instance; }
    }
    private static AudioManager _instance;

    [SerializeField] private AudioSource _BGM;
    [SerializeField] private AudioClip _startSFX;
    [SerializeField] private AudioClip _successSFX;
    [SerializeField] private AudioClip _failureSFX;

    private Queue<AudioSource> _pool;
    private AudioSource _sfxCache;
    private int _volumn;

    private void Awake()
    {
        _instance = this;
        _pool = new Queue<AudioSource>();

        for (int cnt = 0; cnt < 5; cnt++)
        {
            Recovery(CreateAudioSource());
        }

        UpdateVolumn();
    }

    private AudioSource CreateAudioSource()
    {
        GameObject audioSource = new GameObject("SFX");
        audioSource.transform.SetParent(transform);
        audioSource.transform.localPosition = Vector3.zero;

        return audioSource.AddComponent<AudioSource>();
    }

    private AudioSource GetAudioSource()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue();
        }
        else
        {
            return CreateAudioSource();
        }
    }

    private IEnumerator RecoveryAudioSource(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        Recovery(audioSource);
    }

    private void Recovery(AudioSource audioSource)
    {
        _pool.Enqueue(audioSource);
    }

    public void UpdateVolumn()
    {
        _volumn = PlayerPrefs.GetInt("AUDIO_ON", 1);
        _BGM.volume = _volumn == 1 ? 0.3f : 0;
    }

    public void OnStartButtonClicked()
    {
        PlaySFX(_startSFX);
    }

    public void OnCheckSuccess()
    {
        PlaySFX(_successSFX);
    }

    public void OnCheckFailure()
    {
        PlaySFX(_failureSFX);
    }

    private void PlaySFX(AudioClip audioClip)
    {
        _sfxCache = GetAudioSource();
        _sfxCache.volume = _volumn;
        _sfxCache.PlayOneShot(audioClip);
        RecoveryAudioSource(_sfxCache);
    }
}
