using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIVScene : MonoBehaviour {

    public int state = 0;
    public BulletGenerator throwStage;
    public BulletGenerator touchStage;
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

    }
}
