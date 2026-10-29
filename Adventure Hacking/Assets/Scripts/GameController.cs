using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject hackPanel;
    [SerializeField] private TMP_Text tempText;

    public bool Hacking { get; private set; } = false;

    public void StartHack(bool newCode)
    {
        hackPanel.SetActive(true);
        hackPanel.GetComponent<HackController>().OnHack(newCode);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Hacking = true;
    }

    public void EndHack()
    {
        hackPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Hacking = false;
    }

    public void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator NewCode()
    {
        tempText.text = "New Code Obtained!";
        yield return new WaitForSeconds(2.5f);
        tempText.text = "";
    }

    public void NewCodeStart()
    {
        StartCoroutine(NewCode());
    }
}
