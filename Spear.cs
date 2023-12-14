using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child of weapon class with 1 field, 1 property, and 2 methods
    /// </summary>
    public class Spear:Weapon {
        private double _range;

        /// <summary>
        /// This is a parameterised constructor that accepts 2 parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Spear(double x, double y):base(x, y){
            base.Name = "Spear";
            base.Description = "+1 dmg, +5 range";
            base.SpawnChance = 3;
            base.Damage = 1;
            _range = 5;
        }

        /// <summary>
        /// This is a property for _range field using getter and setter
        /// </summary>
        /// <value></value>
        public double Range {
            get {return _range;}
            set {_range = value;}
        }

        /// <summary>
        /// This is an override method to boost the stats of the player
        /// </summary>
        /// <param name="player"></param>
        public override void StatsBoost(Player player)
        {
            //Give damage and range to player
            if(player.Weapon == this){
                player.Damage += base.Damage;
                player.Range += _range;
            }
        }

        /// <summary>
        /// This is an override method to draw the spear object
        /// </summary>
        public override void DrawItem()
        {
            base.ItemBitmap = SplashKit.BitmapNamed("spear.png");
            base.ItemBitmap.SetCellDetails(32, 36, 13, 1, 13);
            base.WeaponAnimation = SplashKit.LoadAnimationScript("spear", "weapon.txt");
            base.ItemSprite = SplashKit.CreateSprite(base.ItemBitmap, base.WeaponAnimation);
            base.ItemSprite.Scale = 2;
            SplashKit.SpriteStartAnimation(base.ItemSprite, "Spear");
            SplashKit.SpriteSetX(base.ItemSprite, (float)base.X);
            SplashKit.SpriteSetY(base.ItemSprite, (float)base.Y);
            SplashKit.DrawSprite(base.ItemSprite);
            SplashKit.UpdateSpriteAnimation(base.ItemSprite);
        }
    }
}