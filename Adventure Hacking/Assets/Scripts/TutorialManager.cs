using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Tutorials
{
    None,
    Move,
    Interact,
    NewCode,
    Glitch,
    Coding
}

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TMP_Text tutoText;
    [SerializeField] private GameObject interPanel;
    [SerializeField] private GameObject hackPanel;
    [SerializeField] private BoxCollider2D glitchPortal;
    [SerializeField] private bool tuto1 = false;
    
    private Tutorials tuto = Tutorials.Move;

    private void Awake()
    {
        NewTuto();
    }

    public void NewTuto()
    {
        if (tuto1)
        {
            tuto = Tutorials.Interact;
            glitchPortal.enabled = false;
        }
        switch (tuto)
        {
            case Tutorials.None:
                if (SceneManager.GetActiveScene().buildIndex == 1) tutoText.text = "Press Spacebar to restart the game!";
                else tutoText.text = "";
                break;
            case Tutorials.Move:
                MoveTuto();
                break;
            case Tutorials.Interact:
                InteractTuto();
                break;
            case Tutorials.NewCode:
                NewCodeTuto();
                break;
            case Tutorials.Glitch:
                HackTuto();
                break;
            case Tutorials.Coding:
                CodingTuto();
                break;
            default:
                Debug.Log("HOW?!");
                break;
        }
    }

    private void MoveTuto()
    {
        tutoText.text = "Use WASD or Arrow Keys to move";
        tuto = Tutorials.Interact;
    }

    private void InteractTuto()
    {
        if (tuto1) tutoText.text = "You can also interact with these:\nCode Blocks                                     .";
        else tutoText.text = "Press E to interact when next to:\n                                        Glitch Portals";
        interPanel.SetActive(true);
        if (tuto1) tuto = Tutorials.NewCode;
        else tuto = Tutorials.Glitch;
        tuto1 = false;
    }

    private void NewCodeTuto()
    {
        tutoText.text = "This New Code Object has given you more Code to use in the Interface to fix Glitches";
        interPanel.transform.Find("Glitch Text").gameObject.GetComponent<TMP_Text>().text = "Reactivated for tutorial purposes";
        glitchPortal.enabled = true;
        tuto = Tutorials.Glitch;
    }

    private void HackTuto()
    {
        interPanel.SetActive(false);
        hackPanel.SetActive(true);
        tutoText.text = "This is the hacking Menu. Use your database to  fix the #missing code";
        tuto = Tutorials.Coding;
    }

    private void CodingTuto()
    {
        tutoText.text = "After fixing the code, press the run button to test it";
        hackPanel.SetActive(false);
        tuto = Tutorials.None;
    }
}
