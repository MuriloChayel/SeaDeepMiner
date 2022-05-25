using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Coletor : MonoBehaviour
{
    #region Variables

    //Private variables --
    private float _elapsedTime;
    private float _tempoDeColeta;
    private bool _coletou = false;
    // ---
    [SerializeField] RectTransform _fillBar;
    public GameObject _XPcounter;
    // ---
    public Vector3 _basePos {get; set;}

    public PontoDeColeta _pontoDeColeta {get; set;}
    public float _baseTempoDeColet{get; set;}
    public Open_Info _infoButton{get; set;} 

    //----
    [SerializeField] Sprite[] images; //
    public float _velocity;
    public float _duration;

    #endregion
    
    // 0 -> cima
    // 1 -> baixo
    int type;

    private void Start(){
        _duration = Vector2.Distance(_basePos, _pontoDeColeta.transform.position);
        _duration *= Time.deltaTime * _velocity;
        SetupLookAt();
    }
    private void FixedUpdate(){
        Coletar();
    }
    private void Coletar(){
        _elapsedTime += Time.fixedDeltaTime;
        float percentageComplete = _elapsedTime / _duration; 
        
        //COLETA
        if(!_coletou){ 

            transform.position = Vector3.MoveTowards(transform.position, _pontoDeColeta.transform.position, Time.deltaTime * _velocity);//Vector2.Lerp(transform.position, _pontoDeColeta.transform.position, Mathf.SmoothStep(0, 1, _duration));
            
            if(transform.position == _pontoDeColeta.transform.position){
                _tempoDeColeta += Time.deltaTime;
                //updating coletor bar
                float percentage =  _tempoDeColeta / _baseTempoDeColet;
                _fillBar.sizeDelta = new Vector2(percentage , 0.08f);
                UpdateTimeBar(percentage);

                //VOLTANDO DA COLETA
                if(_tempoDeColeta >= _baseTempoDeColet){
                    _elapsedTime = 0;
                    _coletou = true;
                    UpdateSpriteDirection(false);
                }
            }
        }
        //DESCARGA
        else{
            //transform.position = Vector2.Lerp(transform.position, _basePos, Mathf.SmoothStep(0, 1, percentageComplete));
            transform.position = Vector3.MoveTowards(transform.position, _basePos, Time.deltaTime * _velocity);
            if(transform.position == _basePos){
            
                _tempoDeColeta -= Time.deltaTime;
                _fillBar.sizeDelta = new Vector2( _tempoDeColeta / _baseTempoDeColet , 0.08f);
                UpdateTimeBar(_tempoDeColeta / _baseTempoDeColet);

                if(_tempoDeColeta <= 0){
                    UpdateXPCounter(_pontoDeColeta._baseXP);
                    _elapsedTime = 0;
                    _coletou = false;

                    GetComponent<ColetorProperties>().AddToInventory(_pontoDeColeta.pontoDeColetaProps._inorganic,_pontoDeColeta.pontoDeColetaProps._organic, _pontoDeColeta.pontoDeColetaProps._amount);

                    UpdateSpriteDirection(true);

                }
            }
        }
    }
    private void UpdateTimeBar(float timeBarFillAmount){
        _infoButton._timeBar.sizeDelta = new Vector2(timeBarFillAmount * 1000f, _infoButton._timeBar.sizeDelta.y);
    }
    private void SetupLookAt(){
        SpriteRenderer _currentSp = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if(_pontoDeColeta.transform.position.x < _basePos.x &&  _pontoDeColeta.transform.position.y > _basePos.y){
            _currentSp.sprite = images[0];
            _currentSp.flipX = true;
            type = 0;
        }
        else if(_pontoDeColeta.transform.position.x > _basePos.x &&  _pontoDeColeta.transform.position.y > _basePos.y){
            _currentSp.sprite = images[0];
            type = 0;

        }
        else if(_pontoDeColeta.transform.position.x < _basePos.x &&  _pontoDeColeta.transform.position.y < _basePos.y){
            _currentSp.sprite = images[1];
            type = 1;

        }
        else{
            _currentSp.sprite = images[1];
            _currentSp.flipX = true;
            type = 1;
        }
    }
    private void UpdateSpriteDirection(bool indo){
        var _currentSp = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if(type == 0){
            type = 1;
        }
        else
        {
            type = 0;
        }
        _currentSp.sprite = images[type];
    }
    private void UpdateXPCounter(int XP){
        print("AAAA");
        _XPcounter.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "" + XP;
        XP_controller.instance.UpdateXP(XP);

    }
}
