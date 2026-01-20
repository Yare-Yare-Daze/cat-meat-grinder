using UnityEngine;
using Zenject;

public class GameSceneIntsaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private CanvasesManager _canvasesManager;
    [SerializeField] private CatsSpawner _catsSpawner;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CatsAmountContainer _catsAmountContainer;
    
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
        Container.Bind<CanvasesManager>().FromInstance(_canvasesManager).AsSingle().NonLazy();
        Container.Bind<CatsSpawner>().FromInstance(_catsSpawner).AsSingle().NonLazy();
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
        Container.Bind<CatsAmountContainer>().FromInstance(_catsAmountContainer).AsSingle().NonLazy();
    }
}
