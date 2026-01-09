using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    Animator anim;

    bool dead = false;

    [Header("Death")]
    public float corpseLifeTime = 7f;
    public float settleTime = 0.4f;          // düşüp oturma süresi
    public bool freezeAfterSettle = true;

    [Header("Ground Fix")]
    public float liftOnGround = 0.12f;       // gömülüyorsa artır (0.05 - 0.25 arası dene)
    public LayerMask groundMask = ~0;        // her şey (istersen Ground layer seçebilirsin)

    CapsuleCollider rootCol;
    Rigidbody rb;
    bool liftedOnce = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rootCol = GetComponent<CapsuleCollider>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (dead) return;
        if (player == null) return;

        agent.SetDestination(player.position);
    }

    public void Die()
    {
        if (dead) return;
        dead = true;

        // 1) agent kapat
        if (agent != null) agent.enabled = false;

        // 2) ölüm animasyonu
        if (anim != null) anim.SetTrigger("Die");

        // 3) root collider kalsın, child colliderları kapat
        DisableChildCollidersKeepRoot();

        // 4) rigidbody ekle ve düşsün
        rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // 5) belli süre sonra sabitle (kaymasın)
        if (freezeAfterSettle)
            Invoke(nameof(FreezeBody), settleTime);

        Destroy(gameObject, corpseLifeTime);
    }

    public void Hit()
    {
        if (dead) return;
        if (anim != null) anim.SetTrigger("Hit");
    }

    void DisableChildCollidersKeepRoot()
    {
        // Root capsule collider varsa açık kalsın
        if (rootCol == null) rootCol = GetComponent<CapsuleCollider>();
        if (rootCol == null) rootCol = gameObject.AddComponent<CapsuleCollider>();

        Collider[] cols = GetComponentsInChildren<Collider>();
        foreach (var c in cols)
        {
            if (c == rootCol) continue;
            c.enabled = false;
        }
    }

    // Zemine ilk temas ettiğinde bir kere yukarı kaldır
    void OnCollisionEnter(Collision collision)
    {
        if (!dead) return;
        if (liftedOnce) return;

        // Ground mask kontrolü
        if (((1 << collision.gameObject.layer) & groundMask) == 0) return;

        liftedOnce = true;
        transform.position += Vector3.up * liftOnGround;
    }

    void FreezeBody()
    {
        if (rb == null) return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
}
