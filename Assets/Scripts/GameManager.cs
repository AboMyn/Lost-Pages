using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalPages = 2; // сколько всего страниц
    private int foundPages = 0;

    public TextMeshProUGUI winText;

    public GameObject pageUI;
    public TextMeshProUGUI pageText;
    public TextMeshProUGUI closeHintText;

    public bool isReadingPage = false;

    private Page currentPage;

    public int pagesUsed = 0;
    public int keysUsed = 0;

    public bool isGameEnded = false;

    public Image fadePanel;

    public AudioClip horrorSound;
    public AudioClip jumpscareSound;

    public AudioClip goodEndingSound;

    private AudioSource audioSource;

    public int totalKeysCollected = 0;

    public int doorsOpenedWithKeys = 0;
    public int doorsOpenedWithPages = 0;

    public GameObject endScreenUI;
    public TextMeshProUGUI hintText;

    void Awake()
    {
        instance = this;
        winText.gameObject.SetActive(false);
        pageUI.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
      if (isGameEnded) return;

      if (pageUI.activeSelf && Input.GetKeyDown(KeyCode.E))
      {
        ClosePage();
      }
    }

    public void ShowPage(Page page, string text)
    {
        currentPage = page;
        
        isReadingPage = true;

        pageUI.SetActive(true);
        pageText.text = text;
        
        closeHintText.gameObject.SetActive(true);
        closeHintText.text = "Press E to close";

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ClosePage()
    {
        pageUI.SetActive(false);

        closeHintText.gameObject.SetActive(false);

        if (currentPage != null)
        {
            currentPage.CollectAfterReading();
            currentPage = null;
        }

        isReadingPage = false;

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PageFound()
    {
        foundPages++;

        Debug.Log("Pages: " + foundPages + "/" + totalPages);

        if (foundPages >= totalPages)
        {
            Debug.Log("All pages found");
        }
    }

    public void ShowHint(string message)
    {
        StartCoroutine(HintRoutine(message));
    }

    IEnumerator HintRoutine(string message)
    {
        hintText.gameObject.SetActive(true);
        hintText.text = message;

        yield return new WaitForSecondsRealtime(2f);

        hintText.gameObject.SetActive(false);
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");

        winText.gameObject.SetActive(true);
        winText.text = "YOU WIN";

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TryFinishGame()
    {
        Debug.Log("Keys doors: " + doorsOpenedWithKeys);
        Debug.Log("Pages doors: " + doorsOpenedWithPages);

        if (doorsOpenedWithKeys > 0 && doorsOpenedWithPages == 0)
        {
            GoodEnding(); // только ключи
        }
        else if (doorsOpenedWithPages > 0 && doorsOpenedWithKeys == 0)
        {
            BadEnding(); // только страницы
        }
        else
        {
            MixedEnding(); // 💀 СМЕШАЛ
        }
    }

    void MixedEnding()
    {
        Debug.Log("MIXED ENDING");
        isGameEnded = true;

        StartCoroutine(MixedEndingSequence());
    }

    void BadEnding()
    {
        Debug.Log("BAD ENDING");
        isGameEnded = true;

        StartCoroutine(BadEndingSequence());
    }

    void GoodEnding()
    {
        Debug.Log("GOOD ENDING");
        isGameEnded = true;

        StartCoroutine(GoodEndingSequence());
    }

    IEnumerator MixedEndingSequence()
    {
        // 🔊 странный звук
        audioSource.PlayOneShot(horrorSound);

        float t = 0;

        // 🌑 затемнение
        while (t < 1)
        {
            t += Time.deltaTime;

            Color c = fadePanel.color;
            c.a = Mathf.Lerp(0, 1, t);
            fadePanel.color = c;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // 🧠 текст
        winText.gameObject.SetActive(true);
        winText.text = "You were not supposed to do both.";

        yield return new WaitForSeconds(3f);

        // 💥 резкий момент
        audioSource.PlayOneShot(jumpscareSound);

        winText.text = "Now it knows.";

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSecondsRealtime(3f);
        ShowEndScreen();
    }

    IEnumerator BadEndingSequence()
    {
        // 🔊 тревожный звук
        audioSource.PlayOneShot(horrorSound);

        float t = 0;

        // 🌑 плавное затемнение
        while (t < 1)
        {
            t += Time.deltaTime * 0.5f;

            Color c = fadePanel.color;
            c.a = Mathf.Lerp(0, 1, t);
            fadePanel.color = c;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // 💥 скример
        audioSource.PlayOneShot(jumpscareSound);

        winText.gameObject.SetActive(true);
        winText.text = "You shouldn't have found them.";

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSecondsRealtime(3f);
        ShowEndScreen();
    }
    IEnumerator GoodEndingSequence()
    {
        // сразу темный экран
        fadePanel.color = new Color(0, 0, 0, 1);

        // 🔊 спокойный звук
        audioSource.PlayOneShot(goodEndingSound);

        float t = 0;

        // 🌤️ плавно “выходим на свет”
        while (t < 1)
        {
            t += Time.deltaTime * 0.5f;

            Color c = fadePanel.color;
            c.a = Mathf.Lerp(1, 0, t);
            fadePanel.color = c;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // 🕊️ текст
        winText.gameObject.SetActive(true);
        winText.text = "You escaped.";

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSecondsRealtime(3f);
        ShowEndScreen();
    }

    void ShowEndScreen()
    {
        endScreenUI.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene"); // имя сцены
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}