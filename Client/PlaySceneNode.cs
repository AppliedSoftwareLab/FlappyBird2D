using System.Collections.Generic;


/**
 * @brief ������ �÷��� �� ����Դϴ�.
 */
class PlaySceneNode : SceneNode
{
    /**
     * @brief ������ �÷��� �� ��忡 �����մϴ�.
     */
    public override void Entry()
    {
        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
        Bird bird = WorldManager.Get().GetGameObject("Bird") as Bird;
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;

        pipeDetector.CanGeneratePipe = true;

        Button pressButton = WorldManager.Get().GetGameObject("PressButton") as Button;
        pressButton.EventAction = () =>
        {
            pressButton.UITexture = (pressButton.UITexture == "PauseButton") ? "ContinueButton" : "PauseButton";

            bird.Movable = !bird.Movable;
            if (bird.State != Bird.EState.DONE)
            {
                floor.Movable = !floor.Movable;
                pipeDetector.CanGeneratePipe = !pipeDetector.CanGeneratePipe;

                List<Pipe> pipes = pipeDetector.DetectPipes;
                foreach (Pipe pipe in pipes)
                {
                    pipe.Movable = !pipe.Movable;
                }
            }
        };
    }


    /**
     * @brief ������ �÷��� �� ���� ���� �����ϴ�.
     */
    public override void Leave()
    {
        Floor floor = WorldManager.Get().GetGameObject("Floor") as Floor;
        PipeDetector pipeDetector = WorldManager.Get().GetGameObject("PipeDetector") as PipeDetector;

        floor.Movable = false;
        pipeDetector.CanGeneratePipe = false;

        List<Pipe> pipes = pipeDetector.DetectPipes;
        foreach (Pipe pipe in pipes)
        {
            pipe.Movable = false;
        }

        DetectSwitch = false;
    }
}