using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

public class SpecialActivateCondition : GameStateCondition
{
    private bool specialActivated;
    public SpecialActivateCondition(GameEventListenerId sendTo): base(sendTo, "specialActivate") {
        this.specialActivated = false;
        this.AddEventToListenFor("special");
    }

    public override bool CheckCond(GameEvent gameEvent) {
        if (!this.specialActivated && gameEvent.GetName().Equals("special")) {
            this.specialActivated = true;
            return true;
        }
        return false;
    }
}
