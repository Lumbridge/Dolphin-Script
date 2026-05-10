using DolphinScript.Core.Models;
using System.Collections.Generic;
using System.Drawing;

namespace DolphinScript.Core.Interfaces
{
    public interface IHumanMouseTrajectoryPlanner
    {
        IReadOnlyList<MouseMovementStep> CreateTrajectory(Point start, Point target, MouseMovementProfile profile);
    }
}