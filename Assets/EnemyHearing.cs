using UnityEngine;
using UnityEngine.AI;

public class EnemyHearing : MonoBehaviour
{
    public NavMeshAgent agent;

    public void OnHearSound(Vector3 soundSource)
    {
        Debug.Log(name + " mendengar suara di " + soundSource);
        agent.SetDestination(soundSource); // Musuh menuju sumber suara
    }
}
