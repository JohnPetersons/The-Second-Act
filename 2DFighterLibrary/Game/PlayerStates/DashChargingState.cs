using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class DashChargingState : GameEventListenerState
{
    private Player player;
    private double timer1, timer;
    public DashChargingState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
        this.timer1 = Settings.dashChargeTimer;
    }

    public override void Begin() {
        base.Begin();
        this.timer = this.timer1;
    }
    
    public override void Tick() {
        base.Tick();
        if (this.timer <= 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "dash", true);
        }
        this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
    }
}
