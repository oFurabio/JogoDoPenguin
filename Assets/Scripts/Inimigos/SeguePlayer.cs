using UnityEngine;

public class SeguePlayer : MonoBehaviour
{
    [Header("Player")]
    public LayerMask playerL;
    public LayerMask chao;
    public float alcance = 5f;

    private PatrulhaWaypoint pw;
    private GameObject player;

    private void Start()
    {
        pw = GetComponent<PatrulhaWaypoint>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (pw.segueOPlayer) {
            if (PlayerOnRange() && !Health.dead) {
                if (IsGrounded()) {
                    pw.enabled = false;
                    FollowPlayer();
                }
            }
            else
                pw.enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcance);
    }

    private void FollowPlayer() {
        Vector3 direction = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;

        transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, new(player.transform.position.x, transform.position.y, player.transform.position.z), (pw.velocidade * 2) * Time.deltaTime), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f));
    }

    private bool PlayerOnRange()
    {
        return Physics.CheckSphere(transform.position, alcance, playerL);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f, chao);
    }
}
