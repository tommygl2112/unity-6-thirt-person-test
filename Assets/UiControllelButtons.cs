using UnityEngine;
using UnityEngine.InputSystem;

public class UiControllelButtons : MonoBehaviour
{
    // INTERACT
    public static string interact_Xbox = "[A]";
    public static string interact_KeyboardMouse = "[E]";

    // INSPECT
    public static string inspect_Pickup_Xbox = "[B]";
    public static string inspect_Pickup_KeyboardMouse = "[Right Click]";

    // Input Actions
    public static string interact = "";
    public static string exitItemInspection = "";

    public bool usandoGamepad;

    public static string GetInteractUiButton()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            interact = interact_Xbox;
        }

        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Mouse.current != null &&
            (Mouse.current.delta.ReadValue() != Vector2.zero ||
             Mouse.current.leftButton.wasPressedThisFrame ||
             Mouse.current.rightButton.wasPressedThisFrame)))
        {
            interact = interact_KeyboardMouse;
        }

        return interact;
    }

    public static string GetExitItemInspectiontUiButton()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            exitItemInspection = inspect_Pickup_Xbox;
        }

        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Mouse.current != null &&
            (Mouse.current.delta.ReadValue() != Vector2.zero ||
             Mouse.current.leftButton.wasPressedThisFrame ||
             Mouse.current.rightButton.wasPressedThisFrame)))
        {
            exitItemInspection = inspect_Pickup_KeyboardMouse;
        }

        return exitItemInspection;
    }
}
