using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.GunSo;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun Button")]

public class GunButtonSo : ScriptableObject
{
    public Sprite gunLogo;
    public GunSo gun;
}
