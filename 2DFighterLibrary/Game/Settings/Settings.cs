using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class Settings: GameStateMachine
{
    private double focusChangeTimerDefault, focusChangeTimer;
    public static float gameSpeed = 1.0f;
    public static float moveForward = 5.0f;
    public static float moveBackward = -2.5f;
    public static float quickStepForward = 30.0f;
    public static float quickStepForwardTimer1 = 0.2f;
    public static float quickStepForwardTimer2 = 0.8f;
    public static float quickStepBackward = -15.0f;
    public static float quickStepBackwardTimer1 = 0.2f;
    public static float quickStepBackwardTimer2 = 0.8f;
    public static float charge = 15.0f;
    public static float dashChargeTimer = 0.5f;
    public static float dash = 45.0f;
    public static float dashTimer = 0.2f;
    public static float chargeRecoveryTimer = 0.5f;
    public static float collisionLoss = -10.0f;
    public static float collisionWin = -2.5f;

    public override void Begin(){
        base.Begin();
        this.focusChangeTimer = this.focusChangeTimerDefault = 0.2;

        SettingsInputState settingsInput = new SettingsInputState(this.listenerId);
        
        GameInputState input = new GameInputState(this.listenerId, 1);
        input.SetInputMapping(GameInputState.LEFT_STICK_UP_DOWN, "leftStickUpDown");
        input.SetInputMapping(GameInputState.LEFT_STICK_LEFT_RIGHT, "leftStickLeftRight");
        input.SetInputMapping(GameInputState.A, "select");
        input.SetInputMapping(GameInputState.B, "close");

        this.AddCurrentState(settingsInput);
        this.AddCurrentState(input);
    }

    public override void Tick(){
        base.Tick();
    }
}
