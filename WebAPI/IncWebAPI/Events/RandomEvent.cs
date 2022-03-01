using System;
using System.Collections.Generic;
using WebAPI.DataMediator.IRepos;
using WebAPI.Model;


namespace WebAPI.Events
{
    public  class RandomEvent

    {
        private static RandomEvent instance;
        private static Object LOCK = new Object();
        private Dictionary<int, Action<Avatar>> Events { get; }
        
        private RandomEvent() {
            Events = new Dictionary<int, Action<Avatar>>
            {
                [0] = BikeStolen,
                [1] = StumbleOnMoney,
                [2] = GetHitByACar,
                [3] = EncounterALongLostFriend,
                [4] = GetLaid
            };
        }

        public static RandomEvent GetInstance() {
            if (instance != null) return instance;
            lock (LOCK)
            {
                instance ??= new RandomEvent();
            }
            return instance;
        }
        
        public void FeelingLuckyPunk(Avatar avatar, int lotteryTicket, IAvatarRepository avatarRepo)
        {
            var r = new Random();
            var rnd = r.Next(0, 5);

            if (rnd != lotteryTicket) {
                avatar.EventDescription = null;
                return;
            }
            
            var randomEvent = r.Next(0, Events.Count);
            
            Events[randomEvent].Invoke(avatar);


            avatarRepo.UpdateAvatarAsync(avatar);
        }

        private void BikeStolen(Avatar avatar)
        {
            avatar.EventDescription = "Your bike was stolen.";
           
            avatar.Status.Decrement(30, "Financial");
            avatar.Status.Decrement(10, "Health");
            avatar.Status.Decrement(10, "Social");
        }

        private void StumbleOnMoney(Avatar avatar)
        {
            avatar.EventDescription = "You found 500 kr in the most unexpected place.";
            avatar.Status.Increment(30, "Financial");
            avatar.Status.Increment(10, "Social");

        }

        private void GetHitByACar(Avatar avatar) 
        {
            avatar.EventDescription = "You got hit by a car.";
            avatar.Status.Decrement(30, "Health");
            avatar.Status.Decrement(10, "Academic");
            avatar.Status.Decrement(10, "Financial");
            avatar.Status.Increment(10, "Social");
        }
        
        private void EncounterALongLostFriend(Avatar avatar) 
        {
            avatar.EventDescription = "You bumped into an old friend and had a pleasant chat.";
            avatar.Status.Decrement(10, "Academic");
            avatar.Status.Increment(30, "Social");
            avatar.Status.Increment(20, "Health");
        }
        
        private void GetLaid(Avatar avatar) 
        {
            avatar.EventDescription = "You just got lucky, bow-chika-bow-wow!";
            avatar.Status.Decrement(10, "Academic");
            avatar.Status.Increment(30, "Social");
            avatar.Status.Increment(20, "Health");
        }
    }
}