using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class CollisionLossState : GameEventListenerState
{

    private double timer;
    private Player player;
    public CollisionLossState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
    }

    public override void Begin() {
        base.Begin();
        Debug.Log("Collision loss");
        this.timer = 2.0;
    }

    public override void Tick() {
        base.Tick();
        this.gameObject.transform.Translate(new Vector3(-15.0f * (float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.player.GetDirection(), 0.0f, 0.0f));
        this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
        if (this.timer < 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "recover", true);
        }
    }
}