using System.Collections.Generic;
using System.Threading;


namespace bytePassion.Lib.ConcurrencyLib
{

	public class FixedThreadExecutor<T> where T : IThreadTask
    {

        private class ThreadKiller : IThreadTask
        {
            public void DoWork() {}
        }

        private readonly Thread[] threads;
        private readonly BlockingQueue<IThreadTask> workQueue;
        private readonly BlockingQueue<IThreadTask> doneWork;

        private bool isShutdown;
        private int workCount;

        public FixedThreadExecutor(int threadCount)
        {
            workCount = 0;
            isShutdown = false;

            workQueue = new BlockingQueue<IThreadTask>();
            doneWork  = new BlockingQueue<IThreadTask>();

            threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = new Thread(() => ThreadWorkingLoop(workQueue, doneWork));
                threads[i].Start();
            }
        }

        public void Shutdown()
        {
            isShutdown = true;

            for (int i = 0; i < threads.Length; i++)
            {
                workQueue.Put(new ThreadKiller());
            }
        }

        public void AddWork(T workItem)
        {
            if (!isShutdown)
            {
                workCount++;
                workQueue.Put(workItem);
            }
        }

        public void AddWork(IEnumerable<T> workItems)
        {
            foreach (var workItem in workItems)
            {
                AddWork(workItem);
            }
        }

        public void WaitTillCurrentSubmitedWorkIsDone()
        {
            while (workCount > 0)
            {
                doneWork.Take();
                workCount--;
            }
        }

        private static void ThreadWorkingLoop(BlockingQueue<IThreadTask> workQueue,
                                              BlockingQueue<IThreadTask> doneWork)
        {
            while (true)
            {
                var task = workQueue.Take();

                if (task is ThreadKiller)
                    return;
                
                task.DoWork();
                doneWork.Put(task);                
            }
        }
    }

}
