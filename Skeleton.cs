using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child of enemy class that has 2 fields, 2 properties and 4 methods
    /// </summary>
    public class Skeleton:Enemy {
        private double _piercingDamage; // Ignore damage reduction from player
        private AnimationScript _skeletonAnimation;

        #nullable disable
        /// <summary>
        /// This is a parameterised constructor that accepts 1 parameter
        /// </summary>
        /// <param name="player"></param>
        public Skeleton(Player player):base(player){
            _piercingDamage = 4;
            base.ClassType = "Skeleton";
            base.Hp = 80;
            base.MaxHp = 80;
            base.Damage = 3;
            base.Range = 300;
            base.AttackRange = 110;
            base.MovementSpeed = 0.8f;
            base.CharacterBitmap = SplashKit.BitmapNamed("skeleton.png");
            _skeletonAnimation = SplashKit.LoadAnimationScript("skeleton", "skeleton.txt");
            base.CharacterSprite = SplashKit.CreateSprite(base.CharacterBitmap, _skeletonAnimation);
            base.CharacterSprite.Scale = 2;
            SplashKit.SpriteSetX(base.CharacterSprite, (float)base.X);
            SplashKit.SpriteSetY(base.CharacterSprite, (float)base.Y);
            SplashKit.SpriteStartAnimation(CharacterSprite, "Idle");
        }

        /// <summary>
        /// This is a property for _piercingDamage using getter and setter
        /// </summary>
        /// <value></value>
        public double PiercingDamage {
            get {return _piercingDamage;}
            set {_piercingDamage = value;}
        }

        /// <summary>
        /// This is a property for _skeletonAnimation using getter and setter
        /// </summary>
        /// <value></value>
        public AnimationScript SkeletonAnimation{
            get {return _skeletonAnimation;}
            set {_skeletonAnimation = value;}
        }

        /// <summary>
        /// This is an override method for skeleton object to attack player
        /// </summary>
        /// <param name="c"></param>
        public override void Attack(Character c)
        {
            if(c is Player){
                Player player = (Player)c;
                if(base.WithinAttackRange(c) && SplashKit.CurrentTicks() - base.AttackCooldown > 500){
                    base.AttackCooldown = SplashKit.CurrentTicks();
                    player.Hp -= _piercingDamage;
                    if(player.Armor > 0){
                        player.Armor -= base.Damage;
                        player.Armor = player.Armor <= 0 ? 0: player.Armor; 
                    }
                    else{
                        player.Armor = 0;
                        player.Hp -= base.Damage;
                    }
                }
            }
            base.CurrentState = EnemyState.Chase;
        }

        /// <summary>
        /// This is an override method to draw skeleton object
        /// </summary>
        public override void DrawEnemy()
        {
            base.CharacterBitmap.SetCellDetails(64, 64, 13, 5, 65);

            // Draw Sprite
            SplashKit.DrawSprite(base.CharacterSprite);
            SplashKit.UpdateSpriteAnimation(base.CharacterSprite);
            
            // Draw HP Bar
            SplashKit.FillRectangle(Color.Red, base.X, base.Y - 15, 60, 10);
            SplashKit.FillRectangle(Color.LightGreen, base.X, base.Y - 15, (base.Hp / base.MaxHp) * 60, 10);
        }

        /// <summary>
        /// This is an override method to drop item when skeleton object dies
        /// </summary>
        /// <returns></returns>
        public override Item DropItem(){
            Random random = new Random();
            int dropChance = random.Next(1, 101);
            
            // Initialise items first
            HpPotion hp = new HpPotion(base.X, base.Y, 15);
            hp.SpawnChance = 15;
            Dagger dagger = new Dagger(base.X, base.Y);
            Sword sword = new Sword(base.X, base.Y);
            dagger.SpawnChance = 10;
            sword.SpawnChance = 10;

            if(dropChance <= hp.SpawnChance){
                return hp;
            }
            else if(dropChance <= hp.SpawnChance + hp.SpawnChance + dagger.SpawnChance){
                return dagger;
            }
            else if(dropChance <= hp.SpawnChance + hp.SpawnChance + dagger.SpawnChance + sword.SpawnChance){
                return sword;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// This is an override method for skeleton object to level up after 3 mins
        /// </summary>
        /// <param name="timeElapsed"></param>
        public override void LevelUp(uint timeElapsed)
        {
            if(SplashKit.CurrentTicks() - timeElapsed > 180000 && base.Damage == 3){
                base.Hp = 100;
                base.MaxHp = 100;
                base.Damage = 5;
                _piercingDamage = 7;
                base.MovementSpeed = 1.1f;
            }
        }
    }
}