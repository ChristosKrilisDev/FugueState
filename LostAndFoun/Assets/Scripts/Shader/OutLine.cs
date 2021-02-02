using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLine : MonoBehaviour
{
    [SerializeField] private Material outlineMat;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRender;


    private void Start()
    {
        outlineRender = CreateOutline(outlineMat , outlineScaleFactor , outlineColor);
    }

    Renderer CreateOutline(Material outMat, float scaleFactor, Color color)
    {
        GameObject outlineObj = Instantiate(this.gameObject , transform.position , transform.rotation, transform);

        Renderer rend = outlineObj.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutLineColor", color);
        rend.material.SetFloat("_Scale",scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObj.GetComponent<OutLine>().enabled = false;
        outlineObj.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;


    }


}
