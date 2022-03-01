using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI.Model
{
    public class Avatar
    {
        public string Owner { get; }
        public string AvatarName { get; set; }
        public Status Status { get; }
        public int TimeBlock { get; set; }
        public string EventDescription { get; set; }
        internal Dictionary<string, ActionDelegate> Actions { get;  set; }
        public delegate void ActionDelegate();

        public Avatar(string owner, string avatarName)
        {
            AvatarName = avatarName;
            Owner = owner;
            TimeBlock = 80;
            Status = new Status(100, 100, 100, 100);
            InitActions();
        }

        [JsonConstructor]
        public Avatar(string owner, string avatarName, Status status, int timeBlock, string eventDescription)
        {
            AvatarName = avatarName;
            Owner = owner;
            Status = status;
            TimeBlock = timeBlock;
            EventDescription = eventDescription;
            InitActions();
        }

        private void InitActions()
       {
           Actions = new Dictionary<string, ActionDelegate>
           {
               ["Sleep"] = Sleep,
               ["Work"] = Work,
               ["StudyAlone"] = StudyAlone,
               ["StudyInGroup"] = StudyInGroup,
               ["Party"] = Party,
               ["Hug"] = Hug
           };
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

        public void Sleep()
        {
            SpendTime(4);
            Status.Increment(30, "Health");
            Status.Decrement(10, "Social");
            Status.Decrement(10, "Academic");
        }

        public void Work()
        {
            SpendTime(2);
            Status.Increment(20, "Financial");
            Status.Decrement(20, "Health");
            Status.Decrement(10, "Academic");
        }

        public void StudyAlone()
        {
            SpendTime(3);
            Status.Increment(20, "Academic");
            Status.Decrement(20, "Health");
            Status.Decrement(10, "Social");
        }

        public void StudyInGroup()
        {
            SpendTime(2);
            Status.Increment(10, "Social");
            Status.Increment(10, "Academic");
            Status.Decrement(20, "Health");
        }

        public void Party()
        {
            SpendTime(4);
            Status.Increment(30, "Social");
            Status.Decrement(10, "Academic");
            Status.Decrement(20, "Health");
            Status.Decrement(30, "Financial");
        }
        
        public void Hug()
        {
            Status.Increment(20,"Social");
            Status.Increment(20,"Health");
        }
        
        public void SpendTime(int amount)
        {
            if (LegalTime(amount))
            {
                TimeBlock -= amount;
            }
        }

        private bool LegalTime(int amount)
        {
            if (TimeBlock - amount > 0)
                return true;

            TimeBlock = 0;
            throw new ArgumentException("Your time has expired. So has the semester. Good job, human!");
        }
    }
}