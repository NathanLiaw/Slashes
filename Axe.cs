using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child of weapon class that has 1 field, 1 property, and 2 methods
    /// </summary>
    public class Axe:Weapon {
        private double _lifesteal;

        /// <summary>
        /// This is a parameterised constructor that accepts 2 parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Axe(double x, double y):base(x, y){
            base.Name = "Axe";
            base.Description = "+1 dmg, +0.4 lifesteal";
            base.SpawnChance = 3;
            base.Damage = 1;
            _lifesteal = 0.03;
        }

        /// <summary>
        /// This is a property for _lifesteal field using getter and setter
        /// </summary>
        /// <value></value>
        public double Lifesteal {
            get {return _lifesteal;}
            set {_lifesteal = value;}
        }

        /// <summary>
        /// This is an override method to boost the stats of a player
        /// </summary>
        /// <param name="player"></param>
        public override void StatsBoost(Player player)
        {
            if(player.Weapon == this){
                player.Damage += base.Damage;
                player.Lifesteal += _lifesteal;
            }
        }

        /// <summary>
        /// This is an override method to draw the axe object
        /// </summary>
        public override void DrawItem()
        {
            base.ItemBitmap = SplashKit.BitmapNamed("axe.png");
            base.ItemBitmap.SetCellDetails(32, 32, 6, 10, 60);
            base.WeaponAnimation = SplashKit.LoadAnimationScript("axe", "weapon.txt");
            base.ItemSprite = SplashKit.CreateSprite(base.ItemBitmap, base.WeaponAnimation);
            base.ItemSprite.Scale = 2;
            SplashKit.SpriteStartAnimation(base.ItemSprite, "Axe");
            SplashKit.SpriteSetX(base.ItemSprite, (float)base.X);
            SplashKit.SpriteSetY(base.ItemSprite, (float)base.Y);
            SplashKit.DrawSprite(base.ItemSprite);
            SplashKit.UpdateSpriteAnimation(base.ItemSprite);
        }
    }
}