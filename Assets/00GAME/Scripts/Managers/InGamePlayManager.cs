using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InGamePlayManager : Singleton<InGamePlayManager>
{
    //Game Controll
    public bool isPause;
    public bool over;
    public bool isSwitching;
    public bool blacked;

    [Header("In Game Variables")]
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

    [Header("Levels")]
    public int level;
    [SerializeField]
    Transform LevelContainer;

    [SerializeField]
    public List<GameObject> levelPrefabs = new List<GameObject>();

    [SerializeField]
    public List<int> bulletsPerLevel = new List<int>();

    private GameObject _currentLevelInstance;
    private Dictionary<int, int> _levelToPrefabIndex = new Dictionary<int, int>();
    
    [Header("Bullet Accumulation System")]
    private int _accumulatedBullets = 0;
    private bool _isFirstLevel = true;
    
    [Header("Lives System")]
    private int _currentLives = 0;
    private Dictionary<int, int> _levelBulletCount = new Dictionary<int, int>();
    private bool _isLevelCompleted = false; // Track if level was completed vs lost
    
    [Header("Score System")]
    private int _currentScore = 0;
    private int _highestScore = 0;
    private int _bulletsShotInCurrentLevel = 0; // Track bullets shot in current level
    [SerializeField]
    TextMeshProUGUI _scoreText;
    
    //Levels
    // Start is called before the first frame update
    void Start()
    {
        blacked = false;
        isPause = false;
        level = 0;

        // Initialize bullet accumulation system
        if (_isFirstLevel)
        {
            _accumulatedBullets = GameManager.instance.initialBulletCount;
            _isFirstLevel = false;
        }
        
        // Initialize lives system
        _currentLives = GameManager.instance.initialLives;
        
        // Initialize score system
        _currentScore = 0;
        _highestScore = PlayerPrefs.GetInt(CONSTANTS.HIGH_SCORE_KEY, 0);
        _bulletsShotInCurrentLevel = 0;

        SpawnCurrentLevel();
        isSwitching = false;
        
        // Reset score for new game
        ResetScore();
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
        }UpdateHighestScoreDisplay();
    }
    IEnumerator PlayOver()
    {
        Debug.Log("PlayOver");
        yield return new WaitForSeconds(0.25f);
        UIPlayScene.instance.BlackSreenFadeIn();
        yield return new WaitForSeconds(0.5f);
        blacked = true;

        if (_isLevelCompleted)
        {
            // Level was completed - advance to next level
            AdvanceToNextLevel();
            level++;
            GameManager.instance.savedLevel = level;
            
            if (level > 24)
            {
                UIPlayScene.instance.PlayEnding();
            }
            else
            {
                // Spawn next level
                SpawnCurrentLevel();
            }
            
            // Reset completion flag
            _isLevelCompleted = false;
        }
        else
        {
            // Level was failed - decrease lives
            _currentLives--;
            
            if (level > 24)
            {
                UIPlayScene.instance.PlayEnding();
            }
            else if (_currentLives <= 0)
            {
                // No more lives - game over, check for high score
                CheckAndSaveHighScore();
                // Go to main menu
                GameManager.instance.ChangeState(GameManager.GAME_STATE.MAINMENU);
            }
            else
            {
                // Still have lives - replay the level with original bullet count
                SpawnCurrentLevel();
            }
        }
        
        yield return new WaitForSeconds(0.25f);
        blacked = false;
        UIPlayScene.instance.BlackSreenFadeOut();
        yield return new WaitForSeconds(0.4f);
        isSwitching = false;
    }

    private void SpawnCurrentLevel()
    {
        if (_currentLevelInstance != null)
        {
            Destroy(_currentLevelInstance);
            _currentLevelInstance = null;
        }

        // Reset level completion flag when spawning a new level
        _isLevelCompleted = false;
        
        // Reset bullets shot counter for new level
        _bulletsShotInCurrentLevel = 0;

        if (levelPrefabs != null && levelPrefabs.Count > 0)
        {
            int prefabIndex = GetPrefabIndexForLevel(level);
            if (prefabIndex >= 0 && prefabIndex < levelPrefabs.Count)
            {
                GameObject prefab = levelPrefabs[prefabIndex];
                if (prefab != null)
                {
                    if (LevelContainer != null)
                        _currentLevelInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity, LevelContainer);
                    else
                        _currentLevelInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                }
            }
        }

        if (BubbleController.instance != null)
        {
            BubbleController.instance.transform.position = new Vector2(0,-3.5f);
            BubbleController.instance.SpawnBubble();
        }

        if (_mainCam != null)
        {
            _mainCam.transform.position = new Vector3(0, 0, _mainCam.transform.position.z);
        }

        // Set bullets using accumulated system instead of per-level fixed amounts
        if (MouseCursorController.instance != null)
        {
            // Check if this is the first time playing this level
            if (!_levelBulletCount.ContainsKey(level))
            {
                // First time playing this level - use accumulated bullets
                _levelBulletCount[level] = _accumulatedBullets;
            }
            else
            {
                // Replaying this level - restore the bullet count from when first played
                _accumulatedBullets = _levelBulletCount[level];
            }
            
            MouseCursorController.instance.bullet = _accumulatedBullets;
        }
    }
    private int GetPrefabIndexForLevel(int levelIndex)
    {
        if (levelPrefabs == null || levelPrefabs.Count == 0)
            return -1;
        int idx;
        if (_levelToPrefabIndex.TryGetValue(levelIndex, out idx))
            return idx;
        // No persistence: do not read PlayerPrefs
        // Build used set from current mappings
        HashSet<int> used = new HashSet<int>();
        foreach (var pair in _levelToPrefabIndex)
        {
            if (pair.Value >= 0 && pair.Value < levelPrefabs.Count)
                used.Add(pair.Value);
        }
        // Build available list excluding used
        List<int> available = new List<int>();
        for (int i = 0; i < levelPrefabs.Count; i++)
        {
            if (!used.Contains(i))
                available.Add(i);
        }
        if (available.Count == 0)
        {
            // all used -> start a new cycle
            available.Clear();
            for (int i = 0; i < levelPrefabs.Count; i++)
                available.Add(i);
            _levelToPrefabIndex.Clear();
        }
        int chosen = available[Random.Range(0, available.Count)];
        _levelToPrefabIndex[levelIndex] = chosen;
        // No persistence: do not write PlayerPrefs
        return chosen;
    }
    
    public void AdvanceToNextLevel()
    {
        // Calculate score for completing this level using new formula
        int levelScore = CalculateLevelScore();
        _currentScore += levelScore;
        
        Debug.Log($"Level {level} completed! Bullets shot: {_bulletsShotInCurrentLevel}, Level Score: {levelScore}, Total Score: {_currentScore}");
        
        // Add 2 bullets when advancing to next level
        _accumulatedBullets += 2;
        
        // Ensure bullets don't go below 0
        if (_accumulatedBullets < 0)
            _accumulatedBullets = 0;
    }
    
    public void CompleteLevel()
    {
        // Mark level as completed and trigger level advancement
        _isLevelCompleted = true;
        over = true;
    }
    
    public void ConsumeBullet()
    {
        _accumulatedBullets--;
        if (_accumulatedBullets < 0)
            _accumulatedBullets = 0;
        
        // Track bullets shot in current level
        _bulletsShotInCurrentLevel++;
    }
    
    public int GetCurrentBulletCount()
    {
        return _accumulatedBullets;
    }
    
    public int GetCurrentLives()
    {
        return _currentLives;
    }
    
    public int GetBulletsShotInCurrentLevel()
    {
        return _bulletsShotInCurrentLevel;
    }
    
    // Score System Methods
    public void AddScore(int points)
    {
        _currentScore += points;
        if (_currentScore < 0)
            _currentScore = 0;
    }
    
    public void SetScore(int score)
    {
        _currentScore = score;
        if (_currentScore < 0)
            _currentScore = 0;
    }
    
    public int GetCurrentScore()
    {
        return _currentScore;
    }
    
    public int GetHighestScore()
    {
        return _highestScore;
    }
    
    private void CheckAndSaveHighScore()
    {
        // Use current accumulated score instead of calculating final score
        int finalScore = _currentScore;
        
        // Check if this is a new high score
        if (finalScore > _highestScore)
        {
            _highestScore = finalScore;
            PlayerPrefs.SetInt(CONSTANTS.HIGH_SCORE_KEY, _highestScore);
            PlayerPrefs.Save();
            Debug.Log("New High Score: " + _highestScore);
        }
        
        Debug.Log("Game Over! Final Score: " + finalScore + " | High Score: " + _highestScore);
    }
    private void UpdateHighestScoreDisplay()
    {
        int highestScore = _currentScore;
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + highestScore.ToString();
        }
    }
    private int CalculateLevelScore()
    {
        // New formula: (500 / số lượng bóng đã bắn ra trong màn) * (1 + (màn vừa vượt qua - 1) * 0.2)
        
        // Prevent division by zero
        if (_bulletsShotInCurrentLevel <= 0)
            _bulletsShotInCurrentLevel = 1;
        
        // Calculate base score: 500 / bullets shot
        float baseScore = 500f / _bulletsShotInCurrentLevel;
        
        // Calculate level multiplier: 1 + (level - 1) * 0.2
        float levelMultiplier = 1f + (level - 1) * 0.2f;
        
        // Final level score
        int finalLevelScore = Mathf.RoundToInt(baseScore * levelMultiplier);
        
        return finalLevelScore;
    }
    
    public void ResetScore()
    {
        _currentScore = 0;
        _bulletsShotInCurrentLevel = 0;
    }
    
    public void ResetGame()
    {
        _currentScore = 0;
        _currentLives = GameManager.instance.initialLives;
        _accumulatedBullets = GameManager.instance.initialBulletCount;
        _isFirstLevel = true;
        level = 0;
        GameManager.instance.savedLevel = 0;
        _levelBulletCount.Clear();
        _levelToPrefabIndex.Clear();
        _bulletsShotInCurrentLevel = 0;
    }
    
    public void StartNewGame()
    {
        ResetGame();
        SpawnCurrentLevel();
    }
}
