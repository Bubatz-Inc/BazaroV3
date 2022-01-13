namespace Bazaro.Web
{
    public static class DotEnv
    {
        public static bool LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))

            {
                var file = File.Create(filePath);
                File.WriteAllText(filePath, "DB_PW=");
                file.Close();
                Console.WriteLine(filePath);
            }
 
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
            return true;
        }

        public static string FromEnvVariable(string name)
        {
            string? value = Environment.GetEnvironmentVariable(name);
            if (value == null)
            {
                return null;
            }
            else
            {
                return value;
            }
            
        }


         
    }
}
