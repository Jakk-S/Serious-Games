using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HackController : MonoBehaviour
{
    [SerializeField] private TMP_Text winText;
    [SerializeField] private TMP_Text addCodeText;
    [SerializeField] private GameObject codePrefab;
    [SerializeField] private GameObject playerCode;
    
    private TMP_Text buttonCode;
    private Button ogButton;
    private GameObject newButton;

    private bool codeFill = false, winTemp = false;

    [Header("Tutorial")]
    [SerializeField] private bool inTuto;

    public void OnCodeClick(Button clickedButton)
    {
        if (ogButton == null)
        {
            ogButton = clickedButton;
        }
        else if (ogButton != clickedButton)
        {
            ogButton.GetComponent<Image>().color = Color.white;
            ogButton = clickedButton;
        }
        else
        {
            ogButton.GetComponent<Image>().color = Color.white;
            ogButton = null;
            buttonCode = null;
            return;
        }
        buttonCode = ogButton.GetComponentInChildren<TMP_Text>();
        ogButton.GetComponent<Image>().color = Color.green;
    }

    public void InsertCode(Button clickedButton)
    {
        if (ogButton == null) return;

        if (buttonCode.text == "if (true)" || buttonCode.text == "num = 2") winTemp = true;

        codeFill = true;
        clickedButton.interactable = false;
        clickedButton.GetComponentInChildren<TMP_Text>().text = $"-{buttonCode.text}";

        if (inTuto) Camera.main.GetComponent<TutorialManager>().NewTuto();
    }

    public void RunCode()
    {
        if (!codeFill) return;

        int currScene = SceneManager.GetActiveScene().buildIndex;
        if (winTemp)
        {
            if (currScene <= 1) StartCoroutine(LoadAsyncScene(currScene + 1));
            else winText.text = "You Win!";
        }
        else winText.text = "You Lose!\nPress Space to Reload";
        //if (currScene <= 1) StartCoroutine(LoadAsyncScene(currScene));
        //else
        //{
        //    if (winTemp) winText.text = "You Win!";
        //    else winText.text = "You Lose!\nPress Space to Reload";
        //}

        if (inTuto) Camera.main.GetComponent<TutorialManager>().NewTuto();
    }

    public void OnHack(bool newCode)
    {
        if (!newCode || newButton != null) return;

        addCodeText.text = "Adding New Code!";

        StartCoroutine(AddingCode());
    }

    IEnumerator AddingCode()
    {
        yield return new WaitForSeconds(2.5f);
        addCodeText.text = "";
        newButton = Instantiate(codePrefab, playerCode.transform);

        Button buttonComponent = newButton.GetComponent<Button>();

        buttonComponent.onClick.RemoveAllListeners();
        buttonComponent.onClick.AddListener(() => OnCodeClick(buttonComponent));
    }

    IEnumerator LoadAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        asyncLoad.allowSceneActivation = false;

        winText.text = "WARPING!";

        yield return new WaitForSeconds(2.5f);

        asyncLoad.allowSceneActivation = true;
    }
}
