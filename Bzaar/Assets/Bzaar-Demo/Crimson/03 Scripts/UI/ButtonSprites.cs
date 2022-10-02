using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSprites : MonoBehaviour
{
    public Sprite selected;
    public Sprite unSelected;

    public Sprite GetSprite(bool isSelected)
    {
        return isSelected ? selected : unSelected;
    }
}
