using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90f;
    public float speed = 2f;

    private bool opening = false;
    private Quaternion startRotation;
    private Quaternion targetRotation;

    public AudioClip openSound;

    public bool isExitDoor = false;

    public int requiredKeys = 1;

    public bool requiresKey = false;
    public bool isFinalDoor = false;

    public Door otherDoor; // вторая створка
    public bool isMainDoor = true;

    void Start()
    {
        startRotation = transform.localRotation;
        targetRotation = startRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        if (opening)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                targetRotation,
                speed * 100f * Time.deltaTime
            );
        }
    }

    public void Open(bool usingKey)
    {
        // 🔑 если открываем ключом
        if (usingKey)
        {
            if (GameManager.instance.keysUsed <= 0)
            {
                Debug.Log("No key!");
                GameManager.instance.ShowHint("You need a something to open it.");
                return;
            }

            GameManager.instance.keysUsed--;
            GameManager.instance.doorsOpenedWithKeys++;
        }

        // 📜 если открываем страницами
        else
        {
            GameManager.instance.pagesUsed++;
            GameManager.instance.doorsOpenedWithPages++;
        }

        opening = true;

        // 🔊 звук
        if (openSound != null)
        {
            AudioSource audio = Camera.main.GetComponent<AudioSource>();
            audio.PlayOneShot(openSound);
        }

        // открываем вторую створку
        if (otherDoor != null)
        {
            otherDoor.OpenSecondary();
        }

        // 🎬 финал
        if (isFinalDoor)
        {
            GameManager.instance.TryFinishGame();
        }
    }
    public void OpenSecondary()
    {
        opening = true;
    }
}