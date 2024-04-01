//Nika Shahbeyk_Shiraz University Of Technology_OS lab_Tamrin 1_StudentID: 400213013
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;


class Program
{
    static void Main(String[] args)
    {
        while(true)
        {
            Console.WriteLine("Please Select one of the options: ");
            Console.WriteLine("1. Run(Start) a Process.");
            Console.WriteLine("2. List all running processes");
            Console.WriteLine("3. Kill a process.");
            Console.WriteLine("4. Get parent of the process. ");
            Console.WriteLine("5. Exit.");


            //we use an integer for choice of user
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please Try Again!");
                continue;
            }

            if(choice == 5)
            {
                Console.WriteLine("See you again!");
                break;
            }

            //we use a switch
            switch(choice)
            {
                //if user select 1 -> go to Start_Process Function
                case 1:
                 Start_Process();
                 break;

               //if user select 2 -> go to List_all_running_processes Function
                case 2:
                 List_all_running_processes();
                 break;

               //if user select 3 -> go to Kill_a_process Function
                case 3:
                 Kill_a_process();
                 break;

              //if user select 4 -> go to Get_Parent Function
                case 4:
                 Get_Parent();
                 break;

                default:
                 Console.WriteLine("invalid input! you did not enter a valid input! please try again!!");
                 break;
            }
        }
    }

    //a Function for starting a process
    static void Start_Process()
    {
        Console.WriteLine("You Entered Choice Number 1");

        //you can use path or name: because sometimes we need to enter the complete path
        Console.WriteLine("Please Enter Path or name of the Program: ");
        string File = Console.ReadLine();

        if(File!=null)
        {
        try
        {
            Process.Start(File);
            Console.WriteLine("Process Started!");

        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        }
    }

    //a Function for showing list all running processes
    static void List_all_running_processes()
    {
        Process[] processes = Process.GetProcesses();

        Console.WriteLine("List of running processes are:");
        foreach (Process process in processes)
        {
            Console.WriteLine($"Process_Name is: {process.ProcessName}, PID is: {process.Id}");
        }
    }

    
    //a Function for killing a process
    static void Kill_a_process()
    {
        Console.WriteLine("Please enter the PID of process, if you do not know go to choice 2: ");
        int ID;

        if (!int.TryParse(Console.ReadLine(), out ID))
        {
            Console.WriteLine("Invalid input. Please enter a valid input: ");
            return;
        }
         try
        {
            Process process = Process.GetProcessById(ID);
            process.Kill();
            Console.WriteLine($"Process killed.");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Process with this PID not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Message: {ex.Message}");
        }
    }

    //a Function for getting parent
    static void Get_Parent()
    {
        int proID;
        Console.WriteLine("Please Enter the id of the process that you want to know parent: ");
        if (!int.TryParse(Console.ReadLine(), out proID))
        {
            Console.WriteLine("Invalid input. Please enter a valid input: ");
            return;
        }
        
        //making process
        Process process;

        try
        {
            process = Process.GetProcessById(proID);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error Message: {ex.Message}");
            return;
        }

        var performanceCounter = new PerformanceCounter("Process", "Creating Process ID", process.ProcessName);
        var parent = GetProcess((int)performanceCounter.RawValue);
        Console.WriteLine("Process: ((process name: {0}) (process id: {1})) Parent: ((Parent name: {2}) (Parent id: {3}))",process.ProcessName, process.Id, parent.ProcessName, parent.ProcessId);
    }

    static Process1 GetProcess(int pid)
    {
      try
      {
        var pr = Process.GetProcessById(pid);
        return new Process1() 
        {
             ProcessId = pr.Id, ProcessName = pr.ProcessName 
        };
       }
      catch (ArgumentException ex)
      {
        Console.WriteLine($"Error Message: {ex.Message}");
        throw; // Re-throw the exception
      }
    }


    struct Process1
    {
      public int ProcessId;
      public string ProcessName;
    }


}