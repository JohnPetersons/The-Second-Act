using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class MovingForwardState : GameEventListenerState
{
    private Player player;
    public MovingForwardState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
    }

    public override void Begin() {
        base.Begin();
    }
    
    public override void Tick() {
        base.Tick();
        this.gameObject.transform.Translate(new Vector3(5.0f * (float)GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime) * this.player.GetDirection(), 0.0f, 0.0f));
    }

    public override GameState GetNextState(GameEvent gameEvent) {
        GameState result = base.GetNextState(gameEvent);
        if (gameEvent.GetName().Equals("moveStick") && gameEvent.GetGameData<double>() == 0) {
            new TypedGameEvent<bool>(this.GetListenerId(), "stop", true);
        }
        return result;
    }
}
