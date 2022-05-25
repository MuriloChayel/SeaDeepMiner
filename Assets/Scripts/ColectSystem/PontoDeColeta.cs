using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoDeColeta : MonoBehaviour
{
    public PontoDeColetaProperties pontoDeColetaProps;

    public int _baseXP;

    //public Types.Inorganics inorganics;
    //public Types.Organics organics;
    public GameObject _itemDataCanvas;

    [SerializeField] GameObject _interactCanva;
    
    //Controllers
    private bool _openCanva;
    //--
    //TODO: REFATORAR PARA MOBILE
    private void OnMouseDown(){
        _openCanva = !_openCanva;

        _interactCanva.SetActive(_openCanva);
    }
    public void Interact(){
        Base.instance.AddNovoPontoDeColeta(this);
        _openCanva = !_openCanva;
        _interactCanva.SetActive(_openCanva);
    }
}
