using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bzaar
{
    public class PreviewManager : MonoBehaviour
    {
        [SerializeField] PhotoCapture photoCapture;
        public GameObject previewObjParent;
        List<(GameObject, Image)> buttonPreviewData = new List<(GameObject, Image)> ();

        public void AddPreviewButtonInfo(GameObject prefab, Image img)
        {
            buttonPreviewData.Add((prefab,img)); 
        }

        public void SetButtonPreviews()
        {
            StartCoroutine(SetButtonPreviews_Coroutine());
        }

        IEnumerator SetButtonPreviews_Coroutine()
        {
            yield return new WaitForEndOfFrame();
            foreach ((GameObject, Image) info in buttonPreviewData)
            {
                yield return new WaitUntil(() => !photoCapture.CurrentlyCapturing);
                photoCapture.CapturePreview(info.Item1,info.Item2);
            }

            Editor.instance.SetStartEditorState();
        }


        

    }

}
