using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GuessTheNumber.Common.Starters
{
    public class SceneStarter : MonoBehaviour
    {
        [Inject] private List<IInitialisableController> m_Controllers;

        private void Start()
        {
            var orderedControllers = m_Controllers.OrderBy(controller => controller.InitOrder);
            foreach (var controller in orderedControllers)
            {
                controller.Init();
            }
        }
    }
}