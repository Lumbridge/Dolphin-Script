namespace DolphinScript.Core.Models
{
    public class MouseMovementProfile
    {
        public int MinimumSpeed { get; set; } = 40;
        public int MaximumSpeed { get; set; } = 60;
        public int MinimumDelayMilliseconds { get; set; } = 5;
        public int MaximumDelayMilliseconds { get; set; } = 18;
        public bool AllowCorrectiveOvershoot { get; set; } = true;
    }
}