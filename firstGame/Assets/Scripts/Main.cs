using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _textCoin;
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _isLife, _nonLife;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private AudioSource _musicSource, _soundSource;
    [SerializeField] private SoundEffector _soundEffector;

    public void ReloadLvl()
    {
        Time.timeScale = 1f;
        _player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void Start()
    {
        _musicSource.volume = (float)PlayerPrefs.GetInt("MusicVolume") / 9;
        _soundSource.volume = (float)PlayerPrefs.GetInt("SoundVolume") / 9;
    }

    private void Update()
    {
        _textCoin.text = _player.GetCoin().ToString();

        for (int i = 0; i < _hearts.Length; i++)
        {
            if (_player.GetHP() > i)
                _hearts[i].sprite = _isLife;
            else
                _hearts[i].sprite = _nonLife;
        }
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
        _player.enabled = false;
        _pauseScreen.SetActive(true);
    }

    public void PauseOff()
    {
        Time.timeScale = 1f;
        _player.enabled = true;
        _pauseScreen.SetActive(false);
    }

    public void Lose()
    {
        _soundEffector.PlayLoseSound();
        Time.timeScale = 0f;
        _player.enabled = false;
        _loseScreen.SetActive(true);

        if (PlayerPrefs.HasKey("Coins"))
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + _player.GetCoin());
        else
            PlayerPrefs.SetInt("Coins", + _player.GetCoin());
    }

    public void Win()
    {
        _soundEffector.PlayWinSound();
        Time.timeScale = 0f;
        _player.enabled = false;
        _winScreen.SetActive(true);

        if (!PlayerPrefs.HasKey("Level") || PlayerPrefs.GetInt("Level") < SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);

        if (PlayerPrefs.HasKey("Coins"))
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + _player.GetCoin());
        else
            PlayerPrefs.SetInt("Coins", + _player.GetCoin());
    }

    public void NextLvl()
    {
        Time.timeScale = 1f;
        _player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        _player.enabled = true;
        SceneManager.LoadScene("Menu");
    }
}