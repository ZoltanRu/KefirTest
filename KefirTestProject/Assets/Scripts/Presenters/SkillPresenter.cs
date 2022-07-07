using KefirTestProject.Views;
using UnityEngine;

namespace KefirTestProject.Presenters
{
    public class SkillPresenter : MonoBehaviour
    {
        private ISkillView _skillView;

        private void Awake()
        {
            _skillView = GetComponent<ISkillView>();

        }
    }
}
