//this program implements the solution to the k-select problem using min heaps as seen in class

//By M@rtin Bebey

using System;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class KSelect
{
    private static int[] n;//to hold the various values for n in this assignment
    private static ExplicitHeap restoreHeap;//used to restore the heap to original shape after deletions

    public int[] N
    {
        get
        {
            return n;
        }

        set
        {
            n = value;
        }
    }

    public static void Main()
    {
        //x0, x, a, p, m and fullPeriodMultiplier are used for the pseudorandom number generator
        int numberOfNValues = 7, i, j, randomNumber, x0, reset, count = 0, x, a, p, m = 0, fullPeriodMultiplier = 0, k, z, kthSmallest;
        n = new int[numberOfNValues];
        Stopwatch stopwatch = new Stopwatch();
        ExplicitHeap explicitHeap;
        Random random = new Random();

        for (i = 0; i < numberOfNValues; ++i)//filling values of n in this assignment
        {
            if (i == 0)
            {
                n[i] = 10;
            }

            else if (i == 1)
            {
                n[i] = n[i - 1] * 5;
            }

            else if (i == 2)
            {
                n[i] = n[i - 1] * 2;
            }

            else
            {
                n[i] = n[i - 1] * 10;
            }
        }



        for (i = 0; i < numberOfNValues; ++i)//for each value of n in this assignment
        {


            explicitHeap = new ExplicitHeap();//a new heap
            restoreHeap = new ExplicitHeap();//backup heap

            stopwatch.Start();
            for (j = 0; j < n[i]; ++j)//insert n values in the heap and time it
            {
                randomNumber = random.Next(1, ((5 * n.Length) + 1));//between 1 and 5n inclusive

                explicitHeap.Insert(randomNumber);
                explicitHeap.Swim(explicitHeap.InsertedNode);
            }

            restoreHeap = (ExplicitHeap)explicitHeap.Clone();
            x0 = random.Next(1, 10);
            reset = x0;
            m = n[i];

            ///this loop is used to find the first full-period multiplier
            for (a = 2; count < 1; ++a)
            {
                p = 0; x0 = 1; x = 0;//for every value of a checked, these are set back to initial values

                //this loop is used to find a multiplier
                while (x != 1 && x0 != 0 && p < (m * 0.2))
                {
                    ++p;
                    x = (x0 * a) % m;
                    x0 = x;
                }

                if (p >= (m * 0.2))//if that multiplier is a full-period multiplier, it is used
                {
                    fullPeriodMultiplier = a;//and stored as the full-period multiplier
                    ++count;//and the number of full-period multipliers found increases by 1 (we just need 1)
                }
            }

            --count;

            for (j = 0; j < Convert.ToInt32(0.2 * n[i]); ++j)
            {
                k = (fullPeriodMultiplier * reset) % m;//formula to generate a random number


                for (z = 0; z < k; ++z)//loop finding the Kth Smalest value from the heap
                {
                    kthSmallest = explicitHeap.Delete(ref explicitHeap);
                }


                explicitHeap = (ExplicitHeap)restoreHeap.Clone();//Heap is reset to original for each K so that calculations on time is not biased
                reset = k;

            }

            stopwatch.Stop();
            Console.WriteLine("\nWhen n = {0}", n[i]);
            Console.WriteLine("The average time to find the Kth smallest value is {0}", Average(stopwatch.Elapsed, Convert.ToInt32(0.2 * n[i])));


        }


        Console.ReadLine();
    }

    public static TimeSpan Average(TimeSpan first, int y)// computes the average times
    {
        return TimeSpan.FromTicks((first).Ticks / y);
    }
}

