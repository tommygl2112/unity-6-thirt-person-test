using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using StarterAssets;

public class Dialogue : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public int index;
    public delegate void DialogueComplete();
    public event DialogueComplete OnDialogueComplete;

    public GameObject text;
    public GameObject doth;

    private StarterAssetsInputs _input;

    void Awake()
    {
        _input = FindFirstObjectByType<StarterAssetsInputs>();
        textComponent = text.GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        doth.SetActive(false);
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Start()
    {

    }

    void Update()
    {
        // if (_input.interact)
        // {
        //     if (textComponent.text == lines[index])
        //     {
        //         NextLine();
        //     }
        //     else
        //     {
        //         StopAllCoroutines();
        //         textComponent.text = lines[index];
        //         doth.SetActive(true);
        //     }
        // }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;  // Limpiar el texto antes de empezar a escribirlo
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        doth.SetActive(true);
    }

    void NextLine()
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
}
