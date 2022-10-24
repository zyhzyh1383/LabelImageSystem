using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zach.Util
{

    public class TaskHelper
    {
        /// <summary>
        /// 多线程处理数据（无返回值）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="list">待处理数据</param>
        /// <param name="action">数据处理方法（有参数无返回值）</param>
        /// <param name="count">处理线程数量</param>
        /// <param name="waitFlag">是否等待执行结束</param>
        public static void RunTask<T>(List<T> list, Action<T> action, int threadCount = 5, bool waitFlag = true)
        {
            ConcurrentQueue<T> queue = new ConcurrentQueue<T>(list);
            Task[] tasks = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    while (queue.TryDequeue(out T t))
                    {
                        action(t);
                    }
                });
            }
            if (waitFlag)
            {
                Task.WaitAll(tasks);
            }
        }

        /// <summary>
        /// 多线程处理数据（返回处理后列表）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="list">待处理数据</param>
        /// <param name="func">数据处理方法（有参数有返回值）</param>
        /// <param name="threadCount">处理线程数量</param>
        /// <returns>数据处理后结果</returns>
        public static List<T> RunTask<T>(List<T> list, Func<T, T> func, int threadCount = 5)
        {
            var result = new List<T>();
            ConcurrentQueue<T> queue = new ConcurrentQueue<T>(list);
            Task<List<T>>[] tasks = new Task<List<T>>[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Run<List<T>>(() =>
                {
                    var rList = new List<T>();
                    while (queue.TryDequeue(out T t))
                    {
                        rList.Add(func(t));
                    }
                    return rList;
                });
            }
            Task.WaitAll(tasks);
            for (int i = 0; i < threadCount; i++)
            {
                result.AddRange(tasks[i].Result);
            }
            return result;
        }
    }
}