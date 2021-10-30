using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Sealer : MonoBehaviour
{
    public float timeCycle;
    public Vector3 pointB;
    public Vector3 pointA;
    private int goingRight;
    private IEnumerator co;
    private IEnumerator coRight;
    private IEnumerator coLeft;
    public void startMoving() {
        co = Move();
        StartCoroutine(co);
    }

    public void stopMoving() {
        if (co == null && coRight == null && coLeft == null) {
            return;
        }
        StopCoroutine(co);
        StopCoroutine(coRight);
        StopCoroutine(coLeft);
        co = null;
        coRight = null;
        coLeft = null;
    }

    IEnumerator Move()
    {
        while(true)
        {
            coRight = MoveObject(transform, pointA, pointB, timeCycle);
            coLeft = MoveObject(transform, pointB, pointA, timeCycle);
            yield return StartCoroutine(coRight);
            yield return StartCoroutine(coLeft);
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i= 0.0f;
        var rate= 1.0f/time;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
