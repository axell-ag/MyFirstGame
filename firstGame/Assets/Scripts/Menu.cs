using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button[] _level;
    [SerializeField] private Text _coinText;
    [SerializeField] private Slider _musicSlider, _soundSlider;
    [SerializeField] private Text _musicText, _soundText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
            for (int i = 0; i < _level.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("Level"))
                    _level[i].interactable = true;
                else
                    _level[i].interactable = false;
            }

        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetInt("MusicVolume", 5);
        if (!PlayerPrefs.HasKey("SoundVolume"))
            PlayerPrefs.SetInt("SoundVolume", 5);

        _musicSlider.value = PlayerPrefs.GetInt("MusicVolume");
        _soundSlider.value = PlayerPrefs.GetInt("SoundVolume");

    }

    private void Update()
    {
        PlayerPrefs.SetInt("MusicVolume", (int)_musicSlider.value);
        PlayerPrefs.SetInt("SoundVolume", (int)_soundSlider.value);
        _musicText.text = _musicSlider.value.ToString();
        _soundText.text = _soundSlider.value.ToString();

        if (PlayerPrefs.HasKey("Coins"))
            _coinText.text = PlayerPrefs.GetInt("Coins").ToString();
        else
            _coinText.text = "0";
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void DelKey()
    {
        PlayerPrefs.DeleteAll();
    }
}
