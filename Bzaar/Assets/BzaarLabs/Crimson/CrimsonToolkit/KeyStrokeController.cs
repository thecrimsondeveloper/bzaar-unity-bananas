using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Crimson
{
    public class KeyStrokeController : MonoBehaviour
    {
        public static KeyStrokeController instance;
        [SerializeField] KeyStroke rotateStroke;
        [SerializeField] KeyStroke moveStroke;


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
        private void Update()
        {
            rotateStroke.Update();
            moveStroke.Update();
        }
    }

    [System.Serializable]
    public class KeyStroke
    {


        [SerializeField] List<KeyCode> activeKeys = new();
        public List<KeyCode> ActiveKeys => activeKeys;
        public UnityEvent onKeyStroke;
        public bool AllActiveKeysPressed => AllKeysPressed();

        public void Update()
        {
            if (AllActiveKeysPressed) onKeyStroke.Invoke();
        }
        public bool AllKeysPressed()
        {

            if (activeKeys.Count == 1)
            {
                return Input.GetKeyDown(activeKeys[0]);
            }
            for (int i = 0; i < activeKeys.Count; i++)
            {
                if (Input.GetKeyDown(activeKeys[i]) == false) continue;
                for (int x = 0; x < activeKeys.Count; x++)
                {
                    if (!Input.GetKey(activeKeys[x])) { return false; };
                }
                return true;
            }

            return false;
        }
    }
}
