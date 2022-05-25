
using UnityEngine;
using System.Collections;

public class TestTouch : MonoBehaviour
{
    private Vector3 dragOrigin;
    [SerializeField] Camera cam;
    [SerializeField] float velocity;
    Vector3 difference;

    private void Update(){
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = cam.ScreenToWorldPoint(touch.position);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                difference = dragOrigin - cam.ScreenToWorldPoint(touch.position);
                //print("origin " + dragOrigin + "newPosition " + cam.ScreenToWorldPoint(touch.position) + " diff " + difference);
                transform.position += difference * Time.deltaTime * velocity;

            }
            if (touch.phase == TouchPhase.Canceled)
            {
                difference = dragOrigin - cam.ScreenToWorldPoint(touch.position);
                print("up");
                StartCoroutine(Estabilize(difference * Time.deltaTime * velocity));
            }

        }
    }
    private IEnumerator Estabilize(Vector3 pos){
        pos.z = -10;
        pos *= 1.4f;
        while(transform.position != pos){
            transform.position = Vector3.Lerp(transform.position, pos, Mathf.SmoothStep(0, 1, Time.deltaTime * 3));
            yield return null;            
        }
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.white;
    }
}
