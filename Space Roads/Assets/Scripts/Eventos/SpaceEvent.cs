using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Space Event", menuName = "Scriptable Objects/Space Event")]
public class SpaceEvent : ScriptableObject
{
    public Sprite EventImage;
    public string Method;
    public string Arguments;
}
