using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class XP_controller : MonoBehaviour
{
    public static XP_controller instance {get; set;}
    
    [SerializeField] TMP_Text xpText; 

    public int currentXP{get; set;}

    public void Awake(){
        instance = this;
    }
    public void UpdateXP(int amount){
        currentXP += amount;
        xpText.text = "" + currentXP;
    }
}
