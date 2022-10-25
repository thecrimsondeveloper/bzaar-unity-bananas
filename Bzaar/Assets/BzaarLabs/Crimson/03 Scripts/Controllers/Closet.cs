using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bzaar
{
    public enum Mode
    {
        outfit,
        texture,
        sculpt,
        render
    }

    public class Closet : MonoBehaviour
    {
        public static Closet instance;

        [Header("References")]
        [SerializeField] GameObject ClosetParent;
        
        [Header("Data")]
        public Visuals clothingVisuals;

        [SerializeField] GameObject closetItemBtnPrefab;

        private void Awake()
        {
            if (instance)
            {
                Destroy(instance);
            }
            else
            {
                instance = this;
            }
        }
        public void AppStateChanged()
        {
            if(App.instance.IsState(AppState.None))
            {

            }
            if(App.instance.IsState(AppState.Editor))
            {

            }
            if(App.instance.IsState(AppState.Closet))
            {
                ClosetParent.SetActive(true);   
            }
        }





        public async void GetClosetItems()
        {
            //get hashes associated with account
            List<string> hashes = new();
            foreach (var hash in hashes)
            {
                OutfitSave saveObj = new();
                //await App.instance.saveManager.GetOutfit(hash);

                SetupClosetItem(saveObj);
            }
        }

        private void SetupClosetItem(OutfitSave save)
        {
            Transform parent = null;
            GameObject outfitBtn = Instantiate(closetItemBtnPrefab, Vector3.zero, Quaternion.identity, parent);
            //if (outfitBtn.TryGetComponent(out ClosetButton button))
            //{


            //}
        }
    }
}
