using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothing Pieces", menuName = "ScriptableObjects/ClothingSet", order = 1)]
public class Clothing : ScriptableObject
{
    [Header("Info")]
    public Material defaultMaterial;
    public GameObject gridImagePrefab;

    [Header("Clothing")]
    public List<GameObject> tops = new List<GameObject>();
    public List<GameObject> bottoms = new List<GameObject>();

    

    //public List<GameObject> shoes = new List<GameObject>();
    //publiic GameObject shoesGrid;


   
}
