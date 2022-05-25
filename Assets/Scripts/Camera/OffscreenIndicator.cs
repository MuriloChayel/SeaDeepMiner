using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenIndicator : MonoBehaviour
{
    [SerializeField] float _radarAreaScale;
    [SerializeField] Transform _base;
    [SerializeField] Transform _cam;
    [SerializeField] Transform radarArea;
    [SerializeField] GameObject point;

    private void Update(){
        point.transform.position = (Vector2)radarArea.position + BasePositionInRadar();
    }
    private Vector2 BasePositionInRadar(){
        Vector2 dir = (_base.position - radarArea.position);
        Vector2 p = dir.normalized * _radarAreaScale * dir.magnitude;
        p.x = Mathf.Clamp(p.x, -1, 1);
        p.y = Mathf.Clamp(p.y, -1, 1);
        return p;
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.white;
        Gizmos.DrawRay(_base.position, BasePositionInRadar());
    }
}
