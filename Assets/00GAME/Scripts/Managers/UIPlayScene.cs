using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIPlayScene : Singleton<UIPlayScene>
{
    [SerializeField]
    Image _blackScreen;

    [SerializeField]
    List<Sprite> numbs = new List<Sprite>();
    [SerializeField]
    Image _bulletTxt;
    [SerializeField]
    Image _livesTxt;
    
    [Header("Score Display")]
    [SerializeField]
    Image _scoreTxt;

    [SerializeField]
    public RectTransform endingScreen;
    [SerializeField]
    Image endingBG;
    [SerializeField]
    TextMeshProUGUI _scoreText;
    [SerializeField]
    List<Sprite> endings = new List<Sprite>();
    [SerializeField]
    Button toMenu;
    // Start is called before the first frame update
    void Start()
    {
        _blackScreen.gameObject.SetActive(false);
        endingScreen.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(InGamePlayManager.instance.level <= 24)
        {
            _bulletTxt.sprite = numbs[MouseCursorController.instance.bullet];
            
            // Display lives count
            if (_livesTxt != null)
            {
                int lives = InGamePlayManager.instance.GetCurrentLives();
                if (lives >= 0 && lives < numbs.Count)
                {
                    _livesTxt.sprite = numbs[lives];
                }
            }
            
            // Display current score
            if (_scoreTxt != null)
            {
                int score = InGamePlayManager.instance.GetCurrentScore();
                if (score >= 0 && score < numbs.Count)
                {
                    _scoreTxt.sprite = numbs[score];
                }
                else
                {
                    // For scores beyond the sprite list, show a default or calculate display
                    // You might want to add more sprite numbers or use TextMeshPro for larger numbers
                    _scoreTxt.sprite = numbs[Mathf.Min(score, numbs.Count - 1)];
                }
            }
        }
        
        // Update score display when score changes
        UpdateScoreDisplay();
        UpdateHighestScoreDisplay();
    }
    
    private void UpdateScoreDisplay()
    {
        if (_scoreTxt != null)
        {
            int score = InGamePlayManager.instance.GetCurrentScore();
            if (score >= 0 && score < numbs.Count)
            {
                _scoreTxt.sprite = numbs[score];
            }
            else
            {
                // For scores beyond the sprite list, show a default or calculate display
                _scoreTxt.sprite = numbs[Mathf.Min(score, numbs.Count - 1)];
            }
        }
    }private void UpdateHighestScoreDisplay()
    {
        int highestScore = InGamePlayManager.instance.GetCurrentScore();
        if (_scoreText != null)
        {
            _scoreText.text = "Your Score: " + highestScore.ToString();
        }
        PlayerPrefs.SetInt("LASTSCORE", InGamePlayManager.instance.GetCurrentScore());
    }
    public void BlackSreenFadeIn()
    {
        _blackScreen.color = new Color(0, 0, 0, 0);
        _blackScreen.gameObject.SetActive(true);
        _blackScreen.DOColor(Color.black, 0.4f);
    }
    public void BlackSreenFadeOut()
    {
        _blackScreen.color = Color.black;
        _blackScreen.DOColor(new Color(0,0,0,0), 0.4f);
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
        endingScreen.gameObject.SetActive(true);
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
    public void ContinueBtn()
    {
        InGamePlayManager.instance.isPause = false;
    }
    public void MenuBtn()
    {
        GameManager.instance.ChangeState(GameManager.GAME_STATE.MAINMENU);
    }
    #endregion
}
