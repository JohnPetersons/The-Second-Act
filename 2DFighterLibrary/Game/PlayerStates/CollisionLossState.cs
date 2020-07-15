using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class CollisionLossState : GameEventListenerState
{

    private double timer, timer2;
    private Player player;
    public CollisionLossState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
    }

    public override void Begin() {
        base.Begin();
        this.timer = 0.25;
        this.timer2 = 0.5;
    }

    public override void Tick() {
        base.Tick();
        if (this.timer2 < 0) {
            this.gameObject.transform.Translate(new Vector3(-6.0f * (float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.player.GetDirection(), 0.0f, 0.0f));
            this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
            if (this.timer < 0) {
                new TypedGameEvent<bool>(this.GetListenerId(), "recover", true);
            }
        } else {
            this.timer2 -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
        }
    }
}