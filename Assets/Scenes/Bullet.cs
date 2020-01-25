using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public float life = 1.0f;
    public float lifeTime = 1.0f;
    public Terrain wall;
    public long Id;
    void Start()
    {
        var tgt = 0f;
        lifeTime = Random.Range(4f, 6.6f);
        DOTween
            .To(() => life, (x) => life = x, tgt, lifeTime)
            .OnComplete(SelfDelete);


    }

    void SelfDelete() {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject == wall.gameObject) {
            Debug.Log(Id + ": " +collision.collider.gameObject);
        }
    }
}
