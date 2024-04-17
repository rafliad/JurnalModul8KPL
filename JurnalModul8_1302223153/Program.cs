using System.Text.Json;

namespace JurnalModul8_1302223153
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankConfig Konfigurasi = new BankConfig();
            if (Konfigurasi.config.lang == "en")
            {
                Console.WriteLine("Please insert the amount of money to transfer:");
            }else if (Konfigurasi.config.lang == "id")
            {
                Console.WriteLine("Masukkan jumlah uang yang akan di-transfer:");
            }
            int inputUang = Convert.ToInt32(Console.ReadLine());
            if (inputUang <= Konfigurasi.config.transfer.threshold)
            {
                if (Konfigurasi.config.lang == "en")
                {
                    Console.WriteLine("Total amount: " + (inputUang + Konfigurasi.config.transfer.low_fee));
                }
                else if (Konfigurasi.config.lang == "id")
                {
                    Console.WriteLine("Total biaya: " + (inputUang + Konfigurasi.config.transfer.low_fee));
                }
            }
            else
            {
                if (Konfigurasi.config.lang == "en")
                {
                    Console.WriteLine("Total amount: " + (inputUang + Konfigurasi.config.transfer.high_fee));
                }
                else if (Konfigurasi.config.lang == "id")
                {
                    Console.WriteLine("Total biaya: " + (inputUang + Konfigurasi.config.transfer.high_fee));
                }
            }
            if (Konfigurasi.config.lang == "en")
            {
                Console.WriteLine("Select transfer method:");
            }
            else if (Konfigurasi.config.lang == "id")
            {
                Console.WriteLine("Pilih metode transfer:");
            }
            int i = 1;
            foreach(var metode in Konfigurasi.config.methods)
            {
                Console.WriteLine(i + ". " + metode);
                i++;
            }
            if (Konfigurasi.config.lang == "en")
            {
                Console.WriteLine("Please type " + Konfigurasi.config.confirmation.en + " to confirm the transaction:");
            }
            else if (Konfigurasi.config.lang == "id")
            {
                Console.WriteLine("Ketik " + Konfigurasi.config.confirmation.id + " untuk mengkonfirmasi transaksi");
            }
            string inputKonfirmasi = Console.ReadLine();
            if (Konfigurasi.config.lang == "en")
            {
                if (Konfigurasi.config.confirmation.en == inputKonfirmasi)
                {
                    Console.WriteLine("The transfer is completed");
                }
                else
                {
                    Console.WriteLine("Transfer is cancelled");
                }
            }
            else if (Konfigurasi.config.lang == "id")
            {
                if (Konfigurasi.config.confirmation.id == inputKonfirmasi)
                {
                    Console.WriteLine("Proses transfer berhasil");
                }
                else
                {
                    Console.WriteLine("Transfer dibatalkan");
                }
            }
        }
    }

    public class Config
    {
        public string lang { get; set; }
        public Transfer transfer { get; set; }
        public List<string> methods { get; set; }
        public Confirmation confirmation { get; set; }

        public Config() { }

        public Config(string lang, Transfer transfer, List<string> methods, Confirmation confirmation)
        {
            this.lang = lang;
            this.transfer = transfer;
            this.methods = methods;
            this.confirmation = confirmation;
        }
    }

    public class Confirmation
    {
        public Confirmation(string en, string id)
        {
            this.en = en;
            this.id = id;
        }
        public string en { get; set; }
        public string id { get; set; }
    }
    public class Transfer
    {
        public Transfer(int threshold, int low_fee, int high_fee)
        {
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }
    }

    class BankConfig
    {
        public Config config;
        public const String filePath = @"C:\Users\RAFLI\Downloads\Percodingan\C#\JurnalModul8_1302223153\bank_transfer_config.json";
        public BankConfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }
        private Config ReadConfigFile()
        {
            String configJsonData = File.ReadAllText(filePath);
            config = JsonSerializer.Deserialize<Config>(configJsonData);
            return config;
        }
        private void SetDefault()
        {
            Confirmation confirmation = new Confirmation("yes", "ya");
            Transfer transfer = new Transfer(25000000, 6500, 15000);
            config = new Config("en", transfer, ["RTO (real-time)", "SKN", "RTGS", "BI FAST"], confirmation);
        }
        private void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            String jsonString = JsonSerializer.Serialize(config, options);
            File.WriteAllText(filePath, jsonString);
        }
    }
}
