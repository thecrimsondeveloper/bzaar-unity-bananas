using Bzaar;
using GLTFast;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;
//using static UnityEditor.Progress;
using static UnityEngine.Rendering.DebugUI;

public class Echo : MonoBehaviour
{
    public const string API_KEY = "empty-queen-9732";
    public const string SECURITY_KEY = "Yx99Xr1XiG1y21U3aQ129hWC";
    public GameObject EchoHologramPrefab;
    public GameObject EchoGLTFPrefab;

    public Echo3DService service;

    public List<Entry> entries = new List<Entry>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PopulateEntries_Routine());
    }

    IEnumerator PopulateEntries_Routine()
    {
        UnityWebRequest www = UnityWebRequest.Get($"https://api.echo3d.co/query?key={API_KEY}");
        yield return www.SendWebRequest();
        entries = service.ParseDatabaseFromJson(www.downloadHandler.text).getEntries().ToList();
    }

 
    public GameObject SpawnEchoAsset(Entry entry, SpawnClothingBtn spawnerBtn)
    {
        string entryID = entry.getId();
        Dictionary<string, string> entryData = entry.getAdditionalData();

        if (entryData.TryGetValue("glbHologramStorageID", out string value))
        {

            //Spawn in the GLTF SPAWNER
            string URL = $"https://api.echo3D.co/query?key={API_KEY}&secKey={SECURITY_KEY}&src=UnitySDK&entries={entryID}&file={value}";
            GameObject obj = Instantiate(EchoGLTFPrefab, Vector3.up * 1000, Quaternion.identity);
            obj.GetComponent<GltfAsset>().url = URL;

            //Wait for the object to spawn in and then get a reference to it.
            StartCoroutine(SpawnEchoAsset_Routine(obj,spawnerBtn));

            return obj;
        }

        return null;
    }



    IEnumerator SpawnEchoAsset_Routine(GameObject echoObject, SpawnClothingBtn spawnerBtn)
    {
        GameObject clothingObject;
        yield return new WaitUntil(()=>echoObject.GetComponentsInChildren<MeshRenderer>().Length > 0);
        clothingObject = echoObject.GetComponentsInChildren<MeshRenderer>()[0].gameObject;
        clothingObject.transform.parent = null;
        spawnerBtn.SetSpawnedArticle(clothingObject);
    }



  

}
