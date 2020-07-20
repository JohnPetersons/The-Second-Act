using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class DashState : GameEventListenerState
{
    private Player player;
    private ChargeStatus chargeStatus;
    private double timer1, timer2, timer;
    public DashState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
        this.chargeStatus = this.gameObject.GetComponent<ChargeStatus>();
        this.timer1 = 0.125;
    }

    public override void Begin() {
        base.Begin();
        this.timer = this.timer1;
        this.chargeStatus.SetActive();
    }
    
    public override void Tick() {
        base.Tick();
        this.gameObject.transform.Translate(new Vector3(60.0f * (float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.player.GetDirection(), 0.0f, 0.0f));
        if (this.timer <= 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "stop", true);
        }
        this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
    }

    public override GameState GetNextState(GameEvent gameEvent) {
        GameState result = base.GetNextState(gameEvent);
        if (result != this) {
            this.chargeStatus.SetInactive();
        }
        return result;
    }
}
