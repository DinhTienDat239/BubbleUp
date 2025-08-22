using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GAME_STATE gameState;
    public bool notfirsttime = false;
    public bool cutScenePlayed = false;
    public bool musicOn = true ;
    public bool soundOn = true;
    public bool level;
    public int savedLevel;
    public bool isShowAd = false;
    
    [Header("Bullet System")]
    [SerializeField]
    public int initialBulletCount = 3;
    
    [Header("Lives System")]
    [SerializeField]
    public int initialLives = 3;
    
    public enum GAME_STATE
    {
        MAINMENU = 0,
        PLAY = 1
    } 
    // Start is called before the first frame update
    void Start()
    {
        
        DontDestroyOnLoad(this);
        gameState = GAME_STATE.MAINMENU;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeState(GAME_STATE state)
    {
        if (gameState == state)
            return;

        if (state == GAME_STATE.MAINMENU)
        {
            Cursor.visible = true;
            if (gameState == GAME_STATE.PLAY)
            {
                LoadSceneManager.instance.LoadScene("MainMenuScene","PlayScene");
            }
        }
        if (state == GAME_STATE.PLAY)
        {
            Cursor.visible = false;
            if (gameState == GAME_STATE.MAINMENU)
            {
                LoadSceneManager.instance.LoadScene("PlayScene", "MainMenuScene");
            }
        }

        gameState = state;
    }

}
