using System;
using NUnit.Framework;

namespace customprogram {
    [TestFixture()]
    public class PotionTest {
        [Test()]
        public void StatsBoostHpTest(){
            Player player = new Player();
            HpPotion hp = new HpPotion(0, 0, 10);

            player.Hp = 85;
            hp.StatsBoost(player);
            Assert.AreEqual(95, player.Hp);

            player.Hp = 94;
            hp.StatsBoost(player);
            Assert.AreEqual(100, player.Hp);
        }

        [Test()]
        public void StatsBoostMpTest(){
            Player player = new Player();
            MpPotion mp = new MpPotion(0, 0, 15);

            player.Mp = 10;
            mp.StatsBoost(player);
            Assert.AreEqual(25, player.Mp);

            player.Mp = 94;
            mp.StatsBoost(player);
            Assert.AreEqual(100, player.Mp);
        }
    }
}