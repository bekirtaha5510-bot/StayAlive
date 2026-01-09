using UnityEngine;
using UnityEngine.AI;

public class SnapToNavMesh : MonoBehaviour
{
    public float maxDistance = 50f;

    void Start()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, maxDistance, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        else
        {
            Debug.LogWarning("NavMesh yakýnýnda yer bulunamadý: " + gameObject.name);
        }
    }
}
