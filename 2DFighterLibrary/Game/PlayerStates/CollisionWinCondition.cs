using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class CollisionWinCondition : GameStateCondition
{
    public CollisionWinCondition(GameEventListenerId sendTo): base(sendTo, "collisionWin") {
        this.AddEventToListenFor("collision");
    }

    public override bool CheckCond(GameEvent gameEvent) {
        ChargeStatus me = this.gameObject.GetComponent<ChargeStatus>();
        ChargeStatus other = gameEvent.GetGameData<GameObject>().GetComponent<ChargeStatus>();
        if (me != null && other != null) {
            if (other.GetTurnNumber() >= me.GetTurnNumber()) {
                return true;
            }
        }
        return false;
    }
}
