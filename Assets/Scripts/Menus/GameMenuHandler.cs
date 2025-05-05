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

        VisualElement root = hud.rootVisualElement;
        SliderInt slider = root.Q<SliderInt>("MouseSensitivitySlider");

        slider.value = Mathf.RoundToInt(Global.MouseSensitivity / 10);

        slider.RegisterValueChangedCallback(e =>
        {
            Global.MouseSensitivity = 10*e.newValue;
        });
    }

    private void Update()
    {
        
    }

    void ToggleVisiblity()
    {
        VisualElement root = hud.rootVisualElement;
        VisualElement gameMenu = root.Q<VisualElement>("GameMenu");
        if ( gameMenu == null )
        {
            return;
        }

        if (gameMenu.style.visibility == Visibility.Visible) {
            gameMenu.style.visibility = Visibility.Hidden;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        else
        {
            gameMenu.style.visibility = Visibility.Visible;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}
