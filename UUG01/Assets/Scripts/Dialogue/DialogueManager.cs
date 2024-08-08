using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.SearchService;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Quests")]
    [SerializeField] private GameObject questOne;
    [SerializeField] private GameObject questTwo;
    [SerializeField] private GameObject questThree;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject toolbarPanel;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    
    private DialogueVariables dialogueVariables;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        questOne.SetActive(false);
        questTwo.SetActive(false);

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        if (canContinueToNextLine && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        toolbarPanel.SetActive(false);
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

        currentStory.BindExternalFunction("ShowFirstQuest", () =>
        {
            Quest1Show();
        });

        currentStory.BindExternalFunction("ShowSecondQuest", () =>
        {
            Quest2Show();
        });

        currentStory.BindExternalFunction("ShowThirdQuest", () =>
        {
            Quest3Show();
        });

        currentStory.BindExternalFunction("DoNotShowFirstQuest", () =>
        {
            Quest1Stop();
        });

        currentStory.BindExternalFunction("DoNotShowSecondQuest", () =>
        {
            Quest2Stop();
        });

        currentStory.BindExternalFunction("DoNotShowThirdQuest", () =>
        {
            Quest3Stop();
        });


        currentStory.BindExternalFunction("PlayDragonCut", () =>
        {
            DragonCut();
        });

        // reset portrait and speaker ... this is good for multiple npcs
        displayNameText.text = "????";
        portraitAnimator.Play("default");

        ContinueStory();
    }

    private void Quest1Show()
    {
        questOne.SetActive(true);
    }

    private void Quest1Stop()
    {
        questOne.SetActive(false);
    }

    private void Quest2Show()
    {
        questTwo.SetActive(true);
    }

    private void Quest2Stop()
    {
        questTwo.SetActive(false);
    }

    private void Quest3Show()
    {
        questThree.SetActive(true);
    }
    private void Quest3Stop()
    {
        questThree.SetActive(false);
    }
    private void DragonCut()
    {
        AudioManager.Instance.PlayDragonLevel1Ending(); // To call the dragon scene!!!
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);
        currentStory.UnbindExternalFunction("ShowFirstQuest");
        currentStory.UnbindExternalFunction("ShowSecondQuest");
        currentStory.UnbindExternalFunction("ShowThirdQuest");

        currentStory.UnbindExternalFunction("DoNotShowFirstQuest");
        currentStory.UnbindExternalFunction("DoNotShowSecondQuest");
        currentStory.UnbindExternalFunction("DoNotShowThirdQuest");

        currentStory.UnbindExternalFunction("PlayDragonCut");

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        toolbarPanel.SetActive(true);
        dialogueText.text = "";

    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            // Handle Tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // empty dialogue text
        dialogueText.text = "";

        // Hide UI Items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time 
        foreach (char letter in line.ToCharArray())
        {
            // checking if player has pressed the submit button (spacebar) to skip to the end of text typing.
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.text = line;
                break;
            }

            // check for rich text tag, if found then add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                dialogueText.text += letter;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);

        // display choices, if any, for this dialogue line
        DisplayChoices();

        canContinueToNextLine = true;

    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }
    private void HandleTags(List<string> currentTags)
    {
        // Loop thorugh each tag and hanlde it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parse: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;

                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;

                default:
                    Debug.LogWarning("Tag came in but is not currenly being handled " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

       // StartCoroutine(SelectFirstChoice());

    }
    /*
    private IEnumerator SelectFirstChoice() // Not needed because it will require the usage of the control input, which messes up the established controls for menu.
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }*/

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
        }

        /*
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
            ContinueStory();
        }*/
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit()
    {
        dialogueVariables.SaveVariables();
    }

}
// Source: https://www.youtube.com/watch?v=vY0Sk93YUhA&list=PL3viUl9h9k78KsDxXoAzgQ1yRjhm7p8kl&index=2