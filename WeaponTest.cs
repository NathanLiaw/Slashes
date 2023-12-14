using System;
using NUnit.Framework;

namespace customprogram {
    [TestFixture()]
    public class WeaponTest {
        [Test()]
        public void DaggerStatsBoostTest(){
            Player player = new Player();
            Dagger dagger = new Dagger(0, 0);
            
            player.Weapon = dagger;
            dagger.StatsBoost(player);
            Assert.AreEqual(11, player.Damage);
        }

        [Test()]
        public void SpearStatsBoostTest(){
            Player player = new Player();
            Spear spear = new Spear(0, 0);

            player.Weapon = spear;
            spear.StatsBoost(player);
            Assert.AreEqual(9, player.Damage);
            Assert.AreEqual(135, player.Range);
        }

        [Test()]
        public void SwordStatsBoost(){
            Player player = new Player();
            Sword sword = new Sword(0, 0);

            player.Weapon = sword;
            sword.StatsBoost(player);
            Assert.AreEqual(10, player.Damage);
            Assert.AreEqual(105, player.MaxHp);
            Assert.AreEqual(105, player.Hp);
        }

        [Test()]
        public void AxeStatsBoostTest(){
            Player player = new Player();
            Axe axe = new Axe(0, 0);

            player.Weapon = axe;
            axe.StatsBoost(player);
            Assert.AreEqual(9, player.Damage);
            Assert.AreEqual(0.08, player.Lifesteal);
        }
    }
}