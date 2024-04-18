using Logic;

namespace Model
{
    public class ModelAPI
    {
        private LogicAPI _logic;

        public ModelAPI()
        {
            _logic = LogicAPI.CreateLogicService();
        }

        public void StartSimulation(int numberOfBalls)
        {
            // Start the simulation using the LogicAPI
            _logic.Start(numberOfBalls, 30, 900, 600);
        }
    }
}
