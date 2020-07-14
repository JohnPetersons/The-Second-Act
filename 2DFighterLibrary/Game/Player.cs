using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;
/*
Player prefab component list:
- GameEventListenerId
- Player1 or Player2
*/
public class Player : GameStateMachine {
    
    private GameInputState input;
    private int direction;

    public const int LEFT = -1;
    public const int RIGHT = 1;
    public override void Begin() {
        base.Begin();

        IdleState Idle = new IdleState(this.listenerId);
        MovingForwardState MovingForward = new MovingForwardState(this.listenerId);
        MovingBackwardState MovingBackward = new MovingBackwardState(this.listenerId);
        ChargingState Charging = new ChargingState(this.listenerId);
        RecoveringState Recovering = new RecoveringState(this.listenerId);
        CollisionImpactState CollisionImpact = new CollisionImpactState(this.listenerId);
        CollisionWinState CollisionWin = new CollisionWinState(this.listenerId);
        CollisionLossState CollisionLoss = new CollisionLossState(this.listenerId);
        KnockedBackState KnockedBack = new KnockedBackState(this.listenerId);
        CollisionRecoverState CollisionRecover = new CollisionRecoverState(this.listenerId);
        VictoryState Victory = new VictoryState(this.listenerId);
        DefeatState Defeat = new DefeatState(this.listenerId);
        UsingSpecialState UsingSpecial = new UsingSpecialState(this.listenerId);

        SpecialAvailableState SpecialAvailable = new SpecialAvailableState(this.listenerId);
        SpecialActivateState SpecialActivate = new SpecialActivateState(this.listenerId);
        SpecialChargingState SpecialCharging = new SpecialChargingState(this.listenerId);
        SpecialUsedState SpecialUsed = new SpecialUsedState(this.listenerId);

        Idle.AddStateChange("moveForward", MovingForward);
        Idle.AddStateChange("moveBackward", MovingBackward);
        Idle.AddStateChange("charge", Charging);
        Idle.AddStateChange("collision", CollisionImpact);
        Idle.AddStateChange("special", UsingSpecial);
        Idle.AddStateChange("victory", Victory);
        Idle.AddStateChange("defeat", Defeat);
        MovingForward.AddStateChange("idle", Idle);
        MovingForward.AddStateChange("moveBackward", MovingBackward);
        MovingForward.AddStateChange("charge", Charging);
        MovingForward.AddStateChange("collision", CollisionImpact);
        MovingForward.AddStateChange("special", UsingSpecial);
        MovingForward.AddStateChange("victory", Victory);
        MovingForward.AddStateChange("defeat", Defeat);
        MovingBackward.AddStateChange("idle", Idle);
        MovingBackward.AddStateChange("moveForward", MovingForward);
        MovingBackward.AddStateChange("charge", Charging);
        MovingBackward.AddStateChange("collision", CollisionImpact);
        MovingBackward.AddStateChange("special", UsingSpecial);
        MovingBackward.AddStateChange("victory", Victory);
        MovingBackward.AddStateChange("defeat", Defeat);
        Charging.AddStateChange("stop", Recovering);
        Charging.AddStateChange("collision", CollisionImpact);
        Charging.AddStateChange("victory", Victory);
        Charging.AddStateChange("defeat", Defeat);
        Recovering.AddStateChange("recover", Idle);
        Recovering.AddStateChange("victory", Victory);
        Recovering.AddStateChange("defeat", Defeat);
        CollisionImpact.AddStateChange("winCollision", CollisionWin);
        CollisionImpact.AddStateChange("loseCollision", CollisionLoss);
        CollisionWin.AddStateChange("knockBack", KnockedBack);
        CollisionLoss.AddStateChange("knockBack", KnockedBack);
        KnockedBack.AddStateChange("stop", CollisionRecover);
        CollisionRecover.AddStateChange("recover", Idle);
        UsingSpecial.AddStateChange("charge", Charging);

        SpecialAvailable.AddStateChange("special", SpecialActivate);
        SpecialActivate.AddStateChange("charge", SpecialCharging);
        SpecialCharging.AddStateChange("stop", SpecialUsed);
        SpecialCharging.AddStateChange("collision", SpecialUsed);

        this.input = new GameInputState(this.listenerId, 0);
        this.input.SetInputMapping(GameInputState.LEFT_STICK_LEFT_RIGHT, "moveStick");
        this.input.SetInputMapping(GameInputState.A, "chargeButton");
        this.input.SetInputMapping(GameInputState.B, "specialButton");

        this.AddCurrentState(this.input);
    }

    public override void Tick() {
        base.Tick();
    }

    public void SetPlayerNumber(int i) {
        this.input.SetPlayerNumber(i);
    }

    public void SetDirection(int i) {
        this.direction = i;
    }

    public int GetDirection() {
        return this.direction;
    }

    public override void HandleGameEvent(GameEvent gameEvent) {
        base.HandleGameEvent(gameEvent);
        if (gameEvent.GetName().Equals("moveStick")) {
            double val = gameEvent.GetGameData<double>() * this.direction;
            if (val > 0) {
                new TypedGameEvent<bool>(this.GetListenerId(), "moveForward", true);
            } else if (val < 0) {
                new TypedGameEvent<bool>(this.GetListenerId(), "moveBackward", true);
            }
        } else if (gameEvent.GetName().Equals("chargeButton") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
            new TypedGameEvent<bool>(this.GetListenerId(), "charge", true);
        } else if (gameEvent.GetName().Equals("specialButton") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
            new TypedGameEvent<bool>(this.GetListenerId(), "special", true);
        }
    }
}
