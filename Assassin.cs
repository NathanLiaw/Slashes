using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child of enemy class that has 2 fields, 2 properties, and 4 methods
    /// </summary>
    public class Assassin:Enemy {
        private double _critChance;
        private AnimationScript _assassinAnimation;

        #nullable disable
        /// <summary>
        /// This is a parameterised constructor that accepts 1 parameter
        /// </summary>
        /// <param name="player"></param>
        public Assassin(Player player):base(player){
            _critChance = 15;
            base.ClassType = "Assassin";
            base.Hp = 100;
            base.MaxHp = 100;
            base.Damage = 5;
            base.Range = 300;
            base.AttackRange = 140;
            base.MovementSpeed = 0.9f;
            base.CharacterBitmap = SplashKit.BitmapNamed("assassin.png");
            _assassinAnimation = SplashKit.LoadAnimationScript("assassinScript", "assassin.txt");
            base.CharacterSprite = SplashKit.CreateSprite(base.CharacterBitmap, _assassinAnimation);
            base.CharacterSprite.Scale = 2;
            SplashKit.SpriteSetX(base.CharacterSprite, (float)base.X);
            SplashKit.SpriteSetY(base.CharacterSprite, (float)base.Y);
            SplashKit.SpriteStartAnimation(CharacterSprite, "Idle");
        }

        /// <summary>
        /// This is a property for _critChance field using getter and setter
        /// </summary>
        /// <value></value>
        public double CritChance {
            get {return _critChance;}
            set {_critChance = value;}
        }

        /// <summary>
        /// This is a property for _assassinAnimation field using getter and setter
        /// </summary>
        /// <value></value>
        public AnimationScript AssassinAnimation{
            get {return _assassinAnimation;}
            set {_assassinAnimation = value;}
        }

        /// <summary>
        /// This is an override method for assassin object to attack a player
        /// </summary>
        /// <param name="c"></param>
        public override void Attack(Character c)
        {
            Random random = new Random();
            int critAttack = random.Next(100);
            if(c is Player){
                Player player = (Player)c;
                if(base.WithinAttackRange(c) && SplashKit.CurrentTicks() - base.AttackCooldown > 500){
                    if(critAttack <= _critChance){
                        base.AttackCooldown = SplashKit.CurrentTicks();
                        if(player.Armor > 0){
                            player.Armor -= (base.Damage * 2);
                            player.Armor = player.Armor <= 0 ? 0: player.Armor; 
                        }
                        else {
                            player.Hp -= (base.Damage * 2);
                        }
                    }
                    else {
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
            }
            base.CurrentState = EnemyState.Chase;
        }

        /// <summary>
        /// This is an override method to draw assassin object
        /// </summary>
        public override void DrawEnemy()
        {
            base.CharacterBitmap.SetCellDetails(80, 80, 23, 5, 115);

            // Draw Sprite
            SplashKit.DrawSprite(base.CharacterSprite);
            SplashKit.UpdateSpriteAnimation(base.CharacterSprite);
            
            // Draw HP Bar
            SplashKit.FillRectangle(Color.Red, base.X + 5, base.Y + 10, 70, 10);
            SplashKit.FillRectangle(Color.LightGreen, base.X + 5, base.Y + 10, (base.Hp / base.MaxHp) * 70, 10);
        }

        /// <summary>
        /// This is an override method that returns an Item object when the assassin object is killed
        /// </summary>
        /// <returns></returns>
        public override Item DropItem(){
            Random random = new Random();
            int dropChance = random.Next(1, 101);
            
            // Initialise items first
            MpPotion mp = new MpPotion(base.X, base.Y, 15);
            mp.SpawnChance = 15;
            Spear spear = new Spear(base.X, base.Y);
            Axe axe = new Axe(base.X, base.Y);
            spear.SpawnChance = 10;
            axe.SpawnChance = 10;

            if(dropChance <= mp.SpawnChance){
                return mp;
            }
            else if(dropChance <= mp.SpawnChance + spear.SpawnChance){
                return spear;
            }
            else if(dropChance <= mp.SpawnChance + spear.SpawnChance + axe.SpawnChance){
                return axe;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// This is an override method that allows assassin object to level up after 2 mins
        /// </summary>
        /// <param name="timeElapsed"></param>
        public override void LevelUp(uint timeElapsed)
        {
            if(SplashKit.CurrentTicks() - timeElapsed > 120000 && base.Damage == 5){
                base.Hp = 120;
                base.MaxHp = 120;
                base.Damage = 6;
                _critChance = 10;
                base.AttackRange = 150;
            }
        }
    }
}