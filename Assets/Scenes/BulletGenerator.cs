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

    public Bullet bullet;
    public bool iginitionOnCreate = false;
    public Terrain wall;
    public float creationStep = 0.4f;

    public Range createionRange = new Range(20, 20);
    public Range xRange = new Range(-9, 9);
    public Range yRange = new Range(4, 8);
    public Range zRange = new Range(-3, 3);

    [SerializeField, Space(15), Header("== readonly ==")
    ]private float _charge = 0;
    [SerializeField] private float _creationPhase = 0f;
    [SerializeField] private float _numCreation = 0;
    [SerializeField] private long _numBullet = 0;

    void OnEnable()    
    {
        Generate();
        StartCharge();
    }

    void StartCharge() {
        if (!enabled) return;
        var chargeTime = Random.Range(.16f, .24f);
        DOTween
            .To(() => _charge, (x) => _charge = x, 1f, chargeTime)
            .OnComplete(OnCharged);
    }

    void OnCharged() {
        Generate();
        _charge = 0;
        StartCharge();
    }

    void Generate()
    {
        var q = new Quaternion();
        _creationPhase += creationStep;
        var cof = (Mathf.Sin(_creationPhase) + 1) / 2;
        _numCreation = (createionRange.max - createionRange.min) * cof + createionRange.min;
        for (int i = 0; i < _numCreation; i++)
        {
            var p = new Vector3(
                Random.Range(xRange.min, xRange.max),
                Random.Range(yRange.min, yRange.max),
                Random.Range(zRange.min, zRange.max)
            );

            var b = Instantiate(bullet, this.transform,false) as Bullet;
            b.transform.localPosition = p;
            b.wall = wall;
            b.Id = _numBullet++;

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
        }else {
         
        }
    }
}
