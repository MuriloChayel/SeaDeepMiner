using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetorProperties : MonoBehaviour
{
    public void AddToInventory(Types.Inorganics ino, Types.Organics org, int amount){
        Inventory.instance.AddItem(ino, org, amount);
    }
}
