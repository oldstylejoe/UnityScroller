//Joe Snider
//6/29/2020
//
//Control the height of something within a rect. 
//Mostly intended as a test.
//Assumes the object is a child of the rect (coordinates).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class HeightFromRect : MonoBehaviour
{
    public RectTransform referenceRect;
    private RectTransform localRect;

    [Header("Pad the bounding box in height")]
    public float objectHeight = 100.0f;

    void Start()
    {
        localRect = GetComponent<RectTransform>();
    }
    private void Awake()
    {
        localRect = GetComponent<RectTransform>();
    }

    public float test;

    /// <summary>
    /// Put the height of the object to the top bottom of a rect.
    /// In update for testing, but could easily be a callback from value changed (or something slower).
    /// Probably not real processor intense though.
    /// </summary>
    void Update()
    {
        float xLocal = localRect.position.x;
        float xReference = referenceRect.position.x;

        //The math gives coordinate for the unique circle from the triangle formed
        //  by the upper middle, and lower left/right of the bounding box (minus the objectHeight fudge factor). 
        float x2 = Mathf.Pow(0.5f * referenceRect.rect.width, 2);
        float y2 = Mathf.Pow(referenceRect.rect.height - objectHeight, 2);
        float r2 = 0.25f * (x2 + y2)*(1.0f + x2/y2);

        var x = xLocal - xReference;

        var pos = localRect.localPosition;
        if (r2 > x * x)
        {
            pos.y = Mathf.Sqrt(r2 - x * x) - referenceRect.rect.position.y - Mathf.Sqrt(r2);
        }
        localRect.localPosition = pos;
        localRect.localRotation = Quaternion.Euler(0.0f, 0.0f, -1.0f * Mathf.Rad2Deg * Mathf.Atan2(xLocal - xReference, pos.y + Mathf.Sqrt(r2)));
    }
}
