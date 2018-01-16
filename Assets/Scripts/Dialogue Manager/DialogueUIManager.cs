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

    [SerializeField] private Image messageBox;
    [SerializeField] private Image characterNameBox;

    [SerializeField] private Sprite normalChatSprite;
    [SerializeField] private Sprite thoughtBubbleSprite;

    private bool messageCompleted = true;
    private bool skipTyping = false;
    private bool freezeTime = false;

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

            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1 - speed));
        }

        messageCompleted = true;
    }

    public void DisplayNewMessage(string characterName, string message, float speed,
        bool isThinking = false, Sprite portrait = null, bool portraitOnLeft = true)
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
                portraitRight.sprite = portrait;
            }
        }

        messageBox.sprite = isThinking ? thoughtBubbleSprite : normalChatSprite;
        characterNameBox.gameObject.SetActive(!isThinking);

        StartCoroutine(TypewriterCoroutine(message, speed));
    }

    // Shows dialogue options, used in the dialogue manager pipeline. For Generic user confirmation options,
    // use ShowGenericOptions() instead.
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

    public void ShowGenericOptions(params GenericAction[] callbacks)
    {
        ToggleDialogueUI(true);
        canvasGroup.alpha = 0;  // We still want to freetime and generally use the toggleDialogueUI function, but
                                // then we want to hide the Message box and etc
        buttonCanvasGroup.alpha = 1;

        for (int i = 0; i < callbacks.Length; i++)
        {
            Button b = Instantiate(optionsButtonPrefab, buttonContainer).GetComponent<Button>();
            b.GetComponentInChildren<Text>().text = callbacks[i].optionText;
            b.onClick.AddListener(ShowGenericOptionsCleanup);

            if (callbacks[i].callback != null)
                b.onClick.AddListener(callbacks[i].callback);
        }
    }

    private void ShowGenericOptionsCleanup()
    {
        ToggleDialogueUI(false);
        buttonCanvasGroup.alpha = 0;
        RemoveAllButtons();
    }

    private void RemoveAllButtons()
    {
        foreach (Transform child in buttonContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public int GetSelectedOptionIndex()
    {
        return selectedOptionIndex;
    }
    public void SetSelectedOptionIndex(int index)
    {
        selectedOptionIndex = index;
        buttonCanvasGroup.alpha = 0;
        RemoveAllButtons();
    }

    public void ToggleDialogueUI(bool active)
    {
        canvasGroup.alpha = active ? 1 : 0;

        if (freezeTime)
        {
            Time.timeScale = active ? 0 : 1;
        }
    }

    public bool IsMessageCompletelyShown()
    {
        return messageCompleted;
    }

    public void SkipTypewriter()
    {
        skipTyping = true;
    }

    public void SetFreezeTime(bool active)
    {
        freezeTime = active;
    }
}

public struct GenericAction
{
    public string optionText;
    public UnityAction callback;

    public GenericAction(string text, UnityAction callb)
    {
        optionText = text;
        callback = callb;
    }
}
