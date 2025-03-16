using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public float roundDuration = 60f;  // 30 second timer
    public TMP_Text timerText;             // UI Text for the timer
    public GameObject leftWinIndicator;  // Left side win icon
    public GameObject rightWinIndicator; // Right side win icon

    public Transform player1StartPos;   // Player 1 spawn point
    public Transform player2StartPos;   // Player 2 spawn point

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    private GameObject player1Instance;
    private GameObject player2Instance;

    public TMP_Text roundMessageText;
    public TMP_Text winnerMessageText; // Reference to the TextMeshPro component

    // Expose these properties to the Unity Inspector
    public float fontSize = 48f; // Font size
    public Color textColor = Color.green; // Text color
    public Vector2 textPosition = new Vector2(0, 100); // Text position
    public Vector2 textSize = new Vector2(400, 100); // Width and height for the text box

    // New Variables for sound
    public AudioClip winSound; // Reference to the win sound effect
    public AudioClip roundSound;
    private AudioSource audioSource; // Reference to AudioSource component

    private float timer;
    private int player1Wins = 0;
    private int player2Wins = 0;
    private bool roundActive = false;

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        StartRound(); // Start the first round
    }

    private void Update()
    {
        if (roundActive)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                EndRound(); // Time's up
            }

            // Check if either player is destroyed
            if (player1Instance == null && !audioSource.isPlaying) // Player 1 destroyed
            {
                audioSource.PlayOneShot(roundSound);  // Play round sound
                PlayerWins(2); // Player 2 wins the round
            }
            else if (player2Instance == null && !audioSource.isPlaying) // Player 2 destroyed
            {
                audioSource.PlayOneShot(roundSound);  // Play round sound
                PlayerWins(1); // Player 1 wins the round
            }
        }
    }


    public void PlayerWins(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Wins++;
            leftWinIndicator.transform.GetChild(player1Wins - 1).gameObject.GetComponent<Image>().enabled = true;
        }
        else if (playerNumber == 2)
        {
            player2Wins++;
            rightWinIndicator.transform.GetChild(player2Wins - 1).gameObject.GetComponent<Image>().enabled = true;
        }

        if (player1Wins == 2)
        {
            Debug.Log("Player 1 Wins the Game!");
            EndGame();
        }
        else if (player2Wins == 2)
        {
            Debug.Log("Player 2 Wins the Game!");
            EndGame();
        }
        else
        {
            StartRound(); // Start the next round
        }
    }

    void StartRound()
    {
        timer = roundDuration;
        roundActive = true;

        // Destroy any existing players
        if (player1Instance != null)
            Destroy(player1Instance);
        if (player2Instance != null)
            Destroy(player2Instance);

        // Spawn new players
        player1Instance = Instantiate(player1Prefab, player1StartPos.position, Quaternion.identity);
        player2Instance = Instantiate(player2Prefab, player2StartPos.position, Quaternion.identity);
    }

    IEnumerator RestartRound()
    {
        roundActive = false;
        yield return new WaitForSeconds(2f); // Short delay before next round
        StartRound(); // Restart round
    }

    void EndRound()
    {
        roundActive = false;

        // Check who wins based on health if both players are still alive
        if (player1Instance != null && player2Instance != null)
        {
            // Assuming you have access to player health here
            PlayerHealthAndShield player1Health = player1Instance.GetComponent<PlayerHealthAndShield>();
            PlayerHealthAndShield player2Health = player2Instance.GetComponent<PlayerHealthAndShield>();

            if (player1Health != null && player2Health != null)
            {
                if (player1Health.currentHealth > player2Health.currentHealth)
                {
                    PlayerWins(1); // Player 1 wins by health
                }
                else if (player2Health.currentHealth > player1Health.currentHealth)
                {
                    PlayerWins(2); // Player 2 wins by health
                }
            }
        }
        else
        {
            // If only one player is alive, that player wins
            if (player1Instance != null)
            {
                PlayerWins(1);
            }
            else if (player2Instance != null)
            {
                PlayerWins(2);
            }
        }

        // Hide message after 2 seconds
        StartCoroutine(HideMessage());
    }

    void EndGame()
    {
        // Play the win sound when either player wins
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound); // Play sound effect once
        }
        roundActive = false;
        Debug.Log("Game Over!");

        // Display the winner's message
        string winnerMessage = player1Wins == 2 ? "Player 1 Winner!" : "Player 2 Winner!";
        StartCoroutine(ShowWinnerMessage(winnerMessage));
    }

    IEnumerator ShowWinnerMessage(string message)
    {
        // Activate the winner message text
        winnerMessageText.gameObject.SetActive(true);
        winnerMessageText.text = message;

        // Customize appearance using the exposed properties
        winnerMessageText.fontSize = fontSize; // Set the font size
        winnerMessageText.color = textColor; // Set the text color
        winnerMessageText.alignment = TextAlignmentOptions.Center; // Center-align the text
        winnerMessageText.rectTransform.anchoredPosition = textPosition; // Set the position

        // Adjust the size of the text box using the sizeDelta
        winnerMessageText.rectTransform.sizeDelta = textSize; // Set width and height

        // Show the winner message for 2 seconds
        yield return new WaitForSeconds(3f);

        // Deactivate the winner message
        winnerMessageText.gameObject.SetActive(false);

        // Immediately load the title screen
        SceneManager.LoadScene("TitleScreen");
    }

    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2f);
        roundMessageText.gameObject.SetActive(false);
    }

}
