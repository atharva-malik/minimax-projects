using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInputReciever : InputReciever
{
    private Vector3 clickPosition;

    private void Update() {
        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)) {
                clickPosition = hit.point;
                OnInputReceived();
            }
        }
    }

    public override void OnInputReceived()
    {
        foreach (var handler in inputHandlers){
            handler.ProcessInput(clickPosition, null, null);
        }
    }

    public void ProcessInput(Vector3 inputPosition, GameObject selectedGameObject, Action callback)
    {
        throw new NotImplementedException();
    }
}
