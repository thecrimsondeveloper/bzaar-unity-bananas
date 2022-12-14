//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Button = UnityEngine.UI.Button;

namespace Bzaar
{
    public class UIManager : MonoBehaviour
    {
        [Header("Visual Panels")]
        [SerializeField] GameObject topsPanel;
        [SerializeField] GameObject bottomsPanel;
        [SerializeField] GameObject avatarsPanel;

        [SerializeField] GameObject texturesPanel;
        [SerializeField] GameObject colorsPanel;
        [SerializeField] GameObject propertiesPanel;

        [Header("Mode Panels")]
        [SerializeField] GameObject materialModePanel;
        [SerializeField] GameObject outfitModePanel;
        [SerializeField] GameObject sculptModePanel;
        [SerializeField] GameObject renderModePanel;


        [Header("Grid Panels")]
        [SerializeField] GameObject topsGridPanel;
        [SerializeField] GameObject bottomsGridPanel;
        [SerializeField] GameObject materialsPanelGrid;

        [Header("Dependancies")]
        [SerializeField] GameObject textureSelectPrefab;
        [SerializeField] TMP_Text messageText;

        [Header("Buttons")]
        [SerializeField] Button topsBtn;
        [SerializeField] Button bottomsBtn;
        [SerializeField] Button avatarBtn;
        [SerializeField] Button texturesBtn;
        [SerializeField] Button colorsBtn;
        [SerializeField] Button propertiesBt;
        [SerializeField] Button takePhotoBtn;
        [SerializeField] Button recordBtn;

        [Header("Prefabs")]
        [SerializeField] GameObject spawnClothingBtnPrefab;

        [Header("References")]
        public CustomDropDown modeDropdown;

        [SerializeField] private Button selectedBtn = null;
        [SerializeField] private GameObject openedPanel = null;

        private void Start()
        {
            RefreshOutFitSelections();
        }

        public void SetupTopsPanel()
        {

        }

        public void SetupTexturePanel()
        {
            foreach (Sprite spr in App.instance.clothingVisuals.textures)
            {
                GameObject go = Instantiate(textureSelectPrefab);
                if (go.TryGetComponent(out UnityEngine.UI.Image img))
                {
                    img.sprite = spr;
                }

                go.transform.parent = materialsPanelGrid.transform;

                go.transform.localScale = Vector3.one;
            }
        }

        public void ClearUIView()
        {
            topsPanel.SetActive(false);
            bottomsPanel.SetActive(false);

            texturesPanel.SetActive(false);
            colorsPanel.SetActive(false);
            propertiesPanel.SetActive(false);

           
            materialModePanel.SetActive(false);
            outfitModePanel.SetActive(false);
            sculptModePanel.SetActive(false);
            renderModePanel.SetActive(false);

        }

        public void ResetUIView()
        {
            SetUIMode();
            topsPanel.SetActive(false);
            bottomsPanel.SetActive(false);  
            texturesPanel.SetActive(false);
            avatarsPanel.SetActive(false);
            texturesPanel.SetActive(false);
            colorsPanel.SetActive(false);
            propertiesPanel.SetActive(false);
        }

       
        public void SetUIMode()
        {
            outfitModePanel.SetActive(App.instance.mode == Mode.outfit);
            materialModePanel.SetActive(App.instance.mode == Mode.texture);
            sculptModePanel.SetActive(App.instance.mode == Mode.sculpt);
            renderModePanel.SetActive(App.instance.mode == Mode.render);
        }


        public void RefreshOutFitSelections()
        {
            PopulateClothingButtons(ClothingType.top);
            PopulateClothingButtons(ClothingType.bottom);
            StartCoroutine(LateRefreshOutFitSelections());
        }
        IEnumerator LateRefreshOutFitSelections()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            //App.instance.previewManager.SetButtonPreviews();
        }

        public void PopulateClothingButtons(ClothingType cloType)
        {
            StartCoroutine(PopulateClothingButtons_Routine(cloType));
        }

