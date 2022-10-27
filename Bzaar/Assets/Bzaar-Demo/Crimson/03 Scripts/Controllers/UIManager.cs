//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

using TMPro;
using Button = UnityEngine.UI.Button;
using Michsky.MUIP;
using System.Linq;

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
        [SerializeField] ButtonManager topsBtn;
        [SerializeField] ButtonManager bottomsBtn;
        [SerializeField] ButtonManager avatarBtn;
        [SerializeField] Button texturesBtn;
        [SerializeField] Button colorsBtn;
        [SerializeField] Button propertiesBt;
        [SerializeField] Button takePhotoBtn;
        [SerializeField] Button recordBtn;

        public CustomDropDownV2 modeDropdown;

        [SerializeField] private Object selectedBtn = null;
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

            RefreshTops();
            RefreshBottoms();
            StartCoroutine(LateRefreshOutFitSelections());
        }
        IEnumerator LateRefreshOutFitSelections()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            App.instance.previewManager.SetButtonPreviews();
        }

        public void RefreshTops()
        {
            Transform[] topsTransform = topsGridPanel.GetComponentsInChildren<Transform>();
            foreach (Transform topTransform in topsTransform)
            {
                if (topTransform != topsGridPanel.transform)
                    Destroy(topTransform.gameObject);
            }

            foreach (GameObject item in App.instance.clothingModels.tops)
            {
                GameObject obj = Instantiate(App.instance.clothingModels.gridImagePrefab);
                obj.transform.parent = topsGridPanel.transform;
                obj.transform.localScale = Vector3.one;

                if (obj.TryGetComponent(out SpawnClothingBtn spawnBtn))
                {
                    spawnBtn.clothingPrefab = item;
                    spawnBtn.spawnType = ClothingType.top;
                    spawnBtn.SetupButton(item.name);
                }

                obj.name = item.name + "-SPAWN_BUTTON";
            }
        }

        public void RefreshBottoms()
        {
            Transform[] bottomsTransform = bottomsGridPanel.GetComponentsInChildren<Transform>();
            foreach (Transform bottomTransform in bottomsTransform)
            {
                if (bottomTransform != bottomsGridPanel.transform)
                    Destroy(bottomTransform.gameObject);
            }

            foreach (GameObject item in App.instance.clothingModels.bottoms)
            {
                GameObject obj = Instantiate(App.instance.clothingModels.gridImagePrefab);
                obj.transform.parent = bottomsGridPanel.transform;
                obj.transform.localScale = Vector3.one;


                if (obj.TryGetComponent(out SpawnClothingBtn spawnBtn))
                {
                    spawnBtn.clothingPrefab = item;
                    spawnBtn.spawnType = ClothingType.bottom;
                    spawnBtn.SetupButton(item.name);
                }

                obj.name = item.name + "-SPAWN_BUTTON";
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

        void ClearActiveUI(Object button, GameObject panel)
        {
            if (selectedBtn == button) return;//Clears the selected UI only if the UI is not what was clicked
            if (openedPanel == panel) return;

            if (selectedBtn is Button) (selectedBtn as Button).GetComponent<Image>().sprite = (selectedBtn as Button).GetComponent<ButtonSprites>().GetSprite(false);

            // List<ButtonManager> buttons = openedPanel.GetComponentsInChildren<ButtonManager>().ToList();
            // foreach (var btn in buttons)
            // {
            //     //btn.transform.DOScale
            // }
            if (openedPanel) openedPanel.SetActive(false);
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

        void ToggleFocussedUI(ButtonManager btnMgr, GameObject panel)
        {
            bool isSelected = !panel.activeSelf;
            panel.SetActive(isSelected);
            openedPanel = panel;
        }



        #region Button Clicks

        public void TopsBtnClicked()
        {

            ClearActiveUI(topsBtn, topsPanel);
            topsPanel.SetActive(!topsPanel.activeSelf);
            openedPanel = topsPanel;

            List<SpawnClothingBtn> buttons = topsPanel.GetComponentsInChildren<SpawnClothingBtn>().ToList();

            if (!topsPanel.gameObject.activeSelf)
            {
                buttons.ForEach(button =>
                {
                    button.transform.localScale = Vector3.zero;
                    button.DOKill();
                });
                return;
            }
            buttons.ForEach(item =>
            {
                item.transform.localScale = Vector3.zero;
                item.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InCubic).SetDelay(item.transform.GetSiblingIndex() * 0.05f);
            });
        }
        public void BottomsBtnClicked()
        {
            ClearActiveUI(bottomsBtn, bottomsPanel);
            ToggleFocussedUI(bottomsBtn, bottomsPanel);
            List<SpawnClothingBtn> buttons = bottomsPanel.GetComponentsInChildren<SpawnClothingBtn>().ToList();
            Debug.Log("BottomsBtnClicked: " + buttons.Count);
            if (!bottomsPanel.gameObject.activeSelf)
            {
                buttons.ForEach(button =>
                {
                    button.transform.localScale = Vector3.zero;
                    button.DOKill();
                });
                return;
            }
            buttons.ForEach(item =>
            {
                item.transform.localScale = Vector3.zero;
                item.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InCubic).SetDelay(item.transform.GetSiblingIndex() * 0.05f);
            });
        }
        public void AvatarBtnClicked()
        {
            ClearActiveUI(avatarBtn, avatarsPanel);
            ToggleFocussedUI(avatarBtn, avatarsPanel);

            List<ButtonManager> buttons = avatarsPanel.GetComponentsInChildren<ButtonManager>().ToList();

            if (!avatarsPanel.gameObject.activeSelf)
            {
                buttons.ForEach(button =>
                {
                    button.transform.localScale = Vector3.zero;
                    button.DOKill();
                });
                return;
            }

            buttons.ForEach(item =>
            {
                item.transform.localScale = Vector3.zero;
                item.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InCubic).SetDelay(item.transform.GetSiblingIndex() * 0.05f);
            });


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

        #endregion
    }
}
