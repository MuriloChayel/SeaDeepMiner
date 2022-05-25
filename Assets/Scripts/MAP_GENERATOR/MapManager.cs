using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    #region 
    public static MapManager instance{get; set;}

    [Header("Voronoi Properties")]
    [SerializeField] BiomeProperties biomas;

    [SerializeField] Tilemap tilemap;
    [SerializeField] Tilemap blurTilemap;
    [SerializeField] Tilemap objects;
    // ---

    [Header("Noise Properties")]
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float scale;
    float[,] map;

    //----
    private Vector3Int pos;

    #endregion

    [Header("props")]
    //[SerializeField] Renderer display;
    [SerializeField] int size;
    private int[] voronoi;

    //EFFECTS
    [Header("Effects")]
    [SerializeField] GameObject bubble;
    [SerializeField] Transform effectsParent;
    private List<GameObject> bubbles = new List<GameObject>();
    private GameObject go;
    public int[] quantB;

    
    //----
    public void Awake(){
        
        instance = this;
    }
    public void Start(){
        //GenerateMap();
       // Vegetation();
    }
    public void PlaceATile(int x, int y, int value, int size){
        tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), biomas.sand[value]);
    }
    public void GenerateMap(){

        //voronoi
        voronoi = GenerateNoise.GenerateVoronoi(biomas.sand.Length, biomas.sand.Length, size);
        SpawnPontosDeColeta(voronoi, biomas.sand.Length, size);
        //noise
        map = GenerateNoise.GenerateMap(width, height, scale);   
        Texture2D tex = GenerateNoise.DrawNoiseMap(map);

        //display.material.mainTexture = tex;
        ApplyBlurAreas(map);
    }   
    public void Vegetation(){ 
        for(int y = 0; y < size; y++){
            for(int x = 0; x < size; x++){
                objects.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), null);
            }
        }               
        for(int a = 0; a< bubbles.Count; a++){
            Destroy(bubbles[a].gameObject);
        }
        bubbles.Clear();
        
        for(int y = 0; y < size; y++){
            for(int x = 0; x < size; x++){
                if(map[x, y] < 0.3f &&  voronoi[x + y * size] == 1){
                    objects.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), biomas.grass[Random.Range(0, biomas.grass.Length)]);
                    int percent = Random.Range(0, 30);

                    if(percent < 1){
                        GameObject currentBubble = Instantiate(bubble, objects.CellToWorld(new Vector3Int(x - size / 2, y - size / 2, 0)), Quaternion.identity);
                        bubbles.Add(currentBubble);
                    }
                }
                else if(map[x, y] < 0.35f &&  voronoi[x + y * size] == 2)
                {
                    int percent = Random.Range(0, 8);
                    if(percent < 2){
                        objects.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), biomas.coral[Random.Range(0, biomas.coral.Length)]);
                    }
                }
                else if(map[x,y] > 0.3f && map[x,y] < 0.5f){
                    int probability = Random.Range(0, 10);
                        if(probability < 1 && voronoi[x + y * size] == 1){
                        objects.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), biomas.rock[Random.Range(0, biomas.rock.Length)]);      
                        }
                    }
            }
        }
    }
    public void ApplyBlurAreas(float[,] noiseMap){
        
        for(int y = 0; y < width; y++)
        {
            for(int x = 0; x < height; x++)
            {
                if(noiseMap[x,y] >= 0 && noiseMap[x,y] < .4f){
                    blurTilemap.SetTile(new Vector3Int(x - size/2, y - size/2, 0), biomas.blur[0]);
                }  
                if(noiseMap[x,y] >= 0 && noiseMap[x,y] < .2f){
                    blurTilemap.SetTile(new Vector3Int(x - size/2, y - size/2, 0), biomas.blur[1]);   
                }
            }
        }
    }

    public void SpawnPontosDeColeta(int[] voronoiMap, int quantBiomas, int size){
        quantB = new int[quantBiomas];
        foreach(var i in voronoiMap){
            quantB[i]++;            
        }
    }    
}
