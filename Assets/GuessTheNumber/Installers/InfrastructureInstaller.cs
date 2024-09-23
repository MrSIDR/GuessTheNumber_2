using GuessTheNumber.Common.Starters;
using GuessTheNumber.MainMenu;
using UnityEngine;
using Zenject;

namespace GuessTheNumber.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private SceneStarter m_SceneStarter; 
        [SerializeField] private CoreUIController m_CoreUIController; 
            
        public override void InstallBindings()
        {
            BindSceneStarter();
            BindMenuHolder();
        }

        private void BindMenuHolder()
        {
            Container.Bind<CoreUIController>().FromInstance(m_CoreUIController).AsSingle();
            Container.Bind<IInitialisableController>().FromInstance(m_CoreUIController).AsSingle();
        }

        private void BindSceneStarter()
        {
            Container.Bind<SceneStarter>().FromInstance(m_SceneStarter).AsSingle();
        }
    }
}