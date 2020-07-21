using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;
using System;

public class SparkEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float opacity, scale, grow;
    private float xDir, yDir;
    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.opacity = 1.0f;
        this.scale = ((float)GameSystem.GetRandom()) * 0.1f;
        this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        float x = (float)((GameSystem.GetGameData<GameObject>(Player1.TAG).transform.position.x + GameSystem.GetGameData<GameObject>(Player2.TAG).transform.position.x) / 2.0);
        float y = 0;
        this.xDir = (float)(GameSystem.GetRandom() * 8.0 - 4.0);
        this.yDir = (float)Math.Sqrt(GameSystem.GetRandom() * 64.0 - this.xDir * this.xDir);
        this.transform.position = new Vector3(x, y, -3);
        this.transform.localScale =  new Vector3(this.scale, this.scale, -3);
        this.grow = ((float)GameSystem.GetRandom()) * 0.25f;
        this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, this.opacity);
    }

    // Update is called once per frame
    void Update()
    {
        if (float.IsNaN(this.yDir)) {
            this.yDir = 0.0f;
        }
        if (float.IsNaN(this.xDir)) {
            this.xDir = 0.0f;
        }
        this.transform.Translate(new Vector3(this.xDir * (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime)), this.yDir * (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime)), 0));
        float yDir2 = this.yDir - (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * 10.0f);
        this.yDir = yDir2;
        if (this.opacity > 0.25f) {
            this.opacity -= (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime));
        }
        this.scale += (float)(GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.grow);
        this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, Math.Max(0.0f, this.opacity));
        this.transform.localScale = new Vector3(this.scale, this.scale, 1);
        if (this.transform.position.y - this.scale / 2.0f <= -0.5f) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
