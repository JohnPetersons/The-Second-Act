using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class CollisionLossState : GameEventListenerState
{

    private double timer, timer2, timer3;
    private Player player;
    public CollisionLossState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
    }

    public override void Begin() {
        base.Begin();
        this.timer = 0.5;
        this.timer2 = 0.5;
        this.timer3 = 0.5;
    }

    public override void Tick() {
        base.Tick();
        if (this.timer > 0) {
            this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
        } else if (this.timer2 > 0) {
            this.timer2 -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
            this.gameObject.transform.Translate(new Vector3(-12.0f * (float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.player.GetDirection(), 0.0f, 0.0f));
        } else if (this.timer3 > 0) {
            this.timer3 -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
        } else {
            new TypedGameEvent<bool>(this.GetListenerId(), "recover", true);
        }
    }
}