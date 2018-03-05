using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private List<float> pinsDegrees;

    public float speed = 100f;

    private void Awake()
    {
        pinsDegrees = new List<float>();
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }

    public void AddPinDegrees(float degrees, out float smaller, out float greater)
    {
        pinsDegrees.Add(degrees);
        pinsDegrees.Sort();
        Vector3.
        int newIndex = pinsDegrees.IndexOf(degrees);
        //pinsDegrees[newIndex - 1];
        smaller = pinsDegrees.Count - 1 >= newIndex + 1 ? pinsDegrees[newIndex + 1] : float.NaN;
        greater = (newIndex - 1) == 0 ? pinsDegrees[newIndex - 1] : float.NaN;
    }

    public List<float> GetPinsDegrees()
    {
        return this.pinsDegrees;
    }

}
