using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class SaveManager : MonoBehaviour
{
    public Dictionary<string, string> saves = new Dictionary<string, string>();

    public OutfitSave test = new OutfitSave();

    private void Start()
    {
        if (!File.Exists("Assets/Resources/outfitHashes.txt")) { PlayerPrefs.DeleteAll(); }

        //for(int i = 0; i < 10; i++)
        //{
        //    SaveOutfit();
        //}
        //GetAllSaves();
        PopulateSaveDictionary();
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            test = GetOutfit("23FA3-89C477-C5B143-2DD6DD-65A2F4");
            
        }
    }
    public void SaveOutfit(OutfitSave save)
    {
        save.key = "Unset";
        
        string saveString = JsonUtility.ToJson(save);
        string hash = Hash128.Compute(saveString).ToString();
        save.key =hash.ToString();
        Debug.Log(hash);
        
        PlayerPrefs.SetString(save.key, saveString);

        using (StreamWriter writer = new StreamWriter("Assets/Resources/outfitHashes.txt",true))
        { 
            writer.WriteLine(save.key);
        }
    }

    public void PopulateSaveDictionary()
    {
        
        using (StreamReader reader = new StreamReader("Assets/Resources/outfitHashes.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                saves.Add(line, PlayerPrefs.GetString(line));
            }
        }
    }

    public OutfitSave GetOutfit(string hash)
    {
        if (saves.TryGetValue(hash, out string outfitJson))
        { 
            return JsonUtility.FromJson<OutfitSave>(outfitJson);
        }
        return null;
    }


   
}


[System.Serializable]
public class OutfitSave
{
    public string key = "Unset";
    public string outfitName;
    public ArticleSave topEntry;
    public ArticleSave bottomEntry;
}

[System.Serializable]
public class ArticleSave 
{
    public Entry entry;
    
    public Color color;
    public string textureName;
    public float opacity;
    public float reflectivity;
    public float luminosity;
}
