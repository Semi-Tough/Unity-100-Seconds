using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class ResourceService : BaseService<ResourceService>
{
    private readonly Dictionary<string, Object> _objectsDic = new();

    public T Load<T>(string path) where T : Object
    {
        if (_objectsDic.ContainsKey(path))
        {
            return _objectsDic[path] as T;
        }

        T obj = Resources.Load<T>(path);
        _objectsDic.Add(path, obj);
        return obj;
    }

    public void LoadAsync<T>(string path, UnityAction<T> callBack = null) where T : Object
    {
        StartCoroutine(LoadAsyncCoroutine(path, callBack));
    }

    private IEnumerator LoadAsyncCoroutine<T>(string path, UnityAction<T> callBack) where T : Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(path);
        yield return request;
        callBack?.Invoke(request.asset as T);
    }

    public void LoadSceneAsync(string sceneName, UnityAction action = null)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName, action));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName, UnityAction action)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        yield return operation;
        action?.Invoke();
    }

    public void SaveXmlData(object data, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";
        using StreamWriter writer = new StreamWriter(path);
        XmlSerializer serializer = new XmlSerializer(data.GetType());
        serializer.Serialize(writer, data);
    }

    public object LoadXmlData(Type type, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";
        if (!File.Exists(path))
        {
            path = Application.streamingAssetsPath + "/" + fileName + ".xml";
            if (!File.Exists(path))
            {
                return Activator.CreateInstance(type);
            }
        }

        using StreamReader reader = new StreamReader(path);
        XmlSerializer serializer = new XmlSerializer(type);
        return serializer.Deserialize(reader);
    }
}