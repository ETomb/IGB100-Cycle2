using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IRaycastEventHandler : IEventSystemHandler {

    // Functions to be called with the messsaging system
    void OnRaycastEnter();
    void OnRaycastExit();
}
