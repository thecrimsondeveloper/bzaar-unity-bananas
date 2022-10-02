using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bzaar
{

    [CreateAssetMenu(fileName = "Clothing Visuals", menuName = "ScriptableObjects/ClothingVisuals", order = 2)]

    public class Visuals : ScriptableObject
    {

        public List<Sprite> textures = new List<Sprite>();

        
    }
}
