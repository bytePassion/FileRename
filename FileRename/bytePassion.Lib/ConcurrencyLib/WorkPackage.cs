using System.Collections.Generic;


namespace bytePassion.Lib.ConcurrencyLib
{
	public class WorkPackage<T>
    {
        private readonly Queue<T> queue;

        private volatile bool abortWork;

        public WorkPackage(IEnumerable<T> workItems = null)
        {
            abortWork = false;

            queue = new Queue<T>();

            if (workItems != null)
                foreach (var item in workItems)
                    queue.Enqueue(item);
        }

        public int CurrentWorkCount
        {
            get
            {
                lock (queue)
                {
                    return queue.Count;
                }
            }
        }

        public void AddWork(IEnumerable<T> newWorkItems)
        {
            foreach (var item in newWorkItems)
                queue.Enqueue(item);
        }

        public bool TakeIfWorkLeft(out T nextTask)
        {
            lock (queue)
            {
                if (queue.Count == 0 || abortWork)
                {
                    nextTask = default(T);
                    return false;
                }
                else
                {
                    nextTask = queue.Dequeue();
                    return true;
                }
            }
        }

        public void AbortWorking()
        {
            abortWork = true;
        }
    }

}
