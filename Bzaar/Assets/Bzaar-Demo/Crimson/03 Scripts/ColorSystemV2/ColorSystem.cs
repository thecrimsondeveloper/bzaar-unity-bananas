using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ColorSystem : MonoBehaviour
{
    [SerializeField] Slider hueSlider;
    public List<ShadeGroup> shadeGroups = new List<ShadeGroup>();

    private void Start()
    {
        shadeGroups = GetComponentsInChildren<ShadeGroup>().ToList();
        // add a shade group for each child of the color system
        SetColor();
    }

    public void SetColor()
    {
        float hue = hueSlider.value;
        hueSlider.handleRect.GetComponent<Image>().color = Color.HSVToRGB(hue, 1, 1);
        foreach (ShadeGroup shadeGroup in shadeGroups)
        {
            if (shadeGroup.colorSystem == null) shadeGroup.colorSystem = this;
            shadeGroup.RefreshColors(hue);
        }
    }



}
