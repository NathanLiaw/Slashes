using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is an abstract child of item class and has 2 fields, 2 properties, and 2 methods
    /// </summary>
    public abstract class Weapon:Item {
        private double _damage;
        private AnimationScript _weaponAnimation;

        #nullable disable
        /// <summary>
        /// This is a parameterised constructor that accepts 2 parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Weapon(double x, double y):base(x, y){
            base.X = x;
            base.Y = y;
            _damage = 10;
            _weaponAnimation = null;
        }

        /// <summary>
        /// This is a property for _damage field using getter and setter
        /// </summary>
        /// <value></value>
        public double Damage {
            get {return _damage;}
            set {_damage = value;}
        }

        /// <summary>
        /// This is a property for _weaponAnimation field using getter and setter
        /// </summary>
        /// <value></value>
        public AnimationScript WeaponAnimation {
            get {return _weaponAnimation;}
            set {_weaponAnimation = value;}
        }

        /// <summary>
        /// This is an abstract override method for stats boost
        /// </summary>
        /// <param name="player"></param>
        public abstract override void StatsBoost(Player player);

        /// <summary>
        /// This is abstract override method to draw the weapon object
        /// </summary>
        public abstract override void DrawItem();
    }
}