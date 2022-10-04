using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingDataTransfer : MonoBehaviour
{
    public OutfitSave data;


    public void SetData(string JSON)
    {
        data = JsonUtility.FromJson<OutfitSave>(JSON);
    }
}
