public static class Reader
{
    public static List<string> ReadFile(string directory)
    {
        List<string> output = new();
        string line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new(directory);
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the line to console window
                output.Add(line);
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
            return output;
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            return new();
        }
    }
}