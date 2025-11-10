using UnityEngine;
using Zenject;

public class GameSceneIntsaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private CanvasesManager _canvasesManager;
    [SerializeField] private CatsSpawner _catsSpawner;
    
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
        Container.Bind<CanvasesManager>().FromInstance(_canvasesManager).AsSingle().NonLazy();
        Container.Bind<CatsSpawner>().FromInstance(_catsSpawner).AsSingle().NonLazy();
    }
}
