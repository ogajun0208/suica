using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_move : MonoBehaviour
{
    Rigidbody2D rigCloud;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] ColliderCallReceiver aroundColliderCall = null;

    [SerializeField] private GameController gameCon = null;

    // Start is called before the first frame update
    void Start()
    {
        rigCloud = GetComponent<Rigidbody2D>();
        aroundColliderCall.TriExitEvent.AddListener(OnAroundTriggerExit);
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey(KeyCode.LeftArrow)){
        //     this.transform.Translate(-0.5f,0.0f,0.0f);
        // }
        // if(Input.GetKey(KeyCode.RightArrow)){
        //     this.transform.Translate(0.5f,0.0f,0.0f);
        // }

        // if(this.transform.position.x >= 663.0f) this.transform.position = new Vector3(663.0f,496.0f,0.0f);
        // if(this.transform.position.x <= 258.0f) this.transform.position = new Vector3(258.0f,496.0f,0.0f);
    }

    public void RightMove(){
        if(this.transform.position.x >= 586.0f) return;
        Vector2 dir = new Vector2(1.0f,0).normalized;
        rigCloud.velocity = dir * speed;
        
    }

    public void LeftMove(){
        if(this.transform.position.x <= 322.0f) return;
        Vector2 dir = new Vector2(-1.0f,0).normalized;
        rigCloud.velocity = dir * speed;
    }

    void OnAroundTriggerExit(Collider2D other){
        gameCon.newFruitFromCloud();
    }
}
