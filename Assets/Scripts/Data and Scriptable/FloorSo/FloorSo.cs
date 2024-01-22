using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Floor")]
public class FloorSo : ScriptableObject
{
    public int skinNo;
    public Material material;
    public float height = 1.6f;

}
