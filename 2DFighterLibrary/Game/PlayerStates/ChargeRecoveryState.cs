using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ChargeRecoveryState : GameEventListenerState
{
    private double timer;
    private ChargeStatus chargeStatus;
    public ChargeRecoveryState(GameEventListenerId listenerId): base(listenerId) {
        this.chargeStatus = this.gameObject.GetComponent<ChargeStatus>();
    }

    public override void Begin() {
        this.timer = Settings.chargeRecoveryTimer;
        this.chargeStatus.SetInactive();
    }

    public override void Tick() {
        this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
        if (timer <= 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "recover", true);
        }
    }
}
