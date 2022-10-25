using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;

namespace Bzaar
{

    public enum ClothingType
    {
        top,
        bottom
    }

   
    public class SpawnClothingBtn : MonoBehaviour
    {

        public GameObject clothingPrefab;
        public ClothingType spawnType;
        [SerializeField] TMP_Text btnText;
        [SerializeField] Image btnImage;
        [SerializeField] Material defaultMaterial;

        private void OnEnable()
        {
            SpawnAnimation();
        }

        private void OnDisable()
        {
            transform.localScale = Vector3.zero;
        }
        public void SpawnAnimation()
        {
            transform.DOScale(1, 1f).SetEase(Ease.OutBounce); ;
        }
        public void SpawnArticle()
        {
            GameObject obj = Instantiate(clothingPrefab);
            obj.tag = "Article";

            if (spawnType == ClothingType.top)
            {
                foreach (Transform child in App.instance.outfit.top.GetComponentInChildren<Transform>())
                {
                    if (child == App.instance.outfit.top.transform) continue;
                    Destroy(child.gameObject);
                }
                obj.transform.parent = App.instance.outfit.top.transform;
            }
            if (spawnType == ClothingType.bottom)
            {
                foreach (Transform child in App.instance.outfit.bottom.GetComponentsInChildren<Transform>())
                {
                    if (child == App.instance.outfit.bottom.transform) continue;
                    Destroy(child.gameObject);
                }
                obj.transform.parent = App.instance.outfit.bottom.transform;

            }



            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

         
            StartCoroutine(lateOutfitSetup(obj));
        }

        IEnumerator lateOutfitSetup(GameObject obj)
        {
            yield return new WaitForEndOfFrame();
            foreach (MeshRenderer item in obj.GetComponentsInChildren<MeshRenderer>())
            {
                item.material = defaultMaterial;
            }

            Outline outline = obj.AddComponent<Outline>();
            outline.OutlineColor = Color.black;
            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineWidth = 5;
            outline.enabled = false;

            App.instance.UI.ResetUIView();
        }

        public void SetupButton(string text)
        {
            btnText.text = "";// text;
            StartCoroutine(LateButtonSetup());
        }

        IEnumerator LateButtonSetup()
        { 
            yield return new WaitForEndOfFrame();
            App.instance.previewManager.AddPreviewButtonInfo(clothingPrefab,btnImage);

        }
    }

}
