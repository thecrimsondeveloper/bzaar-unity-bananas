using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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

      
            GameObject GLTFEchoObj = App.instance.Echo3D_Manager.SpawnEchoAsset(clothingEntry, this);
            GLTFEchoObj.transform.parent = App.instance.outfit.meshParent.transform;
           

            StartCoroutine(SpawnArticle_Routine());
        }


        
        IEnumerator SpawnArticle_Routine()
        {
            yield return new WaitUntil(() => spawnedArticle != null);
            spawnedArticle.tag = "Article";
            
            //SET THE PARENT
            if (spawnType == ClothingType.top)
            {
                spawnedArticle.transform.parent = App.instance.outfit.top.transform;
                App.instance.outfit.spawnedTop = spawnedArticle;
            }
            if (spawnType == ClothingType.bottom)
            {
                spawnedArticle.transform.parent = App.instance.outfit.bottom.transform;
                App.instance.outfit.spawnedBottom = spawnedArticle;
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

           App.instance.UI.ResetUIView();
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
            // App.instance.previewManager.AddPreviewButtonInfo(clothingEntry,btnImage);

        }
    }

}
