using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreenObj;
    [SerializeField] GameObject bzaarLogo;
    [SerializeField] GameObject loadingRing;
    [SerializeField] Image loadingScreenImage;

    bool startLoadingAnimationPlaying = false;
    private void Awake()
    {
        bzaarLogo.transform.localScale = Vector3.zero;
        loadingRing.transform.localScale = Vector3.zero;
    }

    public void StartLoading()
    {
        startLoadingAnimationPlaying = true;
        LoadingScreenObj.SetActive(true);
        bzaarLogo.transform.DOScale(1, 1f).SetEase(Ease.InOutCubic); ;
        loadingRing.transform.DOScale(1, 1f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            startLoadingAnimationPlaying = false;
        });
    }

    public void StopLoading()
    {
        StartCoroutine(StopLoading_Routine());
    }

    IEnumerator StopLoading_Routine()
    {
        yield return new WaitUntil(() => (startLoadingAnimationPlaying == false));
        yield return new WaitForSeconds(2);
        float loadingCircleToLogoDelay = 0.25f;
        bzaarLogo.transform.DOScale(0, 0.25f + loadingCircleToLogoDelay).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            loadingScreenImage.DOColor(new Color(0, 0, 0, 0), 1).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                LoadingScreenObj.SetActive(false);
            });
        });
        loadingRing.transform.DOScale(0, 0.25f).SetEase(Ease.InOutCubic).SetDelay(loadingCircleToLogoDelay);
    }
}
