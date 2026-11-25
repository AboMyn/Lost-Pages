using UnityEngine;

public class Chest : MonoBehaviour
{
    public Transform lid; // крышка
    public float openAngle = 100f;
    public float speed = 2f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    public GameObject pageInside;
    public bool IsOpen => isOpen;
    
    public AudioClip openSound;
    private AudioSource audioSource;

    void Start()
    {
        closedRotation = lid.localRotation;
        openRotation = closedRotation * Quaternion.Euler(openAngle, 0, 0);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isOpen)
        {
            lid.localRotation = Quaternion.Lerp(
                lid.localRotation,
                openRotation,
                Time.deltaTime * speed
            );
        }
    }

    public void OpenChest()
    {
        if (!isOpen)
        {
            Debug.Log("Chest opened!");
            isOpen = true;

            // 🔊 звук сундука
            if (openSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(openSound);
            }

            if (pageInside != null)
                pageInside.SetActive(true);
        }
    }
}