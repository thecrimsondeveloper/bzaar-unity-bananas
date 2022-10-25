using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using System.Threading.Tasks;

public class SaveManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SaveOutfit(GetDebugSave());
        }
    }
    public void SaveOutfit(OutfitSave save)
    {
        
        string saveString = JsonUtility.ToJson(save);
        string hash = Hash128.Compute(saveString).ToString();
        Debug.Log(hash);
        Debug.Log(saveString);
    }


    public OutfitSave GetOutfit(string hash)
    {
        //will need to eventually async fetch the data from firebase.
        string saveJSON = BzaarToFirebase.GetSaveFromHash(hash);
        OutfitSave savedOutfit = JsonUtility.FromJson<OutfitSave>(saveJSON);
        return savedOutfit;
    }

    public OutfitSave GetDebugSave()
    {
        OutfitSave test = new();
        test.outfitName = "TESTING NAME";
        test.bottomEntry = new();
        test.topEntry = new();


        return test;
    }
}


[System.Serializable]
public class OutfitSave
{
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

public static class BzaarToFirebase
{
    public static void Save()
    {

    }

    public async static Task<string> TryGetSaveFromHash(string hash)
    {

        //await for the data fetch
        await Task.Delay(1000);
        return hash;
    }

    public static string GetSaveFromHash(string hash)
    {

        return hash;
    }

}
