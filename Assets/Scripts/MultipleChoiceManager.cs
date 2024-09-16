using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class MultipleChoiceManager : MonoBehaviour
{
    public GameObject multipleChoiceUI;
    public List<GameObject> buttons;

    public int howManyChoices;

    public void StartMultipleChoice(List<string> choices)
    {
        howManyChoices = choices.Count;

        for (int i = 0; i < howManyChoices; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
            buttons[i].SetActive(true);
        }

        multipleChoiceUI.SetActive(true);
    }

    public void EndMultipleChoice()
    {
        multipleChoiceUI.SetActive(false);
    }
}
