using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


public class SaveHandler : MonoBehaviour
{
    Dictionary<string,Tilemap> tilemaps = new Dictionary<string,Tilemap>();
    [SerializeField] BoundsInt bounds;
    [SerializeField] string filename = "tilemapData.json";

    private void Start(){
      InitTilemaps();
    }
    public void Save(){
        List<TilemapData> data = new List<TilemapData>();
        foreach(var mapObj in tilemaps){
            TilemapData mapData = new TilemapData();
            mapData.key = mapObj.Key;
            BoundsInt boundsForThisMap = mapObj.Value.cellBounds;

            for(int x = boundsForThisMap.xMin; x < boundsForThisMap.xMax; x++){
             
                for(int y = boundsForThisMap.yMin; y < boundsForThisMap.yMax; y++){
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase tile  = mapObj.Value.GetTile(pos);
                    if(tile != null){
                        TileInfo ti = new TileInfo(tile, pos);
                        mapData.info.Add(ti);
                    }
                }   
            }
            data.Add(mapData);
        }
        //save
        FileHandler.SaveToJSON<TilemapData>(data, filename);
    }
    public void Load(){
        List<TilemapData> data = FileHandler.ReadListFromJSON<TilemapData>(filename);

        foreach(var mapData in data){
            if(!tilemaps.ContainsKey(mapData.key)){
                Debug.LogError("tilemap does not existis: " + mapData.key);
                continue;
            }
            //get tilemap
            var map = tilemaps[mapData.key];
            //clear
            map.ClearAllTiles();
            if(mapData.info != null && mapData.info.Count > 0){
                foreach(TileInfo tile in mapData.info){
                    map.SetTile(tile.position, tile.tile);
                }
            }
        } 
    }
    private void InitTilemaps(){
        Tilemap[] maps = GameObject.FindObjectsOfType<Tilemap>();
        foreach(var map in maps){
            tilemaps.Add(map.name,map);
        }
    }
}
[Serializable]
public class TilemapData{
    public string key;  
    public List<TileInfo> info = new List<TileInfo>();
}
[Serializable]
public class TileInfo{
    public TileBase tile;
    public Vector3Int position;

    public TileInfo(TileBase tile, Vector3Int position){
        this.tile = tile;
        this.position = position;
    }
}

