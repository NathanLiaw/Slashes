using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is an abstract class that has 10 fields, 10 properties, and 2 methods
    /// </summary>
    public abstract class Character {
        private double _x;
        private double _y;
        private double _hp;
        private double _maxHp;
        private double _damage;
        private double _range;
        private double _movementSpeed;
        #nullable disable
        private Bitmap _characterBitmap;
        private Sprite _characterSprite;
        private uint _attackCooldown;

        /// <summary>
        /// This is a default constructor for character class
        /// </summary>
        public Character(){
            _x = 0;
            _y = 0;
            _hp = 100;
            _maxHp = 100;
            _damage = 10;
            _range = 100;
            _movementSpeed = 1.0f;
            _characterBitmap = null;
            _characterSprite = null;
            _attackCooldown = SplashKit.CurrentTicks();
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
        public double MaxHp {
            get {return _maxHp;}
            set {_maxHp = value;}
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
        /// This is a property for _range field using getter and setter
        /// </summary>
        /// <value></value>
        public double Range {
            get {return _range;}
            set {_range = value;}
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
        /// This is a property for _characterBitmap field using getter and setter
        /// </summary>
        /// <value></value>
        public Bitmap CharacterBitmap {
            get {return _characterBitmap;}
            set {_characterBitmap = value;}
        }

        /// <summary>
        /// This is a property for _characterSprite field using getter and setter
        /// </summary>
        /// <value></value>
        public Sprite CharacterSprite {
            get {return _characterSprite;}
            set {_characterSprite = value;}
        }

        /// <summary>
        /// This is a property for _attackCooldown field using getter and setter
        /// </summary>
        /// <value></value>
        public uint AttackCooldown {
            get {return _attackCooldown;}
            set {_attackCooldown = value;}
        }

        /// <summary>
        /// This is an abstract method for objects to attack
        /// </summary>
        /// <param name="c"></param>
        public abstract void Attack(Character c);
        
        /// <summary>
        /// This is bool method to check if the hp of an object is below 0
        /// </summary>
        /// <returns></returns>
        public bool Die(){
            if(_hp <= 0){
                return true;
            }
            return false;
        }
    }
}