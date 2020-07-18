using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;
/*
MainMenu prefab component list:
- GameEventListenerId
- MainMenu
- MainMenuSprites
TODO: Create the following components/things
- Main menu header
*/
public class MainMenu : GameStateMachine {
    
    private double focusChangeTimerDefault, focusChangeTimer;
    public const string TAG = "mainMenu";
    private bool loaded;

    public override void Begin() {
        base.Begin();
        this.SetListenerId(MainMenu.TAG);
        this.ListenTo(GameMaster.TAG);
        this.loaded = false;
        GameSystem.SetTimeMultiplier(MainMenu.TAG, 1.0);

        StartMatchFocusState startMatchFocus = new StartMatchFocusState();
        StartMatchSelectedState startMatchSelected = new StartMatchSelectedState();

        // TODO
        GameState multiplayerFocus = new GameState();
        GameState multiplayerSelected = new GameState();

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

        GameInputState input = new GameInputState(this.listenerId, 1);
        input.SetInputMapping(GameInputState.LEFT_STICK_UP_DOWN, "leftStick");
        input.SetInputMapping(GameInputState.A, "select");

        this.focusChangeTimer = this.focusChangeTimerDefault = 0.2;
        this.AddCurrentState(startMatchFocus);
        this.AddCurrentState(input);
    }

    public override void Tick() {
        base.Tick();
    }

    public override void HandleGameEvent(GameEvent gameEvent) {
        base.HandleGameEvent(gameEvent);
        if (this.loaded) {
            if (gameEvent.GetName().Equals("select") && gameEvent.GetGameData<string>().Equals(GameInputState.KEY_DOWN)) {
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
                this.focusChangeTimer -= GameSystem.GetDeltaTime(MainMenu.TAG, Time.deltaTime);
            }
        } else if (gameEvent.GetName().Equals("loaded")) {
            this.loaded = true;
        }
        
    }
}
