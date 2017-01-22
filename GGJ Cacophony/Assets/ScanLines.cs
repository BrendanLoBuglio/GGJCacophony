using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class ScanLines : PostEffectsBase{

    public Shader shader;
    private Material m;

    void Start()
    {
        m = new Material(shader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m);
    }
}
