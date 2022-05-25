using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Open_Info : MonoBehaviour
{
    private Button _contentButton;
    public RectTransform InfoArea;
    public Vector2 newSize;
    private bool _open = false;
    private float _amount;

    //CANVAS
    public RectTransform _timeBar;
    //--
    private void Awake(){
        _contentButton = GetComponent<Button>();
    }
    public void OpenInfo(){
        _open = !_open;
    }
    private void FixedUpdate(){
        if(_open){
            _amount = Mathf.Lerp(_amount, 400, Mathf.SmoothStep(0, 1, Time.fixedDeltaTime* 15));    
        }
        else{
            _amount = Mathf.Lerp(_amount, 0, Mathf.SmoothStep(0, 1, Time.fixedDeltaTime * 15));    
        }
        InfoArea.offsetMax = new Vector2(InfoArea.offsetMax.x, _amount);
    }
}
