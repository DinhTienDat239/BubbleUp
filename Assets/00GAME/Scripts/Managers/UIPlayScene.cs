using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayScene : Singleton<UIPlayScene>
{
    [SerializeField]
    GameObject _pauseScreen;
    [SerializeField]
    Image _blackScreen;
    [SerializeField]
    Button _skipLevelBtn;

    [SerializeField]
    List<Sprite> numbs = new List<Sprite>();
    [SerializeField]
    Image _bulletTxt;
    [SerializeField]
    Image _imageTxt;

    [SerializeField]
    GameObject endingScreen;
    [SerializeField]
    Image endingBG;
    [SerializeField]
    List<Sprite> endings = new List<Sprite>();
    [SerializeField]
    Button toMenu;
    [SerializeField]
    Slider _progressSlider;
    [SerializeField]
    Image _progressSliderTop;
    [SerializeField]
    Image tuto;
    [SerializeField]
    Image tuto2;
    // Start is called before the first frame update
    void Start()
    {
        endingScreen.gameObject.SetActive(false);
        _blackScreen.gameObject.SetActive(false);
        _pauseScreen.SetActive(false); endingScreen.SetActive(false);
        InvokeRepeating("RunTuTo", 0, 2f);
    }
    public void SkipLevel()
    {
        GameDistribution.Instance.ShowRewardedAd();
        _skipLevelBtn.interactable = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(InGamePlayManager.instance.level == 0 || InGamePlayManager.instance.level == 24)
        {
            _progressSliderTop.gameObject.SetActive(false);
        }
        else
        {
            _progressSliderTop.gameObject.SetActive(true);
        }
        if(InGamePlayManager.instance.level <= 24)
        {
            _bulletTxt.sprite = numbs[MouseCursorController.instance.bullet];
            _imageTxt.sprite = numbs[InGamePlayManager.instance.level + 1];
        }
        UpdateProgressSlider();
    }
    void RunTuTo()
    {
        StartCoroutine(RunTuToIE());
    }
    IEnumerator RunTuToIE()
    {
        LeanTween.rotateZ(tuto.transform.gameObject, -1.2f, 1f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.rotateZ(tuto2.transform.gameObject, -1.2f, 1f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(1f);
        LeanTween.rotateZ(tuto.transform.gameObject, 1.2f, 1f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.rotateZ(tuto2.transform.gameObject, 1.2f, 1f).setEase(LeanTweenType.easeOutQuad);
    }
    public void UpdateProgressSlider()
    {
        _progressSlider.value = ((float)InGamePlayManager.instance.level)/25;
    }
    public void BlackSreenFadeIn()
    {
        _blackScreen.color = new Color(0, 0, 0, 0);
        _blackScreen.gameObject.SetActive(true);
        LeanTween.color(_blackScreen.rectTransform, Color.black, 0.4f);
    }
    public void BlackSreenFadeOut()
    {
        _blackScreen.color = Color.black;
        LeanTween.color(_blackScreen.rectTransform, new Color(0,0,0,0), 0.4f);
        Invoke("TurnOffBlackScreen",0.5f);
    }
    void TurnOffBlackScreen()
    {
        _blackScreen.gameObject.SetActive(false);
    }
    public void PlayEnding()
    {
        StartCoroutine(PlayEndingIE());
    }
    public IEnumerator PlayEndingIE()
    {
        endingScreen.SetActive(true);
        toMenu.gameObject.SetActive(false);
        int index = 0;
        while (index < endings.Count)
        {
            yield return new WaitForSeconds(0.2f);
            endingBG.sprite = endings[index];
            index++;
        }
        yield return new WaitForSeconds(2f);
        toMenu.gameObject.SetActive(true);
    }
    public void endBtn()
    {
        GameManager.instance.ChangeState(GameManager.GAME_STATE.MAINMENU);
    }
    #region Pause Screen
    public void ShowPauseScreen()
    {
        _pauseScreen.SetActive(true);
    }
    public void ContinueBtn()
    {
        InGamePlayManager.instance.isPause = false;
        _pauseScreen.SetActive(false);
    }
    public void MenuBtn()
    {
        GameManager.instance.ChangeState(GameManager.GAME_STATE.MAINMENU);
    }
    #endregion
}
