using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ParticleManager : MonoBehaviour {

    public Spawn prefab;
    public List<Spawn> spawns= new List<Spawn>();
    public List<Marker> initialMarkers = new List<Marker>();


    public void Create(Marker from, Marker to)
    {
        var s = Instantiate<Spawn>(prefab,transform);
        s.onArrived += OnArrived;
        s.RegisterMarker(from, to );
        spawns.Add(s);
    }

    private void Start()
    {
        foreach (var m in initialMarkers)
        {
            foreach (var n in m.next)
            {
                Create(m, n);
            }
        }
    }

    private void OnArrived(Spawn sp, Marker marker) {

        if (marker.next.Length==1)
        {
            sp.RegisterMarker(marker, marker.next[0]);
            return;
        }
        else if (marker.next.Length == 0)
        {
            var initm = initialMarkers.OrderBy(i => Guid.NewGuid()).FirstOrDefault();

            sp.RegisterMarker(initm, initm.next.OrderBy(i => Guid.NewGuid()).FirstOrDefault());
            return;
        }

        var shuffle = sp.to.next.OrderBy(i => Guid.NewGuid()).ToList();

        sp.RegisterMarker(sp.to, shuffle[0]);
        shuffle.RemoveAt(0);
        foreach (var m in shuffle)
        {
            Debug.Log(m.transform.position);
            if (spawns.Count < 12) {
                Create(marker, m);
            }
        }

    }
}
