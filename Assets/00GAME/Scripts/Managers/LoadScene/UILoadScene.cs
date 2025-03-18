using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoadScene : Singleton<UILoadScene>
{
    [SerializeField]
    Image _imgTransEffect;
    [SerializeField]
    AudioClip _start;
    [SerializeField]
    AudioClip _end;
    // Start is called before the first frame update
    void Start()
    {
        SceneTransitionEffectIn();
    }
    
    public void SceneTransitionEffectIn()
    {
        AudioManager.instance.PlayEffect(_start, false);
        _imgTransEffect.transform.localPosition = new Vector2(3500, 0);
        LeanTween.move(_imgTransEffect.rectTransform, Vector2.zero, 0.5f).setEase(LeanTweenType.easeOutQuad);
    }
    public void SceneTransitionEffectOut()
    {
        AudioManager.instance.PlayEffect(_end, false);
        LeanTween.move(_imgTransEffect.rectTransform, new Vector2(-3500, 0), 0.5f).setEase(LeanTweenType.easeInQuad);
        Invoke("UnloadLoadScene", 0.5f);
    }
    public void UnloadLoadScene()
    {
        SceneManager.UnloadSceneAsync("LoadScene");
    }
}
