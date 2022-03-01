using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentIncubator.Models {
    public class Action {
        
        public string Creator { get; set;}
        
        
        [Required(ErrorMessage = "Activity name should be between 2 and 20 characters"), MinLength(2), MaxLength(20)]
        public string Activity { get; set; }
        [Required, Range(-90, 90)]
        public int Academic { get; set;}
        [Required, Range(-90, 90)]
        public int Financial { get; set;}
        [Required, Range(-90, 90)]
        public int Health { get; set;}
        [Required, Range(-90, 90)]
        public int Social { get; set;}
        [Required, Range(1, 10)]
        public int TimeCost { get; set;}

        
        public Action() {
            
        }
        
        [JsonConstructor]
        public Action(string creator, string activity, int academic, int financial, int health, int social, int timeCost) {
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