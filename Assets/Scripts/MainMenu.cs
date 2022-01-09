using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public string buttonClickSound = "button_click";

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audiomanager found..");
        }
    }

    public void NewGame()
    {
        audioManager.PlaySound(buttonClickSound);
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        audioManager.PlaySound(buttonClickSound);
        Application.Quit();
    }
}