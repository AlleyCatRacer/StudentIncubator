using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI.Model
{
    public class Status
    {
        public Dictionary<string, int> Aspects { get; }
        public int Academic { get; set; }
        public int Financial { get; set; }
        public int Health { get; set; }
        public int Social { get; set; }
        public int MAX { get; }
        public int DEATH { get; }

        public Status(int academic, int financial, int health, int social)
        {
            Academic = academic;
            Financial = financial;
            Health = health;
            Social = social;

            MAX = 100;
            DEATH = 0;

            Aspects = new Dictionary<string, int>();

            Aspects.Add("Academic", Academic);
            Aspects.Add("Financial", Financial);
            Aspects.Add("Health", Health);
            Aspects.Add("Social", Social);
        }

        public int GetStatus(string statusType)
        {
            try
            {
                return Aspects[statusType];
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw new Exception("Not a valid status type");
            }
        }

        public int[] GetAllStatuses()
        {
            int[] statuses = null;
            try
            {
                int i = 0;
                foreach (var key in Aspects.Keys)
                {
                    statuses[i] = Aspects[key];
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}, in Status line 40");
            }

            return statuses;
        }

        public void Increment(int addition, string statusType)
            /*TODO Maybe later
             - should we merge inc and dec, just taking positive/negative int?
                    then we can skip validity check method, because we only use it once*/
        {
            if (Aspects.ContainsKey(statusType))
            {
                int temp = Aspects[statusType];

                if (WithinLimits(addition, temp, true))
                {
                    temp += addition;
                }
                else {
                    temp = 100;
                }

                Aspects[statusType] = temp;
                PropertiesUpdate();
            }
        }

        public void Decrement(int subtraction, string statusType)
        {
            if (!Aspects.ContainsKey(statusType)) return;
            var temp = Aspects[statusType];

            if (WithinLimits(subtraction, temp, false))
            {
                temp -= subtraction;
                Aspects[statusType] = temp;
                PropertiesUpdate();
            }
            else
            {
                Aspects[statusType] = 0;
                PropertiesUpdate();
                throw new Exception($"{statusType} meter has dropped to zero. ");
            }
        }

        private bool WithinLimits(int change, int current, bool addition)
        {
            if (addition)
            {
                return (current + change) <= MAX;
            }
            
            return (current - change) > DEATH;
        }

        private void PropertiesUpdate()
        {
            Academic = Aspects["Academic"];
            Financial = Aspects["Financial"];
            Health = Aspects["Health"];
            Social = Aspects["Social"];
        }
    }
}