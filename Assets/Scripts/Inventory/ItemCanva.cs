using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemCanva : MonoBehaviour
{
    public Types.Inorganics ino;
    [SerializeField] TMP_Text _title; 
    [SerializeField] TMP_Text _amount; 
    private int amount;

    public void SetAmount(int amount){
        this.amount += amount;
        _amount.text = "" + this.amount;
    }
    public void SetName(string name){
        _title.text = name;
    }
}
