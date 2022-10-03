using UnityEngine;
using UnityEngine.UI;

namespace Bzaar
{
    [HelpURL("https://assetstore.unity.com/packages/slug/226108")]
    public class ColorManager : MonoBehaviour
    {
        [SerializeField]private Slider[] sliders;
        [SerializeField]private Text[] values;
        [SerializeField]private Image colorBox;
        [SerializeField]private Text codeHTML;

        public void Save()
        {
            PlayerPrefs.SetFloat("R", sliders[0].value);
            PlayerPrefs.SetFloat("G", sliders[1].value);
            PlayerPrefs.SetFloat("B", sliders[2].value);
        }

        public void SetFinalColor(Color color)
        {
            colorBox.color = color;
            SetValues(color);
            App.instance.outfit.materialController.SetSelectedObjColor(color);
        }

        private void SetValues(Color finalColor)
        {
            codeHTML.text = ColorUtility.ToHtmlStringRGB(finalColor);
            sliders[0].value = finalColor.r;
            values[0].text = ((int)(sliders[0].value * 255)).ToString();
            sliders[1].value = finalColor.g;
            values[1].text = ((int)(sliders[1].value * 255)).ToString();
            sliders[2].value = finalColor.b;
            values[2].text = ((int)(sliders[2].value * 255)).ToString();
        }

        private void CalculateColorParameters(Vector2 point, ref float u, ref float v, ref float w)
        {
            float d20 = Vector2.Dot(point, new Vector2(-0.145f, -0.27f));
            float d21 = Vector2.Dot(point, new Vector2(0.145f, -0.27f));
            //v = 15.3322f * d20 - 8.4816f * d21;
            //w = 15.3322f * d21 - 8.4816f * d20;
            //u = 1f - v - w;
        }
    }
}