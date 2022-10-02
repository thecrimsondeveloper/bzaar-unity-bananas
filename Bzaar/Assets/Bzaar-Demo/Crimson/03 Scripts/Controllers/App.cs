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

    public class App : MonoBehaviour
    {
        public static App instance;

        public UIManager UI;
        public PreviewManager previewManager;
        public Outfit outfit;
        public LoadingScreen loadingScreen;
        public AvatarDisplaying Avatars;

        public Visuals clothingVisuals;
        public Clothing clothingModels;

        public int touchCountLastFrame = 0;
        public Vector3 lastMousePosition;

        public Mode mode;

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

        void Start()
        {
            loadingScreen.StartLoading();
            UI.SetupTexturePanel();
        }

        private void Update()
        {
            SetMode();
        }

        private void LateUpdate()
        {
            touchCountLastFrame = Input.touchCount;
            lastMousePosition = Input.touchCount > 0 ? Input.mousePosition : Vector3.zero;
        }

        public void SetMode()
        {
            mode = (Mode)UI.modeDropdown.currentlySelectedIndex;
            UI.SetUIMode();
        }

        public void SetStartEditorState()
        {
            UI.SetUIMode();
            StartCoroutine(SetStartState_Coroutine());
        }

        IEnumerator SetStartState_Coroutine()
        {
            yield return new WaitForSeconds(0.25f);
            App.instance.UI.ResetUIView();
            loadingScreen.StopLoading();
        }
    }
}
