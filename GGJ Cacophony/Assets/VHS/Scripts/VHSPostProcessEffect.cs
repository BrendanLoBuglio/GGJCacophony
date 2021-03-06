﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent (typeof(Camera))]
public class VHSPostProcessEffect : PostEffectsBase {
	Material m;
	public Shader shader;
	public MovieTexture VHS;

	float yScanline, xScanline;

	public void Start() {
		m = new Material(shader);
		m.SetTexture("_VHSTex", VHS);
		VHS.loop = true;
		VHS.Play();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination){
		yScanline += Time.deltaTime * 0.1f;
		xScanline -= Time.deltaTime * 0.1f;

		if(yScanline >= 1){
			yScanline = 0;
		}
		if(xScanline <= 0){
            xScanline = 1;
		}
		m.SetFloat("_yScanline", yScanline);
		m.SetFloat("_xScanline", xScanline);
		Graphics.Blit(source, destination, m);
	}
}