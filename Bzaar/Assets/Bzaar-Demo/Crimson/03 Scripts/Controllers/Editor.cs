using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bzaar
{
    public enum EditorMode
    {
        outfit,
        texture,
        sculpt,
        render
    }

    public class Editor : MonoBehaviour
    {
        public static Editor instance;

        [Header("References")]
        [SerializeField] GameObject EditorParent;
        public PreviewManager previewManager;
        public Outfit outfit;
        public AvatarDisplaying Avatars;
        
        [Header("Data")]
        public Visuals clothingVisuals;
        
        public EditorMode mode;

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
            App.instance.loadingScreen.StartLoading();
            App.instance.UI.SetupTexturePanel();
        }

        private void Update()
        {
            SetMode();

            
        }



        public void SetMode()
        {
            mode = (EditorMode)App.instance.UI.modeDropdown.currentlySelectedIndex;
            App.instance.UI.SetUIMode();
        }

        public void SetStartEditorState()
        {
            App.instance.UI.SetUIMode();
            StartCoroutine(SetStartState_Coroutine());
        }

        IEnumerator SetStartState_Coroutine()
        {
            yield return new WaitForSeconds(0.25f);
            App.instance.UI.ResetUIView();
            App.instance.loadingScreen.StopLoading();
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
                
            }
        }
    }
}
