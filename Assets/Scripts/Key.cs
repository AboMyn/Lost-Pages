using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip pickupSound;

    private bool collected = false;

    public int totalKeysCollected = 0;

    public void Interact()
    {
        if (collected) return;

        collected = true;

        // 🔊 звук
        if (pickupSound != null)
        {
            AudioSource audio = Camera.main.GetComponent<AudioSource>();
            audio.PlayOneShot(pickupSound);
        }

        // ➕ увеличиваем ключи
        GameManager.instance.keysUsed++;
        GameManager.instance.totalKeysCollected++;

        Debug.Log("Keys: " + GameManager.instance.keysUsed);

        gameObject.SetActive(false);
    }
}