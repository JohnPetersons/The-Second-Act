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
    
    public const string TAG = "mainMenu";

    public override void Begin() {
        base.Begin();
        this.SetListenerId(MainMenu.TAG);
        this.ListenTo(GameMaster.TAG);
        GameSystem.SetTimeMultiplier(MainMenu.TAG, 1.0);

        StartMatchFocusState startMatchFocus = new StartMatchFocusState();
        StartMatchSelectedState startMatchSelected = new StartMatchSelectedState();

        // TODO
        SettingsFocusState settingsFocus = new SettingsFocusState();
        SettingsSelectedState settingsSelected = new SettingsSelectedState();

        ControlsFocusState controlsFocus = new ControlsFocusState();
        ControlsSelectedState controlsSelected = new ControlsSelectedState();
        ExitGameFocusState exitGameFocus = new ExitGameFocusState();
        ExitGameSelectedState exitGameSelected = new ExitGameSelectedState();

        GameState waitingForLoadedState = new GameState();
        MainMenuInputState mainMenuInput = new MainMenuInputState(this.listenerId);
        GameState subMenu = new GameState();


        startMatchFocus.AddStateChange("down", controlsFocus);
        startMatchFocus.AddStateChange("selected", startMatchSelected);
        controlsFocus.AddStateChange("up", startMatchFocus);
        controlsFocus.AddStateChange("down", settingsFocus);
        controlsFocus.AddStateChange("selected", controlsSelected);
        controlsSelected.AddStateChange("closed", controlsFocus);
        settingsFocus.AddStateChange("up", controlsFocus);
        settingsFocus.AddStateChange("down", exitGameFocus);
        settingsFocus.AddStateChange("selected", settingsSelected);
        settingsSelected.AddStateChange("closed", settingsFocus);
        exitGameFocus.AddStateChange("up", settingsFocus);
        exitGameFocus.AddStateChange("selected", exitGameSelected);

/*
        startMatchFocus.AddStateChange("down", controlsFocus);
        startMatchFocus.AddStateChange("selected", startMatchSelected);
        controlsFocus.AddStateChange("up", startMatchFocus);
        controlsFocus.AddStateChange("down", exitGameFocus);
        controlsFocus.AddStateChange("selected", controlsSelected);
        controlsSelected.AddStateChange("closed", controlsFocus);
        exitGameFocus.AddStateChange("up", controlsFocus);
        exitGameFocus.AddStateChange("selected", exitGameSelected);
*/
        waitingForLoadedState.AddStateChange("loaded", mainMenuInput);
        mainMenuInput.AddStateChange("subMenu", subMenu);
        subMenu.AddStateChange("closed", mainMenuInput);

        GameInputState input = new GameInputState(this.listenerId, 1);
        input.SetInputMapping(GameInputState.LEFT_STICK_UP_DOWN, "leftStick");
        input.SetInputMapping(GameInputState.D_PAD_UP_DOWN, "leftStick");
        input.SetInputMapping(GameInputState.A, "select");

        this.AddCurrentState(startMatchFocus);
        this.AddCurrentState(input);
        this.AddCurrentState(waitingForLoadedState);
    }

    public override void Tick() {
        base.Tick();
    }

    public override void HandleGameEvent(GameEvent gameEvent) {
        base.HandleGameEvent(gameEvent);
        if (gameEvent.GetName().Equals("closed")) {
            GameSystem.GetGameData<GameLoader>("MenuLoader").RemoveLoaded();
        }
    }
}
