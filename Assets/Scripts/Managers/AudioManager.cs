using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }

            return _instance;
        }
    }

    #endregion
    
    [SerializeField] private AudioSource source;
    private void OnEnable()
    {
        StackController.Instance.onPathPlaced.AddListener(OnPathPlaced);
    }

    public void PlaySound(AudioClip clip,float p)
    {
        source.PlayOneShot(clip);
        source.pitch = p;
    }
    private void OnPathPlaced(Vector3 v, int combo)
    {
        
    }
}
