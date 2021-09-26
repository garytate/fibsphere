using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationVector
{

    public Vector3 point, target;

    public AnimationVector(Vector3 p, Vector3 t)
    {
        point = p;
        target = t;
    }

    public void Animate(float speed)
    {
        Vector3 dir = target - point;
        float mag = dir.magnitude;
        float targetMag = Mathf.Clamp(Time.deltaTime * speed, 0, mag);

        Vector3 endPoint = point + dir * targetMag;
        point = endPoint;
    }
}
