using System;
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
        VisualElement root = hud.rootVisualElement;
        Label scoreValue = root.Q<Label>("TimeValue");

        // @todo simplify
        int intTime = Mathf.FloorToInt(time);
        int intMinutes = intTime / 60;
        string minutes = intMinutes.ToString();

        float floatSeconds = time - (60 * intMinutes);
        string seconds = floatSeconds.ToString("0.00");
        if (floatSeconds < 10)
        {
            seconds = $"0{seconds}";
        }

        scoreValue.text = $"{minutes}:{seconds}";
    }

    public void UpdateKillCount(int killCount)
    {
        VisualElement root = hud.rootVisualElement;
        Label scoreValue = root.Q<Label>("ScoreValue");
        scoreValue.text = killCount.ToString();
    }
}
