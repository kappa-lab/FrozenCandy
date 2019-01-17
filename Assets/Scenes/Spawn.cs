using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Spawn : MonoBehaviour {
    public Marker from;
    public Marker to;

    public delegate void OnArrived(Spawn spawn, Marker marker);

    public event OnArrived onArrived;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RegisterMarker(Marker from, Marker to) { 
        this.from = from;
        this.to = to;

        var d = Vector3.Distance(from.transform.position, to.transform.position);

        transform.position = from.transform.position;
        transform.DOMove(to.transform.position, d * 0.1f)
            .SetEase(Ease.Linear)
            .OnComplete(Next);
    }

    private void Next()
    {
        if(onArrived != null) onArrived.Invoke(this, to);
    }
}
