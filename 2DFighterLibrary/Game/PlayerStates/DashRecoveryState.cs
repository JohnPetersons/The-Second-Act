using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class DashRecoveryState : GameEventListenerState
{
    private double timer, chargeInActiveTimer;
    private ChargeStatus chargeStatus;
    public DashRecoveryState(GameEventListenerId listenerId): base(listenerId) {
        this.chargeStatus = this.gameObject.GetComponent<ChargeStatus>();
        this.chargeInActiveTimer = 2;
    }

    public override void Begin() {
        this.timer = Settings.dashRecoveryTimer;
    }

    public override void Tick() {
        if (this.chargeInActiveTimer == 0) {
            this.chargeStatus.SetInactive();
            this.chargeInActiveTimer -= 1;
        } else {
            this.chargeInActiveTimer -= 1;
        }
        this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
        if (timer <= 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "recover", true);
        }
    }
}
