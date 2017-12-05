using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RawDataMultiple
{
    class Program
    {
        static HttpClient client = new HttpClient();
        //public static string sas = "SharedAccessSignature sr=https%3a%2f%2fnilmazure.servicebus.windows.net%2fnilmazurehub%2fpublishers%2fraw_data_multiple%2fmessages&sig=ohY6qoEx8KcrJa4QVPLAhsQ2Af5PrypR0Mt8uzHbRJk%3d&se=1522391299&skn=RootManageSharedAccessKey";


        //New account

         public static string sas = "SharedAccessSignature sr=https%3a%2f%2feanilmeventhub.servicebus.windows.net%2fnilmazurehub%2fpublishers%2fraw_data_multiple%2fmessages&sig=vWnSZfh5FgdXsMsNrjnEhmj8PQCcRWCoMM1gc%2btg400%3d&se=1525409699&skn=RootManageSharedAccessKey";

        // Namespace info.
        
        public static string serviceNamespace = "eanilmeventhub";
        //public static string serviceNamespace = "nilmazure";
        public static string hubName = "nilmazurehub";
        public static string deviceId = "raw_data_multiple";

        public static string data;
        public static string DeviceID;
        public static string Token;
        public static string delimeter1;
        public static string delimeter2;
        public static string hash;
        public static int hour1;
        public static int min1;
        public static int sec1;
        public static int date1;
        public static int month1;
        public static string VoltageA;
        public static string VoltageB;
        public static string VoltageC;
        public static string CurrentA;
        public static string CurrentB;
        public static string CurrentC;
        public static string PowerFactorA;
        public static string PowerFactorB;
        public static string PowerFactorC;
        public static string FreqA;
        public static string FreqB;
        public static string FreqC;
        public static string THDVoltA;
        public static string THDVoltB;
        public static string THDVoltC;
        public static string ReactivePowerA;
        public static string ReactivePowerB;
        public static string ReactivePowerC;
        public static string year;
        public static string generateRandomTimeStamp;
        public static float generateRandomVoltageA;
        public static float generateRandomVoltageB;
        public static float generateRandomVoltageC;
        public static float generateRandomCurrentA;
        public static float generateRandomCurrentB;
        public static float generateRandomCurrentC;
        public static float generateRandomPowerFactorA;
        public static float generateRandomPowerFactorB;
        public static float generateRandomPowerFactorC;
        public static float generateRandomFreqA;
        public static float generateRandomFreqB;
        public static float generateRandomFreqC;
        public static float generateRandomTHDVoltA;
        public static float generateRandomTHDVoltB;
        public static float generateRandomTHDVoltC;
        public static float generateRandomReactivePowerA;
        public static float generateRandomReactivePowerB;
        public static float generateRandomReactivePowerC;

        public static void RandomHexString()
        {
            Random rnd = new Random();
            data = "646174613d";
            DeviceID = "3030303030303434";
            Token = "513553726b6b62594655454279627555";
            delimeter1 = "7c";
            delimeter2 = "7e";
            hash = "23";
            year = "e107";
            hour1 = rnd.Next(1, 12);
            min1 = rnd.Next(00, 60);
            sec1 = rnd.Next(00, 60);
            date1 = rnd.Next(1, 31);
            month1 = rnd.Next(1, 12);

            setDateTimeData();

            var hour1Hex = hour1.ToString("X2");
            var min1Hex = min1.ToString("X2");
            var sec1Hex = sec1.ToString("X2");
            var date1Hex = date1.ToString("X2");
            var month1Hex = month1.ToString("X2");
            generateRandomTimeStamp = string.Concat(hour1Hex, min1Hex, sec1Hex, date1Hex, month1Hex);
            data = data.Trim();
            year = year.Trim();

            generateRandomVoltageA = GetRandomFloat(1, 25);
            VoltageA = edianHexFromFloat(generateRandomVoltageA);
            generateRandomVoltageB = GetRandomFloat(26, 50);
            VoltageB = edianHexFromFloat(generateRandomVoltageB);
            generateRandomVoltageC = GetRandomFloat(51, 75);
            VoltageC = edianHexFromFloat(generateRandomVoltageC);

            generateRandomCurrentA = GetRandomFloat(1, 25);
            CurrentA = edianHexFromFloat(generateRandomCurrentA);
            generateRandomCurrentB = GetRandomFloat(26,50);
            CurrentB = edianHexFromFloat(generateRandomCurrentB);
            generateRandomCurrentC = GetRandomFloat(51, 75);
            CurrentC = edianHexFromFloat(generateRandomCurrentC);

            generateRandomPowerFactorA = GetRandomFloat(1, 25);
            PowerFactorA = edianHexFromFloat(generateRandomPowerFactorA);
            generateRandomPowerFactorB = GetRandomFloat(26,50);
            PowerFactorB = edianHexFromFloat(generateRandomPowerFactorB);
            generateRandomPowerFactorC = GetRandomFloat(51,75);
            PowerFactorC = edianHexFromFloat(generateRandomPowerFactorC);

            generateRandomFreqA = GetRandomFloat(1, 25);
            FreqA = edianHexFromFloat(generateRandomFreqA);
            generateRandomFreqB = GetRandomFloat(26,50);
            FreqB = edianHexFromFloat(generateRandomFreqB);
            generateRandomFreqC = GetRandomFloat(51,75);
            FreqC = edianHexFromFloat(generateRandomFreqC);

            generateRandomTHDVoltA = GetRandomFloat(1, 25);
            THDVoltA = edianHexFromFloat(generateRandomTHDVoltA);
            generateRandomTHDVoltB = GetRandomFloat(26,50);
            THDVoltB = edianHexFromFloat(generateRandomTHDVoltB);
            generateRandomTHDVoltC = GetRandomFloat(51,75);
            THDVoltC = edianHexFromFloat(generateRandomTHDVoltC);

            generateRandomReactivePowerA = GetRandomFloat(1, 25);
            ReactivePowerA = edianHexFromFloat(generateRandomReactivePowerA);
            generateRandomReactivePowerB = GetRandomFloat(26,50);
            ReactivePowerB = edianHexFromFloat(generateRandomReactivePowerB);
            generateRandomReactivePowerC = GetRandomFloat(51,75);
            ReactivePowerC = edianHexFromFloat(generateRandomReactivePowerC);

            Console.WriteLine("Data Frame");
            Console.WriteLine("Data:" + data);
            Console.WriteLine("Device ID:" + DeviceID);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("Token:" + Token);
            Console.WriteLine("Hash:" + hash);
            Console.WriteLine("Timestamp:" + generateRandomTimeStamp + year);
            Console.WriteLine("Delimeter:" + delimeter1);
            Console.WriteLine("VoltageA:" + VoltageA);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("VoltageB:" + VoltageB);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("VoltageC:" + VoltageC);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("CurrentA:" + CurrentA);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("CurrentB:" + CurrentB);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("CurrentC:" + CurrentC);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("PowerFactorA:" + PowerFactorA);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("PowerFactorB:" + PowerFactorB);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("PowerFactorC:" + PowerFactorC);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("FreqA:" + FreqA);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("FreqB:" + FreqB);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("FreqC:" + FreqC);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("THDVoltA:" + THDVoltA);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("THDVoltB:" + THDVoltB);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("THDVoltC:" + THDVoltC);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("ReactivePowerA:" + ReactivePowerA);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("ReactivePowerB:" + ReactivePowerB);
            Console.WriteLine("Delimeter:" + delimeter2);
            Console.WriteLine("ReactivePowerC:" + ReactivePowerC); 
            Console.WriteLine("\n");
            var hex = string.Concat(data, DeviceID, delimeter1, Token, hash, generateRandomTimeStamp, year, delimeter1, VoltageA, delimeter2, VoltageB, delimeter2, VoltageC, delimeter2, CurrentA, delimeter2, CurrentB, delimeter2, CurrentC, delimeter2, PowerFactorA, delimeter2, PowerFactorB, delimeter2, PowerFactorC, delimeter2, FreqA, delimeter2, FreqB, delimeter2, FreqC, delimeter2, THDVoltA, delimeter2, THDVoltB, delimeter2, THDVoltC, delimeter2, ReactivePowerA, delimeter2, ReactivePowerB, delimeter2, ReactivePowerC);
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

            //var content = new StringContent(hex, Encoding.UTF8, "application/octet-stream");

            dataToSend.Headers.Add("ContentType", "application/octet-stream");

            var status = httpClient.PostAsync(url, dataToSend);

            Console.WriteLine(status.Result.ReasonPhrase);

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

        private static void Main(string[] args)
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
          //  hour2 = currentdate.Hour;
            min1 = currentdate.Minute;
            sec1 = currentdate.Second;
            date1 = currentdate.Day;
            month1 = currentdate.Month;
          //date2 = date1;
          //  month2 = month1;

            //currentdate.AddMinutes(30);
            //currentdate.AddSeconds(30);

           // min2 = currentdate.Minute;
            sec1 = currentdate.Second;


        }
    }
}