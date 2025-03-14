using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public float roundDuration = 30f;  // 30 second timer
    public TMP_Text timerText;             // UI Text for the timer
    public GameObject leftWinIndicator;  // Left side win icon
    public GameObject rightWinIndicator; // Right side win icon

    public Transform player1StartPos;   // Player 1 spawn point
    public Transform player2StartPos;   // Player 2 spawn point

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    private GameObject player1Instance;
    private GameObject player2Instance;

    private float timer;
    private int player1Wins = 0;
    private int player2Wins = 0;
    private bool roundActive = false;

    private void Start()
    {
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
            if (player1Instance == null)
            {
                PlayerWins(2); // Player 2 wins the round
            }
            else if (player2Instance == null)
            {
                PlayerWins(1); // Player 1 wins the round
            }
        }
    }

    public void PlayerWins(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Wins++;
            leftWinIndicator.transform.GetChild(player1Wins - 1).gameObject.SetActive(true); // Activate win indicator for Player 1
        }
        else if (playerNumber == 2)
        {
            player2Wins++;
            rightWinIndicator.transform.GetChild(player2Wins - 1).gameObject.SetActive(true); // Activate win indicator for Player 2
        }

        // Check if either player has won 2 rounds
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
            StartCoroutine(RestartRound()); // Start next round after a short delay
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
    }

    void EndGame()
    {
        roundActive = false;
        Debug.Log("Game Over!");
    }
}
