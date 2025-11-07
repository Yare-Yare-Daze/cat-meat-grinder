using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ScopeInstaller : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<Player>(Lifetime.Singleton);
    }
}
