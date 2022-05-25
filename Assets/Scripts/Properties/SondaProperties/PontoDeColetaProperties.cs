using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Properties/PontoDeColetaProps")]
public class PontoDeColetaProperties : ScriptableObject
{   
    public Types.Inorganics _inorganic;
    public Types.Organics _organic;
    public int _amount;

    public float _tempoDeColeta;
}
