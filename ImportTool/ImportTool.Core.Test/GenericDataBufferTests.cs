namespace ImportTool.Core.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ImportTool.Core.DataAccess.Buffers;

    using NUnit.Framework;

    [TestFixture]
    public class GenericDataBufferTests
    {
        [Test]
        public void IsEmptyReturnsTrueOnUninitializedCollection()
        {
            GenericDataBuffer<int> buffer = new GenericDataBuffer<int>();
            
            Assert.IsTrue(buffer.IsEmpty);
        }

        [Test]
        public void IsEmptyReturnsTrueOnDepletedCollection()
        {
            List<int> sourceData = new List<int> { 1, 2 };

            GenericDataBuffer<int> buffer = new GenericDataBuffer<int>();

            foreach (int i in sourceData)
            {
                buffer.PushRow(i);
            }

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

            GenericDataBuffer<int> buffer = new GenericDataBuffer<int>();

            foreach (int i in sourceData)
            {
                buffer.PushRow(i);
            }

            Assert.IsFalse(buffer.IsEmpty);
        }

        [Test]
        public void CanPushAndRetrieveFromDataBuffer()
        {
            List<int> sourceData = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> destData = new List<int>();

            GenericDataBuffer<int> buffer = new GenericDataBuffer<int>();

            foreach (int i in sourceData)
            {
                buffer.PushRow(i);
            }

            while (!buffer.IsEmpty)
            {
                destData.Add(buffer.RetrieveNextRow());
            }

            Assert.AreEqual(sourceData, destData);
        }

        [Test]
        public void ThrowsExceptionOnRetrieveFromEmptyCollection()
        {
            GenericDataBuffer<int> buffer = new GenericDataBuffer<int>();
            Assert.Throws<Exception>(() =>
                {
                    int i = buffer.RetrieveNextRow();
                });
        }

        [Test]
        public void DataBufferIsAtLeastKindaThreadSafe()
        {
            List<int> sourceData = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                sourceData.Add(i);
            }

            Action<GenericDataBuffer<int>, int> pushFunction = (internalBuffer, i) =>
                {
                    internalBuffer.PushRow(i);
                };

            Action<GenericDataBuffer<int>, GenericDataBuffer<int>> pullFunction = (sourceBuffer, destBuffer) =>
                {
                    destBuffer.PushRow(sourceBuffer.RetrieveNextRow());
                };

            GenericDataBuffer<int> buffer1 = new GenericDataBuffer<int>();
            GenericDataBuffer<int> buffer2 = new GenericDataBuffer<int>();
            
            TaskFactory factory = new TaskFactory();

            ParallelLoopResult pushResult = Parallel.ForEach(sourceData,
                (i) =>
                    {
                        var pushTask = factory.StartNew(() => pushFunction(buffer1, i));
                        var pullTask = factory.StartNew(() => pullFunction(buffer1, buffer2));

                        Task.WaitAll(pushTask, pullTask);
                    });

            
            
            Assert.AreEqual(sourceData, buffer2.Flush());
        }

        private async void pullFunction(GenericDataBuffer<int> sourceBuffer, GenericDataBuffer<int> destBuffer) {
            while (await !sourceBuffer.IsEmpty)
            {
                
            }
            destBuffer.PushRow(sourceBuffer.RetrieveNextRow());
        };
    }
}
