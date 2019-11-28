using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    Queue<PathRequest1> pathRequestQueue1 = new Queue<PathRequest1>();
    PathRequest1 currentPathRequest1;

    static PathRequestManager instance;
    Pathfinding pathfinding;

    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    public static void RequestPath1(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[]> callback)
    {
        PathRequest1 newRequest = new PathRequest1(pathStart, pathEnd, callback);
        instance.pathRequestQueue1.Enqueue(newRequest);
        instance.TryProcessNext1();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    void TryProcessNext1()
    {
        if (!isProcessingPath && pathRequestQueue1.Count > 0)
        {
            currentPathRequest1 = pathRequestQueue1.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest1.pathStart, currentPathRequest1.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    public void FinishedProcessingPath1(Vector3[] path)
    {
        currentPathRequest1.callback(path);
        isProcessingPath = false;
        TryProcessNext1();
    }



    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }

    }

    struct PathRequest1
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[]> callback;

        public PathRequest1(Vector3 _start, Vector3 _end, Action<Vector3[]> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }

    }
}
