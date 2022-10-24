using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using Color = UnityEngine.Color;

namespace Bzaar {
    public class MaterialController : MonoBehaviour
    {
        [SerializeField] Slider  metallicSlider;
        [SerializeField] Slider opacitySlider;
        Outfit outfit => gameObject.GetComponent<Outfit>();
        Renderer[] rends => outfit.selectedObject?.transform.GetComponentsInChildren<Renderer>();

        public void SetMetallic()
        {
            SetObjShininess(metallicSlider.value);
        }

        public void SetOpacity()
        {
            SetObjOpacity(opacitySlider.value);
            
        }

        public void SetSelectedObjColor(Color color)
        {
            if (!Editor.instance.outfit.selectedObject) return;
            SetObjColor(color);
        }


        public void SetObjSkin(Sprite skin)
        {
            if (!outfit.selectedObject) return;

            if (outfit.selectedObject.TryGetComponent(out Renderer renderer))
            {
                SetMaterialSkin(renderer, skin);
            }
            else
            {
                foreach (Renderer rend in rends)
                {
                    SetMaterialSkin(rend, skin);
                }
            }
        }
        public void SetObjColor(Color color)
        {
            if (!outfit.selectedObject) return;  
            if (outfit.selectedObject.TryGetComponent(out Renderer renderer))
            {
                SetMaterialColor(renderer, color);

            }
            else
            {
                
                foreach (Renderer rend in rends)
                {
                    SetMaterialColor(rend, color);
                }
            }
        }



        public void SetObjOpacity(float opacity)
        {
            if (!outfit.selectedObject) return;
            if (outfit.selectedObject.TryGetComponent(out Renderer renderer))
            {
                SetMaterialOpacity(renderer, opacity);

            }
            else
            {

                foreach (Renderer rend in rends)
                {
                    SetMaterialOpacity(rend, opacity);
                }
            }

        }

        public void SetObjShininess(float shinyVal)
        {
            if (!outfit.selectedObject) return;
            if (outfit.selectedObject.TryGetComponent(out Renderer renderer))
            {
                SetMaterialShininess(renderer, shinyVal);
            }
            else
            {

                foreach (Renderer rend in rends)
                {
                    SetMaterialShininess(rend, shinyVal);
                }
            }
        }


        private void SetMaterialColor(Renderer rend, Color col)
        {
            col.a = rend.material.color.a;
            rend.material.color = col;
        }

        private void SetMaterialOpacity(Renderer rend, float val)
        {


            MeshRenderer mr = (MeshRenderer)rend;
            Color col = mr.material.color;
            col.a = val; // pass float value here
            rend.material.color = col;

        }
        private void SetMaterialSkin(Renderer rend, Sprite skin)
        {
            rend.material.mainTexture = skin.texture;
        }

        private void SetMaterialShininess(Renderer rend, float shinyVal)
        {
            float metallic = Mathf.Pow(shinyVal, 1);
            float smoothness = Mathf.Pow(shinyVal, 5);
           
            rend.material.SetFloat("_Metallic", metallic);
            rend.material.SetFloat("_Glossiness",smoothness);

        }
    }
}
