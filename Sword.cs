using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child of weapon class that has 3 fields, 3 properties, and 2 methods
    /// </summary>
    public class Sword:Weapon {
        private double _hp;
        private double _maxHp;
        private double _armor;

        /// <summary>
        /// This is a parameterised method that accpets 2 parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Sword(double x, double y):base(x, y){
            base.Name = "Sword";
            base.Description = "+2 dmg, +5 hp, +5 max hp, +10 armor";
            base.SpawnChance = 3;
            base.Damage = 2;
            _hp = 5;
            _maxHp = 5;
            _armor = 10;
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
        /// This is a property for _maxHp field using getter and setter
        /// </summary>
        /// <value></value>
        public double MaxHP {
            get {return _maxHp;}
            set {_maxHp = value;}
        }

        /// <summary>
        /// This is a property for _armor field using getter and setter
        /// </summary>
        /// <value></value>
        public double Armor {
            get {return _armor;}
            set {_armor = value;}
        }

        /// <summary>
        /// This is an override method to boost the stats of the player
        /// </summary>
        /// <param name="player"></param>
        public override void StatsBoost(Player player)
        {
            // Give player damage, max hp, hp, and armor
            if(player.Weapon == this){
                player.Damage += base.Damage;
                player.MaxHp += _maxHp;
                player.Hp += _hp;
                player.Armor += _armor;
            }
        }

        /// <summary>
        /// This is an override method to draw the sword object
        /// </summary>
        public override void DrawItem()
        {
            base.ItemBitmap = SplashKit.BitmapNamed("sword.png");
            base.ItemBitmap.SetCellDetails(36, 34, 2, 6, 12);
            base.WeaponAnimation = SplashKit.LoadAnimationScript("sword", "weapon.txt");
            base.ItemSprite = SplashKit.CreateSprite(base.ItemBitmap, base.WeaponAnimation);
            base.ItemSprite.Scale = 2;
            SplashKit.SpriteStartAnimation(base.ItemSprite, "Sword");
            SplashKit.SpriteSetX(base.ItemSprite, (float)base.X);
            SplashKit.SpriteSetY(base.ItemSprite, (float)base.Y);
            SplashKit.DrawSprite(base.ItemSprite);
            SplashKit.UpdateSpriteAnimation(base.ItemSprite);
        }
    }
}