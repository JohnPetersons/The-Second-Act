using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;
using System;

public class CollisionEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float opacity, scale;
    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.opacity = 1.0f;
        this.scale = 0.0f;
        this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        float x = (float)((GameSystem.GetGameData<GameObject>(Player1.TAG).transform.position.x + GameSystem.GetGameData<GameObject>(Player2.TAG).transform.position.x) / 2.0);
        float y = -0.5f;
        this.transform.position = new Vector3(x, y, -3);
        this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, this.opacity);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0.0f, (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime)), 0.0f));
        this.opacity -= (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime));
        this.scale += (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * 2.0);
        this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, Math.Max(0.0f, this.opacity));
        this.transform.localScale = new Vector3(this.scale, this.scale, 1);
        if (this.opacity <= 0 || GameSystem.GetGameData<GameObject>(Player1.TAG) == null) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
