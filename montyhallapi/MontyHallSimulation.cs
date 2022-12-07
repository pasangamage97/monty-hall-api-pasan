namespace montyhallapi;

public class MontyHallSimulationRequest
{
    public int numOfSimulations { get; set; }
    public string type { get; set; }
}

public class MontyHallSimulationResponse
{
    public List<ResultRawData> rawData { get; set; } = null!;
    public ResultCalculatedData calculatedData { get; set; } = null!;
}

public class ResultRawData
{
    public int numOfIteration { get; set; }
    public int  doorWithTheCar { get; set; }
    public int choosenDoor { get; set; }
    public bool switchedOrNot { get; set; }
    public string result { get; set; } = string.Empty;
}

public class ResultCalculatedData
{
    public double winningPercentage { get; set; } = 0.00;
    public double losingPercentage { get; set; } = 0.00;
    public bool isSwitched { get; set; } = false;
    public int numberOfSimulations { get; set; }
}


