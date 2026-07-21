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
    public static string inspect_Read_Xbox = "[X]";
    public static string inspect_Read_KeyboardMouse = "[R]";
    public static string inspect_Rotate_Xbox = "[Left Joystick]";
    public static string inspect_Rotate_KeyboardMouse = "[Mouse]";

    // Input Actions
    public static string interact = "";
    public static string exitItemInspection = "";
    public static string readItemText = "";
    public static string look = "";

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

    public static string GetReadItemTextUiButton()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            readItemText = inspect_Read_Xbox;
        }

        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Mouse.current != null &&
            (Mouse.current.delta.ReadValue() != Vector2.zero ||
             Mouse.current.leftButton.wasPressedThisFrame ||
             Mouse.current.rightButton.wasPressedThisFrame)))
        {
            readItemText = inspect_Read_KeyboardMouse;
        }

        return readItemText;
    }

    public static string GetLookUiButton()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            look = inspect_Rotate_Xbox;
        }

        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Mouse.current != null &&
            (Mouse.current.delta.ReadValue() != Vector2.zero ||
             Mouse.current.leftButton.wasPressedThisFrame ||
             Mouse.current.rightButton.wasPressedThisFrame)))
        {
            look = inspect_Rotate_KeyboardMouse;
        }

        return look;
    }
}
