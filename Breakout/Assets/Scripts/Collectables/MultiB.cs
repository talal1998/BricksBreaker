using System.Linq;

public class MultiB : Collectable
{

    protected override void ApplyEffect()
    {
        foreach (Ball ball in BallsManager.Instance.Balls.ToList())
        {
            BallsManager.Instance.SpawnBalls(ball.gameObject.transform.position);
            BallsManager.Instance.SpawnBalls(ball.gameObject.transform.position);
        }
    }
}