        public IEnumerator PopulateClothingButtons_Routine(ClothingType cloType)
        {
            GameObject parentPanel = cloType == ClothingType.top ? topsGridPanel : bottomsGridPanel;
            Transform[] topsTransform = parentPanel.GetComponentsInChildren<Transform>();
            foreach (Transform cloTransforms in topsTransform)
            {
                if (cloTransforms != parentPanel.transform)
                    Destroy(cloTransforms.gameObject);
            }

            yield return new WaitUntil(()=> App.instance.Echo3D_Manager.entries.Count > 0);
            foreach (Entry entry in App.instance.Echo3D_Manager.entries)
            {
                GameObject obj = Instantiate(spawnClothingBtnPrefab, Vector3.zero, Quaternion.identity, parentPanel.transform);
                obj.transform.localScale = Vector3.one;
                if (!obj.GetComponent<SpawnClothingBtn>()) continue;
                
                    obj.GetComponent<SpawnClothingBtn>().clothingEntry = entry;
                    obj.GetComponent<SpawnClothingBtn>().spawnType = cloType;
                    obj.GetComponent<SpawnClothingBtn>().SetupButton(entry);
                
            }
        }


        

        public void SendMessage(string message, float time = 5)
        {
            if (messageSent) return;
            StartCoroutine(SendMessage_Routine(message, time));
        }

        bool messageSent = false;
        IEnumerator SendMessage_Routine(string message, float time)
        {
            messageText.text = message;
            messageSent = true;
            yield return new WaitForSeconds(time);
            messageSent = false;
            messageText.text = "";
        }

        void ClearActiveUI(Button button, GameObject panel)
        {
            if (!selectedBtn) return;
            if (!openedPanel) return;
            if (selectedBtn == button) return;//Clears the selected UI only if the UI is not what was clicked
            if (openedPanel == panel) return;

            selectedBtn.GetComponent<Image>().sprite = selectedBtn.GetComponent<ButtonSprites>().GetSprite(false);
            openedPanel.SetActive(false); 
            selectedBtn = null;
            openedPanel = null;
            
        }
        void ToggleFocussedUI(Button button, GameObject panel)
        {
            ClearActiveUI(button, panel);//Clear selected UI
            bool isSelected = !panel.activeSelf;
            panel.SetActive(isSelected);//Toggle tops panel on or off    
            button.GetComponent<Image>().sprite = button.GetComponent<ButtonSprites>().GetSprite(isSelected);

            selectedBtn = button;
            openedPanel = panel;
        }

        #region Button Clicks
        public void TopsBtnClicked()
        {
            ToggleFocussedUI(topsBtn,topsPanel);
        }
        public void BottomsBtnClicked()
        {
            ToggleFocussedUI(bottomsBtn, bottomsPanel);
        }
        public void AvatarBtnClicked()
        {
            ToggleFocussedUI(avatarBtn, avatarsPanel);
        }

        public void TexturesBtnClicked()
        {
            ToggleFocussedUI(texturesBtn, texturesPanel);
        }
        public void ColorsBtnClicked()
        {
            ToggleFocussedUI(colorsBtn, colorsPanel);
        }
        public void PropertiesBtnClicked()
        {
            ToggleFocussedUI(propertiesBt, propertiesPanel);
        }

        //A BLANK SPACE FOR SCULPT MODE

        //END SCULPT MODE SPACE

        public void TakePhotoBtnClicked()
        {

        }
        public void RecordVideoBtnClicked()
        {

        }

        public void SpawnAvatar(int index)
        {
            ToggleFocussedUI(avatarBtn, avatarsPanel);
            App.instance.Avatars.SpawnAvatar(index);
        }

        public void SaveCurrentOutfit()
        {
            App.instance.SaveManager.SaveOutfit(App.instance.outfit.SaveCurrentOutfit());
        }

        #endregion
    }
}
