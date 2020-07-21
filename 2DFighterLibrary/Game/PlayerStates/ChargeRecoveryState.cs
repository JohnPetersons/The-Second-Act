using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ChargeRecoveryState : GameEventListenerState
{
    private double timer;
    public ChargeRecoveryState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        this.timer = Settings.chargeRecoveryTimer;
    }

    public override void Tick() {
        this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
        if (timer <= 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "recover", true);
        }
    }
}
