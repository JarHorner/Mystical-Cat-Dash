using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Powerup", menuName = "ScriptableObjects/Powerup", order = 1)]
public class PowerUpScriptableObject : ScriptableObject
{
    public float length;
    public float currentPowerupTime;
    public bool pickedUp;
}
