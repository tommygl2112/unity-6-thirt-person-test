using UnityEngine;
using UnityEngine.InputSystem;

public class UiControllelButtons : MonoBehaviour
{
    // INTERACT
    public static string interactKeyboardMouse = "[E]";
    public static string interactXbox = "[A]";
    public static string interactButton = "";

    public bool usandoGamepad;

    public static string CheckForInteractUiButton()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            interactButton = interactXbox;
        }

        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Mouse.current != null &&
            (Mouse.current.delta.ReadValue() != Vector2.zero ||
             Mouse.current.leftButton.wasPressedThisFrame ||
             Mouse.current.rightButton.wasPressedThisFrame)))
        {
            interactButton = interactKeyboardMouse;
        }

        return interactButton;
    }
}
