using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private string _loadLevel;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _bar;

    public void Load ()
    {
        _loadingScreen.SetActive(true);
        SceneManager.LoadScene(_loadLevel);
        //StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_loadLevel);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            _bar.value = asyncLoad.progress;

            if(asyncLoad.progress >= .9f && !asyncLoad.allowSceneActivation)
            {
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
