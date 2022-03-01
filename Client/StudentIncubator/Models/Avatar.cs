using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudentIncubator.Models
{
    public class Avatar
    {
        public string Owner { get; }
        public string AvatarName { get; set; }
        public Status Status { get; }
        public int TimeBlock { get; }
        public string EventDescription { get; }

        public Avatar(string owner, string avatarName)
        {
            Owner = owner;
            AvatarName = avatarName;
            Status = new Status(100, 100, 100, 100);
            TimeBlock = 80;
            EventDescription = null;
        }

        [JsonConstructor]
        public Avatar(string owner, string avatarName, Status status, int timeBlock, string eventDescription)
        {
            AvatarName = avatarName;
            Owner = owner;
            Status = status;
            TimeBlock = timeBlock;
            EventDescription = eventDescription;
        }
        
        public int GetAcademic()
        {
            return Status.GetStatus("Academic");
        }

        public int GetFinancial()
        {
            return Status.GetStatus("Financial");
        }

        public int GetHealth()
        {
            return Status.GetStatus("Health");
        }
        
        public int GetSocial()
        {
            return Status.GetStatus("Social");
        }
    }
}