using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Inventory : MonoBehaviour
{
    public static Inventory instance{get; set;}
    [SerializeField] GameObject itemCanva;
    [SerializeField] List<Item> _itens;
    [SerializeField] List<GameObject> _itensInCanvas;
    
    private void Awake(){
        instance = this;
    }
    public void AddItem(Types.Inorganics ino, Types.Organics org, int amount){
        //AddInCanva(ino, org, amount);
        //UpdateCanva(ino, org);
        foreach(var i in _itens){
            return;
        }  
        //_itens.Add(item);
        AddInCanva(ino, org, amount);
    }

    public void AddInCanva(Types.Inorganics ino, Types.Organics org, int amount){
        int id = 0;
        foreach(var i in _itensInCanvas){
            
            print("" + id + " " + ino);
            print(id + ": " + i.GetComponent<ItemCanva>().ino);

            id++;
            if(i.GetComponent<ItemCanva>().ino == ino)
            {
                i.GetComponent<ItemCanva>().SetAmount(amount);
                return;
            }
        }

        GameObject _itemCanva = Instantiate(this.itemCanva, transform.GetChild(0)) as GameObject;
        _itemCanva.GetComponent<ItemCanva>().ino = ino;
        _itemCanva.GetComponent<ItemCanva>().SetAmount(amount);
        _itemCanva.GetComponent<ItemCanva>().SetName(ino.ToString());
        _itensInCanvas.Add(_itemCanva.gameObject);
    }
    public void UseItem(int amount){

    }
}
