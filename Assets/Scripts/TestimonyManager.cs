using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class TestimonyManager : MonoBehaviour
{
    public GameObject testimonyUI;
    public List<GameObject> buttons;
    public int howManyChoices;

    public float rotateSpeed;
    public bool stressed;

    public void StartTestimony(List<string> choices)
    {
        ResetSpinner();

        howManyChoices = choices.Count;

        for (int i = 0; i < howManyChoices; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
            buttons[i].SetActive(true);
        }

        testimonyUI.SetActive(true);
    }

    public void EndTestimony()
    {
        ResetSpinner();

        testimonyUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        testimonyUI.transform.Rotate(0, 0, rotateSpeed, Space.Self);

        if (stressed)
        {
            foreach (GameObject x in buttons)
            {
                x.transform.Rotate(0, 0, rotateSpeed, Space.Self);
            }
        }
        else
        {
            foreach (GameObject x in buttons)
            {
                x.transform.Rotate(0, 0, -rotateSpeed, Space.Self);
            }
        }

    }

    public void ResetSpinner()
    {
        Vector3 zeroVec = new Vector3(0, 0, 0);
        testimonyUI.transform.rotation = quaternion.Euler(zeroVec);

        foreach (GameObject x in buttons)
        {
            x.transform.rotation = quaternion.Euler(zeroVec); ;
        }
    }
}
