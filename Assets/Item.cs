using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName;

    public Text text;
    public bool dialogueAction; //configurar en el editor
    public Transform itemInteractUiPosition;

    public void Interact()
    {
        //dialogue
        if (dialogueAction)
        {
            if (!text.isDialogueActive)
            {
                text.dialogueName = itemName;
                text.lines = new string[]{"texto de prueba", "texto de prueba 2"};
                text.StartDialogue();
            }
        }
    }
}
