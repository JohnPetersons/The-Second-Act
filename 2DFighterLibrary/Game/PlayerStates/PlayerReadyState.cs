using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class PlayerReadyState: GameEventListenerState
{
    private double timer;
    public PlayerReadyState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        this.timer = 2.99999;
    }

    public override void Tick() {
        Debug.Log((int)this.timer + 1);
        this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
        if (this.timer <= 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "play", true);
            Debug.Log("GO");
        }
    }
}