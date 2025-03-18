using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePlayManager : Singleton<InGamePlayManager>
{
    //Game Controll
    public bool isPause;
    public bool over;
    public bool isSwitching;
    public bool blacked;

    //In Game Variables
    [SerializeField]
    public float bubMoveSpeed;
    [SerializeField]
    public float bubPopForce;
    [SerializeField]
    public float bubShootForce;
    [SerializeField]
    public float bubCollideForce;
    [SerializeField]
    public float windForce;

    [SerializeField]
    Camera _mainCam;

    [SerializeField]
    List<GameObject> pressR = new List<GameObject>();

    [Header("Levels")]
    public int level;

    [SerializeField]
    public GameObject level1;
    [SerializeField]
    public Vector2 level1Spawn;
    [SerializeField]
    public GameObject level2;
    [SerializeField]
    public Vector2 level2Spawn;
    [SerializeField]
    public GameObject level3;
    [SerializeField]
    public Vector2 level3Spawn;
    [SerializeField]
    public GameObject level4;
    [SerializeField]
    public Vector2 level4Spawn;
    [SerializeField]
    public GameObject level5;
    [SerializeField]
    public Vector2 level5Spawn;
    [SerializeField]
    public GameObject level6;
    [SerializeField]
    public Vector2 level6Spawn;
    [SerializeField]
    public GameObject level7;
    [SerializeField]
    public Vector2 level7Spawn;
    [SerializeField]
    public GameObject level8;
    [SerializeField]
    public Vector2 level8Spawn;
    [SerializeField]
    public GameObject level9;
    [SerializeField]
    public Vector2 level9Spawn;
    [SerializeField]
    public GameObject level10;
    [SerializeField]
    public Vector2 level10Spawn;
    [SerializeField]
    public GameObject level11;
    [SerializeField]
    public Vector2 level11Spawn;
    [SerializeField]
    public GameObject level12;
    [SerializeField]
    public Vector2 level12Spawn;
    [SerializeField]
    public Vector2 level13Spawn;
    [SerializeField]
    public Vector2 level14Spawn;
    [SerializeField]
    public Vector2 level15Spawn;
    [SerializeField]
    public Vector2 level16Spawn;
    [SerializeField]
    public Vector2 level17Spawn;
    [SerializeField]
    public Vector2 level18Spawn;
    [SerializeField]
    public Vector2 level19Spawn;
    [SerializeField]
    public Vector2 level20Spawn;
    [SerializeField]
    public Vector2 level21Spawn;
    [SerializeField]
    public Vector2 level22Spawn;
    [SerializeField]
    public Vector2 level23Spawn;
    [SerializeField]
    public Vector2 level24Spawn;
    [SerializeField]
    public Vector2 level25Spawn;
    //Levels
    // Start is called before the first frame update
    void Start()
    {
        blacked = false;
        isPause = false;
        level = GameManager.instance.savedLevel;
        if (level == 0)
        {
            BubbleController.instance.transform.position = level1Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(0, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 1)
        {
            BubbleController.instance.transform.position = level2Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(25, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 2)
        {
            BubbleController.instance.transform.position = level3Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(50, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 3)
        {
            BubbleController.instance.transform.position = level4Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(75, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 4)
        {
            BubbleController.instance.transform.position = level5Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(100, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 5)
        {
            BubbleController.instance.transform.position = level6Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(125, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 6)
        {
            BubbleController.instance.transform.position = level7Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(150, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 7)
        {
            BubbleController.instance.transform.position = level8Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(175, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 8)
        {
            BubbleController.instance.transform.position = level9Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(200, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 9)
        {
            BubbleController.instance.transform.position = level10Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(225, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 10)
        {
            BubbleController.instance.transform.position = level11Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(250, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 11)
        {
            BubbleController.instance.transform.position = level12Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(275, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 12)
        {
            BubbleController.instance.transform.position = level13Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(300, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 13)
        {
            BubbleController.instance.transform.position = level14Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(325, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 3;
        }
        if (level == 14)
        {
            BubbleController.instance.transform.position = level15Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(350, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 1;
        }
        if (level == 15)
        {
            BubbleController.instance.transform.position = level16Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(375, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 16)
        {
            BubbleController.instance.transform.position = level17Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(400, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level == 17)
        {
            BubbleController.instance.transform.position = level18Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(425, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 1;
        }
        if (level == 18)
        {
            BubbleController.instance.transform.position = level19Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(450, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 19)
        {
            BubbleController.instance.transform.position = level20Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(475, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 1;
        }
        if (level == 20)
        {
            BubbleController.instance.transform.position = level21Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(500, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level == 21)
        {
            BubbleController.instance.transform.position = level22Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(525, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level == 22)
        {
            BubbleController.instance.transform.position = level23Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(550, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level == 23)
        {
            BubbleController.instance.transform.position = level24Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(575, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 5;
        }
        if (level == 24)
        {
            BubbleController.instance.transform.position = level25Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(600, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level > 24)
        {
            BubbleController.instance.transform.position = level1Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(0, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
            level = 0;
            GameManager.instance.savedLevel = 0;
        }
        isSwitching = false;
        InvokeRepeating("RunTuTo", 0, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.ChangeState(GameManager.GAME_STATE.MAINMENU);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            InGamePlayManager.instance.over = true;
        }
        if (over)
        {
            over = false;
            if (isSwitching)
                return;

            isSwitching = true;
            StartCoroutine(PlayOver());
        }
    }
    void RunTuTo()
    {
        StartCoroutine(RunTuToIE());
    }

    IEnumerator RunTuToIE()
    {
        foreach (GameObject o in pressR)
        {
            LeanTween.rotateZ(o, -1.2f, 1f).setEase(LeanTweenType.easeOutQuad);
        }
        yield return new WaitForSeconds(1f);
        foreach (GameObject o in pressR)
        {
            LeanTween.rotateZ(o, 1.2f, 1f).setEase(LeanTweenType.easeOutQuad);
        }
    }
    IEnumerator PlayOver()
    {

        yield return new WaitForSeconds(0.25f);
        UIPlayScene.instance.BlackSreenFadeIn();
        yield return new WaitForSeconds(0.5f);
        blacked = true;

        if (level == 0)
        {
            BubbleController.instance.transform.position = level1Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(0, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 1)
        {
            BubbleController.instance.transform.position = level2Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(25, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 2)
        {
            BubbleController.instance.transform.position = level3Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(50, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 3)
        {
            BubbleController.instance.transform.position = level4Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(75, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 4)
        {
            BubbleController.instance.transform.position = level5Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(100, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 5)
        {
            BubbleController.instance.transform.position = level6Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(125, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 6)
        {
            BubbleController.instance.transform.position = level7Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(150, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 7)
        {
            BubbleController.instance.transform.position = level8Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(175, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 8)
        {
            BubbleController.instance.transform.position = level9Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(200, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 9)
        {
            BubbleController.instance.transform.position = level10Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(225, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 10)
        {
            BubbleController.instance.transform.position = level11Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(250, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 11)
        {
            BubbleController.instance.transform.position = level12Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(275, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 12)
        {
            BubbleController.instance.transform.position = level13Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(300, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 13)
        {
            BubbleController.instance.transform.position = level14Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(325, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 3;
        }
        if (level == 14)
        {
            BubbleController.instance.transform.position = level15Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(350, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 1;
        }
        if (level == 15)
        {
            BubbleController.instance.transform.position = level16Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(375, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 16)
        {
            BubbleController.instance.transform.position = level17Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(400, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level == 17)
        {
            BubbleController.instance.transform.position = level18Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(425, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 1;
        }
        if (level == 18)
        {
            BubbleController.instance.transform.position = level19Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(450, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 19)
        {
            BubbleController.instance.transform.position = level20Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(475, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 2;
        }
        if (level == 20)
        {
            BubbleController.instance.transform.position = level21Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(500, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level == 21)
        {
            BubbleController.instance.transform.position = level22Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(525, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level == 22)
        {
            BubbleController.instance.transform.position = level23Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(550, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level == 23)
        {
            BubbleController.instance.transform.position = level24Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(575, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 5;
        }
        if (level == 24)
        {
            BubbleController.instance.transform.position = level25Spawn;
            BubbleController.instance.SpawnBubble();
            _mainCam.transform.position = new Vector3(600, 0, _mainCam.transform.position.z);
            MouseCursorController.instance.bullet = 4;
        }
        if (level > 24)
        {
            UIPlayScene.instance.PlayEnding();
        }
        yield return new WaitForSeconds(0.25f);
        blacked = false;
        UIPlayScene.instance.BlackSreenFadeOut();
        yield return new WaitForSeconds(0.4f);
        isSwitching = false;


    }
}
