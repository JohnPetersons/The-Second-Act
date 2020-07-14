using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class MainMenu : GameStateMachine {
    
    private double focusChangeTimerDefault, focusChangeTimer;

    public override void Begin() {
        base.Begin();
        this.gameObject.GetComponent<GameEventListenerId>().SetListenerId("mainMenu");
        GameSystem.SetTimeMultiplier("mainMenu", 1.0);

        // TODO: Change these GameState objects to their specific GameState child classes
        StartMatchFocusState startMatchFocus = new StartMatchFocusState();
        StartMatchSelectedState startMatchSelected = new StartMatchSelectedState();
        ControlsFocusState controlsFocus = new ControlsFocusState();
        ControlsSelectedState controlsSelected = new ControlsSelectedState();
        ExitGameFocusState exitGameFocus = new ExitGameFocusState();
        ExitGameSelectedState exitGameSelected = new ExitGameSelectedState();

        startMatchFocus.AddStateChange("down", controlsFocus);
        startMatchFocus.AddStateChange("selected", startMatchSelected);
        controlsFocus.AddStateChange("up", startMatchFocus);
        controlsFocus.AddStateChange("down", exitGameFocus);
        controlsFocus.AddStateChange("selected", controlsSelected);
        controlsSelected.AddStateChange("selected", controlsFocus);
        exitGameFocus.AddStateChange("up", controlsFocus);
        exitGameFocus.AddStateChange("selected", exitGameSelected);

        GameInputState input = new GameInputState("mainMenu", 1);
        input.SetInputMapping("leftStickUpDown1", "leftStick");
        input.SetInputMapping("a1", "select");

        this.focusChangeTimer = this.focusChangeTimerDefault = 0.2;
        this.AddCurrentState(startMatchFocus);
        this.AddCurrentState(input);
    }

    public override void Tick() {
        base.Tick();
    }

    public override void HandleGameEvent(GameEvent gameEvent) {
        base.HandleGameEvent(gameEvent);
        if (gameEvent.GetName().Equals("select") && gameEvent.GetGameData<string>().Equals(GameInputState.START)) {
            new TypedGameEvent<bool>(this.GetListenerId(), "selected", true);
        } else if (this.focusChangeTimer <= 0) {
            if (gameEvent.GetName().Equals("leftStick")) {
                if (gameEvent.GetGameData<double>() == -1) {
                    this.focusChangeTimer = this.focusChangeTimerDefault;
                    new TypedGameEvent<bool>(this.GetListenerId(), "up", true);
                } else if (gameEvent.GetGameData<double>() == 1) {
                    this.focusChangeTimer = this.focusChangeTimerDefault;
                    new TypedGameEvent<bool>(this.GetListenerId(), "down", true);
                }
            }
        } else {
            this.focusChangeTimer -= GameSystem.GetDeltaTime("mainMenu", Time.deltaTime);
        }
    }
}
