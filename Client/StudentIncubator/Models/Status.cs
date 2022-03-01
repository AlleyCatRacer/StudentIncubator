using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudentIncubator.Models
{
    public class Status
    {
        public Dictionary<string, int> Aspects { get; }
        public int Academic { get; }
        public int Financial { get; }
        public int Health { get; }
        public int Social { get; }
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
    }
}