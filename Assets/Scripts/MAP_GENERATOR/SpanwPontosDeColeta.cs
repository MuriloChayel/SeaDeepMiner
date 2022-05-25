using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwPontosDeColeta : MonoBehaviour
{
    [SerializeField] float _minRadius;
    [SerializeField] float _radiusOffset;
    [SerializeField] int _quantWaves;
    
    [SerializeField] List<GameObject> pontosDeColeta;

    public void CreateCircles(){
        
        float dis = _radiusOffset;
        for(int a = 1; a <= _quantWaves; a++){
            //Gizmos.DrawWireSphere(transform.position, _minRadius * (a == 0? 1 : a) + dis);
            RandomPointInAnnulus(transform.position, _minRadius * a  + dis,  _minRadius * a + Mathf.Pow(dis, 2));
            dis *= _radiusOffset;
        }
    }
    //TODO:: CORRIGIR PONTOS E LIMITES ENTRE AREAS DE SPAWN

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius){
 
        var randomDirection = (Random.insideUnitCircle).normalized;
 
        var randomDistance = Random.Range(minRadius, maxRadius);
 
        var point = origin + randomDirection * randomDistance;

        if(Vector2.Distance(origin, point) < 20){
            Instantiate(pontosDeColeta[0], point, Quaternion.identity);
        }
        else if(Vector2.Distance(origin, point) > 20 ){
            Instantiate(pontosDeColeta[1], point, Quaternion.identity);

        }
        
        return point;
    }

    private void OnDrawGizmosSelected(){
        float dis = _radiusOffset;
        for(int a = 1; a <= _quantWaves; a++){
            Gizmos.DrawWireSphere(transform.position, _minRadius * a + dis);
            dis *= _radiusOffset;
        }
    }
}
