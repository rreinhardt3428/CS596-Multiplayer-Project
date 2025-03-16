using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections; // For Coroutine

public class StartScreenManager : MonoBehaviour
{
    public AudioClip hoverSound; // Sound when hovering over button
    public AudioClip clickSound; // Sound when clicking the button
    public AudioSource audioSource;

    public Button startButton; // Reference to the start button
    public Button quitButton;  // Reference to the quit button

    private void Start()
    {
        // Add EventTrigger for hover on the start button
        AddHoverEffect(startButton);
        // Add EventTrigger for hover on the quit button
        AddHoverEffect(quitButton);

        // Add listener for button clicks
        startButton.onClick.AddListener(OnStartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    // Adds hover effect to the given button
    private void AddHoverEffect(Button button)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // Create a new entry for pointer enter (hover over the button)
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        pointerEnterEntry.callback.AddListener((data) => { PlayHoverSound(); });
        trigger.triggers.Add(pointerEnterEntry);

        // Create a new entry for pointer exit (hover out from the button)
        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
        pointerExitEntry.eventID = EventTriggerType.PointerExit;
        pointerExitEntry.callback.AddListener((data) => { StopHoverSound(); });
        trigger.triggers.Add(pointerExitEntry);
    }

    // Play hover sound
    private void PlayHoverSound()
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // Stop hover sound (optional, but doesn't need to play again)
    private void StopHoverSound()
    {
        // No need to play the hover sound again when the pointer exits
        // You can leave it empty or use Stop() if you want to stop any sound
        // audioSource.Stop(); // Uncomment if you want to stop any sound playing on hover exit
    }

    // Play click sound and start the game with a delay
    private void OnStartButtonClick()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        StartCoroutine(LoadGameAfterSound()); // Start the coroutine to delay the scene load
    }

    // Coroutine to wait for the sound to finish before loading the scene
    private IEnumerator LoadGameAfterSound()
    {
        // Wait for the click sound to finish (in seconds)
        yield return new WaitForSeconds(clickSound.length);

        // After the sound finishes, load the scene
        SceneManager.LoadScene("Game");
    }

    // Play click sound and quit the game
    private void OnQuitButtonClick()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        Application.Quit();
    }
}
