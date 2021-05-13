using System;
namespace Banker
{
    public class Process
    {
        public static int numberOfResources;
        private int[] allocation = new int[numberOfResources];
        private int[] max = new int[numberOfResources];
        private int[] need = new int[numberOfResources];
        private string status;

        public int[] Allocation {
            set { allocation = value; }
            get { return allocation; }
        }
        public int[] Max
        {
            set { max = value; }
            get { return max; }
        }
        public int[] Need
        {
            set { need = value; }
            get { return need; }
        }
        public string Status
        {
            set { status = value; }
            get { return status; }
        }

        public Process()
        {
            status = "active";

        }
        public Process(int[] all , int[] maxin)
        {
            allocation = all;
            max = maxin;
            status = "active";
        }

    }
}
