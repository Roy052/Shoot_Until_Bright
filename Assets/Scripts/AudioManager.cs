using UnityEngine;

public enum SFX
{
    Shoot,
    Reload,
    Broke
}

public class AudioManager : Singleton
{   
    public AudioSource sfxAudioSource;
    public AudioClip[] sfxClip;

    private void Awake()
    {
        if (audioManager == null)
            audioManager = this;
        else
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (audioManager == this)
            audioManager = null;
    }

    public void PlaySFX(SFX sfx)
    {
        sfxAudioSource.Stop();
        sfxAudioSource.clip = sfxClip[(int)sfx];
        sfxAudioSource.Play();
    }
}
