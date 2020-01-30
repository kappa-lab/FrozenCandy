using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIVScene : MonoBehaviour {

    public int state = 0;
    public BulletGenerator throwStage;
    public BulletGenerator touchStage;
    public Animator spotLightTerrace;
    public Animator spotLightGround;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //次シーン
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            state = (++state) % 3;
            throwStage.SetEnabled(state == 1);
            touchStage.SetEnabled(state == 2);
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            var enable = spotLightTerrace.gameObject.activeSelf;
            spotLightTerrace.gameObject.SetActive(!enable);
            return;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            var enable = spotLightGround.gameObject.activeSelf;
            spotLightGround.gameObject.SetActive(!enable);
            return;
        }

    }
}
