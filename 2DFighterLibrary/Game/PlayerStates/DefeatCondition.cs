using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class DefeatCondition : GameStateCondition
{
    public DefeatCondition(GameEventListenerId sendTo): base(sendTo, "defeat") {
        this.AddEventToListenFor("collision");
    }

    public override bool CheckCond(GameEvent gameEvent) {
        EndZone other = gameEvent.GetGameData<GameObject>().GetComponent<EndZone>();
        if (other != null) {
            new TypedGameEvent<bool>(this.sendTo.GetListenerId(), "defeat", true);
            new TypedGameEvent<bool>(Player1.TAG, "victory", true);
            new TypedGameEvent<bool>(Player2.TAG, "victory", true);
        }
        return false;
    }
}
