using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI messageText;

    private Page currentPage;

    private Chest currentChest;

    private Key currentKey;

    private Door currentDoor;

    void Update()
    {
        if (GameManager.instance.isReadingPage)
        {
            interactText.gameObject.SetActive(false);
            return;
        }
        
        CheckForPage();

        if (currentPage != null && Input.GetKeyDown(KeyCode.E))
        {
            interactText.gameObject.SetActive(false);
            currentPage.Interact(this);
        }

        if (currentChest != null && Input.GetKeyDown(KeyCode.Q))
        {
            interactText.gameObject.SetActive(false);
            currentChest.OpenChest();
        }
        
        if (currentKey != null && Input.GetKeyDown(KeyCode.E))
        {
            interactText.gameObject.SetActive(false);
            currentKey.Interact();
        }

        if (currentDoor != null && Input.GetKeyDown(KeyCode.Q))
        {
            interactText.gameObject.SetActive(false);
            Debug.Log("TRY OPEN DOOR");
            currentDoor.Open(true);
        }
    }

    void CheckForPage()
    {
        currentPage = null;
        currentChest = null;
        currentKey = null;
        currentDoor = null;
        interactText.gameObject.SetActive(false);

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        RaycastHit[] hits = Physics.RaycastAll(ray, interactDistance);
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            // 🔥 сначала ищем страницу
            Page page = hit.collider.GetComponent<Page>();
            if (page != null)
            {
                currentPage = page;
                interactText.text = "Press E to read";
                interactText.gameObject.SetActive(true);
                return;
            }

            // 🔑 ключ
            Key key = hit.collider.GetComponent<Key>();
            if (key != null)
            {
                currentKey = key;
                interactText.text = "Press E to take key";
                interactText.gameObject.SetActive(true);
                return;
            }

            // 🚪 дверь
            Door door = hit.collider.GetComponentInParent<Door>();
            if (door != null)
            {
                currentDoor = door;

                if (door.requiresKey)
                    interactText.text = "Press Q to unlock";
                else
                    interactText.text = "Press Q to open";

                interactText.gameObject.SetActive(true);
                return;
            }

            // 🔥 потом сундук (НО ТОЛЬКО ЕСЛИ ЗАКРЫТ)
            Chest chest = hit.collider.GetComponentInParent<Chest>();
            if (chest != null && !chest.IsOpen)
            {
                currentChest = chest;
                interactText.text = "Press Q to open";
                interactText.gameObject.SetActive(true);
                return;
            }
        }
    }

    public void ShowMessage(string message)
    {
        if (messageText != null)
        {
            interactText.gameObject.SetActive(false);

            messageText.gameObject.SetActive(true);
            messageText.text = message;

            CancelInvoke(nameof(HideMessage));
            Invoke(nameof(HideMessage), 2f);
        }
    }

    void HideMessage()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }
}