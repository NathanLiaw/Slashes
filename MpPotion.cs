using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child of potion class that has 1 field, 1 property, and 2 methods
    /// </summary>
    public class MpPotion:Potion {
        private double _mp;

        /// <summary>
        /// This is a parameterised constructor that accepts 3 parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mp"></param>
        /// <returns></returns>
        public MpPotion(double x, double y, double mp):base(x, y){
            base.Name = "Mp Potion";
            base.Description = "Regen " + mp + "mp";
            base.SpawnChance = 5;
            base.ItemBitmap = SplashKit.BitmapNamed("mana potion.png");
            base.ItemSprite = SplashKit.CreateSprite(base.ItemBitmap);
            _mp = mp;
        }

        /// <summary>
        /// This is a property for _mp field using getter and setter
        /// </summary>
        /// <value></value>
        public double Mp {
            get {return _mp;}
            set {_mp = value;}
        }

        /// <summary>
        /// This is an override method to boost the stats of the player
        /// </summary>
        /// <param name="player"></param>
        public override void StatsBoost(Player player)
        {
            player.Mp += _mp;
            player.Mp = player.Mp > player.MaxMp ? player.MaxMp : player.Mp;
        }

        /// <summary>
        /// This is an override to draw mp potion object
        /// </summary>
        public override void DrawItem()
        {
            SplashKit.DrawSprite(base.ItemSprite);
            SplashKit.SpriteSetX(base.ItemSprite, (float)base.X);
            SplashKit.SpriteSetY(base.ItemSprite, (float)base.Y);
        }
    }
}