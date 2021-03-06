﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using com.rhfung.P2PDictionary;

namespace TestP2PDict
{
    class Program
    {
        static void Main(string[] args)
        {
            // FileStream fs = new FileStream("setup-jammanhci-2.3.exe", FileMode.Open);
            //byte[] buffer = new byte[fs.Length];
            //fs.Read(buffer, 0, (int)fs.Length);
            com.rhfung.P2PDictionary.P2PDictionary port80 = new P2PDictionary("port 80", 80, "test", P2PDictionaryServerMode.Hidden, P2PDictionaryClientMode.ManualConnect);
            port80.SetDebugBuffer(new StreamWriter("output-80.txt"), 0, true);

            //port80.AddSubscription("*");
            port80["hidden"] = "Server at 80";
            port80["junk"] = "junk junk junk";

            com.rhfung.P2PDictionary.P2PDictionary port81 = new P2PDictionary("port 81", 81, "test", P2PDictionaryServerMode.Hidden, P2PDictionaryClientMode.ManualConnect);
            //client.StartClient("127.0.0.1", 80);
            port81.DebugBuffer = new StreamWriter("output-81.txt");
            port81["hidden"] = "Server at 81";
            port81["number"] = 1;
            port81["text"] = "Hello world!";

            com.rhfung.P2PDictionary.P2PDictionary port82 = new P2PDictionary("port 82", 82, "test", P2PDictionaryServerMode.Hidden, P2PDictionaryClientMode.ManualConnect);
            port82.DebugBuffer = new StreamWriter("output-82.txt");
            port81["hidden"] = "Server at 82";
            port82["ohayo"] = "o-ha-yo go-za-i-ma-su";
            port82["binary"] = new byte[] { 88, 88, 77, 77, 88, 88 };
            port82["bool"] = true;

            com.rhfung.P2PDictionary.P2PDictionary port83 = new P2PDictionary("port 83", 83, "test", P2PDictionaryServerMode.Hidden, P2PDictionaryClientMode.ManualConnect);
            port83.DebugBuffer = new StreamWriter("output-83.txt");
            port83["hidden"] = "Server at 83";
            
            //server3.AddSubscription("*");


            P2PDictionary port92 = new P2PDictionary("port 92", 92, "test", P2PDictionaryServerMode.Hidden, P2PDictionaryClientMode.ManualConnect);
            port92.DebugBuffer = new StreamWriter("output-92.txt");
            // stuck.AddSubscription("*");
            port92.AddSubscription("number");

            /*
            FormPeerOverview form1 = new FormPeerOverview();
            form1.Show();
            form1.SetServer(server);

            FormPeerOverview form2 = new FormPeerOverview();
            form2.Show();
            form2.SetServer(client);

            FormPeerOverview form3 = new FormPeerOverview();
            form3.Show();
            form3.SetServer(client2);

            FormPeerOverview form4 = new FormPeerOverview();
            form4.Show();
            form4.SetServer(server3);

            FormPeerOverview form5 = new FormPeerOverview();
            form5.Show();
            form5.SetServer(stuck);

            System.Console.WriteLine("Press space bar to quit");

            System.Windows.Forms.Application.Run(form1);
             */

            port81.OpenClient(System.Net.IPAddress.Loopback, 80);
            port82.OpenClient(System.Net.IPAddress.Loopback, 81);
            port83.OpenClient(System.Net.IPAddress.Loopback, 82);
            port92.OpenClient(System.Net.IPAddress.Loopback, 83);
            port92.OpenClient(System.Net.IPAddress.Loopback, 3333);

            Console.WriteLine("debug peers 80-83, 92 using a web browser and request /data");
            Console.WriteLine("press space bar to quit or any other key for write/delete on port 82");
            ConsoleKeyInfo k;
            do
            {
                 k = System.Console.ReadKey();

                 port82["junk"] = "new junk";
                 port82.Remove("junk");
            }
            while (k.KeyChar != ' ');

            var stuff = port80.GetEnumerator();

            // break connection and modify keys
            Console.WriteLine("wrote hidden key in peer 80, press any key to quit");
            port83.Close();
            port80["hidden"] = "won't make it to 92";
            System.Console.ReadKey();


            System.Console.WriteLine("Stopping...");
            port82.Close();
            port83.Close();
            port81.Close();
            port80.Close();
            port92.Close();

            port80.DebugBuffer.Flush();
            port81.DebugBuffer.Flush();
            port82.DebugBuffer.Flush();
            port83.DebugBuffer.Flush();
            port92.DebugBuffer.Flush();
        }
    }
}
