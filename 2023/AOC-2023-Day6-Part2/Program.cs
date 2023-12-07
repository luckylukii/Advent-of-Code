internal static class Program
{
    private static string[] inputs = new string[]{
        "59707878",
        "430121812131276",
    };
    private static void Main()
    {
        //Init
        ulong time = ulong.Parse(inputs[0]);
        ulong distance = ulong.Parse(inputs[1]);

        ulong numPossibilities = 0;

        //Eval
        for (ulong i = 0; i < time; i++)
        {
            if (GetDistance(i, time) > distance)
            {
                numPossibilities++;
            }
            else if (numPossibilities > time/2) break;
        }

        Console.WriteLine(numPossibilities);
        Console.ReadLine();
    }

    private static ulong GetDistance(ulong millisecondsPressed, ulong totalTime)
    {
        ulong speed_mmPerMs = millisecondsPressed;
        ulong distance = speed_mmPerMs * (totalTime-millisecondsPressed);
        
        return distance;
    }
}