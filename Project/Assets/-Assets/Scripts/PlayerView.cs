using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Vector3 _offset = new Vector3(0, 10, -7);

    public IEnumerator Move(Transform _target)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = _target.position + _offset;
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
    }
}
