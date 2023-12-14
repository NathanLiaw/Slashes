using System;
using NUnit.Framework;

namespace customprogram {
    [TestFixture()]
    public class PlayerTest {
        [Test()]
        public void UseUltimateTest(){
            Player player = new Player();
            List<Enemy> enemies = new List<Enemy>();
            Skeleton s = new Skeleton(player);
            Golem g = new Golem(player);

            enemies.Add(s);
            enemies.Add(g);
            player.Mp = 100;
            Assert.AreEqual(100, player.Mp);
            player.UseUltimate(enemies);
            Assert.AreEqual(0, player.Mp);
        }

        [Test()]
        public void LevelUpTest(){
            Player player = new Player();
            
            player.Exp = 200;
            player.LevelUp();

            Assert.AreEqual(2, player.Level);
        }
    }
}