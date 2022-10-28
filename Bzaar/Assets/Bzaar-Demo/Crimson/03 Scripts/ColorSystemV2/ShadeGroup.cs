using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading.Tasks;

public class ShadeGroup : MonoBehaviour
{

    public ColorSystem colorSystem;
    public void RefreshColors(float hue = 0)
    {
        List<Image> images = GetComponentsInChildren<Image>().ToList();

        float value = 1 - (colorSystem.shadeGroups.IndexOf(this) / (float)colorSystem.shadeGroups.Count);

        for (int i = 0; i < images.Count; i++)
        {
            float saturation = (float)i / (float)images.Count;
            images[i].color = Color.HSVToRGB(hue, saturation, value);
        }
    }


}
