using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with increasing priorities.
    // Expected Result: The items should be stored in the order they were added, and the highest priority item should be removed first.
    // Defect(s) Found: The priority search skipped the item at the back of the queue.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 1);
        priorityQueue.Enqueue("second", 2);
        priorityQueue.Enqueue("third", 3);

        Assert.AreEqual("[first (Pri:1), second (Pri:2), third (Pri:3)]", priorityQueue.ToString());
        Assert.AreEqual("third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items where two items have the same highest priority.
    // Expected Result: The first item with the highest priority should be removed first, and each dequeued item should be removed from the queue.
    // Defect(s) Found: The queue did not remove the dequeued item and did not preserve FIFO order for equal priorities.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("first high", 5);
        priorityQueue.Enqueue("second high", 5);

        Assert.AreEqual("first high", priorityQueue.Dequeue());
        Assert.AreEqual("second high", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Try to dequeue from an empty priority queue.
    // Expected Result: An InvalidOperationException should be thrown with the message "The queue is empty."
    // Defect(s) Found: None.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}
