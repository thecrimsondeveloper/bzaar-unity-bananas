using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Bzaar
{
    public class PhotoCapture : MonoBehaviour
    {
        [SerializeField] PreviewManager previewManager;

        const int PREVIEW_RESOLUTION = 100;
        const int RENDER_TEXTURE_DEPTH = 24;

        private Texture2D screenCapture;
        private Camera PreviewCamera => GameObject.FindGameObjectWithTag("Preview Camera")?.GetComponent<Camera>();


        [HideInInspector] public bool CurrentlyCapturing = false;
        private void Awake()
        {
            PreviewCamera.targetTexture = new RenderTexture(PREVIEW_RESOLUTION, PREVIEW_RESOLUTION, RENDER_TEXTURE_DEPTH);
        }

        public void CapturePreview(GameObject obj, Image img)
        {
            StartCoroutine(CapturePreview_Coroutine(obj,img));
        }

        IEnumerator CapturePreview_Coroutine(GameObject obj, Image img)
        {
            CurrentlyCapturing = true;
            GameObject spawnedObject = Instantiate(obj,previewManager.previewObjParent.transform);

  
            yield return new WaitForEndOfFrame();

            Texture2D preview = new Texture2D(PREVIEW_RESOLUTION, PREVIEW_RESOLUTION, TextureFormat.RGB24, false);
            PreviewCamera.Render();
            RenderTexture.active = PreviewCamera.targetTexture;
            preview.ReadPixels(new Rect(0, 0, PREVIEW_RESOLUTION, PREVIEW_RESOLUTION), 0, 0);
            preview.Apply();
            Sprite returnSprite = Sprite.Create(preview, new Rect(0, 0, PREVIEW_RESOLUTION, PREVIEW_RESOLUTION), Vector2.one / 2, 100.0f);
            img.sprite = returnSprite;

            Destroy(spawnedObject);

            yield return new WaitForEndOfFrame();
            CurrentlyCapturing = false;


            
        }
    }

}

