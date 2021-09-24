using System;

namespace Rain
{
    class Program
    {
        static void Main(string[] args)
        {



            //Drop anc = new Drop();
            //Drop newdrop = Drop.MakeADrop();
            //anc.next = newdrop;
            //
            //newdrop.next = Drop.MakeADrop();
            //newdrop = newdrop.next;
            //newdrop.next = Drop.MakeADrop();
            //newdrop = newdrop.next;
            //newdrop.next = Drop.MakeADrop();
            //newdrop = newdrop.next;
            //newdrop.next = Drop.MakeADrop();
            //newdrop = newdrop.next;
            //Drop.ShowDropList(anc);


            //break; only breaks inner loop




            Run();

        }

   

        public static void Run()
        {
            Drop anchor = new Drop();
            Drop runner = anchor;
            anchor.next = Drop.MakeADrop();

            int c = 1;
            while (!(runner.next.start == 1 && runner.next.end == 100))//here runner.next is anchor.next
            {
                Drop newDrop = Drop.MakeADrop();
                c++;
                Console.WriteLine(newDrop.start + " --> " + newDrop.end);

                while (runner != null)
                {

                    //if (anchor.next == null)//first drop exists??
                    //{
                    //    anchor.next = newDrop;
                    //    break;
                    //}

                    
           /*else*/if (newDrop.start >= runner.start && newDrop.end <= runner.end)//if swallowed by runner
                    {
                        break;
                    }
                    else if (newDrop.start <= runner.start && newDrop.end >= runner.end)//if swallows runner
                    {
                        runner.start = newDrop.start;
                        runner.end = newDrop.end;
                        break;
                    }
                    else if (newDrop.start <= runner.start && newDrop.end <= runner.end)//if connects at runner's start
                    {
                        runner.start = newDrop.start;
                        break;
                    }
                    else if (newDrop.start <= runner.end && newDrop.end >= runner.end)//if connects at runner's end 
                    {
                        runner.end = newDrop.end;
                        break;
                    }
                    else if (runner.next != null)
                    {
                        if (newDrop.start > runner.end && newDrop.end < runner.next.start)//if fits between, without fusion
                        {
                            newDrop.next = runner.next;
                            runner.next = newDrop;
                            break;
                        }
                    }
                    else if (runner.next == null && newDrop.start > runner.end)
                    {
                        newDrop.next = runner.next;
                        runner.next = newDrop;
                        break;
                    }

                    runner = runner.next; //don't remove                   
                }
                runner = anchor;
                while (runner.next != null)
                {
                    if (runner.next.start <= runner.end) //close untied tails
                    {
                        runner.end = runner.next.end;
                        runner.next = runner.next.next;
                        break;
                    }
                    runner = runner.next;
                }
                runner = anchor;
            }

            Console.WriteLine();
            Drop.ShowDropList(anchor);
            Console.WriteLine("number of drops: " + c);
        }

    }




    //-----Class Drop
    public class Drop
    {
        public Drop()
        {
            this.value = -1;
            this.start = -1;
            this.end = -1;
        }
        public Drop(float value)
        {
            this.value = value;
        }
        public Drop(float start, float end)
        {
            this.start = start;
            this.end = end;
        }
        public Drop(Drop next, Drop preDrop, float start, float end)
        {
            this.next = next;
            this.preDrop = preDrop;
            this.start = start;
            this.end = end;
        }

        public Drop next { get; set; }
        public Drop preDrop { get; set; }
        public float value { get; set; }
        public float start { get; set; }
        public float end { get; set; }
        //---------------Class Drop methods----------------

        public static Drop MakeADrop()
        {
            Random random = new Random();
            float num = (float)random.NextDouble() * (100 - 1) + 1; //1-100

            Drop drop = new Drop(num);
            if (num - 2 < 1)
                drop.start = 1;
            else drop.start = num - 2;
            if (num + 2 > 100)
                drop.end = 100;
            else drop.end = num + 2;

            //drop.value = (drop.end +drop.start)/ 2;

            return drop;
        }
        public static void ShowDropList(Drop anchor)
        {
            Drop Node = anchor; //its a reference, not a value
            int count = 0;
            while (Node.next != null)
            {
                Console.WriteLine("[" + count + "]");
                //Console.WriteLine("drop point: " + Node.next.value);
                Console.WriteLine("start: [" + Node.next.start + "] --> end: [" + Node.next.end + "]");
                Node = Node.next;
                count++;
            }
        }

    }



    public static class Methods //Not in use
    {



        public static Drop FuseDrops(Drop drop1, Drop drop2)
        {
            if (drop1.start < 0 || drop1.end < 0 || drop2.start < 0 || drop2.end < 0)
                throw new Exception("Nodes don't meet the demanded terms: start/end are smaller than 0");


            float[] arr = { drop1.start, drop1.end, drop2.start, drop2.end };
            float small = 0, big = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] >= arr[i - 1])
                    big = arr[i];
                if (arr[i] < arr[i - 1])
                    small = arr[i];
            }
            Drop drop = new Drop(small, big);
            return drop;

        }
    }

}