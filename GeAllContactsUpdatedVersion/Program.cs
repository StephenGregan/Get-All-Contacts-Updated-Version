using GeAllContactsUpdatedVersion;
//using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using static ConsoleAppMainProject.Json;

namespace GetAllContactsFromInterpreterIntelligence
{

    class Program
    {
        static void Main(String[] args)
        {
            LoginAndDeserailizeJson();

            //continueLoopItteration();
        }

        //private static void continueLoopItteration()
        //{

        //}

        static void LoginAndDeserailizeJson()
        {
            using (var client = new WebClientEx())
            {
                var values = new NameValueCollection
                {
                    { "j_username", "" },
                    { "j_password", "" },
                };
                Console.WriteLine("Trying to validate username and password.....\n");

                client.UploadValues("https://tie.interpreterintelligence.com/j_spring_security_check", values);

                Console.WriteLine("Successfully logged in to ii.....\n");

                var recordCount = 10400;
                var count = 0;
                var recSkipped = false;

                for (var i = 0; i < recordCount; i++)
                {
                    try
                    {

                        if (count < 11000)
                        {
                            if (recSkipped == true)
                            {
                                recSkipped = false;
                            }
                            else
                            {
                                count++;
                            }
                            var json = client.DownloadString("https://tie.interpreterintelligence.com:443/api/contact/" + count);
                            dynamic rootjson = JsonConvert.DeserializeObject(json);
                            //Console.WriteLine(json);
                            File.AppendAllText(@"C:\Users\Ronan\source\repos\GeAllContactsUpdatedVersion\GeAllContactsUpdatedVersion\allContacts.json", JsonConvert.SerializeObject(rootjson));
                            File.AppendAllText(@"C:\Users\Ronan\source\repos\GeAllContactsUpdatedVersion\GeAllContactsUpdatedVersion\allContacts.json", Environment.NewLine);
                            Console.WriteLine("Contents successfully writing to file.....Record: " + count + " Batch 1");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(count + " : Record not found.....");
                        count++;
                        recSkipped = true;
                    }
                }
            }
        }
    }
}