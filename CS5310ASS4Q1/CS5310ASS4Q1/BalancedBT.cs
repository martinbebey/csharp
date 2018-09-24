using System;
using System.Diagnostics;

    public class BalancedBT
    {
        private static int[] n;

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
            int numberOfNValues = 7, i, j, randomNumber, insertIndex;
            n = new int[numberOfNValues];
            Stopwatch implicitStopwatch, explicitStopwatch, implicitTotalTime = new Stopwatch(), explicitTotalTime = new Stopwatch();
            ImplicitHeap implicitHeap;
            ExplicitHeap explicitHeap;
            Random random = new Random();

            for (i = 0; i < numberOfNValues; ++i)
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
                
                implicitHeap = new ImplicitHeap(i);
                explicitHeap = new ExplicitHeap();
                implicitStopwatch = new Stopwatch();
                explicitStopwatch = new Stopwatch();

                for(j = 0; j < n[i]; ++j)//insert n values in each representation and time it
                {
                    randomNumber = random.Next(1, ((5 * n.Length) + 1));//between 1 and 5n inclusive

                    
                    implicitStopwatch.Start();
                    implicitTotalTime.Start();
                    insertIndex = implicitHeap.Insert(randomNumber);
                    implicitHeap.Swim(insertIndex - 1);
                    implicitStopwatch.Stop();
                    implicitTotalTime.Stop();
                   

                    explicitStopwatch.Start();
                    explicitTotalTime.Start();
                    explicitHeap.Insert(randomNumber);
                    explicitHeap.Swim(explicitHeap.InsertedNode);
                    explicitStopwatch.Stop();
                    explicitTotalTime.Stop();
                }

                Console.WriteLine("\nWhen n = {0}", n[i]);
                Console.WriteLine("Running time for array based heap is {0}", implicitStopwatch.Elapsed);
                Console.WriteLine("Running time for explicit tree based heap is {0}", explicitStopwatch.Elapsed);

            }

            Console.WriteLine("\nAverage implicit running time is {0}", Average(implicitTotalTime.Elapsed));
            Console.WriteLine("Average explicit running time is {0}", Average(explicitTotalTime.Elapsed));
            Console.ReadLine();    

        }

        public static TimeSpan Average(TimeSpan first)
        {
            return TimeSpan.FromTicks((first).Ticks / 7);
        }

        public static string Average(string first)
        {
            TimeSpan firstSpan = TimeSpan.Parse(first);
            //TimeSpan secondSpan = TimeSpan.Parse(second);
            return Average(firstSpan).ToString();
        }
    }

public class ImplicitHeap
{
        private BalancedBT balancedTree = new BalancedBT();
        private int nextEmpty = 0;
        private int[] arrayBasedHeap;

        public ImplicitHeap(int i)
        {
            arrayBasedHeap = new int[balancedTree.N[i]];
            //arrayBasedHeap[0] = -1; //when tree is empty
        }

        public int Insert(int item)
        {
            return Add(item);
        }

        private int Add(int item)
        {
            if (arrayBasedHeap[nextEmpty] == 0)
            {
                arrayBasedHeap[nextEmpty] = item;
                ++nextEmpty;
            }

           /* else if (arrayBasedHeap[(2 * index) + 1] == 0)
            {
                arrayBasedHeap[(2 * index) + 1] = item;
            }

            else if (arrayBasedHeap[(2 * index) + 2] == 0)
            {
                Add(item, (2 * index) + 2);
            }*/

            return nextEmpty;
        }

        public void Swim(int index)
        {
            int temp;

            //for parent take upper limit of index and -1
            while (index > 0 && arrayBasedHeap[Convert.ToInt32(Math.Ceiling(Convert.ToDouble(index) / 2)) - 1] > arrayBasedHeap[index])
            {
                temp = arrayBasedHeap[Convert.ToInt32(Math.Ceiling(Convert.ToDouble(index) / 2)) - 1];
                arrayBasedHeap[Convert.ToInt32(Math.Ceiling(Convert.ToDouble(index) / 2)) - 1] = arrayBasedHeap[index];
                arrayBasedHeap[index] = temp;
                index = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(index) / 2)) - 1;
            }
        }

}

public class ExplicitHeap
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

    public void Swim(Node node)
    {
        int temp;
        //Node tempNode = node;

        while (node.Parent != null && node.Parent.Item > node.Item)
        {
            temp = node.Item;
            node.Item = node.Parent.Item;
            node.Parent.Item = temp;
            node = node.Parent;
        }
    }
}

public class Node
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

    public int Size//size of a single node is 1
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
