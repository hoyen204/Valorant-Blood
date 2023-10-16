using System.Diagnostics;
using ValorantBlood.Properties;

namespace ValorantBlood
{
    public class Valorant_Blood
    {
        public static string VALORANT_DEFAULT_PATH = @"C:\Riot Games\VALORANT";
        public static string VALORANT_PROCESS = "VALORANT.exe";
        public static string PAKS_PATH = @"ShooterGame\Content\Paks";
        public static string VNG_LOGO_PAK = Path.Combine(PAKS_PATH, "VNGLogo-WindowsClient.pak");
        public static string VNG_LOGO_SIG = Path.Combine(PAKS_PATH, "VNGLogo-WindowsClient.sig");
        public static string MATURE_PAK = Path.Combine(PAKS_PATH, "MatureData-WindowsClient.pak");
        public static string MATURE_SIG = Path.Combine(PAKS_PATH, "MatureData-WindowsClient.sig");

        public static void Main(string[] args)
        {
            Console.Write($"Select Valorant path ({VALORANT_DEFAULT_PATH}): ");
            string input = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                VALORANT_DEFAULT_PATH = input;
            }

            var ValorantLivePath = Path.Combine(VALORANT_DEFAULT_PATH, "live");
            var ValorantProcessPath = Path.Combine(ValorantLivePath, VALORANT_PROCESS);
            var VNGLogoPak = Path.Combine(ValorantLivePath, VNG_LOGO_PAK);
            var VNGLogoSig = Path.Combine(ValorantLivePath, VNG_LOGO_SIG);
            var MaturePak = Path.Combine(ValorantLivePath, MATURE_PAK);
            var MatureSig = Path.Combine(ValorantLivePath, MATURE_SIG);

            if (!File.Exists(ValorantProcessPath))
            {
                Console.WriteLine($"Not found {ValorantProcessPath}");
                Console.ReadLine();
                return;
            }

            if (File.Exists(VNGLogoPak))
                File.Delete(VNGLogoPak);
            if (File.Exists(VNGLogoSig))
                File.Delete(VNGLogoSig);

            if (!File.Exists(MaturePak))
                using (MemoryStream resourceStream = new MemoryStream(Resources.MatureDataPak))
                {
                    if (resourceStream != null)
                    {
                        using (FileStream fileStream = File.Create(MaturePak))
                        {
                            resourceStream.CopyTo(fileStream);
                        }

                        Console.WriteLine("File copied successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Resource not found.");
                    }
                }

            if (!File.Exists(MatureSig))
                using (MemoryStream resourceStream = new MemoryStream(Resources.MatureDataSig))
                {
                    if (resourceStream != null)
                    {
                        using (FileStream fileStream = File.Create(MatureSig))
                        {
                            resourceStream.CopyTo(fileStream);
                        }

                        Console.WriteLine("File copied successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Resource not found.");
                    }
                }

            Process.Start(Path.Combine(VALORANT_DEFAULT_PATH.Replace("VALORANT", "Riot Client"),"RiotClientServices.exe"), " --launch-product=valorant --launch-patchline=live");
        }
    }
}