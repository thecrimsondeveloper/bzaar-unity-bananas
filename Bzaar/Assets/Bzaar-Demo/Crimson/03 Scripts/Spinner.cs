using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] Vector3 spinDir;
    [SerializeField] float spinSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinDir * Time.deltaTime * spinSpeed);
    }
}
