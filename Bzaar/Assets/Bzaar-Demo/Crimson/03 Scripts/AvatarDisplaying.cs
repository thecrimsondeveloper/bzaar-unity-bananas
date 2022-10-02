using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvatarDisplaying : MonoBehaviour
{

    public List<GameObject> avatars = new List<GameObject>();

    private void Start()
    {
        SpawnAvatar();
    }

    public void SpawnAvatar(int whichOne = 0)
    {
        for (int i = 0; i < avatars.Count; i++)
        {
            if (whichOne == i)
            {
                avatars[i].SetActive(true);
            }
            else
            {
                avatars[i].SetActive(false);
            }
        }
    }
}
