using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Drop : MonoBehaviour, Dropable
{
    public int ResultOfRollingTheDice { get; set; }
    public bool DueToRemove { get; set; }
    string Dropable.Name { get => GetName();}
    int Dropable.DropChance { get => GetDropChance(); }

    public abstract string GetName();
    public abstract int GetDropChance();
    
    public void ResetTheresultOfRollingThDice()
    {
        ResultOfRollingTheDice = 0;
        DueToRemove = false;
    }
}
