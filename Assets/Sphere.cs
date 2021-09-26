using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{

    public int points = 100;
    public int radius = 10;
    public Color startColor, endColor;

    public float speed = 1f;
    public float size = 0.2f;

    List<AnimationVector> vectors = new List<AnimationVector>();

    float psi = (1 + Mathf.Sqrt(0.5f)) / 2;

    void OnValidate()
    {
        points = Mathf.Max(0, points);
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        if (vectors.Count > points)
        {
            int dif = vectors.Count - points;
            vectors.RemoveRange(points, dif);
        }

        transform.Rotate(0.1f * speed, 0f, 0.2f * speed, Space.Self);

        for (int i = 0; i < points; i++)
        {
            Vector3 targetPoint = PointAtIndex(i);

            if (i >= vectors.Count)
            {
                vectors.Add(new AnimationVector(Origin(), targetPoint));
            }
            else
            {
                vectors[i].target = targetPoint;
            }

            vectors[i].Animate(speed);
        }
    }

    Vector3 PointAtIndex(int index)
    {
        float j = index + 0.5f;
        float phi = Mathf.Acos(1 - 2 * j / points);
        float theta = 2 * Mathf.PI * (j / psi);

        float x = Mathf.Cos(theta) * Mathf.Sin(phi);
        float y = Mathf.Sin(theta) * Mathf.Sin(phi);
        float z = Mathf.Cos(phi);

        Vector3 newPoint = new Vector3(x, y, z);

        return newPoint;
    }

    Vector3 Origin()
    {
        return transform.position;
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < vectors.Count; i++)
        {
            float t = Mathf.InverseLerp(0, vectors.Count, i);
            Gizmos.color = Color.Lerp(startColor, endColor, t);

            Vector3 targetPos = transform.TransformPoint(vectors[i].point) * radius;
            Gizmos.DrawSphere(targetPos, size);
        }
    }
}
