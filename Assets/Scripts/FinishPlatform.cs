using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Path"))
        {
            StackController.Instance.PathReachToFinish();
        }
    }
}
