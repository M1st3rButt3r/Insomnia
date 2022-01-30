using TMPro;
using UnityEngine;

public enum ExplainType
{
    KeyPress,
    Text
}

public class TutorialManager : MonoBehaviour
{
    public GameObject deathMenu;
    public GameObject explainCanvas;
    public TMP_Text before;
    public TMP_Text after;
    public TMP_Text key;

    public ExplainType explainType;
    
    public string beforeText;
    public string afterText;
    public string keyChar;

    public TMP_Text explainTextElement;
    public string explainTextText;

    private void Update()
    {
        if (deathMenu.activeSelf)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject go = GameManager.Instance.secondRun ?
            GameManager.Instance.secondPlayer : GameManager.Instance.player;

        if (! col.gameObject.Equals(go)) return;

        explainCanvas.SetActive(true);
        switch (explainType)
        {
            case ExplainType.KeyPress:
                before.text = beforeText;
                after.text = afterText;
                key.text = keyChar;
                break;
            case ExplainType.Text:
                explainTextElement.text = explainTextText;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        GameObject go = GameManager.Instance.secondRun ?
            GameManager.Instance.secondPlayer : GameManager.Instance.player;

        if (! col.gameObject.Equals(go)) return;

        explainCanvas.SetActive(false);
    }
}
