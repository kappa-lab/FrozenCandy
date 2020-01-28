using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BulletGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Range {
        public int min, max;
        public Range(int min, int max) {
            this.min = min;
            this.max = max;
        }
    }
    public bool iginitionOnCreate = false;
    public Bullet bullet;
    public float charge = 0;
    public Terrain wall;
    public int numCreation = 20;
    public long numBullet=0;
    public Range xRange = new Range(-9, 9);
    public Range yRange = new Range(4, 8);
    public Range zRange = new Range(-3, 3);

    void OnEnable()    
    {
        Generate();
        StartCharge();
    }

    void StartCharge() {
        if (!enabled) return;
        var chargeTime = Random.Range(.16f, .24f);
        DOTween
            .To(() => charge, (x) => charge = x, 1f, chargeTime)
            .OnComplete(OnCharged);
    }

    void OnCharged() {
        Generate();
        charge = 0;
        StartCharge();
    }

    void Generate()
    {
        var q = new Quaternion();
        for (int i = 0; i < numCreation; i++)
        {
            var p = new Vector3(
                Random.Range(xRange.min, xRange.max),
                Random.Range(yRange.min, yRange.max),
                Random.Range(zRange.min, zRange.max)
            );

            var b = Instantiate(bullet, this.transform,false) as Bullet;
            b.transform.localPosition = p;
            b.wall = wall;
            b.Id = numBullet++;

            if (iginitionOnCreate) b.Ignition();
            
        }
    }

    public void SetEnabled(bool value) {
        enabled = value;
        transform.parent.gameObject.SetActive(value);
        if (value) {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
