using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SAveLoad : MonoBehaviour
{
    public Tilemap map;
    void Start()
    {
        SaveData _save = new SaveData();        
        _save.map = map;
        SaveGame(_save);
        //SaveData load = LoadGame();
        //print(load.name);

    }
    private void SaveGame(SaveData s){
        BinaryFormatter bf = new BinaryFormatter();

        string path = Application.persistentDataPath;
        FileStream file = File.Create(path + "/savegame.save");
        bf.Serialize(file, s);
        file.Close();

        Debug.Log("Game Saved");
    }
    public SaveData LoadGame(){
        
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath;
        FileStream file;

        if(File.Exists(path + "/savegame.save")){
            file = File.Open(path + "/savegame.save", FileMode.Open);

            SaveData l = (SaveData)bf.Deserialize(file);
            file.Close();

            Debug.Log("game loaded");

            return l;
        }
        return null;
    }
}
