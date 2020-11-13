using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFade : MonoBehaviour
{

    private float maxAlpha = 1;
    private float minAlpha = 0;
    private float changeAmount = .05f;

    private EdgeLineValues elv;
    private bool increasingAlpha = true;
    private Renderer mat;

    // Start is called before the first frame update
    void Start()
    {
        elv = GetComponentInParent<EdgeLineValues>();
        maxAlpha = elv.maxAlpha;
        minAlpha = elv.minAlpha;
        changeAmount = elv.changeAmount;
        mat = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ToggleAlpha();
    }

    private void ToggleAlpha()
    {

        if (increasingAlpha)
        {
            Color color = mat.material.color;
            color.a += Time.deltaTime*changeAmount;
            if (color.a > maxAlpha)
            {
                color.a = maxAlpha;
                increasingAlpha = false;
            }
            mat.material.color = color;

        }
        else
        {
            Color color = mat.material.color;
            color.a -= Time.deltaTime * changeAmount;
            if (color.a < minAlpha)
            {
                color.a = minAlpha;
                increasingAlpha = true;
            }
            mat.material.color = color;

        }
    }
}
