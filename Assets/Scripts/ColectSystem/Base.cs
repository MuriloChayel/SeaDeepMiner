using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public static Base instance {get; set;}

    [Header("Main Properties")]
    public BaseProperties _properties;
    
    [Header("Debug")]
    [SerializeField] int _sondasAtivasCount = 0;
    [Header("Basic properties")]
    public XP_controller xP_Controller;
    //pontos de coleta que o jogador esta coletando
    [SerializeField] float _baseRadius;
    [SerializeField] GameObject _SondasMenu;
    private bool _openSondasMenu = false;
    [SerializeField] Canvas _topCanvas;

    [SerializeField] GameObject _coletor;
    [SerializeField] GameObject _coletorInfo;
    [SerializeField] RectTransform _coletorInfoParent;
    [SerializeField] List<PontoDeColeta> pontosDeColeta;
    [Header("XP Properties")]
    [SerializeField] GameObject _XPbaseObj;
    [SerializeField] Transform _XPparent;

    private void Awake(){
        instance = this;
    }
    public void AddNovoPontoDeColeta(PontoDeColeta novoPonto){
        if(_sondasAtivasCount < _properties._sondasCount){ //limite de sondas
            if(!pontosDeColeta.Contains(novoPonto)){
                pontosDeColeta.Add(novoPonto);
                CriarColetor(novoPonto);
                _sondasAtivasCount++;
                return;
            }
        }
    }
    public Vector2 aroundBasePosition;
    public void CriarColetor(PontoDeColeta pontoDeColeta){
       
        GameObject newColetor = Instantiate(_coletor, transform.position, Quaternion.identity);
        
      
        aroundBasePosition = pontoDeColeta.transform.position - transform.position;
        Vector2 p = aroundBasePosition.normalized * _baseRadius;
        
        newColetor.GetComponent<Coletor>()._basePos = (Vector2) this.transform.position + p;


        newColetor.GetComponent<Coletor>()._pontoDeColeta = pontoDeColeta;
        newColetor.GetComponent<Coletor>()._baseTempoDeColet = pontoDeColeta.pontoDeColetaProps._tempoDeColeta;

        //CANVAS
        GameObject _newOpenInfoCanvas = Instantiate(_coletorInfo, _coletorInfoParent);
        //--
        newColetor.GetComponent<Coletor>()._infoButton = _newOpenInfoCanvas.GetComponent<Open_Info>();

        GameObject _currentXPCounter = Instantiate(_XPbaseObj, _XPparent);
        _currentXPCounter.GetComponent<Canvas>().worldCamera = Camera.main;
        newColetor.GetComponent<Coletor>()._XPcounter = _currentXPCounter;
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, aroundBasePosition.normalized * _baseRadius);
    }
    public void OnMouseDown(){
        //_baseCanvas.enabled = !_baseCanvas.enabled;
        _openSondasMenu = !_openSondasMenu;
        _SondasMenu.GetComponent<Animator>().SetBool("open", _openSondasMenu);
    }
}
