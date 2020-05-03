using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AssetUsageFinder
{
    [Serializable]
    internal sealed class InStageDependencyFinder : DependencyAbstractFinder
    {
        [SerializeField] private string _stagePath;
        [SerializeField] private PrefabStage _stage;

        public string ScenePath
        {
            get { return _stagePath; }
        }

        public InStageDependencyFinder(Object target, string stagePath)
        {
            Target = new SearchTarget(target, FindModeEnum.Stage, stagePath);
            _stagePath = stagePath;
            _stage = Target.Stage;
            Title = stagePath;

            var name = target is Component ? target.GetType().Name : target.name;

            TabContent = new GUIContent
            {
                text = name,
                image = AssetPreview.GetMiniTypeThumbnail(Target.Target.GetType()) ?? AssetPreview.GetMiniThumbnail(Target.Target)
            };

            FindDependencies();
        }

        public override void FindDependencies()
        {
            Dependencies = Group(DependencyFinderEngine.GetDependenciesInStage(Target, _stage)).ToArray();
        }


        public override DependencyAbstractFinder Nest(Object o)
        {
            return new InStageDependencyFinder(o, _stagePath) {Parent = this};
        }
    }
}