using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class MainMenuSprites : GameSpriteStateMachine
{

    public override void Begin() {
        base.Begin();

        GameSpriteState startMatchFocus = new GameSpriteState(this.listenerId, "MainMenu/StartMatchFocus");
        GameSpriteState startMatchSelected = new GameSpriteState(this.listenerId, "MainMenu/StartMatchSelected");
        GameSpriteState controlsFocus = new GameSpriteState(this.listenerId, "MainMenu/ControlsFocus");
        GameSpriteState controlsSelected = new GameSpriteState(this.listenerId, "MainMenu/ControlsSelected");
        GameSpriteState settingsFocus = new GameSpriteState(this.listenerId, "MainMenu/ControlsFocus");
        GameSpriteState settingsSelected = new GameSpriteState(this.listenerId, "MainMenu/ControlsSelected");
        GameSpriteState exitGameFocus = new GameSpriteState(this.listenerId, "MainMenu/ExitGameFocus");
        GameSpriteState exitGameSelected = new GameSpriteState(this.listenerId, "MainMenu/ExitGameSelected");
/*
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
      */
        startMatchFocus.AddStateChange("down", controlsFocus);
        startMatchFocus.AddStateChange("selected", startMatchSelected);
        controlsFocus.AddStateChange("up", startMatchFocus);
        controlsFocus.AddStateChange("down", exitGameFocus);
        controlsFocus.AddStateChange("selected", controlsSelected);
        controlsSelected.AddStateChange("closed", controlsFocus);
        exitGameFocus.AddStateChange("up", controlsFocus);
        exitGameFocus.AddStateChange("selected", exitGameSelected);

        this.AddCurrentState(startMatchFocus);
    }
}
