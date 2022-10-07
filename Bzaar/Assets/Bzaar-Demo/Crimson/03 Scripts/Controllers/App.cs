using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

namespace Bzaar{
    public enum AppState
    {
        None,
        Closet,
        Editor
    }


    public class App : MonoBehaviour
    {
        public static App instance;

        public UIManager UI;
        public SaveManager saveManager;
        public LoadingScreen loadingScreen;
        public Echo Echo3D_Manager;

        [SerializeField] AppState state = AppState.None;
        public UnityEvent onAppStateChange;
        public int touchCountLastFrame = 0;
        public Vector3 lastMousePosition;

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
        private void LateUpdate() 
        {
             touchCountLastFrame = Input.touchCount;
            lastMousePosition = Input.touchCount > 0 ? Input.mousePosition : Vector3.zero;

        }
        async void Start()
        {
            await Task.Delay(500);
            SetState(AppState.Closet);    
        }
        public void SetState(AppState newState)
        {
            state = newState;
            onAppStateChange.Invoke();
        }
        public bool IsState(AppState testState)
        {
            return state == testState;
        }
    }
}
