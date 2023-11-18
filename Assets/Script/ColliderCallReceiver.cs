using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ColliderCallReceiver : MonoBehaviour
{
    public class TriggerEvent : UnityEvent<Collision2D>{};
    public class TriEvent : UnityEvent<Collider2D>{};

    public TriggerEvent TriggerEnterEvent = new TriggerEvent();
    public TriggerEvent TriggerExitEvent = new TriggerEvent();

    public TriEvent TriEnterEvent = new TriEvent();
    public TriEvent TriStayEvent = new TriEvent();
    public TriEvent TriExitEvent = new TriEvent();
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other){
        TriggerEnterEvent?.Invoke(other);
    }

    void OnCollisionExit2D(Collision2D other) {
        TriggerExitEvent?.Invoke(other);
    }

    void OnTriggerEnter2D(Collider2D other){
        TriEnterEvent?.Invoke(other);
    }
    void OnTriggerExit2D(Collider2D other){
        TriExitEvent?.Invoke(other);
    }

    void OnTriggerStay2D(Collider2D other){
        TriStayEvent?.Invoke(other);
    }
}
