using Cysharp.Threading.Tasks;

public interface ISceneLoader
{
    UniTask Load(SceneNames name);
}