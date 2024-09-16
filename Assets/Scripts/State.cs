using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    [TextArea(14,10)][SerializeField] string defaultText;
    public bool internalThoughts;

    public Sprite background;
    public Sprite speakerImage;
    public string speakerName;
    public AudioClip audioClip;
    public string locationName;
    public List<Evidence> evidenceAcquire;
    public State nextState;

    public bool isTestimony;
    public bool isStressed;
    public bool isMultipleChoice;
    public int correctChoice;
    public Evidence correctEvidence;
    public List<string> choices;
    public State errorState;

    public string GetStateDialogue()
    {
        return defaultText;
    }

}
