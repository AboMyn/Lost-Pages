using UnityEngine;

public class Page : MonoBehaviour
{
    public bool isCorrectPage;
    public Door[] doors;

    [TextArea] public string pageContent = "Some creepy text...";
    [TextArea] public string correctMessage = "Correct Page!";
    [TextArea] public string wrongMessage = "Wrong Page!";

    private bool alreadyUsed = false;

    public AudioClip pageSound;
    
    public void Interact(PlayerInteract player)
    {
        if (alreadyUsed) return;

        AudioSource audio = Camera.main.GetComponent<AudioSource>();
        audio.PlayOneShot(pageSound);

        GameManager.instance.ShowPage(this, pageContent);
    }

    public void CollectAfterReading()
    {
        if (alreadyUsed) return;

        if (isCorrectPage)
        {
            GameManager.instance.PageFound();
            GameManager.instance.pagesUsed++;

            if (doors != null)
            {
                foreach (Door d in doors)
                {
                    if (d != null)
                        d.Open(false);
                }
            }
        }

        gameObject.SetActive(false);
        alreadyUsed = true;
    }
}