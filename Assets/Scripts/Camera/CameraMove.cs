using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class CameraMove : MonoBehaviour
{
    public static CameraMove instance{get; private set;}


    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;


    private Controllers controller;

    #region Default
    private void Awake(){
        instance = this;

        controller = new Controllers();
    }
    private void OnEnable(){
        controller.Enable();
    }
    private void OnDisable(){
        controller.Disable();
    }
    #endregion 
    
    private void Start(){
        controller.Touch.TouchPress.started += ctx => StartTouch(ctx);
        controller.Touch.TouchPress.canceled+= ctx => EndTouch(ctx);
    }
    private void StartTouch(InputAction.CallbackContext context){
        if(OnStartTouch != null) OnStartTouch(controller.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        print("start Touch");
    }
    private void EndTouch(InputAction.CallbackContext context){
        if(OnEndTouch != null) OnEndTouch(controller.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        print("End Touch");

    }

}

