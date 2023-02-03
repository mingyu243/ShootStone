using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Transform _object;

    public IEnumerator Show()
    {
        yield return Move(new Vector3(0, -1, 0), new Vector3(0, 0, 0));
    }

    public IEnumerator Hide()
    {
        yield return Move(new Vector3(0, 0, 0), new Vector3(0, -1, 0));
    }

    IEnumerator Move(Vector3 startPos, Vector3 endPos)
    {
        for (float t = 0; t < 0.3f; t += Time.deltaTime)
        {
            _object.localPosition = Vector3.Lerp(startPos, endPos, t / 0.3f);
            yield return null;
        }
        _object.localPosition = endPos;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<MovedObject>(out MovedObject movedObject))
        {
            movedObject.IsDie = true;
        }
    }
}
