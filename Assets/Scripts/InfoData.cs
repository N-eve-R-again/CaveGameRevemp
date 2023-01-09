using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "InfoData", order = 1)]
public class InfoData : ScriptableObject
{
    
    public Sprite artwork;
    public string[] texts;
    public int codexIndex;
    //public AnimationClip loadAnimation;

}
