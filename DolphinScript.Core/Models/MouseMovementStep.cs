using System.Drawing;

namespace DolphinScript.Core.Models
{
    public class MouseMovementStep
    {
        public MouseMovementStep(Point position, int delayMilliseconds)
        {
            Position = position;
            DelayMilliseconds = delayMilliseconds;
        }

        public Point Position { get; }
        public int DelayMilliseconds { get; }
    }
}