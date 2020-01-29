using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public float cloneRate = 0;
    public float maxVelocity = 60;

    [Space(15), Header("== readonly ==")]
    public bool firing = false;
    public float life = 1.0f;
    public float lifeTime = 1.0f;
    public Terrain wall;
    public long Id;
    public ParticleSystem explosion;
    public ParticleSystem warhead;
    public ParticleSystem tail;
    public Rigidbody rigidbody;

    private Tween tween;
    private float sqrMaxMagnitude;
    void Start()
    {
        sqrMaxMagnitude = maxVelocity * maxVelocity;
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
        tail.transform.SetParent(transform.root, true);

        explosion.transform.SetParent(null, true);
        explosion.gameObject.SetActive(true);

        tween.Kill();

        var l = 1f;
        tween = DOTween
            .To(() => l, (x) => l = x, 0, explosion.duration)
            .OnComplete(()=> {
                Destroy(tail.gameObject);
                Destroy(explosion.gameObject); 
            });

        SelfDelete();
    }

    private void Clone() 
    {
        var c = Instantiate(gameObject, transform.parent, false);
        var b = c.GetComponent<Bullet>();
        c.GetComponent<Collider>().isTrigger = true;
        b.cloneRate = -1;
        var v = rigidbody.velocity;
        v.x += Random.Range(-1, 1);
        v.y += Random.Range(-1, 1);
        b.rigidbody.velocity = v;
    }

    public void Ignition() 
    {
        firing = true;
        warhead.gameObject.SetActive(true);
    }

    public void RoundMaxVelocity()
    {
        var v = rigidbody.velocity;
        if (v.sqrMagnitude > sqrMaxMagnitude)
        {
            rigidbody.velocity = v.normalized * maxVelocity;
            Debug.Log(v.sqrMagnitude + " => " + rigidbody.velocity.sqrMagnitude);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == null) return;

        var bulet = collision.gameObject.GetComponent<Bullet>();
        if (collision.gameObject.name.Contains("bone")
         || collision.gameObject.name.Contains("palm")
         //|| (bulet && bulet.firing)
         ){
            if (Random.value < cloneRate) Clone();

            Ignition();
        }

        RoundMaxVelocity();

        if (collision.gameObject == wall.gameObject && warhead.gameObject.activeSelf) {
//            Debug.Log(Id + ": " +collision.collider.gameObject);
            Explode();
        }
    }
}
