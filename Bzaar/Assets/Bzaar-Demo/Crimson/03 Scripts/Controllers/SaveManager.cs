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
        
        if (save.key == "Unset")
        {
            save.key = GenerateSaveString();
        }

        string saveString = JsonUtility.ToJson(save);

        Debug.Log(saveString);
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


    string GenerateSaveString()
    {
        string outStr = "";

        for (int i = 0; i < 5; i++)
        { 
            if(i == 0) outStr += $"{GetRandomSavePArtition()}";
            else outStr += $"-{GetRandomSavePArtition()}";
        }

        return outStr;
    }
    string GetRandomSavePArtition()
    {
        return UnityEngine.Random.Range(0, 16777215).ToString("X");
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
