using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButtonAction : MonoBehaviour
{
    bool push = false;

    [SerializeField] Cloud_move cloud = null;
    
    public void PushDown(){
        push = true;
    }

    public void PushUp(){
        push = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(push){
            cloud.LeftMove();
        }   
    }
}