[Serializable] public class ExplicitHeap : ICloneable //most of this as in question 1
{
    private Node root, insertedNode;
    private int sizeRight, sizeLeft;

    public Node InsertedNode
    {
        get
        {
            return insertedNode;
        }

        set
        {
            insertedNode = value;
        }
    }

    public Object Clone()//A deep copy of the explicit heap used to set it back to default state after all the deletions to find the kth value
    {
        MemoryStream ms = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(ms, this);
        ms.Position = 0;
        object obj = bf.Deserialize(ms);
        ms.Close();
        return obj;
    }

    public void Insert(int item)
    {
        root = Add(item, root);
    }

    private Node Add(int item, Node node)
    {
        if (node == null)
        {
            insertedNode =  new Node(item);
            return insertedNode;

        }

        else
        {
            if(node.LeftChild == null)
            {
                sizeLeft = 0;
            }

            else
            {
                sizeLeft = node.LeftChild.Size;
            }

            if(node.RightChild == null)
            {
                sizeRight = 0;
            }

            else
            {
                sizeRight = node.RightChild.Size;
            }

            if (sizeLeft <= sizeRight)
            {
                node.LeftChild = Add(item, node.LeftChild);
                node.LeftChild.Parent = node;
                ++node.Size;
                return node;
            }

            else
            {
                node.RightChild = Add(item, node.RightChild);
                node.RightChild.Parent = node;
                ++node.Size;
                return node;
            }
        }
    }

    public void Swim(Node node)//reheap up
    {
        int temp;

        while (node.Parent != null && node.Parent.Item > node.Item)
        {
            temp = node.Item;
            node.Item = node.Parent.Item;
            node.Parent.Item = temp;
            node = node.Parent;
        }
    }

    public int Delete(ref ExplicitHeap heap)// returns the root, adjusts the sizes, determines the next lastly inserted node based on sizes and calls sink
    {
        int minValue = heap.root.Item, leftSize, rightSize;
        heap.root.Item = heap.InsertedNode.Item;
        Node node = heap.InsertedNode;

        while (node.Parent != null)// adjusts sizes
        {
            --node.Parent.Size;
            node = node.Parent;
        }

        if (heap.insertedNode.Parent != null)// finds the next last inserted node
        {

            if (heap.InsertedNode.Parent.RightChild == heap.InsertedNode)
            {
                heap.InsertedNode.Parent.RightChild = null;
            }

            else
            {
                heap.InsertedNode.Parent.LeftChild = null;
            }

            while (node.LeftChild != null)
            {
                leftSize = node.LeftChild.Size;

                if (node.RightChild != null)
                {
                    rightSize = node.RightChild.Size;
                }

                else
                {
                    rightSize = 0;
                }

                if (leftSize > rightSize)
                {
                    node = node.LeftChild;
                }

                else
                {
                    node = node.RightChild;
                }
            }

            heap.InsertedNode = node;
        }

        else
        {
            heap.insertedNode = null;
        }

        Sink(heap.root);
 
        return minValue;
    }

    public void Sink(Node node)//reheap down
    {
        int temp, leftItem, rightItem;
        bool sinked = true;

        while(node.LeftChild != null && sinked)
        {
            leftItem = node.LeftChild.Item;

            if(node.RightChild != null)
            {
                rightItem = node.RightChild.Item;
            }

            else
            {
                rightItem = int.MaxValue;
            }

            if(leftItem <= rightItem && leftItem < node.Item)//ties are broken in the favor of the left
            {
                temp = node.Item;
                node.Item = node.LeftChild.Item;
                node.LeftChild.Item = temp;
                node = node.LeftChild;
            }

            else if(node.RightChild != null && rightItem < leftItem && rightItem < node.Item)
            {
                temp = node.Item;
                node.Item = node.RightChild.Item;
                node.RightChild.Item = temp;
                node = node.RightChild;
            }

            else
            {
                sinked = false;
            }
        }
    }
}

[Serializable] public class Node //a node
{
    private Node parent, leftChild, rightChild;
    private int size, item;

    public Node(int item)
    {
        parent = leftChild = rightChild = null;
        size = 1;
        this.item = item;
    }

    public Node Parent
    {
        get
        {
            return parent;
        }

        set
        {
            parent = value;
        }
    }

    public Node LeftChild
    {
        get
        {
            return leftChild;
        }

        set
        {
            leftChild = value;
        }
    }

    public Node RightChild
    {
        get
        {
            return rightChild;
        }

        set
        {
            rightChild = value;
        }
    }

    public int Size//size of a single node is 1 (a node with no children)
    {
        get
        {
            return size;
        }

        set
        {
            size = value;
        }
    }

    public int Item
    {
        get
        {
            return item;
        }

        set
        {
            item = value;
        }
    }
}
