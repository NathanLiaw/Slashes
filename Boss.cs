using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child of enemy class that has 2 fields, 2 properties and 4 methods
    /// </summary>
    public class Boss:Enemy {
        private double _manaDrain;
        private AnimationScript _bossAnimation;
  
        #nullable disable
        /// <summary>
        /// This is a parameterised constructor that accepts 1 parameter
        /// </summary>
        /// <param name="player"></param>
        public Boss(Player player):base(player){
            _manaDrain = 2;
            base.ClassType = "Boss";
            base.Hp = 3000;
            base.MaxHp = 3000;
            base.Damage = 10;
            base.Range = 300;
            base.MovementSpeed = 0.8f;
            base.AttackRange = 130;
            base.CharacterBitmap = SplashKit.BitmapNamed("boss.png");
            _bossAnimation = SplashKit.LoadAnimationScript("boss", "boss.txt");
            base.CharacterSprite = SplashKit.CreateSprite(base.CharacterBitmap, _bossAnimation);
            base.CharacterSprite.Scale = 3;
            SplashKit.SpriteSetX(base.CharacterSprite, (float)base.X);
            SplashKit.SpriteSetY(base.CharacterSprite, (float)base.Y);
            SplashKit.SpriteStartAnimation(CharacterSprite, "Idle");
            SplashKit.CreateTimer("lastDamaged");
        }

        /// <summary>
        /// This is a property for _manaDrain field using getter and setter
        /// </summary>
        /// <value></value>
        public double ManaDrain {
            get {return _manaDrain;}
            set {_manaDrain = value;}
        }

        /// <summary>
        /// This is a property for _bossAnimation field using getter and setter
        /// </summary>
        /// <value></value>
        public AnimationScript BossAnimation{
            get {return _bossAnimation;}
            set {_bossAnimation = value;}
        }

        /// <summary>
        /// This is an override method for boss object to attack a player
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
                        }
                        else {
                            player.Hp -= base.Damage;
                        }
                    player.Mp -= _manaDrain;
                    player.Mp = player.Mp > player.MaxMp ? player.MaxMp : player.Mp;
                }
            }
            base.CurrentState = EnemyState.Chase;
        }

        /// <summary>
        /// This is an override method to draw boss object
        /// </summary>
        public override void DrawEnemy()
        {
            base.CharacterBitmap.SetCellDetails(100, 102, 10, 9, 90);
            
            // Draw Sprite
            SplashKit.DrawSprite(base.CharacterSprite);
            SplashKit.UpdateSpriteAnimation(base.CharacterSprite);
            
            // Draw HP Bar
            SplashKit.FillRectangle(Color.Red, base.X - 15, base.Y - 45, 120, 10);
            SplashKit.FillRectangle(Color.LightGreen, base.X - 15, base.Y - 45, (base.Hp / base.MaxHp) * 120, 10);
        }

        /// <summary>
        /// This is an override method for boss object to drop item after it's death
        /// </summary>
        /// <returns></returns>
        public override Item DropItem(){
            Random random = new Random();
            int dropChance = random.Next(1, 101);
            
            // Initialise items first
            HpPotion hp = new HpPotion(base.X, base.Y, 10);
            MpPotion mp = new MpPotion(base.X, base.Y, 10);
            mp.SpawnChance = 15;
            Spear spear = new Spear(base.X, base.Y);
            Dagger dagger = new Dagger(base.X, base.Y);
            Sword sword = new Sword(base.X, base.Y);
            Axe axe = new Axe(base.X, base.Y);
            spear.SpawnChance = 10;
            sword.SpawnChance = 10;

            if(dropChance <= mp.SpawnChance){
                return mp;
            }
            else if(dropChance <= mp.SpawnChance + spear.SpawnChance){
                return spear;
            }
            else if(dropChance <= mp.SpawnChance + spear.SpawnChance + sword.SpawnChance){
                return sword;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// This is an override method that allows the boss object to level up after a certain interval
        /// </summary>
        /// <param name="timeElapsed"></param>
        public override void LevelUp(uint timeElapsed)
        {
            if(SplashKit.CurrentTicks() - timeElapsed > 90000){
                base.Damage = 15;
                _manaDrain = 4;
                base.MovementSpeed = 1.3f;
            }
            else if(SplashKit.CurrentTicks() - timeElapsed > 230000){
                base.Range = 800;
                base.Damage = 17;
                _manaDrain = 5;
                base.MovementSpeed = 1.4f;
            }
        }

    }
}