using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public void CloseScroll()
    {
        GameObject.Find("UIManager").GetComponent<UIAnimationManager>().DisableScroll();
    }

    public void EnableTutorial()
    {
        GameObject.Find("GameManager").GetComponent<InputController>().isTutorialReady = true;
    }
}
