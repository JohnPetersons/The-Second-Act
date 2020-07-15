using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class DefeatState : GameEventListenerState
{

    private double timer;
    private int check;
    public DefeatState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
        this.timer = 2.99999;
        this.check = 3;
        Debug.Log(this.GetListenerId() + " loses");
        GameSystem.SetTimeMultiplier(GameSystem.GAMEPLAY, 0.1);
    }

    public override void Tick() {
        if (this.check > this.timer) {
            Debug.Log("Main menu in: " + ((int)this.timer + 1));
            this.check -= 1;
        }
        this.timer -= GameSystem.GetDeltaTime(Time.deltaTime);
        if (this.timer <= 0) {
            new TypedGameEvent<bool>(GameMaster.TAG, "mainMenu", true);
        }
    }
}