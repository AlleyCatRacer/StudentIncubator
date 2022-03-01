using System;
using WebAPI.Model;
using Xunit;

namespace UnitTestingWebAPI
{
    public class AvatarGameLogicUnitTest
    {
        private Avatar TestAvatar = new Avatar("testOwner", "testName");

        private void BeforeEach()
        {
            //Setting all stats to 50 for easier testing
            var status = new Status(50, 50, 50, 50);
            TestAvatar = new Avatar("testOwner", "testName",status,80,null );
        }

        //Testing method results
        [Fact]
        public void Sleep_inc30Health_dec10Social_dec10Academic_dec4Time()
        {
            BeforeEach();
            TestAvatar.Sleep();

            Assert.Equal(80, TestAvatar.GetHealth());
            Assert.Equal(40, TestAvatar.GetSocial());
            Assert.Equal(40, TestAvatar.GetAcademic());
            Assert.Equal(76, TestAvatar.TimeBlock);
        }

        [Fact]
        public void Work_inc20Financial_dec20Health_dec10Academic_dec2Time()
        {
            BeforeEach();
            TestAvatar.Work();

            Assert.Equal(70, TestAvatar.GetFinancial());
            Assert.Equal(30, TestAvatar.GetHealth());
            Assert.Equal(40, TestAvatar.GetAcademic());
            Assert.Equal(78, TestAvatar.TimeBlock);
        }

        [Fact]
        public void StudyAlone_inc20Academic_dec20Health_dec10Social_dec3Time()
        {
            BeforeEach();
            TestAvatar.StudyAlone();

            Assert.Equal(70, TestAvatar.GetAcademic());
            Assert.Equal(30, TestAvatar.GetHealth());
            Assert.Equal(40, TestAvatar.GetSocial());
            Assert.Equal(77, TestAvatar.TimeBlock);
        }

        [Fact]
        public void StudyInGroup_inc10Social_inc10Academic_dec20Health_dec2Time()
        {
            BeforeEach();
            TestAvatar.StudyInGroup();

            Assert.Equal(60, TestAvatar.GetSocial());
            Assert.Equal(30, TestAvatar.GetHealth());
            Assert.Equal(60, TestAvatar.GetAcademic());
            Assert.Equal(78, TestAvatar.TimeBlock);
        }

        [Fact]
        public void Party_inc30Social_dec30Financial_dec20Health_dec10Academic_dec4Time()
        {
            BeforeEach();
            TestAvatar.Party();

            Assert.Equal(20, TestAvatar.GetFinancial());
            Assert.Equal(30, TestAvatar.GetHealth());
            Assert.Equal(40, TestAvatar.GetAcademic());
            Assert.Equal(80, TestAvatar.GetSocial());
            Assert.Equal(76, TestAvatar.TimeBlock);
        }

        [Fact]
        public void Hug_inc20Social_inc20Health_NoTimeChange()
        {
            BeforeEach();
            TestAvatar.Hug();

            Assert.Equal(70, TestAvatar.GetHealth());
            Assert.Equal(70, TestAvatar.GetSocial());
            Assert.Equal(80, TestAvatar.TimeBlock);
        }

        //Status limits, upper: not over 100, lower: not under 0 with thrown exception
        //All stats are of same data type and use same logic, only need to test one
        [Fact]
        public void StatDrain_StatToZero_ThrownException()
        {
            Assert.Throws<Exception>(() => TestAvatar.Status.Decrement(110, "Financial"));
            Assert.Equal(0, TestAvatar.GetFinancial());
        }

        [Fact]
        public void StatOverLimit_StatToHundred()
        {
            TestAvatar.Status.Increment(101, "Financial");
            Assert.Equal(100, TestAvatar.GetFinancial());

            TestAvatar.Status.Increment(999, "Financial");
            Assert.Equal(100, TestAvatar.GetFinancial());
        }

        //TimeBlock lower limit, should set to zero and throw exception
        [Fact]
        public void TimeBlockLimit_TimeToZero_ThrownException()
        {
            BeforeEach();

            TestAvatar.SpendTime(0);
            Assert.Equal(80, TestAvatar.TimeBlock);

            TestAvatar.SpendTime(79);
            Assert.Equal(1, TestAvatar.TimeBlock);

            Assert.Throws<ArgumentException>(() => TestAvatar.SpendTime(80));
            Assert.Equal(0, TestAvatar.TimeBlock);

            Assert.Throws<ArgumentException>(() => TestAvatar.SpendTime(81));
            Assert.Equal(0, TestAvatar.TimeBlock);

            Assert.Throws<ArgumentException>(() => TestAvatar.SpendTime(124));
            Assert.Equal(0, TestAvatar.TimeBlock);
        }
    }
}