using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName;

    public Text text;
    public bool dialogueAction; //configurar en el editor

    public void Interact()
    {
        //dialogue
        if (dialogueAction)
        {
            if (!text.isDialogueActive)
            {
                text.lines = new string[]{itemName};
                text.StartDialogue();
            }
        }
    }
}
