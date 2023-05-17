using CPaaS_Software_Test.Services;
using Microsoft.Extensions.Configuration;

namespace CPaaS_Software_Test;

using System;
using System.Timers;

class Program
{
    static Timer timer;
    static MessageService _messageService = new MessageService();
    private readonly IConfiguration _configuration;


    static void Main(string[] args)
    {
        schedule_Timer();
        Console.ReadLine();
    }

    public Program(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Timer that reactivates every 24 hours at 14:00 PM
    static void schedule_Timer()
    {
        Console.WriteLine("### Timer Started ###");

        DateTime nowTime = DateTime.Now;
        DateTime scheduledTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 14, 0, 0, 0);

        if (nowTime > scheduledTime)
        {
            scheduledTime = scheduledTime.AddDays(1);
        }

        double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
        timer = new Timer(tickTime);
        timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        timer.Start();
    }

    static void timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Console.WriteLine("### Timer Stopped ### \n");
        timer.Stop();
        Console.WriteLine("### Scheduled Task Started ### \n\n");
        Console.WriteLine("Performing scheduled task\n");
        RunTasks();
        Console.WriteLine("### Task Finished ### \n\n");
        schedule_Timer();
    }

    // Method to run task to send SMS
    static void RunTasks()
    {
        _messageService.SmsSender(GetApikey());
    }

    // Getting apikey from appsettings.json using configurationbuilder
    static string GetApikey()
    {
        IConfigurationBuilder builder =
            new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
        IConfigurationRoot root = builder.Build();
        return root["apikey"];
    }
}