using UnityEngine;
using StarterAssets;

public class Text : MonoBehaviour
{
    private ThirdPersonController thirdPersonController;
    public bool isDialogueActive = false;
    private Interact interact;
    public GameObject dialogueUi;
    private Dialogue dialogue;
    public event System.Action DialogueCompleteEvent;
    public string[] lines;
    public string dialogueName;

    void Awake()
    {
        thirdPersonController = gameObject.GetComponent<ThirdPersonController>();
        dialogue = dialogueUi.GetComponent<Dialogue>();
        interact = gameObject.GetComponent<Interact>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        isDialogueActive = true;
        thirdPersonController.enabled = false;
        interact.canInteract = false;

        dialogue.dialogueName = dialogueName;
        dialogue.lines = lines;

        dialogue.OnDialogueComplete -= OnDialogueComplete;
        dialogue.OnDialogueComplete += OnDialogueComplete;

        dialogueUi.SetActive(true);
    }

    private void OnDialogueComplete()
    {
        thirdPersonController.enabled = true;
        interact.canInteract = true;

        dialogue.OnDialogueComplete -= OnDialogueComplete;
        DialogueCompleteEvent?.Invoke();

        dialogueUi.SetActive(false);
        isDialogueActive = false;
    }

    public void DialogueNextLine()
    {
        dialogue.NextLine(); 
    }
}
