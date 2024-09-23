using GuessTheNumber.Common.Events;
using Zenject;

namespace GuessTheNumber.Installers
{
    public class GlobalServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindingEventManager();
        }

        private void BindingEventManager()
        {
            Container.Bind<IEventManager>().To<EventManager>().AsSingle();
        }
    }
}