using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNoise
{
    #region 
   public static float[,] GenerateMap(int width, int height, float scale){
        float[,] noiseMap = new float[width, height];

        for(int y = 0; y < height; y++){

            for(int x = 0; x < width; x++){
                float samplePosX = x / scale;
                float samplePosY = y / scale;
           
                float PerlinNoise =  Mathf.PerlinNoise(samplePosX, samplePosY); 
                noiseMap[x,y] = PerlinNoise;
           }
        }
        return noiseMap;
    }
    public static Texture2D DrawNoiseMap(float[,] noiseMap ){
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D tex = new Texture2D(width, height);

        Color[] colorMap = new Color[height * width];

            for(int y = 0; y < height; y++){
                for(int x = 0; x < width; x++){
                    colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x,y]);    
            }
        }
        tex.SetPixels(colorMap);
        tex.Apply();
        return tex;
    }

#endregion
    public static int[] GenerateVoronoi(int regionAmount, int regionColorAmount, int size){
        Vector2[] points = new Vector2[regionAmount];
        Color[] _regionColors = new Color[regionColorAmount];

        for(int a = 0; a < regionAmount; a++){
            points[a] = new Vector2(Random.Range(0, size), Random.Range(0,size));
        }            
        
        /*for(int a = 0; a < regionColorAmount; a++){
            _regionColors[a] = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
        } 
        */
        //_regionColors = cores;           

        int[] biomes = new int[size * size];

        for(int y = 0; y < size; y++){
            for(int x = 0; x < size; x++){
                float distance = float.MaxValue;
                int value = 0;

                for(int a = 0; a < regionAmount; a++){
                    if(Vector2.Distance(new Vector2(x,y), points[a]) < distance){
                        distance = Vector2.Distance(new Vector2(x,y),points[a]);
                        value = a;
                    }
                }
                
                //pixelColors[x + y * size] = _regionColors[value];
                biomes[x + y * size] = value;
                MapManager.instance.PlaceATile(x,y, value, size);
            }
        }     
        Texture2D tex = new Texture2D(size,size);
        //tex.SetPixels(pixelColors);
        //tex.Apply();
        //p.material.mainTexture = tex;
        return biomes;
    }
}
