using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    bool _isLoading;
    AsyncOperation _sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        _isLoading = false;
        DontDestroyOnLoad(this);
    }

    public void LoadScene(string loadsceneName, string unloadSceneName)
    {
        if (_isLoading)
            return;

        SceneManager.LoadSceneAsync("LoadScene",LoadSceneMode.Additive);
        _sceneToLoad = SceneManager.LoadSceneAsync(loadsceneName, LoadSceneMode.Additive);
        _sceneToLoad.allowSceneActivation = false;
        
        _isLoading = true;
        StartCoroutine(LoadingScene(unloadSceneName));
    }
    IEnumerator LoadingScene(string unloadSceneName)
    {
        //Input sth before loading Scene and minimum load scene time
        yield return new WaitForSeconds(1.5f);

        float totalProcess = 0;
        while (!_sceneToLoad.isDone)
        {
            totalProcess += _sceneToLoad.progress;
            if (_sceneToLoad.progress >= 0.9f)
            {
                
                // Almost done.
                break;
            }

            yield return null;
        }

        //Input sth after loading Scene
        SceneManager.UnloadSceneAsync(unloadSceneName);
        _sceneToLoad.allowSceneActivation = true;
        _isLoading = false;

        yield return _sceneToLoad;
        UILoadScene.instance.SceneTransitionEffectOut();

    }
}
