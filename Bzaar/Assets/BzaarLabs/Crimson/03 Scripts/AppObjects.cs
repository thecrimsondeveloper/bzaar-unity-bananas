using System.Collections;
using System.Collections.Generic;
using Bzaar;
using Crimson;
using UnityEngine;

public class AppObjects : MonoBehaviour
{
    public static AppObjects instance;


    [SerializeField] SaveManager saveManager;
    public SaveManager SaveManager => saveManager;

    [SerializeField] UIManager uiManager;
    public UIManager UIManager => uiManager;

    [SerializeField] LoadingScreen loadingScreen;
    public LoadingScreen LoadingScreen => loadingScreen;

    [SerializeField] Echo echo3D_Manager;
    public Echo Echo3D_Manager => echo3D_Manager;

    [SerializeField] KeyStrokeController keyStrokeController;
    public KeyStrokeController KeyStrokeController => keyStrokeController;

    [SerializeField] StateController stateController;
    public StateController StateController => stateController;

    [SerializeField] App app;
    public App App => app;

    [SerializeField] Closet closet;
    public Closet Closet => closet;

    [SerializeField] Editor editor;
    public Editor Editor => editor;

    [SerializeField] GameObject rotatorGizmo;
    public GameObject RotatorGizmo => rotatorGizmo;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

}
