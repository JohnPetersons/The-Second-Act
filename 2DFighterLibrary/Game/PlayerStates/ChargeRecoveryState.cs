using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ChargeRecoveryState : GameEventListenerState
{
    private double timer, chargeInActiveTimer;
    private ChargeStatus chargeStatus;
    public ChargeRecoveryState(GameEventListenerId listenerId): base(listenerId) {
        this.chargeStatus = this.gameObject.GetComponent<ChargeStatus>();
        this.chargeInActiveTimer = 2;
    }

    public override void Begin() {
        this.timer = GameSystem.GetGameData<Settings>("Settings").GetSetting("chargeRecoveryTimer");
        this.chargeStatus.SetInactive();
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
