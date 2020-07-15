using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class PlayerPausedState: GameEventListenerState
{
    public PlayerPausedState(GameEventListenerId listenerId): base(listenerId) {

    }

    public override void Begin() {
        base.Begin();
    }

    public override GameState GetNextState(GameEvent gameEvent) {
        GameState result = base.GetNextState(gameEvent);
        if (gameEvent.GetName().Equals("chargeButton") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
            GameSystem.SetTimeMultiplier(GameSystem.GAMEPLAY, 1.0);
            new TypedGameEvent<bool>(Player1.TAG, "play", true);
            new TypedGameEvent<bool>(Player2.TAG, "play", true);
        }
        return result;
    }
}
