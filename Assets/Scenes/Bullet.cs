using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public float life = 1.0f;
    public float lifeTime = 1.0f;
    public Terrain wall;
    public long Id;
    public ParticleSystem explosion;
    public ParticleSystem warhead;
    private Tween tween;
    void Start()
    {
        var tgt = 0f;
        lifeTime = Random.Range(4f, 6.6f);
        tween = DOTween
            .To(() => life, (x) => life = x, tgt, lifeTime)
            .OnComplete(SelfDelete);

        explosion.gameObject.SetActive(false);
    }

    void SelfDelete() {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Explode()
    {
        explosion.transform.SetParent(transform.root, true);
        explosion.gameObject.SetActive(true);

        tween.Kill();

        var l = 1f;
        tween = DOTween
            .To(() => l, (x) => l = x, 0, explosion.duration)
            .OnComplete(()=> { Destroy(explosion.gameObject); });

        SelfDelete();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject == wall.gameObject) {
            Debug.Log(Id + ": " +collision.collider.gameObject);
            Explode();
        }
    }
}
