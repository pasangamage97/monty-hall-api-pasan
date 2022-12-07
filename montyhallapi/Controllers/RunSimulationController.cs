using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace montyhallapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunSimulationController : ControllerBase
    {
        [HttpPost]
        public MontyHallSimulationResponse RunSimulation(MontyHallSimulationRequest req)
        {

                if (req == null || req.numOfSimulations == 0)
                {

                    throw new BadHttpRequestException("Invalid Request");

                }

                if(req.type != "switch")
                {
                    if(req.type != "keep")
                    {
                      throw new BadHttpRequestException("Invalid Request");
                    }

                }

                MontyHallSimulationResponse resultOfTheSimulation = new MontyHallSimulationResponse();

                int numberOfSimulations = req.numOfSimulations;
                bool isSwitchedTheDoor = (req.type == "switch") ? true : false;

                Random rnd = new Random();
                Random random = new Random();

                int numberOfWinnings = 0;
                int numberOfLossing = 0;

                resultOfTheSimulation.calculatedData = new ResultCalculatedData();
                resultOfTheSimulation.rawData = new List<ResultRawData>();


                for (int i = 0; i < numberOfSimulations; i++)
                {
                    ResultRawData rawData = new ResultRawData();
                    ResultCalculatedData calculatedData = new ResultCalculatedData();

                    List<int> doors = new List<int> { 1, 2, 3 };

                    //Put the car in to a door
                    int doorWithTheCar = rnd.Next(1, 4);
                    rawData.doorWithTheCar = doorWithTheCar;
                    //resultOfTheSimulation.rawData[i].doorWithTheCar = doorWithTheCar;

                    //Choose a door Randomly
                    int choosenDoor = random.Next(1, 4);
                    rawData.choosenDoor = choosenDoor;
                    //resultOfTheSimulation.rawData[i].choosenDoor = choosenDoor;

                    //Remove a door with goat
                    int removedDoor = doors.Where(x => x != choosenDoor && x != doorWithTheCar).First();
                    doors.Remove(removedDoor);

                    //when Switched the door
                    if (isSwitchedTheDoor)
                        choosenDoor = doors.Where(x => x != choosenDoor).First();

                    if (choosenDoor == doorWithTheCar)
                    {
                        //resultOfTheSimulation.rawData[i].result = "Won";
                        rawData.result = "Won";
                        numberOfWinnings++;
                    }
                    else
                    {
                        //resultOfTheSimulation.rawData[i].result = "Loss";
                        rawData.result = "Loss";
                        numberOfLossing++;
                    }

                    rawData.numOfIteration = i + 1;
                    rawData.switchedOrNot = isSwitchedTheDoor;

                    resultOfTheSimulation.rawData.Add(rawData);

                    //resultOfTheSimulation.rawData[i].numOfIteration = i;
                    //resultOfTheSimulation.rawData[i].switchedOrNot = isSwitchedTheDoor;


                }

                double winningPercentage = ((double)numberOfWinnings / numberOfSimulations) * 100;
                double loosingPercentage = ((double)numberOfLossing / numberOfSimulations) * 100;

                resultOfTheSimulation.calculatedData.winningPercentage = Math.Round((Double)winningPercentage, 2);
                resultOfTheSimulation.calculatedData.losingPercentage = Math.Round((Double)loosingPercentage, 2);
                resultOfTheSimulation.calculatedData.numberOfSimulations = numberOfSimulations;
                resultOfTheSimulation.calculatedData.isSwitched = isSwitchedTheDoor;

                return resultOfTheSimulation;

        }
    }
}
