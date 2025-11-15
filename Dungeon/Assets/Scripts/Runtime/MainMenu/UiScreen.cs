using UnityEngine;

public abstract class UiScreen : MonoBehaviour
{
    public virtual void Initialize(){}
    public abstract void Show();
    public abstract void Hide();
}