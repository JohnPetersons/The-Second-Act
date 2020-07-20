using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;
using System;

public class DustEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float opacity, scale;
    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.opacity = 0.25f;
        this.scale = 0.0f;
        this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        float y = -0.5f;
        this.transform.position = new Vector3(this.transform.position.x, y, -3);
        this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, this.opacity);
    }

    // Update is called once per frame
    void Update()
    {
        this.opacity -= (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) / 10.0f);
        this.scale += (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime)) * 2.0f;
        this.transform.Translate(new Vector3(0.0f, (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime)), 0.0f));
        this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, Math.Max(0.0f, this.opacity));
        this.transform.localScale = new Vector3(this.scale, this.scale, 1);
        if (this.opacity <= 0) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
