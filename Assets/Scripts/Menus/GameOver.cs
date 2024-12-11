using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private UIHandler uiHandler;

    private Button retryButton = null;

    public void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        UIDocument hud = GetComponent<UIDocument>();

        VisualElement root = hud.rootVisualElement;
        retryButton = root.Q<Button>("Retry");

        retryButton.clicked += RestartGame;

        // initialize score variables]
        GameObject GameOverMenu = GameObject.Find("GameOverMenu");
        GameOverMenu.TryGetComponent(out uiHandler);

        uiHandler.UpdateKillCount(Global.KillCount);
        uiHandler.UpdateGameTimer(Global.GameTime);
    }

    public void RestartGame()
    {
        retryButton.clicked -= RestartGame;
        SceneManager.LoadScene("Hunted");
    }
}
