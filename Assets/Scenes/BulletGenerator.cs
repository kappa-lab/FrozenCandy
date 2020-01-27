using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BulletGenerator : MonoBehaviour
{
    public Bullet bullet;
    public float charge = 0;
    public Terrain wall;
    public long numBullet=0;
    // Use this for initialization
    void Start()
    {
        Generate();
        StartCharge();
    }

    void StartCharge() {
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
        for (int i = 0; i < 20; i++)
        {
            var p = new Vector3(
                Random.Range(-8, 8),
                Random.Range(4, 8),
                Random.Range(-3, 3)
            );

            var b = Instantiate(bullet, this.transform,false) as Bullet;
            b.transform.localPosition = p;
            b.wall = wall;
            b.Id = numBullet++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
