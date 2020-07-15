using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class PlayerReadyState: GameEventListenerState
{
    private double timer;
    private int check;
    public PlayerReadyState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        this.timer = 2.99999;
        this.check = 3;
        GameSystem.SetTimeMultiplier(GameSystem.GAMEPLAY, 0.25);
    }

    public override void Tick() {
        if (this.check > this.timer) {
            Debug.Log((int)this.timer + 1);
            this.check -= 1;
        }
        this.timer -= GameSystem.GetDeltaTime(Time.deltaTime);
        if (this.timer <= 0) {
            GameSystem.SetTimeMultiplier(GameSystem.GAMEPLAY, 1.0);
            new TypedGameEvent<bool>(this.GetListenerId(), "play", true);
            Debug.Log("GO");
        }
    }
}