static List<string> ReadNamesFromFile()
{
    List<string> names = new();

    try
    {
        using StreamReader reader = new("us_names.txt");
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            names.Add(line);
        }
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"File us_names not exist.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while reading file: {ex.Message}");
    }

    return names;
}

static bool FindName(string inputName, List<string> names)
{
    foreach (var knownName in names)
    {
        if (LevenshteinDistance(inputName, knownName) == 1)
        {
            Console.WriteLine($"Did you mean {knownName}? Y/N");
            string? userInput = Console.ReadLine();
            if (userInput == "Y" || userInput == "y")
            {
                Console.WriteLine($"Hello, {knownName}!");
                return true;
            }
        }
    }
    return false;
}
static int LevenshteinDistance(string str1, string str2)
{
    int[,] distanceMatrix = new int[str1.Length + 1, str2.Length + 1];

    for (int i = 0; i <= str1.Length; i++)
    {
        for (int j = 0; j <= str2.Length; j++)
        {
            if (i == 0)
            {
                distanceMatrix[i, j] = j;
            }
            else if (j == 0)
            {
                distanceMatrix[i, j] = i;
            }
            else
            {
                int substitutionCost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;
                distanceMatrix[i, j] = Math.Min(Math.Min(
                    distanceMatrix[i - 1, j] + 1,   // Deletion
                    distanceMatrix[i, j - 1] + 1),  // Insertion
                    distanceMatrix[i - 1, j - 1] + substitutionCost);  // Substitution
            }
        }
    }

    return distanceMatrix[str1.Length, str2.Length];
}

List<string> names = ReadNamesFromFile();
Console.Write(">Enter name: ");
string? inputName = Console.ReadLine();
if (inputName != null)
{
    inputName = inputName.Trim();
}
if (inputName != null && names.Contains(inputName))
{
    Console.WriteLine($"Hello, {inputName}!");
}
else
{
    if (inputName == null || !FindName(inputName, names))
    {
        Console.WriteLine("Your name was not found.");
    }
}