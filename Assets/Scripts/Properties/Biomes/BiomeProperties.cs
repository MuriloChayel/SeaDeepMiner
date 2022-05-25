using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Biome/newBiome")]
public class BiomeProperties : ScriptableObject
{
    // Sand Tiles
    [Header("Sand Tiles")]
    public  Tile[] sand;
    // Sand Tiles
    [Header("Rock Tiles")]
    public Tile[] rock;
    // Sand Tiles
    [Header("Organic Tiles")]
    public Tile[] organics;
    [Header("Alga Tiles")]
    public Tile[] grass;
    [Header("Coral Tiles")]
    public Tile[] coral;
    [Header("Blur Tiles")]
    public Tile[] blur;

    

}
