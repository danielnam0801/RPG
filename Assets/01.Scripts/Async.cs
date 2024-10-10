using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Test : IEnumerable
{
    private int[] _arr = { 1, 2, 3, 4, 5 };
    private int _index;

    public IEnumerator GetEnumerator()
    {
        while(_index < _arr.Length)
        {
            yield return _arr[_index++];
        }
    }
}

public class CoroutineHandle : IEnumerator
{
    public bool IsDone { get; private set; }
    public object Current { get; }

    public bool MoveNext()
    {
        return !IsDone;
    }

    public void Reset()
    { }

    public CoroutineHandle(MonoBehaviour owner, IEnumerator coroutine)
    {
        Current = owner.StartCoroutine(Wrap(coroutine));
    }

    private IEnumerator Wrap(IEnumerator coroutine)
    {
        yield return coroutine;
        IsDone = true;
    }
}

public class Async : MonoBehaviour
{
    private void Start()
    {
        if(Thread.CurrentThread.Name == null)
        {
            Thread.CurrentThread.Name = "MainThread";
        }
        Debug.Log(Thread.CurrentThread.Name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartJob();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("I'm Alive");
        }
    }

    private async void StartJob()
    {
        _num = 0;
        var t1 = Task.Run(() => Inc());
        var t2 = Task.Run(() => Dec());

        await Task.WhenAll(new[] { t1, t2 });
        Debug.Log(_num);
    }

    private int _num = 0;
    private object obj = new object();
    private void Inc()
    {
        for(int i = 0; i < 99999999; i++)
        {
            lock (obj)
            {
                _num++;
            }
        }
    }

    private void Dec()
    {
        for (int i = 0; i < 99999999; i++)
        {
            lock (obj)
            {
                _num--;
            }
        }
    }
    #region Coroutine
    //private int count;
    //private IEnumerator Start()
    //{
    //    var handle1 = this.RunCoroutine(CoA());
    //    var handle2 = this.RunCoroutine(CoA());

    //    while( !handle1.IsDone && !handle2.IsDone)
    //    {
    //        yield return null;
    //    }
    //    Debug.Log("Complete");
    //}

    //IEnumerator CoA()
    //{
    //    StartCoroutine(CoB());
    //    yield return new WaitForSeconds(1f);
    //    Debug.Log("A Complete");
    //}

    //IEnumerator CoB()
    //{
    //    yield return new WaitForSeconds(3f);
    //    Debug.Log("B Complete");
    //}


    //private void Start()
    //{
    //    //Test t = new Test();
    //    //foreach(var a in t)
    //    //{
    //    //    Debug.Log(a);
    //    //}
    //}
    #endregion
}
