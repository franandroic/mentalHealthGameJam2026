using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator PlumetAnimator;

    [SerializeField] Animator GuideAnimator;
    public void CloseScroll()
    {
        GameObject.Find("UIManager").GetComponent<UIAnimationManager>().DisableScroll();
    }

    public void EnableTutorial()
    {
        GameObject.Find("GameManager").GetComponent<InputController>().isTutorialReady = true;
        animator.SetBool("Start", true);
    }

    public void EnablePlummetTutorial()
    {
        GameObject.Find("GameManager").GetComponent<InputController>().PlumetTutorial = true;
        PlumetAnimator.SetBool("Start", true);
    }

    public void EnableHookGuide()
    {
        GuideAnimator.SetBool("Start", true);
    }
}
