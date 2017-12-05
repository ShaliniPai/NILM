using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace SimulatorNILM
{
    class Program
    {
        //creates a new instance of HttpClient class
        static HttpClient client = new HttpClient();

       // public static string  sas = "SharedAccessSignature sr=https%3a%2f%2fnilmazure.servicebus.windows.net%2fnilmazurehub%2fpublishers%2falarms%2fmessages&sig=gCCnuyCwzuEswfaud6lN2zgpkseHnSQSGlrN3bab3EQ%3d&se=1522391111&skn=RootManageSharedAccessKey";

        //New account

         public static string sas = "SharedAccessSignature sr=https%3a%2f%2feanilmeventhub.servicebus.windows.net%2fnilmazurehub%2fpublishers%2falarms%2fmessages&sig=8OmedhFg7rwrJQ%2b%2fs%2fDaB5950U%2fT4wOvxgLBM4aHNYk%3d&se=1525409210&skn=RootManageSharedAccessKey";
        // Namespace info.
        public static string serviceNamespace = "eanilmeventhub";
       // public static string serviceNamespace = "nilmazure";
        public static string hubName = "nilmazurehub";
        public static string deviceId = "alarms";

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
        public static string delimeter1;
        public static string delimeter2;
        public static int[] AlarmID = new int[20] { 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
        public static string generateRandomnTimestamp1;
        public static string generateRandomnTimestamp2;
        public static int generateRandomnAlarm;
        public static string year;

        public static void RandomHexString()
        {
            Random rnd = new Random();
            data = "646174613d";
            DeviceID = "3030303030303434";
            Token = "513553726b6b62594655454279627555";
            delimeter1 = "7c";
            delimeter2 = "7e";
            year = "e107";
            data = data.Trim();
            year = year.Trim();
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

            min2 = rnd.Next(00, 60);
            sec2 = rnd.Next(00, 60);
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

            var randomIndex = rnd.Next(0, AlarmID.Length);
            generateRandomnAlarm = AlarmID[randomIndex];
            var generateRandomnAlarmHex = generateRandomnAlarm.ToString("X2");

            Console.WriteLine("Data Frame");
            Console.WriteLine("Data:" + data);
            Console.WriteLine("Device ID:" + DeviceID);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("Token:" + Token);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("Timestamp1:" + generateRandomnTimestamp1 + year);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("AlarmID1:" + generateRandomnAlarm);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("Timestamp2:" + generateRandomnTimestamp2 + year);
            Console.WriteLine("AlarmID2:" + generateRandomnAlarm);
            Console.WriteLine("\n");
            var hex = string.Concat(data, DeviceID, delimeter1, Token, delimeter1, generateRandomnTimestamp1, year, delimeter2, generateRandomnAlarmHex, delimeter1, generateRandomnTimestamp2, year, delimeter2, generateRandomnAlarmHex);
            Console.WriteLine("HEX VALUE:");
            Console.WriteLine(hex);
            Console.WriteLine(hex.Length);

            var dataToSend = new ByteArrayContent(HexStringToByteArray(hex));
            var url = string.Format("{0}/publishers/{1}/messages", hubName, deviceId);

            // Create client.
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(string.Format("https://{0}.servicebus.windows.net/", serviceNamespace))
            };

            // var payload = JsonConvert.SerializeObject(deviceTelemetry);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", sas);
            dataToSend.Headers.Add("ContentType", "application/octet-stream");
            var status = httpClient.PostAsync(url, dataToSend);
            Console.WriteLine(status.Result.ReasonPhrase);
        }



        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        public static void Main(string[] args)
        {
            for (int i = 0; i <100; i++)
            {
                RandomHexString();

               //Console.Write("press any key to send another data set");
               //Console.ReadKey();
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