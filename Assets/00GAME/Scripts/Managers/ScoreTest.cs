using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    [Header("Test Controls")]
    [SerializeField] private KeyCode addScoreKey = KeyCode.S;
    [SerializeField] private KeyCode resetScoreKey = KeyCode.R;
    [SerializeField] private KeyCode showScoreKey = KeyCode.T;
    [SerializeField] private KeyCode simulateLevelCompleteKey = KeyCode.L;
    
    void Update()
    {
        // Test adding score manually
        if (Input.GetKeyDown(addScoreKey))
        {
            if (InGamePlayManager.instance != null)
            {
                InGamePlayManager.instance.AddScore(50);
                Debug.Log("Added 50 points manually! Current Score: " + InGamePlayManager.instance.GetCurrentScore());
            }
        }
        
        // Test resetting score
        if (Input.GetKeyDown(resetScoreKey))
        {
            if (InGamePlayManager.instance != null)
            {
                InGamePlayManager.instance.ResetScore();
                Debug.Log("Score reset! Current Score: " + InGamePlayManager.instance.GetCurrentScore());
            }
        }
        
        // Test showing current score and high score
        if (Input.GetKeyDown(showScoreKey))
        {
            if (InGamePlayManager.instance != null)
            {
                Debug.Log("Current Score: " + InGamePlayManager.instance.GetCurrentScore());
                Debug.Log("Highest Score: " + InGamePlayManager.instance.GetHighestScore());
                Debug.Log("Bullets shot in current level: " + InGamePlayManager.instance.GetBulletsShotInCurrentLevel());
            }
        }
        
        // Test simulating level completion (for testing score calculation)
        if (Input.GetKeyDown(simulateLevelCompleteKey))
        {
            if (InGamePlayManager.instance != null)
            {
                Debug.Log("Simulating level completion...");
                // This will trigger the score calculation when advancing to next level
                InGamePlayManager.instance.AdvanceToNextLevel();
            }
        }
    }
}
