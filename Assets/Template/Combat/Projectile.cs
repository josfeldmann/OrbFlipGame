using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask target;
    public float speed = 5f;
    public float damage = 1f;
    public float expireTime = 10f;

    [Header("Push")]
    public float pushForce = 500f;


    private void Update() {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }

    public void Init() {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (Layers.InMask(target, collision.gameObject.layer)) {

            Unit u = collision.gameObject.GetComponent<Unit>();
            u.TakeDamage(damage);
            u.rb.AddForce((collision.gameObject.transform.position - transform.position).normalized * pushForce);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 0) {
            Destroy(gameObject);
        }
    }



}
