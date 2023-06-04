using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguePlayer : MonoBehaviour
{
    [Header("Player")]
    public LayerMask playerLayer;
    public float alcance = 5f;
    public LayerMask ground;

    private PatrulhaWaypoint pw;
    private GameObject player;
    private bool following = false;

    private void Start()
    {
        pw = GetComponent<PatrulhaWaypoint>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (pw.segueOPlayer && !Health.dead)
        {
            if (Physics.CheckSphere(transform.position, alcance, playerLayer))
                following = true;
            else
                following = false;

            if (following && TaNoChao()) {
                pw.enabled = false;
                SegueOPlayer();
            }
            else
                pw.enabled = true;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcance);
    }

    private void SegueOPlayer() {
        Vector3 direction = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        transform.position = Vector3.MoveTowards(transform.position, new(player.transform.position.x, transform.position.y, player.transform.position.z), (pw.velocidade * 2) * Time.deltaTime);
    }

    private bool TaNoChao() {
        if (Physics.Raycast(transform.position, Vector3.down, 0.5f, ground))
            return true;

        return false;
    }
}
