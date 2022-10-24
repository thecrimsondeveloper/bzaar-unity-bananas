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
                
            }
        }
    }
}
