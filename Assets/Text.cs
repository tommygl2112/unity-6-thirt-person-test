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
        thirdPersonController.enabled = false;
        interact.enabled = false;

        dialogue.lines = lines;

        dialogueUi.SetActive(true);

        dialogue.OnDialogueComplete -= OnDialogueComplete;
        dialogue.OnDialogueComplete += OnDialogueComplete;

        isDialogueActive = true;
    }

    private void OnDialogueComplete()
    {
        thirdPersonController.enabled = true;
        interact.enabled = true;

        dialogue.OnDialogueComplete -= OnDialogueComplete;
        DialogueCompleteEvent?.Invoke();

        isDialogueActive = false;

        dialogueUi.SetActive(false);
    }
}
