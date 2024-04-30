using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Button retryButton = null;

    public void Start()
    {
        UIDocument hud = GetComponent<UIDocument>();

        VisualElement root = hud.rootVisualElement;
        retryButton = root.Q<Button>("Retry");

        retryButton.clicked += RestartGame;

        Debug.Log("script run");
    }

    public void RestartGame()
    {
        Debug.Log("work??");
        retryButton.clicked -= RestartGame;
        SceneManager.LoadScene("Hunted");
    }
}
