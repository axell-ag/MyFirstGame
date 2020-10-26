using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffector : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpSound, _coinSound, _winSound, _loseSound;

    public void PlayJumpSound()
    {
        _audioSource.PlayOneShot(_jumpSound);
    }
    public void PlayCoinSound()
    {
        _audioSource.PlayOneShot(_coinSound);
    }
    public void PlayWinSound()
    {
        _audioSource.PlayOneShot(_winSound);
    }
    public void PlayLoseSound()
    {
        _audioSource.PlayOneShot(_loseSound);
    }
}
