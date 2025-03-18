using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuScene : MonoBehaviour
{
    [SerializeField]
    GameObject _mainScreen;
    [SerializeField]
    GameObject _settingScreen;

    [Header("Setting Components")]
    [SerializeField]
    List<Sprite> _sounds = new List<Sprite>(); 
    [SerializeField]
    List<Sprite> _sounds2 = new List<Sprite>();
    [SerializeField]
    Image _sound;
    [SerializeField]
    List<Sprite> _musics = new List<Sprite>();
    [SerializeField]
    List<Sprite> _musics2 = new List<Sprite>();
    [SerializeField]
    Image _music;
    [SerializeField]
    List<Sprite> _backs = new List<Sprite>();
    [SerializeField]
    Image _back;
    [SerializeField]
    List<Sprite> _options = new List<Sprite>();
    [SerializeField]
    Image _option;

    

    [Header("Menu Components")]
    [SerializeField]
    Image menuBG;
    [SerializeField]
    List<Sprite> menuSprites = new List<Sprite>();

    [SerializeField]
    Image title;
    [SerializeField]
    Button _playBtn;
    [SerializeField]
    Button _settingBtn;
    [Header("Cut Scene Components")]
    [SerializeField]
    GameObject cutSceneScreen;
    [SerializeField]
    Image cutSceneBG;
    [SerializeField]
    List<Sprite> cutSceneSprites = new List<Sprite>();

    [Header("Sounds")]
    [SerializeField]
    AudioClip _bgMusic;
    [SerializeField]
    AudioClip _buttonSelection;

    [Header("Flash Screen Components")]
    [SerializeField]
    Image flash; 
    [SerializeField]
    List<Sprite> flashes = new List<Sprite>();
    int menuSpriteIndex = 0; 
    // Start is called before the first frame update
    void Start()
    {
        GameDistribution.Instance.ShowAd();
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Init()
    {
        if (!GameManager.instance.notfirsttime)
        {
            AudioManager.instance.AdjustBGVolume(0.8f);
            AudioManager.instance.AdjustEffectVolume(0.8f);
            //StartCoroutine(FlashScreen());
            GameManager.instance.notfirsttime = true;
        }
        else
        {
            flash.gameObject.SetActive(false);
        }
        MusicBtn();
        SoundBtn();
        MusicBtn();
        SoundBtn();
        AudioManager.instance.PlayBG(_bgMusic,false);
        _playBtn.transform.localScale = Vector3.zero;
        _settingBtn.transform.localScale = Vector3.zero;
        LeanTween.scale(_playBtn.gameObject, Vector3.one, 0.5f);
        LeanTween.scale(_settingBtn.gameObject, Vector3.one, 0.5f);

        _mainScreen.SetActive(true);
        _settingScreen.SetActive(false);
        cutSceneScreen.gameObject.SetActive(false);

        InvokeRepeating("RunAnimBG", 0, 0.2f);
        InvokeRepeating("RunButtonAnim", 0, 2f);
        InvokeRepeating("RunTitleAnim", 0, 2f);
        InvokeRepeating("RunSettingsAnim", 0, 0.3f);
    }
    IEnumerator FlashScreen()
    {
        flash.color = new Color(0, 0, 0);
        LeanTween.color(flash.rectTransform, Color.white, 0.5f);

        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while(i < flashes.Count)
        {
            flash.sprite = flashes[i];
            yield return new WaitForSeconds(0.3f);
            i++;
        }
        yield return new WaitForSeconds(0.5f);
        LeanTween.color(flash.rectTransform, new Color(0, 0, 0,0) , 0.5f);
        yield return new WaitForSeconds(0.5f);
        flash.gameObject.SetActive(false);

    }
    IEnumerator Play()
    {
        if (!GameManager.instance.cutScenePlayed)
        {
            cutSceneScreen.gameObject.SetActive(true);
            cutSceneBG.color = new Color(0, 0, 0, 0);
            LeanTween.color(cutSceneBG.rectTransform, Color.white, 1f);
            yield return new WaitForSeconds(1f);
            int index = 0;
            while (index < cutSceneSprites.Count)
            {
                yield return new WaitForSeconds(0.2f);
                cutSceneBG.sprite = cutSceneSprites[index];
                index++;
            }
            GameManager.instance.cutScenePlayed = true; 
        }
        GameManager.instance.ChangeState(GameManager.GAME_STATE.PLAY);
    }
    #region Button
    public void SkipBtn()
    {
        GameManager.instance.cutScenePlayed = true;
        GameManager.instance.ChangeState(GameManager.GAME_STATE.PLAY);
    }
    public void MusicBtn()
    {
        if (GameManager.instance.musicOn)
        {
            GameManager.instance.musicOn = false;
            AudioManager.instance.AdjustBGVolume(0f);
        }
        else
        {
            GameManager.instance.musicOn = true;
            AudioManager.instance.AdjustBGVolume(0.8f);
        }
    }
    public void SoundBtn()
    {
        if (GameManager.instance.soundOn)
        {
            GameManager.instance.soundOn = false;
            AudioManager.instance.AdjustEffectVolume(0f);
        }
        else
        {
            GameManager.instance.soundOn = true;
            AudioManager.instance.AdjustEffectVolume(0.8f);
        }
    }

    public void PlayBtn()
    {
        //GameManager.instance.ChangeState(GameManager.GAME_STATE.PLAY);
        AudioManager.instance.PlayEffect(_buttonSelection, false);
        StartCoroutine(Play());
    }
    public void SettingBtn()
    {
        AudioManager.instance.PlayEffect(_buttonSelection, false);
        _mainScreen.SetActive(false);
        _settingScreen.SetActive(true);
    }
    public void ExitBtn()
    {
        Application.Quit();
    }
    public void BackBtn()
    {
        _mainScreen.SetActive(true);
        _settingScreen.SetActive(false);
    }
    void RunAnimBG()
    {
        if (menuSpriteIndex >= menuSprites.Count)
            menuSpriteIndex = 0;

        menuBG.sprite = menuSprites[menuSpriteIndex];

        menuSpriteIndex++;
    }
    void RunTitleAnim()
    {
        StartCoroutine(RunTitleAnimIE());
    }
    IEnumerator RunTitleAnimIE()
    {

        LeanTween.rotateZ(title.gameObject, -1.5f, 1f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(1f);
        LeanTween.rotateZ(title.gameObject, 1.5f, 1f).setEase(LeanTweenType.easeOutQuad);
    }
    void RunSettingsAnim()
    {
        if(_settingScreen.activeSelf)
            StartCoroutine(RunSettingAnimIE());
    }
    IEnumerator RunSettingAnimIE()
    {
        int i = 0;
        if (GameManager.instance.musicOn)
        {
            _music.sprite = _musics[i];
        }
        else
        {
            _music.sprite = _musics2[i];
        }
        if (GameManager.instance.soundOn)
        {
            _sound.sprite = _sounds[i];
        }
        else
        {
            _sound.sprite = _sounds2[i];
        }
        _back.sprite = _backs[i];
        _option.sprite = _options[i];
        yield return new WaitForSeconds(0.1f);
        i++;
        if (GameManager.instance.musicOn)
        {
            _music.sprite = _musics[i];
        }
        else
        {
            _music.sprite = _musics2[i];
        }
        if (GameManager.instance.soundOn)
        {
            _sound.sprite = _sounds[i];
        }
        else
        {
            _sound.sprite = _sounds2[i];
        }
        _back.sprite = _backs[i];
        _option.sprite = _options[i];
        yield return new WaitForSeconds(0.1f);
        i++;
        if (GameManager.instance.musicOn)
        {
            _music.sprite = _musics[i];
        }
        else
        {
            _music.sprite = _musics2[i];
        }
        if (GameManager.instance.soundOn)
        {
            _sound.sprite = _sounds[i];
        }
        else
        {
            _sound.sprite = _sounds2[i];
        }
        _back.sprite = _backs[i];
        _option.sprite = _options[i];
        yield return new WaitForSeconds(0.1f);
    }
    void RunButtonAnim()
    {
        StartCoroutine(RunButtonAnimIE());
    }
    IEnumerator RunButtonAnimIE()
    {

        LeanTween.rotateZ(_playBtn.gameObject, 1.75f, 1f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.rotateZ(_settingBtn.gameObject, 1.75f, 1f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(1f);
        LeanTween.rotateZ(_playBtn.gameObject, -1.75f, 1f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.rotateZ(_settingBtn.gameObject, -1.75f, 1f).setEase(LeanTweenType.easeOutQuad);
    }
    #endregion
}
