using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child for enemy class that has 1 field, 1 property, 4 methods
    /// </summary>
    public class Golem:Enemy {
        private double _damageReduction;

        /// <summary>
        /// This is a parameterised constructor that accepts 1 parameter
        /// </summary>
        /// <param name="player"></param>
        public Golem(Player player):base(player){
            _damageReduction = 0.4;
            base.ClassType = "Golem";
            base.Hp = 150;
            base.MaxHp = 150;
            base.Damage = 2;
            base.Range = 350;
            base.AttackRange = 130;
            base.MovementSpeed = 0.9f;
            base.CharacterBitmap = SplashKit.BitmapNamed("golem.png");
            base.CharacterSprite = SplashKit.CreateSprite(base.CharacterBitmap);
            SplashKit.SpriteSetX(base.CharacterSprite, (float)base.X);
            SplashKit.SpriteSetY(base.CharacterSprite, (float)base.Y);
        }

        /// <summary>
        /// This is a property for _damageReduction field using getter and setter
        /// </summary>
        /// <value></value>
        public double DamageReduction {
            get {return _damageReduction;}
            set {_damageReduction = value;}
        }

        /// <summary>
        /// This is an override method for golem object to attack player
        /// </summary>
        /// <param name="c"></param>
        public override void Attack(Character c)
        {
            if(c is Player){
                Player player = (Player)c;
                if(base.WithinAttackRange(c) && SplashKit.CurrentTicks() - base.AttackCooldown > 500){
                    base.AttackCooldown = SplashKit.CurrentTicks();
                    if(player.Armor > 0){
                            player.Armor -= base.Damage;
                            player.Armor = player.Armor <= 0 ? 0: player.Armor; 
                        }
                    else {
                        player.Hp -= base.Damage;
                    }
                }
            }
            base.CurrentState = EnemyState.Chase;
        }

        /// <summary>
        /// This is an override method to draw the golem object
        /// </summary>
        public override void DrawEnemy()
        {
            // Draw Sprite
            SplashKit.DrawSprite(base.CharacterSprite);
            
            // Draw HP Bar
            SplashKit.FillRectangle(Color.Red, base.X, base.Y - 10, 80, 10);
            SplashKit.FillRectangle(Color.LightGreen, base.X, base.Y - 10, (base.Hp / base.MaxHp) * 80, 10);
        }

        #nullable disable
        /// <summary>
        /// This is an override method for golem class to drop item upon death
        /// </summary>
        /// <returns></returns>
        public override Item DropItem(){
            Random random = new Random();
            int dropChance = random.Next(1, 101);
            
            // Initialise items first
            HpPotion hp = new HpPotion(base.X, base.Y, 10);
            MpPotion mp = new MpPotion(base.X, base.Y, 10);
            hp.SpawnChance = 20;
            mp.SpawnChance = 10;
            Sword sword = new Sword(base.X, base.Y);
            sword.SpawnChance = 5;

            if(dropChance <= hp.SpawnChance){
                return hp;
            }
            else if(dropChance <= hp.SpawnChance + mp.SpawnChance){
                return mp;
            }
            else if(dropChance <= hp.SpawnChance + mp.SpawnChance + sword.SpawnChance){
                return sword;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// This is an override method that allows golem object to kevel up after 1.30 mins
        /// </summary>
        /// <param name="timeElapsed"></param>
        public override void LevelUp(uint timeElapsed)
        {
            if(SplashKit.CurrentTicks() - timeElapsed > 90000 && base.Damage == 2){
                base.Hp = 180;
                base.MaxHp = 180;
                base.Damage = 4;
                base.AttackRange = 150;
                _damageReduction = 0.8;
            }
        }
    }
}