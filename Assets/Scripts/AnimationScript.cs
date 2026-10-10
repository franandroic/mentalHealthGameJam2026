using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public void CloseScroll()
    {
        GameObject.Find("UIManager").GetComponent<UIAnimationManager>().DisableScroll();
    }
}
