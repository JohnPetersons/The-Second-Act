﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class ChargingState : GameEventListenerState
{
    private Player player;
    private ChargeStatus chargeStatus;
    public ChargingState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
        this.chargeStatus = this.gameObject.GetComponent<ChargeStatus>();
    }

    public override void Begin() {
        base.Begin();
        this.chargeStatus.SetActive();
    }
    
    public override void Tick() {
        base.Tick();
        this.gameObject.transform.Translate(new Vector3(15.0f * (float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.player.GetDirection(), 0.0f, 0.0f));
    }

    public override GameState GetNextState(GameEvent gameEvent) {
        GameState result = base.GetNextState(gameEvent);
        if (gameEvent.GetName().Equals("chargeButton") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_UP)) {
            new TypedGameEvent<bool>(this.GetListenerId(), "stop", true);
        }
        if (result != this) {
            this.chargeStatus.SetInactive();
        }
        return result;
    }
}

