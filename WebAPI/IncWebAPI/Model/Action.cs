using System.Text.Json.Serialization;

namespace WebAPI.Model
{
    public class Action
    {
        public string Creator { get; }
        public string Activity { get; }
        public int Academic { get; }
        public int Financial { get; }
        public int Health { get; }
        public int Social { get; }
        public int TimeCost { get; }

        [JsonConstructor]
        public Action(string creator, string activity, int academic, int financial, int health, int social,
            int timeCost)
        {
            Creator = creator;
            Activity = activity;
            Academic = academic;
            Financial = financial;
            Health = health;
            Social = social;
            TimeCost = timeCost;
        }
    }
}