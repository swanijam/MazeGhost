using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using static AssetUsageFinder.PrefabUtilities;
using Object = UnityEngine.Object;

namespace AssetUsageFinder
{
    [Serializable]
    internal class SearchTarget
    {
        public Object Target;
        public Object[] Nested;
        public Object Root;
        public Scene Scene;
        public PrefabStage Stage;

        public SearchTarget(Object target, FindModeEnum findMode, string sceneOrStagePath = null)
        {
            Asr.IsNotNull(target, "Asset you're trying to search is corrupted");

            Target = target;

            var path = sceneOrStagePath ?? AssetDatabase.GetAssetPath(Target);

            PrefabProperties properties = null;

            if (target.GetType() == typeof(GameObject))
            {
                properties = PrefabUtilities.GetPrefabProperties(target as GameObject);
            }

            if (string.IsNullOrEmpty(sceneOrStagePath) && AssetDatabase.IsSubAsset(Target))
            {
                Root = AssetDatabase.LoadMainAssetAtPath(path);
                Nested = AufUtils.LoadAllAssetsAtPath(path);
                //Nested = new[] { Target };
            }
            else if (!string.IsNullOrEmpty(sceneOrStagePath) && Target is GameObject && findMode == FindModeEnum.Stage)
            {
                // object in Stage
                var gg = (GameObject)Target;
                Nested = gg.GetComponents<Component>().OfType<Object>().ToArray();
                Stage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            }
            else if (properties != null && (properties.IsRootOfAnyPrefab || properties.IsAssetRoot))
            {
                Root = AssetDatabase.LoadMainAssetAtPath(path);
                Nested = AufUtils.LoadAllAssetsAtPath(path);
            }
             else if (properties != null && (properties.IsPartOfAnyPrefab || properties.IsPartOfPrefabAsset))
            {
                Nested = new[] { Target };
            }
            else if (string.IsNullOrEmpty(sceneOrStagePath) && !AssetDatabase.IsSubAsset(Target) && (target is SceneAsset))
            {
                Nested = new[] { Target };
            }
            else if (string.IsNullOrEmpty(sceneOrStagePath) && !AssetDatabase.IsSubAsset(Target) && !(target is SceneAsset))
            {
               // file in Project
                Nested = AufUtils.LoadAllAssetsAtPath(path);
            }
            else if (!string.IsNullOrEmpty(sceneOrStagePath) && Target is GameObject && findMode == FindModeEnum.Scene)
            {
                // object in Scene
                var gg = (GameObject)Target;
                Nested = gg.GetComponents<Component>().OfType<Object>().ToArray();
                Scene = SceneManager.GetSceneByPath(sceneOrStagePath);
            }
            else if (!string.IsNullOrEmpty(sceneOrStagePath) && Target is Component)
            {
                Nested = new[] { Target };
                var comp = (Component)Target;
                Root = comp.gameObject;
                Scene = SceneManager.GetSceneByPath(sceneOrStagePath);
            }
            else if (!string.IsNullOrEmpty(sceneOrStagePath) && !(Target is GameObject) && findMode == FindModeEnum.Scene)
            {
                // component from Dependency Window by Scene button
                Scene = SceneManager.GetSceneByPath(sceneOrStagePath);
                //Nested = AUFUtils.LoadAllAssetsAtPath(path);
                Nested = new[] { Target };
            }
            else
            {
                Root = Target;
                Scene = SceneManager.GetSceneByPath(sceneOrStagePath);
                Nested = new Object[0];
            }
        }

        public bool Check(Object t)
        {
            var tt = Target == t;
            return t && Nested.Aggregate(tt, (current, o) => current || o == t);
        }
    }
}