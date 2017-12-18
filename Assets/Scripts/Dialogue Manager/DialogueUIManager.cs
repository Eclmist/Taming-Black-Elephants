using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class DialogueUIManager : MonoBehaviour 
{
    public static DialogueUIManager Instance;

    [Header("Gameobject Elements")]
    [SerializeField] private Text characterNameObj;
    [SerializeField] private Text messageObj;
    [SerializeField] private Image portraitLeft;
    [SerializeField] private Image portraitRight;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject optionsButtonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private CanvasGroup buttonCanvasGroup;

    private bool messageCompleted = true;
    private bool skipTyping = false;

    // Options
    private int selectedOptionIndex = -1;

    protected void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("DialogUIManager already exist.");
        }

        Instance = this;
    }

    protected void Start()
    {
        buttonCanvasGroup.alpha = 0;
        canvasGroup.alpha = 0;
    }

    protected IEnumerator TypewriterCoroutine(string text, float speed)
    {
        List<char> textRemaining = new List<char>(text.ToCharArray());

        while (textRemaining.Count > 0)
        {
            if (skipTyping)
            {
                messageObj.text = text;
                break;
            }

            char next = textRemaining[0];
            textRemaining.RemoveAt(0);

            messageObj.text += next;

            yield return new WaitForSeconds(1-speed);
        }

        messageCompleted = true;
    }

    public void DisplayNewMessage(string characterName, string message, float speed,
        Sprite portrait = null, bool portraitOnLeft = true)
    {
        StopAllCoroutines();
        characterNameObj.text = characterName;

        portraitLeft.enabled = false;
        portraitRight.enabled = false;

        messageObj.text = "";
        messageCompleted = false;
        skipTyping = false;

        if (portrait != null)
        {
            if (portraitOnLeft)
            {
                portraitLeft.enabled = true;
                portraitLeft.sprite = portrait;
            }
            else
            {
                portraitRight.enabled = true;
                portraitLeft.sprite = portrait;
            }
        }

        StartCoroutine(TypewriterCoroutine(message, speed));
    }

    public void ShowOptions(string[] options)
    {
        selectedOptionIndex = -1;

        for (int i = 0; i < options.Length; i++)
        {
            Button b = Instantiate(optionsButtonPrefab, buttonContainer).GetComponent<Button>();
            b.GetComponentInChildren<Text>().text = options[i];

            int localIndex = i;
            b.onClick.AddListener(() => SetSelectedOptionIndex(localIndex));
        }

        buttonCanvasGroup.alpha = 1;
    }

    public int GetSelectedOptionIndex()
    {
        return selectedOptionIndex;
    }
    public void SetSelectedOptionIndex(int index)
    {
        selectedOptionIndex = index;
        buttonCanvasGroup.alpha = 0;

        foreach (Transform child in buttonContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ToggleDialogueUI(bool active)
    {
        canvasGroup.alpha = active ? 1 : 0;
    }

    public bool IsMessageCompletelyShown()
    {
        return messageCompleted;
    }

    public void SkipTypewriter()
    {
        skipTyping = true;
    }
}
