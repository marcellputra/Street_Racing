using UnityEngine;

public class Level2Checkpoint : MonoBehaviour
{
    public Level2LapFinish finishScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (finishScript != null)
            {
                finishScript.SetCheckpointPassed();
            }
        }
    }
}