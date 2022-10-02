using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreenObj;

    public void StartLoading()
    {
        LoadingScreenObj.SetActive(true);
    }

    public void StopLoading()
    {
        LoadingScreenObj.SetActive(false);
    }
}
