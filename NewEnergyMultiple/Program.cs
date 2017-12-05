using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EnergyMultiple
{
    class Program
    {
        static HttpClient client = new HttpClient();
        // public static string sas = "SharedAccessSignature sr=https%3a%2f%2fnilmazure.servicebus.windows.net%2fnilmazurehub%2fpublishers%2fenergy_multiple%2fmessages&sig=nJ%2byo1d5AWmBpvKb5CiYEJrnrXk%2fjsU6VamqwNV3izs%3d&se=1522391256&skn=RootManageSharedAccessKey";


        //New account

        public static string sas = "SharedAccessSignature sr=https%3a%2f%2feanilmeventhub.servicebus.windows.net%2fnilmazurehub%2fpublishers%2fenergy_multiple%2fmessages&sig=GVjlLBWkpSKmTaEaL15RV5CSmuH%2fCSdFtsxNXoCYoUA%3d&se=1525409560&skn=RootManageSharedAccessKey";

        // Namespace info.
        public static string serviceNamespace = "eanilmeventhub";
        //public static string serviceNamespace = "nilmazure";
        public static string hubName = "nilmazurehub";
        public static string deviceId = "energy_multiple";

        public static string data;
        public static string DeviceID;
        public static string Token;
        public static string delimeter1;
        public static string delimeter2;
        public static string delimeter3;
        public static string delimeter4;
        public static string hash;
        public static string FromTimeStamp;
        public static string ToTimeStamp;
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
        public static string SourceID;
        public static string TotalEnergy;
        public static string ApplianceEnergy1;
        public static string ApplianceEnergy2;
        public static string AppIDOneByte;

        public static string[] ApplianceID = new string[7] { "020302b60301", "04030dc80001", "020b0a460502", "02021eee0201", "020604320001", "010209b00401", "02071f400601" };
        public static float generateRandomTotalEnergy;
        public static float generateRandomnApplianceEnergy1;
        public static float generateRandomnApplianceEnergy2;
        public static string generateRandomnApplianceID;
        public static string year;

        public static void RandomHexString()
        {
            
            Random rnd = new Random();
            data = "646174613d";
            DeviceID = "3030303030303434";
            Token = "513553726b6b62594655454279627555";
            delimeter1 = "7c";
            delimeter2 = "7e";
            delimeter3 = "2c";
            delimeter4 = "5f";
            hash = "23";
            year = "e107";
            SourceID = "01";
            AppIDOneByte = "01";
            hour1 = rnd.Next(1, 12);
            hour2 = rnd.Next(1, 12);
            while (hour1 < hour2)
            {
                hour1 = rnd.Next(1, 12);
                hour2 = rnd.Next(1, 12);
            }

            min1 = rnd.Next(00, 60);
            sec1 = rnd.Next(00, 60);
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

            FromTimeStamp = string.Concat(hour1Hex, min1Hex, sec1Hex, date1Hex, month1Hex);
            ToTimeStamp = string.Concat(hour2Hex, min2Hex, sec2Hex, date2Hex, month2Hex);
            var randomIndex = rnd.Next(0, ApplianceID.Length);
            generateRandomnApplianceID = ApplianceID[randomIndex];
            data = data.Trim();
            year = year.Trim();
            
            generateRandomTotalEnergy = GetRandomFloat(1, 50);
            TotalEnergy=edianHexFromFloat(generateRandomTotalEnergy);

            generateRandomnApplianceEnergy1 = GetRandomFloat(1,25);
            ApplianceEnergy1 = edianHexFromFloat(generateRandomnApplianceEnergy1);

            generateRandomnApplianceEnergy2 = GetRandomFloat(1, 25);
            ApplianceEnergy2 = edianHexFromFloat(generateRandomnApplianceEnergy2);

            Console.WriteLine("Data Frame");
            Console.WriteLine("Data:" + data);
            Console.WriteLine("Device ID:" + DeviceID);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("Token:" + Token);
            Console.WriteLine("Hash:" + hash);
            Console.WriteLine("FromTimestamp:" + FromTimeStamp + year);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("ToTimestamp:" + ToTimeStamp + year);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("SourceID:" + SourceID);
            Console.WriteLine("Delimeter:" + delimeter3);
            Console.WriteLine("TotalEnergy:" + TotalEnergy);
            Console.WriteLine("ApplianceID:" + generateRandomnApplianceID);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("ApplianceEnergy1:" + ApplianceEnergy1);
            Console.WriteLine("Delimeter:" + delimeter4);
            Console.WriteLine("ApplianceID:" + generateRandomnApplianceID);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("ApplianceEnergy2:" + ApplianceEnergy2);
            Console.WriteLine("\n");
            Console.WriteLine("HEX VALUE:");

            var hex = string.Concat(data, DeviceID, delimeter1, Token, hash, FromTimeStamp, year, delimeter1, ToTimeStamp, year, delimeter1, SourceID, delimeter3, TotalEnergy, delimeter3, generateRandomnApplianceID, delimeter2, ApplianceEnergy1, delimeter4, generateRandomnApplianceID, delimeter2, ApplianceEnergy2);
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
            dataToSend.Headers.Add("ContentType", "application/octet-stream");
            var status = httpClient.PostAsync(url, dataToSend);
            Console.WriteLine(status.Result.ReasonPhrase);
        }

        private static void Main(string[] args)
        {
            for (int i = 0; i < 50; i++)
            {
                RandomHexString();
                Thread.Sleep(2000);
            }
        }

        

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        public static float GetRandomFloat(float min, float max)
        {
            Random r = new Random();
            double ramdomvalue = min + ((r.NextDouble() * (max - min)));
            return (float)ramdomvalue;
        }
        
        public static string edianHexFromFloat(float f)
        {
           
           var data = BitConverter.GetBytes(f);
           string TotalEnergy = String.Concat(data.Select(b => b.ToString("x2")));
           return TotalEnergy;
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