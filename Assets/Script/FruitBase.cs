using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FruitBase : MonoBehaviour
{   
    //コライダーの設定
    [SerializeField] ColliderCallReceiver aroundColliderCall = null;
    //生成プレハブの容易
    [SerializeField] GameObject FruitPrefab = null;
    GameController gameCon;
    Button liftOffButton;
    public UnityEvent GameOverEvent = new UnityEvent();
    public UnityEvent<GameObject> FruitsDestroyEvent = new UnityEvent<GameObject>();
    public string FruitsTag;
    //ゲームオーバー処理
    bool isInArea = false;
    float timeInArea = 0f;
    float requiredTime = 30f;

    // Start is called before the first frame update
    void Start()
    {   
        gameCon = GameObject.Find("Canvas").GetComponent<GameController>();
        liftOffButton = GameObject.Find("LiftOffButton").GetComponent<Button>();
        aroundColliderCall.TriggerEnterEvent.AddListener(OnAroundTriggerEnter);
        aroundColliderCall.TriStayEvent.AddListener(OnARoundTriStay);
        //aroundColliderCall.TriEnterEvent.AddListener(OnAroundTriEnter);
        aroundColliderCall.TriExitEvent.AddListener(OnAroundTriExit);
        liftOffButton.onClick.AddListener(liftoff);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    void OnAroundTriggerEnter(Collision2D other){
        if(other.gameObject.tag == this.gameObject.tag){
            gameCon.BoolFruit(this.transform,this.gameObject.tag);
            FruitsDestroyEvent?.Invoke(this.gameObject);
            //Destroy(this.gameObject);
        }
    }
   //ゲームオーバー時の処理(箱から飛び出たとき)
    // void OnAroundTriEnter(Collider2D other){
    //     Debug.Log("AA");
    //     if(other.gameObject.tag != "boader" || this.gameObject.tag == "Fruits") return;
    //         isInArea = true;
            
    //         StartCoroutine(CheckTimeInArea());
        
    // }

    void OnARoundTriStay(Collider2D other){

        if(other.gameObject.tag != "boader" || this.gameObject.tag == "Fruits") return;
            isInArea = true;
            
            StartCoroutine(CheckTimeInArea());
    }


    void OnAroundTriExit(Collider2D other){
        isInArea = false;
        timeInArea = 0f;
        StopCoroutine(CheckTimeInArea());
    }

    IEnumerator CheckTimeInArea(){
        while(isInArea){
            yield return new WaitForSeconds(1f);
            timeInArea += 1f;

            if(timeInArea >= requiredTime){
                GameOverEvent?.Invoke();
                timeInArea = 0f;
                
                yield break;
            }
        }
    }

    void liftoff(){
        liftOffButton.onClick.RemoveAllListeners();
        this.gameObject.transform.parent = null;
        if(this.GetComponent<Rigidbody2D>() != null)this.GetComponent<Rigidbody2D>().simulated = true;
        this.gameObject.tag = FruitsTag;
        //StartCoroutine(PauseFunction());
    }


    // IEnumerator PauseFunction(){
    //     yield return new WaitForSeconds(0.1f);
    //     gameCon.newFruitFromCloud();
    //     yield break;
    // }
}
