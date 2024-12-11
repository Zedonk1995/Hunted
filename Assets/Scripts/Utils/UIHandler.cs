using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private UIDocument hud;

    public void UpdateHealth(float health, float maxHealth)
    {
        VisualElement root = hud.rootVisualElement;
        VisualElement currentHealthElement = root.Q<VisualElement>("CurrentHealth");
        currentHealthElement.style.width = Length.Percent(Mathf.Clamp( 100 * health / maxHealth, 0, 100));
    }

    public void UpdateGameTimer(float time)
    {

    }

    public void UpdateKillCount(int killCount)
    {
        VisualElement root = hud.rootVisualElement;
        Label scoreValue = root.Q<Label>("ScoreValue");
        scoreValue.text = killCount.ToString();
    }
}
