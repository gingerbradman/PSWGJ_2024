using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;


    private TMP_Text textTMP;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    private bool finishedWriting = false;
    private bool readyToWrite = false;
    public bool getFinishedWriting(){ return finishedWriting;}

    public void SetUpWriter(TMP_Text text, string textToWrite, float timePerCharacter)
    {
        this.textTMP = text;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
        readyToWrite = true;
        finishedWriting = false;
    }

    public void QuickFinishWrite()
    {
        finishedWriting = true;
        textTMP.text = textToWrite;
    }

    //return true on complete
    public void Update()
    {
        if(readyToWrite != true){ return; }

        if (finishedWriting != true)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                
                text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";

                textTMP.text = text;
                if (characterIndex >= textToWrite.Length)
                {
                    finishedWriting = true;
                    return;
                }
            }
        }
    }
}
