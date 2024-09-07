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
    public string evidenceAcquire;
    public State nextState;


    public string GetStateDialogue()
    {
        return defaultText;
    }

}
