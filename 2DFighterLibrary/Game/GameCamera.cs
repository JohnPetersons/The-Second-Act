using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class GameCamera : MonoBehaviour
{

    private GameObject player1, player2;
    // Start is called before the first frame update
    void Start()
    {
        this.player1 = GameSystem.GetGameData<GameObject>(Player1.TAG);
        this.player2 = GameSystem.GetGameData<GameObject>(Player2.TAG);
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = this.gameObject.GetComponent<Camera>();
        float right = this.player1.transform.position.x;
        float left = this.player2.transform.position.x;
        this.transform.Translate(new Vector3(((left + right) / 2 - this.transform.position.x), 0, 0));
        float y = camera.orthographicSize;
        float temp = Math.Abs(right - left);
        float x = Math.Max(temp / 2 - 2.5f, 5);
        camera.orthographicSize += (x - y) * (((float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * 7.5f));
    }
}
