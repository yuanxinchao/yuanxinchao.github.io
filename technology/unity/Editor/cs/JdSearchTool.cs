
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.SceneManagement;

namespace JediEditor
{
    internal static class JdSearchTool
    {
        static bool CheckDup(List<JDCheckListItem> list, string path)
        {
            int n = list.Count;
            for (int i = 0; i < n; i++)
            {
                if (list[i].Path == path)
                    return true;
            }

            return false;
        }


        internal static List<JDCheckListItem> SearchInAssets<T>(Func<string, JDCheckListItem> check, string[] folders = null) where T : Object
        {
            string filter = "t:" + typeof(T).Name;
            return SearchInAssets(typeof(T), filter, check, folders);
        }


        internal static List<JDCheckListItem> SearchInAssets(Type type, string filter, Func<string, JDCheckListItem> check, string[] folders = null)
        {
            EditorUtility.DisplayProgressBar("在Assets中查找", "开始查找", 0);

            var guids = folders == null ? AssetDatabase.FindAssets(filter) : AssetDatabase.FindAssets(filter, folders);

            int idx = 0;
            List<JDCheckListItem> list = new List<JDCheckListItem>(256);
            foreach (var guid in guids)
            {
                ++idx;

                string path = AssetDatabase.GUIDToAssetPath(guid);

                //跳过特定目录
                if(JDUtils.IsIgnoreDirectory(path))
                    continue;

                //忽略重复名单
                if (CheckDup(list, path))
                {
                    //                    Debug.LogWarning("duplicate: " + path);
                    continue;
                }

                var item = check(path);

                if (item != null)
                {
                    item.Guid = guid;
                    item.Type = type;
                    item.Path = path;
                    list.Add(item);
                }
                EditorUtility.DisplayProgressBar("在Assets中查找", path, idx / (float)guids.Length);
            }

            EditorUtility.ClearProgressBar();
            return list;
        }

        internal static List<JDCheckListItem> SearchInBuildScenes(Func<GameObject, EditorBuildSettingsScene, bool> check)
        {
            return SearchInScenes(JDUtils.GetAllEnabledScene(), check);
        }

        internal static List<JDCheckListItem> SearchInScenes(List<EditorBuildSettingsScene> scenes, Func<GameObject, EditorBuildSettingsScene, bool> check)
        {
            bool result = true;
            List<JDCheckListItem> list = new List<JDCheckListItem>(256);
            string currentScene = SceneManager.GetActiveScene().path;
            try
            {
                int sceneCount = scenes.Count;
                for (int i = 0; i < scenes.Count; i++)
                {
                    string scenePath = scenes[i].path;
                    //				string sceneName = Path.GetFileNameWithoutExtension(scenePath);

                    Debug.Log("search scene: " + scenePath);

                    if (EditorUtility.DisplayCancelableProgressBar(string.Format("搜索场景[{0}]", scenePath),
                        string.Format("搜索场景{0}[{1}/{2}]", scenePath, i + 1, sceneCount), i + 1 / (float)sceneCount))
                    {
                        break;
                    }

                    if (JDUtils.GetCurrentScenePath() != scenePath)
                    {
                        JDUtils.OpenScene(scenePath);
                    }

                    // if we're scanning currently opened scene and going to scan more scenes,
                    // we need to close all additional scenes to avoid duplicates
                    else if (EditorSceneManager.loadedSceneCount > 1)
                    {
                        JDUtils.CloseAllScenesButActive();
                    }

                    GameObject[] gameObjects = JDUtils.GetAllSuitableGameObjectsInCurrentScene();
                    int objectsCount = gameObjects.Length;

                    for (int j = 0; j < objectsCount; j++)
                    {
                        var obj = gameObjects[j];
                        if (EditorUtility.DisplayCancelableProgressBar("遍历Gameobjects", obj.name,
                            j + 1 / (float)objectsCount))
                        {
                            result = false;
                            break;
                        }

                        if (check(obj, scenes[i]))
                        {
                            list.Add(new JDCheckListItem()
                            {
                                Path = scenePath,
                                FullPath = JDUtils.GetFullTransformPath(obj.transform),
                                InScene = true,
                                ObjectId = JDUtils.GetLocalIdentifierInFileForObject(obj),
                            });
                        }
                    }

                    if (!result) break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }


            Debug.Log("final open scene: " + currentScene);
            JDUtils.OpenScene(currentScene);

            EditorUtility.ClearProgressBar();
            return list;
        }

        internal static List<JDCheckListItem> SearchInCurrentScenes<T>(Func<T, bool> check) where T:Object
        {
            List<JDCheckListItem> list = new List<JDCheckListItem>(128);
            try
            {
                T[] objects = Resources.FindObjectsOfTypeAll<T>();
                int objectsCount = objects.Length;

                for (int j = 0; j < objectsCount; j++)
                {
                    var obj = objects[j];
                    if (check(obj))
                    {
                        if(obj is Component)
                        {
                            var component = obj as Component;
                            list.Add(new JDCheckListItem()
                            {
                                Path = component.gameObject.scene.path,
                                FullPath = JDUtils.GetFullTransformPath(component.transform),
                                InScene = true,
                                ObjectId = JDUtils.GetLocalIdentifierInFileForObject(obj),
                                Instance = obj,
                            });
                        }
                        else if (obj is GameObject)
                        {
                            var go = obj as GameObject;
                            list.Add(new JDCheckListItem()
                            {
                                Path = go.gameObject.scene.path,
                                FullPath = JDUtils.GetFullTransformPath(go.transform),
                                InScene = true,
                                ObjectId = JDUtils.GetLocalIdentifierInFileForObject(obj),
                                Instance = obj,
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return list;
        }

        /// <summary>
        /// 获取指定路径下pfefab的所有指定组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static T[] GetComponentByPath<T>(string path) where T : Component
        {
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (obj == null)
                Debug.Assert(false, "can't load obj, path: " + path);
            T[] component = obj.GetComponentsInChildren<T>(true);
            return component;
        }

        //      internal static T[] GetComponentByPath<T>(string path) where T : Component
        //        {
        //            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        //            Debug.Assert(obj);
        //            T[] component = obj.GetComponentsInChildren<T>();
        //            return component;
        //        }
    }


    /// <summary>
    /// 表示搜索结果列表中的一项
    /// </summary>
    internal class JDCheckListItem : IComparable<JDCheckListItem>
    {
        public string Guid;
        public string Path; //如果是Assets目录中搜索, 表示asset文件的全路径; 如果是Scene中搜索,表示Scene文件路径
        public string FullPath; //如果搜索的是某个GameObject，表示从root GameObject开始到目标GameObject的路径
        public Object Instance; // 目标Object
        public bool InScene; //目标Object是否在Scene中，如果不在scene中，则在Assets中
        public long ObjectId; //
        public Type Type; //目标Object类型
        public string ExtraInfo; //默认Item会显示ExtranInfo + (Path + GameObjectPath)
        public int SortOrder;
        public List<JDIssue> Issues = new List<JDIssue>(8);

        public int CompareTo(JDCheckListItem other)
        {
            if (SortOrder == other.SortOrder)
                return 0;
            if (SortOrder > other.SortOrder)
                return 1;
            return -1;
        }
    }

}

