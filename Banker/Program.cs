using System;

namespace Banker
{
    class MainClass
    {
        public static void printNeed(Process[] processes , int numberOfProcesses, int numberOfResources) {
            Console.Write("\n");
            Console.Write("\n");

            Console.WriteLine("##########################");
            Console.WriteLine("####### NEED METRIX ######");
            Console.Write("   ");

            for (int i = 0; i < numberOfResources; i++)
            {
                Console.Write("R" + (i+1)+" ");

            }
            Console.Write("\n");

            for (int i = 0; i < numberOfProcesses; i++)
            {

                Console.Write("P" + (i+1)+" ");

                for (int j = 0; j < numberOfResources; j++)
                {
                    Console.Write( processes[i].Need[j]+ " ");


                }

                Console.Write("\n");


            }
            Console.WriteLine("##########################");
        }


        public static void Main(string[] args)
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

            for (int i = 0; i < numberOfProcesses; i++)
            {
                int[] allocationArr = new int[numberOfResources];
                int[] maxArr = new int[numberOfResources];
                Array.Clear(allocationArr, 0, numberOfResources);
                Array.Clear(maxArr, 0, numberOfResources);

                for (int j = 0; j < numberOfResources; j++)
                {
                    Console.Write("Please Enter Allocation Of Resource " + j  + " Of Process " + i  + ":");
                    allocationArr[j] = Convert.ToInt32(Console.ReadLine());



                }

                for (int j = 0; j < numberOfResources; j++)
                {


                    Console.Write("Please Enter Max Of Resource " + j + " Of Process " + i  + ":");
                    maxArr[j] = Convert.ToInt32(Console.ReadLine());
                }


                processesArr[i] = new Process(allocationArr, maxArr);

            }
            //processesArr[0] = new Process();
            //processesArr[1] = new Process();
            //processesArr[2] = new Process();
            //processesArr[3] = new Process();
            //processesArr[4] = new Process();

            //resourcesArr[0] = new Resource();
            //resourcesArr[1] = new Resource();
            //resourcesArr[2] = new Resource();

            //processesArr[0].Allocation[0] = 0 ;
            //processesArr[0].Allocation[1] = 1;
            //processesArr[0].Allocation[2] = 0;
            //processesArr[1].Allocation[0] = 2;
            //processesArr[1].Allocation[1] = 0;
            //processesArr[1].Allocation[2] = 0;
            //processesArr[2].Allocation[0] = 3;
            //processesArr[2].Allocation[1] = 0;
            //processesArr[2].Allocation[2] = 2;
            //processesArr[3].Allocation[0] = 2;
            //processesArr[3].Allocation[1] = 1;
            //processesArr[3].Allocation[2] = 1;
            //processesArr[4].Allocation[0] = 0;
            //processesArr[4].Allocation[1] = 0;
            //processesArr[4].Allocation[2] = 2;

            //processesArr[0].Max[0] = 7;
            //processesArr[0].Max[1] = 5;
            //processesArr[0].Max[2] = 3;
            //processesArr[1].Max[0] = 3;
            //processesArr[1].Max[1] = 2;
            //processesArr[1].Max[2] = 2;
            //processesArr[2].Max[0] = 9;
            //processesArr[2].Max[1] = 0;
            //processesArr[2].Max[2] = 2;
            //processesArr[3].Max[0] = 2;
            //processesArr[3].Max[1] = 2;
            //processesArr[3].Max[2] = 2;
            //processesArr[4].Max[0] = 4;
            //processesArr[4].Max[1] = 3;
            //processesArr[4].Max[2] = 3;

            //resourcesArr[0].Available = 3;
            //resourcesArr[1].Available = 3;
            //resourcesArr[2].Available = 2;

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
            for (int i = 0; i < numberOfResources; i++)
            {


                int avl;
                Console.Write("Please Enter Available Of Resource " + i + ":");
                availableArr[i] = Convert.ToInt32(Console.ReadLine());
                resourcesArr[i] = new Resource(availableArr[i]);
            }

            MainClass.printNeed(processesArr, numberOfProcesses, numberOfResources);

            Console.Write("Do you want to enquiry if the system is in a safe state: ");

            string q1Answer;
            int[] timeLine = new int[numberOfProcesses];
            bool toBeProceed = true;
            bool safeStateBool = true;

            q1Answer = Console.ReadLine();
            if (q1Answer == "yes" || q1Answer == "y" || q1Answer == "Yes" || q1Answer == "YES" || q1Answer == "Y")
            {
                while (numberOfRemainProcesses > 0) {


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
                        if(j<numberOfProcesses-1)
                            Console.Write(",");

                    }
                    Console.Write(">");

                }
            }

            string q2Answer;
            int numberOfProccessIR;
            int[] timeLineIR = new int[numberOfProcesses];
            int[] allocationIR = new int[numberOfResources];

            bool toBeProceedIR;
            bool safeStateBoolIR = true;
            Console.Write("\n");
            Console.Write("Do you want to enquiry if a certain immediate request by one of the processes can be granted: ");

            q2Answer = Console.ReadLine();
            if (q2Answer == "yes" || q2Answer == "y" || q2Answer == "Yes" || q2Answer == "YES" || q2Answer == "Y")
            {
                numberOfRemainProcesses = numberOfProcesses;
                for (int i = 0; i < numberOfResources; i++)
                {
                    resourcesArr[i].Available = availableArr[i];
                }

                for (int i =0; i< numberOfProcesses;i++)
                {
                    processesArr[i].Status = "active";
                }

                Console.Write("Enter the number of the process: ");
                numberOfProccessIR = Convert.ToInt32(Console.ReadLine());

                for (int j = 0; j < numberOfResources; j++)
                {
                    Console.Write("Please Enter Allocation Of Resource " + j  + ":");
                    allocationIR[j] = Convert.ToInt32(Console.ReadLine());
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
                    Console.Write("Yes request can be granted with safe state, Safe state < ");

                    for (int j = 0; j < numberOfProcesses; j++)
                    {
                        Console.Write("P" + timeLineIR[j]);

                        if (timeLineIR[j] ==  numberOfProccessIR)
                            Console.Write("req");
                        if (j < numberOfProcesses - 1)
                            Console.Write(",");

                    }
                    Console.Write(">");

                }
            }


        }
    }
}
