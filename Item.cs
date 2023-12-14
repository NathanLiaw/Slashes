using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is an abstract item class with 7 fields, 7 properties, and 2 abstract methods
    /// </summary>
    public abstract class Item {
        private double _x;
        private double _y;
        private string _name;
        private string _description;
        private double _spawnChance;
        private Bitmap _itemBitmap;
        private Sprite _itemSprite;

        #nullable disable
        /// <summary>
        /// This is a parameterised constructor that accepts 2 parameters
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Item(double x, double y){
            _x = x;
            _y = y;
            _name = "";
            _description = "";
            _spawnChance = 0;
            _itemBitmap = null;
        }

        /// <summary>
        /// This is a property for _x field using getter and setter
        /// </summary>
        /// <value></value>
        public double X {
            get {return _x;}
            set {_x = value;}
        }

        /// <summary>
        /// This is a property for _y field using getter and setter
        /// </summary>
        /// <value></value>
        public double Y {
            get {return _y;}
            set {_y = value;}
        }

        /// <summary>
        /// This is a property for _name field using getter and setter
        /// </summary>
        /// <value></value>
        public string Name {
            get {return _name;}
            set {_name = value;}
        }

        /// <summary>
        /// This is a property for _description field using getter and setter
        /// </summary>
        /// <value></value>
        public string Description {
            get {return _description;}
            set {_description = value;}
        }

        /// <summary>
        /// This is a property for _spawnChance field using getter and setter
        /// </summary>
        /// <value></value>
        public double SpawnChance {
            get {return _spawnChance;}
            set {_spawnChance = value;}
        }

        /// <summary>
        /// This is a property for _itemBitmap field using getter and setter
        /// </summary>
        /// <value></value>
        public Bitmap ItemBitmap {
            get {return _itemBitmap;}
            set {_itemBitmap = value;}
        }

        /// <summary>
        /// This is a property for _itemSprite field using getter and setter
        /// </summary>
        /// <value></value>
        public Sprite ItemSprite {
            get {return _itemSprite;}
            set {_itemSprite = value;}
        }

        /// <summary>
        /// This is an abstract method that boost the stats of the player
        /// </summary>
        /// <param name="player"></param>
        public abstract void StatsBoost(Player player);

        /// <summary>
        /// This is an abstract method to draw the item
        /// </summary>
        public abstract void DrawItem();
    }
}