using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Dropable
{
    public string Name { get;}
    public int DropChance { get; }
    public int ResultOfRollingTheDice { get; set; }
    public bool DueToRemove { get; set; }
    public void ResetTheresultOfRollingThDice();
    
}
