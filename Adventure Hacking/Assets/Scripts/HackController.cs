using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HackController : MonoBehaviour
{
    [SerializeField] private TMP_Text winText;
    
    private TMP_Text buttonCode;
    private Button ogButton;

    private bool codeFill = false, winTemp = false;

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

        if (buttonCode.text == "if (true)") winTemp = true;

        codeFill = true;
        clickedButton.interactable = false;
        clickedButton.GetComponentInChildren<TMP_Text>().text = $"-{buttonCode.text}";
    }

    public void RunCode()
    {
        if (!codeFill) return;

        if (winTemp) winText.text = "You Win!";
        else winText.text = "You Lose!";
    }
}
