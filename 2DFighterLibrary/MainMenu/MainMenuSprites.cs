using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class MainMenuSprites : GameSpriteStateMachine
{

    public override void Begin() {
        base.Begin();

        GameSpriteState startMatchFocus = new GameSpriteState(this.listenerId, "StartMatchFocus");
        GameSpriteState startMatchSelected = new GameSpriteState(this.listenerId, "StartMatchSelected");
        GameSpriteState controlsFocus = new GameSpriteState(this.listenerId, "ControlsFocus");
        GameSpriteState controlsSelected = new GameSpriteState(this.listenerId, "ControlsSelected");
        GameSpriteState exitGameFocus = new GameSpriteState(this.listenerId, "ExitGameFocus");
        GameSpriteState exitGameSelected = new GameSpriteState(this.listenerId, "ExitGameSelected");

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
