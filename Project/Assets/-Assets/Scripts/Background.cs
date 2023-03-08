using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Background : MonoBehaviour
{
    private void Start()
    {
        Camera cam = Camera.main;

        float pos = (cam.farClipPlane - 1.0f);

        transform.position = cam.transform.position + cam.transform.forward * pos;
        transform.LookAt(cam.transform);
        float h = (Mathf.Tan(cam.fieldOfView * Mathf.Deg2Rad * 0.5f) * pos * 2f) * cam.aspect / 10.0f;
        float w = h * Screen.height / Screen.width;
        transform.localScale = new Vector3(h, w, 1);
    }
}
