using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallContactGlow : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color flashColor;
    public float animTime;
    public AnimationCurve flashAnimCurve;

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        // Debug.Log("WallHit");
        // meshRenderer.materials[0].SetColor("_BaseColor", Color.clear);
        if(meshRenderer.materials[0].GetColor("_BaseColor").a == 0)
        {
            meshRenderer.materials[0].DOColor(flashColor, "_BaseColor", animTime).SetEase(flashAnimCurve);
        }
    }
}
