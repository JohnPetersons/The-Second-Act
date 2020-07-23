using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;
using System.IO;

public class SettingsMenu: GameStateMachine
{
    private double focusChangeTimerDefault, focusChangeTimer;

    public override void Begin(){
        base.Begin();

        this.focusChangeTimer = this.focusChangeTimerDefault = 0.2;

        SettingsMenuInputState settingsInput = new SettingsMenuInputState(this.listenerId);
        
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
