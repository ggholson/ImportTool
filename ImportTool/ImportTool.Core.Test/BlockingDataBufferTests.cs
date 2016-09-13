namespace ImportTool.Core.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ImportTool.Core.Components.Buffers;
    using ImportTool.Core.Contracts.Pipeline;

    using NUnit.Framework;

    [TestFixture]
    public class BlockingDataBufferTests
    {
        [Test]
        public void IsEmptyReturnsTrueOnUninitializedCollection()
        {
            BlockingDataBuffer<int> buffer = new BlockingDataBuffer<int>();
            
            Assert.IsTrue(buffer.IsEmpty);
        }

        [Test]
        public void IsEmptyReturnsTrueOnDepletedCollection()
        {
            List<int> sourceData = new List<int> { 1, 2 };

            BlockingDataBuffer<int> buffer = new BlockingDataBuffer<int>();

            foreach (int i in sourceData)
            {
                buffer.PushRow(i);
            }

            buffer.CompletedAdding();

            foreach (int i in sourceData)
            {
                buffer.RetrieveNextRow();
            }

            Assert.IsTrue(buffer.IsEmpty);
        }

        [Test]
        public void IsEmptyReturnsFalseOnPopulatedCollection()
        {
            List<int> sourceData = new List<int> { 1, 2 };

            BlockingDataBuffer<int> buffer = new BlockingDataBuffer<int>();

            foreach (int i in sourceData)
            {
                buffer.PushRow(i);
            }
            
            Assert.IsFalse(buffer.IsEmpty);
        }

        [Test]
        public void PushToClosedBufferThrowsException()
        {
            BlockingDataBuffer<int> buffer = new BlockingDataBuffer<int>();
            
            buffer.CompletedAdding();

            Assert.Throws<InvalidOperationException>(() => buffer.PushRow(1));
        }

        [Test]
        public void ReadFromOpenBufferThrowsException()
        {
            BlockingDataBuffer<int> buffer = new BlockingDataBuffer<int>();

            buffer.PushRow(1);

            Assert.Throws<InvalidOperationException>(() => buffer.RetrieveNextRow());
        }

        [Test]
        public void ReadFromEmptyBufferThrowsException()
        {
            BlockingDataBuffer<int> buffer = new BlockingDataBuffer<int>();

            buffer.CompletedAdding();

            Assert.Throws<InvalidOperationException>(() => buffer.RetrieveNextRow());
        }

        [Test]
        public void CanPushAndRetrieveFromDataBuffer()
        {
            List<int> sourceData = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> destData = new List<int>();

            BlockingDataBuffer<int> buffer = new BlockingDataBuffer<int>();

            foreach (int i in sourceData)
            {
                buffer.PushRow(i);
            }

            buffer.CompletedAdding();

            while (!buffer.IsEmpty)
            {
                destData.Add(buffer.RetrieveNextRow());
            }

            Assert.AreEqual(sourceData, destData);
        }

        [Test]
        public void ThrowsExceptionOnRetrieveFromEmptyCollection()
        {
            BlockingDataBuffer<int> buffer = new BlockingDataBuffer<int>();
            Assert.Throws<InvalidOperationException>(() => buffer.RetrieveNextRow());
        }

        [Test]
        public void DataBufferIsAtLeastKindaThreadSafe()
        {
            BlockingDataBuffer<int> blockingBuffer = new BlockingDataBuffer<int>();

            // Function to add a bunch of numbers to our queue when dispatched from a thread
            Action<IDataBuffer<int>, int, int> producer = (buffer, start, end) =>
                {
                    for (int i = start; i < end; i++)
                    {
                        buffer.PushRow(i);
                    }
                };

            // Function to read out all of our results in a single thread when we are done. 
            Action<IDataBuffer<int>, Action<IList<int>>> consumer = (buffer, cb) =>
            {
                IList<int> destination = new List<int>();

                while (!buffer.IsEmpty)
                {
                    destination.Add(buffer.RetrieveNextRow());
                }

                cb(destination);
            };

            // Have to place our assertions out here, since they are executed
            // as a callback of our consumer thread.
            Action<IList<int>> assertions = data =>
                {
                    Assert.AreEqual(data.Count, 300);
                    Assert.AreEqual(data.Min(), 0);
                    Assert.AreEqual(data.Max(), 300);
                    Assert.AreEqual(data.Distinct().Count(), 300);
                };

            // Subscribe to the event indicating that there is data available to read and dispatch a new thread to read it.
            blockingBuffer.CanBeRead += (sender, args) => Task.Run(() => consumer(sender as IDataBuffer<int>, assertions));

            // Start the actual execution here... 
            // Spin up 3 threads to stuff data into our buffer simultaneously.
            Task[] producers = new Task[3];
            producers[0] = Task.Run(() => producer(blockingBuffer, 0, 100));
            producers[1] = Task.Run(() => producer(blockingBuffer, 100, 200));
            producers[2] = Task.Run(() => producer(blockingBuffer, 200, 300));

            // When all 3 producer threads are finished, tell buffer that we are done adding.
            Task.Factory.ContinueWhenAll(producers, (p) => blockingBuffer.CompletedAdding());
        }

        [Test]
        public void CorrectlyDisposesUnderlyingCollection()
        {
            BlockingDataBuffer<int> buffer = new BlockingDataBuffer<int>();

            buffer.PostExecute();

            Assert.Throws<ObjectDisposedException>(() => buffer.PushRow(1));
        }
    }
}
