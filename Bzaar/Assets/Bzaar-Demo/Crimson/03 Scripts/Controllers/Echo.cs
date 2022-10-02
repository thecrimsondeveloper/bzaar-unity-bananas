using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Echo : MonoBehaviour
{
    public GameObject EchoHologramPrefab;

    public Echo3DService service;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(test());

    //}

    //IEnumerator test()
    //{ 
    //    UnityWebRequest www = UnityWebRequest.Get("https://api.echo3d.co/query?key=delicate-resonance-0908");
    //    yield return www.SendWebRequest();

    //    Entry[] entries = service.ParseDatabaseFromJson(www.downloadHandler.text).getEntries().ToArray();

    //    foreach (Entry item in entries)
    //    {
    //        Debug.Log(item.getId());    
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnEchoHologram("6fe6f6bb-a772-46f1-aecd-6b35e571e541");
        }
    }


    public void SpawnEchoHologram(string entryID)
    {
        GameObject obj = Instantiate(EchoHologramPrefab,Vector3.zero,Quaternion.identity);
        obj.GetComponent<Echo3DHologram>().entries = entryID;
        StartCoroutine(Late_SpawnEchoHologram(obj));
    }

    IEnumerator Late_SpawnEchoHologram(GameObject spawnedHologram)
    {
        yield return new WaitUntil(() => spawnedHologram.GetComponentsInChildren<MeshRenderer>().Length > 0);

        GameObject obj = spawnedHologram.GetComponentsInChildren<MeshRenderer>()[0].gameObject;
        obj.transform.parent = null;
        //Destroy(spawnedHologram);
    }

}
