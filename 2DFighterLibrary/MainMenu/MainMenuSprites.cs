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
        GameSpriteState exitGameFocus = new GameSpriteState(this.listenerId, "MainMenu/ExitGameFocus");
        GameSpriteState exitGameSelected = new GameSpriteState(this.listenerId, "MainMenu/ExitGameSelected");

        startMatchFocus.AddStateChange("down", controlsFocus);
        startMatchFocus.AddStateChange("selected", startMatchSelected);
        controlsFocus.AddStateChange("up", startMatchFocus);
        controlsFocus.AddStateChange("down", exitGameFocus);
        controlsFocus.AddStateChange("selected", controlsSelected);
        controlsSelected.AddStateChange("selected", controlsFocus);
        exitGameFocus.AddStateChange("up", controlsFocus);
        exitGameFocus.AddStateChange("selected", exitGameSelected);
        
        this.AddCurrentState(startMatchFocus);
    }
}
