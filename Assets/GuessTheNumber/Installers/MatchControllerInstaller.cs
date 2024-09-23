using GuessTheNumber.Common.Starters;
using GuessTheNumber.Match;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GuessTheNumber.Installers
{
    public class MatchControllerInstaller : MonoInstaller
    {
        [FormerlySerializedAs("m_MatchController")] [SerializeField] private AiMatchController aiMatchController;
        
        public override void InstallBindings()
        {
            BindMatchController();
        }

        private void BindMatchController()
        {
            Container
                .Bind<AiMatchController>()
                .FromInstance(aiMatchController)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<IInitialisableController>()
                .FromInstance(aiMatchController)
                .AsCached()
                .NonLazy();
        }
    }
}