using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class PlayerPlayingState: GameEventListenerState
{
    private Player player;
    public PlayerPlayingState(GameEventListenerId listenerId): base(listenerId) {
        this.player = this.gameObject.GetComponent<Player>();
    }

    public override void Begin() {
        base.Begin();
    }

    public override GameState GetNextState(GameEvent gameEvent) {
        GameState result = base.GetNextState(gameEvent);
        if (gameEvent.GetName().Equals("moveStick")) {
            double val = gameEvent.GetGameData<double>() * this.player.GetDirection();
            if (val > 0) {
                new TypedGameEvent<bool>(this.GetListenerId(), "moveForward", true);
            } else if (val < 0) {
                new TypedGameEvent<bool>(this.GetListenerId(), "moveBackward", true);
            }
        } else if (gameEvent.GetName().Equals("chargeButton") && (gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN) ||
            gameEvent.GetGameData<string>().Equals(GameInputState.KEY_HELD))) {
            new TypedGameEvent<bool>(this.GetListenerId(), "charge", true);
        } else if (gameEvent.GetName().Equals("quickStepButton") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
            new TypedGameEvent<bool>(this.GetListenerId(), "quickStep", true);
        } else if (gameEvent.GetName().Equals("dashButton") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
            new TypedGameEvent<bool>(this.GetListenerId(), "dashCharge", true);
        } else if (gameEvent.GetName().Equals("feintButton") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
            new TypedGameEvent<bool>(this.GetListenerId(), "feint", true);
        } else if (gameEvent.GetName().Equals("triggerEnter")) {
            new TypedGameEvent<GameObject>(this.GetListenerId(), "collision", gameEvent.GetGameData<GameObject>());
        } else if (gameEvent.GetName().Equals("start")) {
            GameSystem.SetTimeMultiplier(GameSystem.GAMEPLAY, 0);
            new TypedGameEvent<bool>(Player1.TAG, "stop", true);
            new TypedGameEvent<bool>(Player1.TAG, "pause", true);
            new TypedGameEvent<bool>(Player2.TAG, "stop", true);
            new TypedGameEvent<bool>(Player2.TAG, "pause", true);
        }
        return result;
    }
}
