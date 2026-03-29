using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;
    private bool isStunning = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunning)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        rb.AddForce(dir * speed);
    }

    public void DoStun(float duration)
    {
        StartCoroutine(StunEnemyRoutine(duration));
    }

    private IEnumerator StunEnemyRoutine(float duration)
    {
        isStunning = true;
        yield return new WaitForSeconds(duration);
        isStunning = false;
    }
}