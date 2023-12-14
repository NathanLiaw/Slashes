using System;
using NUnit.Framework;

namespace customprogram {
    [TestFixture()]
    public class EnemyTest {
        [Test()]
        public void WithinAttackRangeTest(){
            Player player = new Player();
            Assassin assassin = new Assassin(player);
            player.X = 10;
            player.Y = 10;
            assassin.X = 20;
            assassin.Y = 20;
            Assert.AreEqual(true, assassin.WithinAttackRange(player));
        }

    }
}