using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class UIBounce : MonoBehaviour
{
    public int direction;
    public float bounceDivisor;
    public float speedMultiplier;
    private LayoutElement layoutElement;
    private float cachedHeight;

    private void Start()
    {
        layoutElement = GetComponent<LayoutElement>();
        cachedHeight = layoutElement.flexibleHeight;
    }

    private void Update()
    {
        layoutElement.flexibleHeight = cachedHeight + (Mathf.Sin(Time.time * speedMultiplier) / bounceDivisor) * direction;
    }
}
