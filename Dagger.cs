using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child od weapon class with 1 field, 1 property, and 2 methods
    /// </summary>
    public class Dagger:Weapon {
        private double _movementSpeed;
        /// <summary>
        /// This is a parameterised constructor that accepts 2 parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Dagger(double x, double y):base(x, y){
            base.Name = "Dagger";
            base.Description = "+3 dmg, +0.06 movement speed";
            base.SpawnChance = 3;
            base.Damage = 3;
            _movementSpeed = 0.06;
        }

        /// <summary>
        /// This is a property for _movementSpeed field using getter and setter
        /// </summary>
        /// <value></value>
        public double MovementSpeed {
            get {return _movementSpeed;}
            set {_movementSpeed = value;}
        }

        /// <summary>
        /// This is an override method to boost stats of player
        /// </summary>
        /// <param name="player"></param>
        public override void StatsBoost(Player player)
        {
            if(player.Weapon == this){
                player.Damage += base.Damage;
                if(player.MovementSpeed < 3.5f){
                    player.MovementSpeed += _movementSpeed;
                }
            }
        }

        /// <summary>
        /// This is an override method to draw the dagger object
        /// </summary>
        public override void DrawItem()
        {
            base.ItemBitmap = SplashKit.BitmapNamed("dagger.png");
            base.ItemBitmap.SetCellDetails(32, 36, 13, 1, 13);
            base.WeaponAnimation = SplashKit.LoadAnimationScript("dagger", "weapon.txt");
            base.ItemSprite = SplashKit.CreateSprite(base.ItemBitmap, base.WeaponAnimation);
            base.ItemSprite.Scale = 2;
            SplashKit.SpriteStartAnimation(base.ItemSprite, "Dagger");
            SplashKit.SpriteSetX(base.ItemSprite, (float)base.X);
            SplashKit.SpriteSetY(base.ItemSprite, (float)base.Y);
            SplashKit.DrawSprite(base.ItemSprite);
            SplashKit.UpdateSpriteAnimation(base.ItemSprite);
        }
    }
}