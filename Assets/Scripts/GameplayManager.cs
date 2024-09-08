using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private float writeSpeed = 0.001f;
    [SerializeField] TextWriter textWriter;

    //Text box that displays choices and story
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] GameObject dialogueTextObject;

    //Text box that displays location in top left
    [SerializeField] TMP_Text locationText;
    //GameObject parent that holds the location text, will appear after first scene.
    [SerializeField] GameObject locationTextObject;
    [SerializeField] Image speakerImage;
    [SerializeField] GameObject speakerImageObject;
    [SerializeField] TMP_Text speakerTextName;
    [SerializeField] GameObject speakerTextObject; 

    //State where the game begins, can be changed for debugging
    [SerializeField] State startingState;
    //Holds a list of all possible next states and is also what is used for input directions.
    [SerializeField] State nextState;

    [SerializeField] List<string> evidence;

    //Music
    public AudioSource audioSource;

    //Background displayed behind Text
    public Image background;
    //Temp Background used when transitioning between scenes
    public Image nextBackground;
    //Transition time for transition speed, lower the float value the faster the transition
    public float transitionTime;

    //Temp color used for resetting alpha of backgrounds
    private Color tempColor;
    //Bool that keeps the player from making a choice before the transitions are complete.
    private bool transitionComplete = true;

    //State is not a state machine and is the name of the current place we are in, in the story.
    [SerializeField] State state;

    // Start is called before the first frame update
    void Start()
    {
        //Starting state is assigned as the current state
        state = startingState;
        nextState = state.nextState;
        dialogueTextObject.SetActive(true);
        //Takes the story from the current state + whatever role and put them in the text
        textWriter.SetUpWriter(dialogueText, ConstructText(), writeSpeed);
        //Background startup
        background.sprite = state.background;
        locationText.text = state.locationName;

        //Slowly transition
        StartCoroutine(TransitionStartArtwork(background));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        ManageState();
    }

    private void ManageState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textWriter.getFinishedWriting() == false)
            {
                textWriter.QuickFinishWrite();
                return;
            }

            if (transitionComplete == false) { return; }

            GoToNextState();

            textWriter.SetUpWriter(dialogueText, ConstructText(), writeSpeed);

            if (state.background != null)
            {
                nextBackground.sprite = state.background;
                StartCoroutine(TransitionSceneArtwork(background, nextBackground));
            }

            if(state.GetStateDialogue() == "")
            {
                dialogueTextObject.SetActive(false);
            }
            else 
            {
                dialogueTextObject.SetActive(true);
            }
            
            if (state.locationName != "")
            {
                locationText.text = state.locationName;
                locationTextObject.SetActive(true);
            }
            else 
            {
                locationTextObject.SetActive(false);
            }

            if (state.speakerName == "self")
            {
                speakerTextObject.SetActive(false);
            }
            else 
            {
                speakerTextName.text = state.speakerName;
                speakerTextObject.SetActive(true);
            }

            if (state.speakerImage != null)
            {
                speakerImage.sprite = state.speakerImage;
                speakerImageObject.SetActive(true);
            }
            else 
            {
                speakerImageObject.SetActive(false);
            }

            if (state.audioClip != null)
            {
                audioSource.clip = state.audioClip;
                audioSource.Play();
            }
        }

    }

    private void GoToNextState()
    {
        //Set the next state to the current state.
        state = nextState;

        //Grab the future state.
        nextState = state.nextState;
    }

    private string ConstructText()
    {
        string text = "";
        text += state.GetStateDialogue() + "\n";

        return text;
    }

    private YieldInstruction fadeInstruction = new YieldInstruction();
    IEnumerator TransitionSceneArtwork(Image currentBackground, Image nextBackground)
    {
        transitionComplete = false;
        float elapsedTime = 0.0f;
        Color c = currentBackground.color;
        while (elapsedTime < transitionTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / transitionTime);
            currentBackground.color = c;
        }

        elapsedTime = 0.0f;
        c = nextBackground.color;
        while (elapsedTime < transitionTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / transitionTime);
            nextBackground.color = c;
        }

        background.sprite = state.background;
        QuickAlphaChange(background, 1f);
        QuickAlphaChange(nextBackground, 0f);
        transitionComplete = true;
    }

    private YieldInstruction fadeStartInstruction = new YieldInstruction();
    IEnumerator TransitionStartArtwork(Image background)
    {
        transitionComplete = false;

        float elapsedTime = 0.0f;
        Color c = background.color;
        while (elapsedTime < transitionTime)
        {
            yield return fadeStartInstruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / transitionTime);
            background.color = c;
        }

        background.sprite = state.background;
        transitionComplete = true;
    }

    private void QuickAlphaChange(Image image, float alpha)
    {
        tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }
}
