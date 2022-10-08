using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Net;
using System;
using UnityEngine.Networking;
using System.Threading.Tasks;

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
        public TMP_Text btnText;
        [SerializeField] Image btnImage;
        [SerializeField] Material defaultMaterial;

        

        private GameObject spawnedArticle = null;
      
        public async void SpawnArticle()
        {
            App.instance.UI.ResetUIView();

            if (spawnedArticle != null) return;

      
            GameObject GLTFEchoObj = App.instance.Echo3D_Manager.SpawnEchoAsset(clothingEntry, this);

            GLTFEchoObj.transform.parent = Editor.instance.outfit.meshParent.transform;

            while (spawnedArticle == null)
            {
                await Task.Delay(500);    
            }

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
            spawnedArticle.GetComponent<MeshRenderer>().material = defaultMaterial;
            spawnedArticle.AddComponent<MeshCollider>();

            Outline outline = spawnedArticle.AddComponent<Outline>();
            outline.OutlineColor = Editor.instance.modelOutlineColor;
            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineWidth = Editor.instance.modelOutlineWidth;
            outline.enabled = false;

            spawnedArticle.transform.localScale = Vector3.one;
            spawnedArticle.transform.localPosition = Vector3.zero;
            spawnedArticle.transform.localRotation = Quaternion.identity;

            GLTFEchoObj.transform.parent = spawnedArticle.transform;

            spawnedArticle = null;
        }

       

        public void SetSpawnedArticle(GameObject obj)
        {
            
            spawnedArticle = obj;
        }

        public async void SetupButton(Entry entry, string imgVal)
        { 
            await TaskHelper.WaitFrame();
            
            if (imgVal != null)//clothingEntry.getAdditionalData().TryGetValue("screenShotStorageID", out string screenShotVal))
            {
                string downloadLink = "https://api.echo3d.co/query?key="+Echo.API_KEY+"&file="+imgVal;
                using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(downloadLink))
                {
                    // Request and wait for the desired page.
                    
                    await Task.Yield();
                    webRequest.SendWebRequest();

                    while (webRequest.result == UnityWebRequest.Result.InProgress)
                    {
                        await Task.Delay(100);
                    }
                    string[] pages = downloadLink.Split('/');
                    int page = pages.Length - 1;


                    if (webRequest.result == UnityWebRequest.Result.Success)
                    {
                        Texture2D webTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture as Texture2D;
                        Sprite sprite = Sprite.Create(webTexture, new Rect(0.0f, 0.0f, webTexture.width, webTexture.height), new Vector2(0.5f, 0.5f));
                        btnImage.sprite = sprite;
                    }
                    else
                    {
                        throw new Exception("Error retrieving button image: " + webRequest.error);
                    }
                }
            }
            else
            {
                btnText.text = "Button Image not found.";
            }
        }

       
    }

}
