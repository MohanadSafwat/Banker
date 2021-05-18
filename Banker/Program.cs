using System;
using System.IO;
using System.Text;

namespace Banker
{
    class MainClass
    {
        public static void printNeed(Process[] processes, int numberOfProcesses, int numberOfResources)
        {
            Console.Write("\n");
            Console.Write("\n");

            Console.WriteLine("Need Matrix:");
            Console.WriteLine("\n");
            Console.Write("     ");

            for (int i = 0; i < numberOfResources; i++)
            {
                Console.Write(String.Format("{0,-10}","R" + (i + 1)));

            }
            Console.Write("\n");

            for (int i = 0; i < numberOfProcesses; i++)
            {

                Console.Write("P" + i  + "   ");

                for (int j = 0; j < numberOfResources; j++)
                {
                    Console.Write(String.Format("{0,-10}",processes[i].Need[j] ));


                }


                Console.Write("\n");


            }
            Console.WriteLine("\n");
        }


        public static void Main(string[] args)
        {
            while (true)
            {
                int numberOfProcesses, numberOfResources;

                Console.Write("Please Enter Number Of Processes:");
                numberOfProcesses = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please Enter Number Of Resources:");

                numberOfResources = Convert.ToInt32(Console.ReadLine());

                int numberOfRemainProcesses = numberOfProcesses;

                Process[] processesArr = new Process[numberOfProcesses];
                Resource[] resourcesArr = new Resource[numberOfResources];

                Process.numberOfResources = numberOfResources;

                


                Console.WriteLine("Please Enter Max Matrix:");

            
                for (int i =0; i < numberOfProcesses; i++)
                {
                    string tmp;
                    tmp = Console.ReadLine();
                    int[] maxArr = new int[numberOfResources];
                    Array.Clear(maxArr, 0, numberOfResources);
                    string[] lineValues = tmp.Split(' ');
                    for (int j = 0; j < numberOfResources; j++)
                    {
                         maxArr[j] = Convert.ToInt32(lineValues[j]);
                    }
                    processesArr[i] = new Process();
                    processesArr[i].Max = maxArr;

                }

                Console.WriteLine("Please Enter Allocation Matrix:");
                
                for (int i = 0; i < numberOfProcesses; i++)
                {
                    string tmp;
                    tmp = Console.ReadLine();
                    int[] allArr = new int[numberOfResources];
                    Array.Clear(allArr, 0, numberOfResources);

                    string[] lineValues = tmp.Split(' ');
                    for (int j = 0; j < numberOfResources; j++)
                    {
                        allArr[j] = Convert.ToInt32(lineValues[j]);
                    }
                    processesArr[i].Allocation = allArr;

                }

                for (int i = 0; i < numberOfProcesses; i++)
                {
                    int[] needArr = new int[numberOfResources];
                    Array.Clear(needArr, 0, numberOfResources);


                    for (int j = 0; j < numberOfResources; j++)
                    {
                        needArr[j] = processesArr[i].Max[j] - processesArr[i].Allocation[j];
                    }

                    processesArr[i].Need = needArr;

                }
                int[] availableArr = new int[numberOfResources];

                Console.WriteLine("Please Enter Available Matrix:");

                string tmpAvailable;
                tmpAvailable = Console.ReadLine();
                string[] tmpAvailableArr =tmpAvailable.Split(' ');
                for (int i = 0; i < numberOfResources; i++)
                {
                    availableArr[i] = Convert.ToInt32(tmpAvailableArr[i]);
                    resourcesArr[i] = new Resource(availableArr[i]);
                }

                MainClass.printNeed(processesArr, numberOfProcesses, numberOfResources);

                Console.Write("If you want to Know if the system is safe or not enter 1 and if you you to grant request enter 2: ");

                string q;
                int[] timeLine = new int[numberOfProcesses];
                bool toBeProceed = true;
                bool safeStateBool = true;

                q = Console.ReadLine();
                if (q =="1")
                {
                    while (numberOfRemainProcesses > 0)
                    {


                        if (!safeStateBool) { break; }
                        for (int i = 0; i < numberOfProcesses; i++)
                        {
                            toBeProceed = true;
                            if (processesArr[i].Status != "done")
                            {
                                for (int j = 0; j < numberOfResources; j++)
                                {
                                    if (processesArr[i].Need[j] > resourcesArr[j].Available)
                                    {
                                        toBeProceed = false;
                                        safeStateBool = false;
                                    }

                                }


                                if (toBeProceed)
                                {
                                    for (int j = 0; j < numberOfResources; j++)
                                    {
                                        resourcesArr[j].Available += processesArr[i].Allocation[j];
                                    }
                                    processesArr[i].Status = "done";
                                    timeLine[numberOfProcesses - numberOfRemainProcesses] = i;
                                    numberOfRemainProcesses--;
                                    safeStateBool = true;
                                }
                            }
                        }
                    }

                    if (safeStateBool)
                    {
                        Console.Write("Yes , Safe state <");

                        for (int j = 0; j < numberOfProcesses; j++)
                        {
                            Console.Write("P" + timeLine[j]);
                            if (j < numberOfProcesses - 1)
                                Console.Write(",");

                        }
                        Console.Write(">");

                        Console.Write("\n");

                    }
                    else
                    {
                        Console.WriteLine("No, It is Not Safe.");
                    }
                }

            

                else if (q=="2")
                {
                    int numberOfProccessIR;
                    int[] timeLineIR = new int[numberOfProcesses];
                    int[] allocationIR = new int[numberOfResources];

                    bool toBeProceedIR;
                    bool safeStateBoolIR = true;

                    numberOfRemainProcesses = numberOfProcesses;
                    for (int i = 0; i < numberOfResources; i++)
                    {
                        resourcesArr[i].Available = availableArr[i];
                    }

                    for (int i = 0; i < numberOfProcesses; i++)
                    {
                        processesArr[i].Status = "active";
                    }

                    Console.Write("Enter the number of the process: ");
                    numberOfProccessIR = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please Enter Request Matrix: ");
                    string tmpRequest;
                    tmpRequest = Console.ReadLine();
                    string[] tmpRequestArr= tmpRequest.Split(' ');
                    for (int i = 0; i < numberOfResources; i++)
                    {
                        allocationIR[i] = Convert.ToInt32(tmpRequestArr[i]);
                    }

                    for (int j = 0; j < numberOfResources; j++)
                    {
                        if (allocationIR[j] > processesArr[numberOfProccessIR].Need[j])
                            safeStateBoolIR = false;
                        if (allocationIR[j] > resourcesArr[j].Available)
                            safeStateBoolIR = false;

                        resourcesArr[j].Available -= allocationIR[j];
                        processesArr[numberOfProccessIR].Need[j] -= allocationIR[j];
                        processesArr[numberOfProccessIR].Allocation[j] += allocationIR[j];

                    }



                    while (numberOfRemainProcesses > 0)
                    {


                        if (!safeStateBoolIR) { break; }
                        for (int i = 0; i < numberOfProcesses; i++)
                        {
                            toBeProceedIR = true;
                            if (processesArr[i].Status != "done")
                            {
                                for (int j = 0; j < numberOfResources; j++)
                                {
                                    if (processesArr[i].Need[j] > resourcesArr[j].Available)
                                    {
                                        toBeProceedIR = false;
                                        safeStateBoolIR = false;
                                    }

                                }


                                if (toBeProceedIR)
                                {
                                    for (int j = 0; j < numberOfResources; j++)
                                    {
                                        resourcesArr[j].Available += processesArr[i].Allocation[j];
                                    }
                                    processesArr[i].Status = "done";
                                    timeLineIR[numberOfProcesses - numberOfRemainProcesses] = i;
                                    numberOfRemainProcesses--;
                                    safeStateBoolIR = true;
                                }
                            }
                        }
                    }

                    if (safeStateBoolIR)
                    {
                        Console.Write("Yes request can be granted with safe state, Safe state <");
                        Console.Write("P"+ numberOfProccessIR+"req,");

                        for (int j = 0; j < numberOfProcesses; j++)
                        {
                            Console.Write("P" + timeLineIR[j]);

                            //if (timeLineIR[j] == numberOfProccessIR)
                            //    Console.Write("req");
                            if (j < numberOfProcesses - 1)
                                Console.Write(",");

                        }
                        Console.Write(">");
                        Console.Write("\n");


                    }
                    else
                    {
                        Console.WriteLine("No your request can't be granted.");

                    }
                }
                else
                {
                    Console.WriteLine("Wrong Input.");
                }


            }
        }
    }
}