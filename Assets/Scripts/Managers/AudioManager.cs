using UnityEngine;

public enum Sound
{
    Shot,
    Step,
    Reload,
    Damage
}
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip _shotSFX;
    [SerializeField] private AudioClip _stepSFX;
    [SerializeField] private AudioClip _reloadSFX;
    [SerializeField] private AudioClip _damageSFX;

    [SerializeField] private AudioSource _playerAudio;

    public void PlaySFX(Sound sound)
    {
        if (_playerAudio.isPlaying)
            _playerAudio.Stop();
        switch (sound)
        {
            case Sound.Shot:
                _playerAudio.clip = _shotSFX;
                break;
            case Sound.Step:
                _playerAudio.clip = _stepSFX;
                break;
            case Sound.Reload:
                _playerAudio.clip = _reloadSFX;
                break;
            case Sound.Damage:
                _playerAudio.clip = _damageSFX;
                break;
        }
        _playerAudio.Play();
    }
}
