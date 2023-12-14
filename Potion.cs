using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is an abstract child of item class with 1 field, 1 property ad 2 methods
    /// </summary>
    public abstract class Potion:Item {
        private string _potionType; 

        /// <summary>
        /// This is a parameterised constructor for potion class that accepts 2 parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Potion(double x, double y):base(x, y){
            _potionType = "";
        }

        /// <summary>
        /// This is a property for potion type using getter and setter
        /// </summary>
        /// <value></value>
        public string PotionType {
            get {return _potionType;}
            set {_potionType = value;}
        }

        /// <summary>
        /// This is an abstract override method for stats boost to boost the stats of the player
        /// </summary>
        /// <param name="player"></param>
        public abstract override void StatsBoost(Player player);

        /// <summary>
        /// This is an abstract override method for drawing item
        /// </summary>
        public abstract override void DrawItem();
    }
}