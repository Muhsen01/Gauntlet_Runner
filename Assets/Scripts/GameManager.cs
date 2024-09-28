using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextureScroller gorund;
    public float gameTime = 10;

    float totalTimeElapsed = 0; // Total time elapsed during gameplay
    float totalDistanceTraveled = 0;
    bool isGameOver = false;

    public float powerUpTimeIncrease = 3f; // Amount to increase game time by when a power-up is collected
    public float worldSlowDuration = 1f; // Duration of the world slowing down effect

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the time scale is set to 1 at the beginning
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is over to prevent further updates
        if (isGameOver)
            return;

        float speed = 5f;

        // Update elapsed time and game time
        totalTimeElapsed += Time.deltaTime;
        gameTime -= Time.deltaTime;

        totalDistanceTraveled += speed * Time.deltaTime;

        if (gameTime <= 0)
            GameOver();
    }

    // Method to adjust game time
    public void AdjustTime(float amount)
    {
        gameTime += amount; // Increase or decrease game time based on the provided amount
        if (amount < 0)
            SlowWorldDown(); // Slow down the world if the adjustment is a penalty
    }

    private void SlowWorldDown()
    {
        CancelInvoke(); // Cancel any existing invokes
        Time.timeScale = 0.5f; // Set the time scale to slow down the game
        Invoke("SpeedWorldUp", worldSlowDuration); // Invoke a method to speed the world back up after a delay
    }

    void SpeedWorldUp()
    {
        Time.timeScale = 1f;
    }

    void GameOver()
    {
        isGameOver = true;
    }

    // Method to restart the game by reloading the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // this is using Unity's legacy GUI system
    private void OnGUI()
    {
        if (!isGameOver)
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = 26; // Change the font size for the box
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 22; // Change the font size for the label

            Rect boxRect = new Rect(Screen.width / 2 - 100, 0, 200, 100);
            GUI.Box(boxRect, "Time Remaining", boxStyle);

            Rect labelRect = new Rect(Screen.width / 2 - 20, 60, 50, 50);
            GUI.Label(labelRect, ((int)gameTime).ToString(), labelStyle);
        }

        else
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = 65; //Game Over!

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 45; //total time & total distance

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 55; // retsart

            Rect boxRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 500, 450, 150);
            GUI.Box(boxRect, "Game Over!", boxStyle);

            Rect timeLabelRect = new Rect(Screen.width / 2 - 140, Screen.height / 2 - 335, 400, 50);
            GUI.Label(timeLabelRect, "Total Time: " + (int)totalTimeElapsed, labelStyle);

            Rect distanceLabelRect = new Rect(Screen.width / 2 - 140, Screen.height / 2 - 270, 400, 50);
            GUI.Label(distanceLabelRect, "Total Distance: " + (int)totalDistanceTraveled, labelStyle);
            Time.timeScale = 0;

            Rect restartButtonRect = new Rect(Screen.width / 2 - 115, Screen.height / 2 + 100, 400, 150);
            if (GUI.Button(restartButtonRect, "Restart", buttonStyle))
            {
                RestartGame(); // Restart the game if the button is clicked
            }
        }
    }
}