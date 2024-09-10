using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Evidence")]
public class Evidence : ScriptableObject
{
    public Sprite evidenceImage;
    public string evidenceName;
    [TextArea(14,10)] public string evidenceDescription;

}
