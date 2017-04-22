using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour
{
    public int StarsMax = 500;
    public float StarSize = 0.35f;
    public float StarDistance = 50;
    public float StarClipDistance = 40;
    public Gradient GradientColor;

    private ParticleSystem _particleSystem;
    private Transform _transform;
    private ParticleSystem.Particle[] points;
    private float[][] _Zmatrix;
    private float _starClipDistanceSqr;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _transform = transform;
        _starClipDistanceSqr = StarClipDistance * StarClipDistance;
        SetZMatrix(0.001f);
        CreateStars();
    }

    private void SetZMatrix(float angle)
    {
        _Zmatrix = new float[3][];

        for (int i = 0; i < _Zmatrix.Length; i++)
            _Zmatrix[i] = new float[3];

        _Zmatrix[0][0] = Mathf.Cos(angle);
        _Zmatrix[0][1] = 0;
        _Zmatrix[0][2] = Mathf.Sin(angle);

        _Zmatrix[1][0] = 0;
        _Zmatrix[1][1] = 1;
        _Zmatrix[1][2] = 0;

        _Zmatrix[2][0] = -Mathf.Sin(angle);
        _Zmatrix[2][1] = 0;
        _Zmatrix[2][2] = Mathf.Cos(angle);
    }

    private void CreateStars()
    {
        points = new ParticleSystem.Particle[StarsMax];

        for (int i = 0; i < StarsMax; i++)
        {
            points[i].position = Random.insideUnitSphere * StarDistance + _transform.position;
            points[i].startColor = GradientColor.Evaluate(0);
            points[i].startSize = StarSize;
        }
    }

    private void Update()
    {
        UpdateStars();
        _particleSystem.SetParticles(points, points.Length);
    }

    private Vector3 ZRotatedPosition(Vector3 position)
    {
        Vector3 result = new Vector3();

        result.x = _Zmatrix[0][0] * position.x + _Zmatrix[0][1] * position.y + _Zmatrix[0][2] * position.z;
        result.y = _Zmatrix[1][0] * position.x + _Zmatrix[1][1] * position.y + _Zmatrix[1][2] * position.z;
        result.z = _Zmatrix[2][0] * position.x + _Zmatrix[2][1] * position.y + _Zmatrix[2][2] * position.z;

        return result;
    }

    private void UpdateStars()
    {
        for (int i = 0; i < StarsMax; i++)
        {
            if ((points[i].position - _transform.position).sqrMagnitude <= _starClipDistanceSqr)
            {
                var currentSqrMagnitude = (points[i].position - _transform.position).sqrMagnitude;
                float percent = currentSqrMagnitude / _starClipDistanceSqr;
                points[i].startColor = GetGradientColor(currentSqrMagnitude, percent);
                points[i].startSize = percent * StarSize;
            }

            points[i].position = ZRotatedPosition(points[i].position);
        }
    }

    private Color GetGradientColor(float currentSqrMagnitude, float percent)
    {
        Color color;

        if (GradientColor == null)
        {
            color = Color.white;
            color.a *= percent;
            return color;
        }

        var value = (currentSqrMagnitude + Time.time) * 0.01f;
        value = value - Mathf.Floor(value);
        color = GradientColor.Evaluate(value);
        color.a *= percent;

        return color;
    }
}
