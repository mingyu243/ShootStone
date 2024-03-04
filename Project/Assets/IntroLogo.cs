using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLogo : MonoBehaviour
{
    [Serializable]
    public class Data
    {
        public GameObject obj;
        public Vector2 start = new Vector2(0.5f, 0.1f);
        public Vector2 end = new Vector2(0.2f, 0.5f);
        public float time = 1f;
    }

    [SerializeField] AnimationCurve _curve;

    [SerializeField] List<Data> datas = new List<Data>();

    IEnumerator Start()
    {
        while(true)
        {
            for (int i = 0; i < datas.Count; i++)
            {
                datas[i].obj.SetActive(false);
            }

            yield return new WaitForSeconds(2);

            for (int i = 0; i < 5; i++)
            {
                Data data = datas[i];
                data.obj.SetActive(true);
                StartCoroutine(DoLerp(data.obj.GetComponent<RectTransform>(), data.start, data.end, data.time));
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator DoLerp(RectTransform rect, Vector2 start, Vector2 end, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            Vector2 value = Vector2.Lerp(start, end, _curve.Evaluate(i / time));

            rect.anchorMin = value;
            rect.anchorMax = value;
            yield return null;
        }
    }

    void Update()
    {

    }
}
