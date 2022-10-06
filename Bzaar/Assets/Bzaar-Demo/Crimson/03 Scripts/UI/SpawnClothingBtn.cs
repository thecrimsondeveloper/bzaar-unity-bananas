using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Net;
using System;
using UnityEngine.Networking;

namespace Bzaar
{

    public enum ClothingType
    {
        top,
        bottom
    }

    public class SpawnClothingBtn : MonoBehaviour
    {

        public Entry clothingEntry;
        public ClothingType spawnType;
        [SerializeField] TMP_Text btnText;
        [SerializeField] Image btnImage;
        [SerializeField] Material defaultMaterial;

        private GameObject spawnedArticle = null;
      
        public void SpawnArticle()
        {
            if (spawnedArticle != null) return;

      
            GameObject GLTFEchoObj = Editor.instance.Echo3D_Manager.SpawnEchoAsset(clothingEntry, this);
            GLTFEchoObj.transform.parent = Editor.instance.outfit.meshParent.transform;
           

            StartCoroutine(SpawnArticle_Routine());
        }


        
        IEnumerator SpawnArticle_Routine()
        {
            yield return new WaitUntil(() => spawnedArticle != null);
            spawnedArticle.tag = "Article";
            
            //SET THE PARENT
            if (spawnType == ClothingType.top)
            {
                spawnedArticle.transform.parent = Editor.instance.outfit.top.transform;
                Editor.instance.outfit.spawnedTop = spawnedArticle;
            }
            if (spawnType == ClothingType.bottom)
            {
                spawnedArticle.transform.parent = Editor.instance.outfit.bottom.transform;
                Editor.instance.outfit.spawnedBottom = spawnedArticle;
            }

            spawnedArticle.transform.localScale = Vector3.one;
            spawnedArticle.transform.localPosition = Vector3.zero;
            spawnedArticle.transform.localRotation = Quaternion.identity;

            yield return new WaitForEndOfFrame();
            spawnedArticle.GetComponent<MeshRenderer>().material = defaultMaterial;

            spawnedArticle.AddComponent<MeshCollider>();

            Outline outline = spawnedArticle.AddComponent<Outline>();
            outline.OutlineColor = Color.black;
            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineWidth = 5;
            outline.enabled = false;

            
            spawnedArticle = null;

            Editor.instance.UI.ResetUIView();
        }

        public void SetSpawnedArticle(GameObject obj)
        {
            spawnedArticle = obj;
        }

        public void SetupButton(Entry entry)
        {

            //btnText.text = entry.getId();// text;
            StartCoroutine(LateButtonSetup());
        }

        IEnumerator LateButtonSetup()
        { 
            yield return new WaitForEndOfFrame();
            if (clothingEntry.getAdditionalData().TryGetValue("screenShotStorageID", out string screenShotVal))
            {
                string downloadLink = $"https://api.echo3d.co/query?key={Echo.API_KEY}&file={screenShotVal}";
                using (UnityWebRequest webRequest = UnityWebRequest.Get(downloadLink))
                {
                    // Request and wait for the desired page.
                    yield return webRequest.SendWebRequest();

                    string[] pages = downloadLink.Split('/');
                    int page = pages.Length - 1;

                    switch (webRequest.result)
                    {
                        case UnityWebRequest.Result.ConnectionError:
                        case UnityWebRequest.Result.DataProcessingError:
                            Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                            break;
                        case UnityWebRequest.Result.ProtocolError:
                            Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                            break;
                        case UnityWebRequest.Result.Success:
                            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                            break;
                    }
                }
            }
            // App.instance.previewManager.AddPreviewButtonInfo(clothingEntry,btnImage);

        }
    }

}
