using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private float _fadeTime = 1;
    
    public UniTask Show()
    {
        return _canvasGroup
            .DOFade(1, _fadeTime)
            .WithCancellation(Application.exitCancellationToken);
    }
    
    public UniTask Hide()
    {
        return _canvasGroup
            .DOFade(0, _fadeTime)
            .WithCancellation(Application.exitCancellationToken);
    }
}