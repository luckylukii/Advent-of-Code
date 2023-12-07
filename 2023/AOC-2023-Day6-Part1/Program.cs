internal static class Program
{
    private record Race(int time, int recordDistance);

    private static string[] inputs = new string[]{
        "59 70 78 78",
        "430 1218 1213 1276",
    };
    private static void Main()
    {
        //Init
        var times = inputs[0].Split(' ');
        var distances = inputs[1].Split(' ');

        List<Race> races = new();

        List<int> numPossibilities = new();

        for (int i = 0; i < times.Length; i++)
        {
            int time = int.Parse(times[i]);
            int distance = int.Parse(distances[i]);
            races.Add(new(time, distance));
        }

        //Eval
        for (int i = 0; i < races.Count; i++)
        {
            for (int j = 0; j < races[i].time; j++)
            {
                if (GetDistance(j, races[i].time) > races[i].recordDistance){
                    if (i >= numPossibilities.Count) numPossibilities.Insert(i, 0);
                    numPossibilities[i]++;
                }
            }
        }

        int result = numPossibilities.MultiplyArray();

        Console.WriteLine(result);
        Console.ReadLine();
    }

    private static int MultiplyArray(this List<int> arr){
        int result = 1;
        foreach (var item in arr)
        {
            result *= item;
        }
        return result;
    }

    private static int GetDistance(int millisecondsPressed, int totalTime){
        int speed = millisecondsPressed;
        int distance = speed * (totalTime - millisecondsPressed);

        return distance;
    }
}