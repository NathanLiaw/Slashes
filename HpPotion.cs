using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child of potion class with 1 field, 1 property and 2 methods
    /// </summary>
    public class HpPotion:Potion {
        private double _hp;

        /// <summary>
        /// This is a parameterised constructor that accepts 3 parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="hp"></param>
        /// <returns></returns>
        public HpPotion(double x, double y, double hp):base(x, y){
            base.Name = "Hp Potion";
            base.Description = "Heal " + hp + "hp";
            base.SpawnChance = 15;
            base.ItemBitmap = SplashKit.BitmapNamed("health potion.png");
            base.ItemSprite = SplashKit.CreateSprite(base.ItemBitmap);
            _hp = hp;
        }

        /// <summary>
        /// This is a property for _hp field using getter and setter
        /// </summary>
        /// <value></value>
        public double Hp {
            get {return _hp;}
            set {_hp = value;}
        }

        /// <summary>
        /// This is an override to boost stats of player
        /// </summary>
        /// <param name="player"></param>
        public override void StatsBoost(Player player)
        {
            player.Hp += _hp;
            player.Hp = player.Hp > player.MaxHp ? player.MaxHp : player.Hp;
        }

        /// <summary>
        /// This is an override to draw the hp potion object
        /// </summary>
        public override void DrawItem()
        {
            SplashKit.DrawSprite(base.ItemSprite);
            SplashKit.SpriteSetX(base.ItemSprite, (float)base.X);
            SplashKit.SpriteSetY(base.ItemSprite, (float)base.Y);
        }
    }
}