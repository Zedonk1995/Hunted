using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameMenuHandler : MonoBehaviour
{
    IShowMenuInput input = null;

    [SerializeField]
    private UIDocument hud;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<IShowMenuInput>();
        input.MenuCallback = ToggleVisiblity;
    }

    void ToggleVisiblity()
    {
        VisualElement root = hud.rootVisualElement;
        VisualElement gameMenu = root.Q<VisualElement>("GameMenu");
        if ( gameMenu == null )
        {
            return;
        }
        Debug.Log(gameMenu.style.visibility);

        if (gameMenu.style.visibility == Visibility.Visible) {
            gameMenu.style.visibility = Visibility.Hidden;
        } else
        {
            gameMenu.style.visibility = Visibility.Visible;
        }
    }
}
