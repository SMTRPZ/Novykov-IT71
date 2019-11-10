using ClassLib.Factory_Method;
using ClassLib.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.State
{
    public class Game
    {
        public IGameState State { get; set; }
     
        public Robot Robot { get; protected set; }
        public Map Map { get; protected set; }
        public GameHistory GameHistory { get; protected set; }
        public int MoveCounter { get; protected set; }

        public Game(IGameState gameState)
        {
            State = gameState;
        }

        public string Turn(string input)
        {
            return State.Turn(this, input);
        }

        public void GenerateNewGame()
        {
            Random rnd = new Random();
            int random = rnd.Next(10);
            if (random < 5)
            {
                Robot = new WorkerCreator().Create("workerimage");
            }
            else if (random > 7)
            {
                Robot = new ScientistCreator().Create("scientistimage");
            }
            else
            {
                Robot = new CyborgCreator().Create("cyborgimage");
            }
            Map = new Map();
            GameHistory = new GameHistory();
            MoveCounter = 0;
        }

        public void Save()
        {
            GameHistory.Add(new GameMemento(Robot.SaveState(), MoveCounter, (Map)Map.Clone()));
        }

        public void IncrementMoveCounter()
        {
            MoveCounter++;
        }

        internal void SetMap(Map map)
        {
            Map = map;
        }

        internal void SetMoveCounter(int moveCounter)
        {
            MoveCounter = moveCounter;
        }

        public string NextStage()
        {
            return State.NextStage(this);
        }

        public string PreviousState()
        {
            return State.PreviousState(this);
        }
    }
}
