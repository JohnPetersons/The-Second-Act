using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class DefeatState : GameEventListenerState
{

    private double timer;
    public DefeatState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        this.timer = 2.99999;
        GameSystem.SetTimeMultiplier(GameSystem.GAMEPLAY, 0.1);
    }

    public override void Tick() {
        if (this.timer > 0) {
            this.timer -= GameSystem.GetDeltaTime(Time.deltaTime);
            if (this.timer <= 0) {
                new TypedGameEvent<string>(LoadScreen.TAG, "fadeOutGame", "mainMenu");
            }
        }
    }
}