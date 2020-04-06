using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects / My scriptable obect")] 
public class ScriptableTest : ScriptableObject
{
    public string ObjectName;
    [SerializeField] private int ObjectTestInt;
    [SerializeField] private Sprite ObjectSprite;

    //
}