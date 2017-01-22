using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOnStart : MonoBehaviour {
    public float fadeInTime;
    CanvasGroup group;
    float timer;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        group.alpha = Mathf.Lerp(0, 1, timer / fadeInTime);
        if(timer > fadeInTime)
        {
            Destroy(this);
        }
	}
}
