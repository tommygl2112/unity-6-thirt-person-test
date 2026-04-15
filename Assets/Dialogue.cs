using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using StarterAssets;

public class Dialogue : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameComponent;
    public string[] lines;
    public string dialogueName;
    public float textSpeed;

    public int index;
    public delegate void DialogueComplete();
    public event DialogueComplete OnDialogueComplete;

    public GameObject nameUi;
    public GameObject textUi;
    public GameObject doth;

    private StarterAssetsInputs _input;

    void Awake()
    {
        _input = FindFirstObjectByType<StarterAssetsInputs>();
        nameComponent = nameUi.GetComponent<TextMeshProUGUI>();
        textComponent = textUi.GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        doth.SetActive(false);

        nameComponent.text = string.Empty;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        nameComponent.text = dialogueName;
        textComponent.text = string.Empty;  // Limpiar el texto antes de empezar a escribirlo
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        doth.SetActive(true);
    }

    public void NextLine()
    {
        if (textComponent.text == lines[index])
        {
            doth.SetActive(false);
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                gameObject.SetActive(false);
                OnDialogueComplete?.Invoke(); // Notificar que el diálogo ha terminado
            }            
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
            doth.SetActive(true);
        } 
    }
}
