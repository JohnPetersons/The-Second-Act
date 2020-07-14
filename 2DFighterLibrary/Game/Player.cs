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
        GameState Charging = new GameState();
        GameState Recovering = new GameState();
        GameState CollisionImpact = new GameState();
        GameState CollisionWin = new GameState();
        GameState CollisionLoss = new GameState();
        GameState KnockedBack = new GameState();
        GameState CollisionRecover = new GameState();
        GameState Victory = new GameState();
        GameState Defeat = new GameState();
        GameState UsingSpecial = new GameState();

        GameState SpecialAvailable = new GameState();
        GameState SpecialActivate = new GameState();
        GameState SpecialCharging = new GameState();
        GameState SpecialUsed = new GameState();

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
}
