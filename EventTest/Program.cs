using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Timers;

namespace SimulatorNILM
{
    class Events
    {
        static HttpClient client = new HttpClient();
      // public static string sas = "SharedAccessSignature sr=https%3a%2f%2fnilmazure.servicebus.windows.net%2fnilmazurehub%2fpublishers%2fevents%2fmessages&sig=KzZHdkBR7LbYwyTVl%2b%2fLe4d8vw%2fIw2XptMw4a5UlFNY%3d&se=1522391027&skn=RootManageSharedAccessKey";


        //New account

        public static string sas = "SharedAccessSignature sr=https%3a%2f%2feanilmeventhub.servicebus.windows.net%2fnilmazurehub%2fpublishers%2fevents%2fmessages&sig=fcw6%2frVv1fX1uzgqvZmDqBWsihnk60IUftVqI8qofHU%3d&se=1525409377&skn=RootManageSharedAccessKey";
        // Namespace info.
         public static string serviceNamespace = "eanilmeventhub";
      // public static string serviceNamespace = "nilmazure";
        public static string hubName = "nilmazurehub";
        public static string deviceId = "events";

        public static string data;
        public static string DeviceID;
        public static string Token;
        public static int hour1;
        public static int hour2;
        public static int min1;
        public static int min2;
        public static int sec1;
        public static int sec2;
        public static int date1;
        public static int date2;
        public static int month1;
        public static int month2;

        public static string FromState1;
        public static string ToState1;
        public static string FromStaten;
        public static string ToStaten;
        public static string delimeter1;
        public static string delimeter2;

        public static string generateRandomnTimestamp1;
        public static string generateRandomnTimestamp2;
        public static string generateRandomApplianceID;
        public static string year;
        public static string[] ApplianceID = new string[7] { "020302b60301", "04030dc80001", "020b0a460502", "02021eee0201", "020604320001", "010209b00401", "02071f400601" };
        public static void RandomHexString()
        {
            Random rnd = new Random();
            data = "646174613d";
            DeviceID = "3030303030303434";
            Token = "513553726b6b62594655454279627555";
            delimeter1 = "7c";
            delimeter2 = "7e";
            year = "e107";
            FromState1 = "00";
            FromStaten = "01";
            ToState1 = "01";
            ToStaten = "00";
            data = data.Trim();
            year = year.Trim();
            var randomIndex = rnd.Next(0, ApplianceID.Length);
            generateRandomApplianceID = ApplianceID[randomIndex];

            hour1 = rnd.Next(1, 12);
            hour2 = rnd.Next(1, 12);
            while (hour1 < hour2)
            {
                hour1 = rnd.Next(1, 12);
                hour2 = rnd.Next(1, 12);
            }

            min1 = rnd.Next(01, 60);
            sec1 = rnd.Next(01, 60);
            date1 = rnd.Next(1, 31);
            month1 = rnd.Next(1, 12);

            min2 = rnd.Next(01, 60);
            sec2 = rnd.Next(01, 60);
            date2 = rnd.Next(1, 31);
            month2 = rnd.Next(1, 12);

            setDateTimeData();
            var hour1Hex = hour1.ToString("X2");
            var min1Hex = min1.ToString("X2");
            var sec1Hex = sec1.ToString("X2");
            var date1Hex = date1.ToString("X2");
            var month1Hex = month1.ToString("X2");
            

            var hour2Hex = hour2.ToString("X2");
            var min2Hex = min2.ToString("X2");
            var sec2Hex = sec2.ToString("X2");
            var date2Hex = date2.ToString("X2");
            var month2Hex = month2.ToString("X2");

            generateRandomnTimestamp1 = string.Concat(hour1Hex, min1Hex, sec1Hex, date1Hex, month1Hex);
            generateRandomnTimestamp2 = string.Concat(hour2Hex, min2Hex, sec2Hex, date2Hex, month2Hex);
            Console.WriteLine("Data Frame");
            Console.WriteLine("Data:" + data);
            Console.WriteLine("Device ID:" + DeviceID);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("Token:" + Token);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("Timestamp1:" + generateRandomnTimestamp1 + year);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("ApplianceID:" + generateRandomApplianceID);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("FromState:" + FromState1);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("ToState:" + ToState1);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("Timestamp2:" + generateRandomnTimestamp2 + year);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("ApplianceID:" + generateRandomApplianceID);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("FromState:" + FromStaten);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("ToState:" + ToStaten);
            Console.WriteLine("\n");
            var hex = string.Concat(data, DeviceID, delimeter1, Token, delimeter1, generateRandomnTimestamp1, year, delimeter1, generateRandomApplianceID, delimeter2, FromState1, delimeter2, ToState1, delimeter1, generateRandomnTimestamp2, year, delimeter1, generateRandomApplianceID, delimeter2, FromStaten, delimeter2, ToStaten);
            Console.WriteLine("HEX VALUE:");
            Console.WriteLine(hex.Length);
            Console.WriteLine(hex);

            var dataToSend = new ByteArrayContent(HexStringToByteArray(hex));
            var url = string.Format("{0}/publishers/{1}/messages", hubName, deviceId);

            // Create client.
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(string.Format("https://{0}.servicebus.windows.net/", serviceNamespace))
            };

            // var payload = JsonConvert.SerializeObject(deviceTelemetry);

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", sas);

           // var content = new StringContent(hex, Encoding.UTF8, "application/octet-stream");

            dataToSend.Headers.Add("ContentType", "application/octet-stream");

            var status = httpClient.PostAsync(url, dataToSend).Result;

            Console.WriteLine(status.ReasonPhrase);

        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        public static void Main(string[] args)
        {
            for (int i = 0; i < 50; i++)
            {
                RandomHexString();
                Thread.Sleep(2000);
            }
        }

        public static void setDateTimeData()
        {
            DateTime currentdate = DateTime.Now;

            hour1 = currentdate.Hour;
            hour2 = currentdate.Hour;
            min1 = currentdate.Minute;
            sec1 = currentdate.Second;
            date1 = currentdate.Day;
            month1 = currentdate.Month;
            date2 = date1;
            month2 = month1;

            currentdate.AddMinutes(30);
            currentdate.AddSeconds(30);

            min2 = currentdate.Minute;
            sec1 = currentdate.Second;


        }
    }
}