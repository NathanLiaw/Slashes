using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a child of character class that has 11 fields, 10 properties, 12 methods
    /// </summary>
    public class Player:Character {
        private const int SCREEN_BORDER = 100;
        private double _mp;
        private double _maxMp;
        private double _lifesteal;
        private double _armor;
        private int _exp;
        private int _level;
        private int _kills;
        private Weapon _weapon;
        private AnimationScript _playerAnimation;
        private KeyCode _lastKeyPressed;

        #nullable disable
        /// <summary>
        /// This is a default constructor for player class
        /// </summary>
        public Player():base(){
            base.X = 2000;
            base.Y = 2000;
            base.MovementSpeed = 1.0f;
            base.Range = 130;
            base.Damage = 200;
            _armor = 30;
            _mp = 0;
            _maxMp = 100;
            _lifesteal = 0.05;
            _exp = 0;
            _level = 1;
            _kills = 0;
            _weapon = null;
            base.CharacterBitmap = SplashKit.BitmapNamed("player.png");
            _playerAnimation = SplashKit.LoadAnimationScript("player", "player.txt");
            base.CharacterSprite = SplashKit.CreateSprite(base.CharacterBitmap, _playerAnimation);
            SplashKit.SpriteStartAnimation(base.CharacterSprite, "IdleRight");
            SplashKit.SpriteSetX(base.CharacterSprite, (float)base.X);
            SplashKit.SpriteSetY(base.CharacterSprite, (float)base.Y); 
        }

        /// <summary>
        /// This is a property for _mp field using getter and setter
        /// </summary>
        /// <value></value>
        public double Mp {
            get {return _mp;}
            set {_mp = value;}
        }

        /// <summary>
        /// This is a property for _maxMp field using getter and setter
        /// </summary>
        /// <value></value>
        public double MaxMp {
            get {return _maxMp;}
            set {_maxMp = value;}
        }

        /// <summary>
        /// This is a property for _lifesteal field using getter and setter
        /// </summary>
        /// <value></value>
        public double Lifesteal {
            get {return _lifesteal;}
            set {_lifesteal = value;}
        }

        /// <summary>
        /// This is a property for _exp field using getter and setter
        /// </summary>
        /// <value></value>
        public int Exp {
            get {return _exp;}
            set {_exp = value;}
        }

        /// <summary>
        /// This is a property for _level field using getter and setter
        /// </summary>
        /// <value></value>
        public int Level {
            get {return _level;}
            set {_level = value;}
        }

        /// <summary>
        /// This is a property for _kills field using getter and setter
        /// </summary>
        /// <value></value>
        public int Kills {
            get {return _kills;}
            set {_kills = value;}
        }

        /// <summary>
        /// This is a property for _weapon field using getter and setter
        /// </summary>
        /// <value></value>
        public Weapon Weapon {
            get {return _weapon;}
            set {_weapon = value;}
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
        /// This is a property for _playerAnimation field using getter and setter
        /// </summary>
        /// <value></value>
        public AnimationScript PlayerAnimation {
            get {return _playerAnimation;}
            set {_playerAnimation = value;}
        }

        /// <summary>
        /// This is a property for _lastKeyPressed field using getter and setter
        /// </summary>
        /// <value></value>
        public KeyCode LastKeyPressed {
            get {return _lastKeyPressed;}
            set {_lastKeyPressed = value;}
        }

        /// <summary>
        /// This is an override method for player to attack other enemies class
        /// </summary>
        /// <param name="c"></param>
        public override void Attack(Character c)
        {
            // Mouse position
            double mouseX = Camera.X + SplashKit.MousePosition().X;
            double mouseY = Camera.Y + SplashKit.MousePosition().Y;

            // Distance of enemy to player
            double distanceX = c.X - base.X;
            double distanceY = c.Y - base.Y;
            double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);   

            if(c is Enemy && !c.Die()){
                // Right side of player
                if(SplashKit.MouseClicked(MouseButton.LeftButton) && mouseX > base.X && SplashKit.CurrentTicks() - base.AttackCooldown > 5){
                    SplashKit.SpriteStartAnimation(base.CharacterSprite, "AttackRight");
                    
                    if (distance <= base.Range && c.X > base.X) 
                    {
                        base.AttackCooldown = SplashKit.CurrentTicks();
                        if(c is Golem){
                            Golem golem = (Golem)c;
                            golem.Hp -= base.Damage - golem.DamageReduction;
                        }
                        c.Hp -= base.Damage;
                        base.Hp += base.Damage * _lifesteal;
                        base.Hp = base.Hp > base.MaxHp ? base.MaxHp : base.Hp;
                    }
                }
                // Left side of player
                else if(SplashKit.MouseClicked(MouseButton.LeftButton) && mouseX < base.X && SplashKit.CurrentTicks() - base.AttackCooldown > 5){
                    SplashKit.SpriteStartAnimation(base.CharacterSprite, "AttackLeft");
                    if(distance <= base.Range && c.X < base.X)
                    {
                        base.AttackCooldown = SplashKit.CurrentTicks();
                        if(c is Golem){
                            Golem golem = (Golem)c;
                            golem.Hp -= base.Damage - golem.DamageReduction;
                        }
                        c.Hp -= base.Damage;
                        base.Hp += base.Damage * _lifesteal;
                        base.Hp = base.Hp > base.MaxHp ? base.MaxHp : base.Hp;
                    }
                }
            }    
        }

        /// <summary>
        /// This is a method for player to use ultimate on the enemies
        /// </summary>
        /// <param name="enemies"></param>
        public void UseUltimate(List<Enemy> enemies){
            Circle circle = new Circle();
            circle.Center.X = Camera.X + SplashKit.MousePosition().X;
            circle.Center.Y = Camera.Y + SplashKit.MousePosition().Y;
            circle.Radius = 300;
            SplashKit.FillCircle(Color.MintCream, circle);

            foreach(Enemy enemy in enemies){
                if(Math.Abs(enemy.CharacterSprite.X - circle.Center.X) < circle.Radius && Math.Abs(enemy.CharacterSprite.Y - circle.Center.Y) < circle.Radius){
                    enemy.Hp -= 100 + base.Damage * 2;
                }
            }
            _mp -= 100;
        }

        /// <summary>
        /// This is a method for player to move in the map using keyboard inputs
        /// </summary>
        public void Move()
        {
            // Calculate camera position to keep player in center
            double cameraX = base.X - 50 - SplashKit.ScreenWidth() / 2;
            double cameraY = base.Y - 50 - SplashKit.ScreenHeight() / 2;

            // Test edge of screen boundaries to adjust the camera
            double leftEdge = cameraX + SCREEN_BORDER;
            double rightEdge = leftEdge + SplashKit.ScreenWidth() - 2 * SCREEN_BORDER;
            double topEdge = cameraY + SCREEN_BORDER;
            double bottomEdge = topEdge + SplashKit.ScreenHeight() - 2 * SCREEN_BORDER;
            
            if(base.X >= 650 && base.X <= 4050 && base.Y >= 360 && base.Y <= 4150){  
                if (SplashKit.KeyDown(KeyCode.AKey)) { 
                    base.X -= base.MovementSpeed;
                }
                if (SplashKit.KeyDown(KeyCode.DKey)){
                    base.X += base.MovementSpeed;
                }  
                if (SplashKit.KeyDown(KeyCode.SKey)){
                    base.Y += base.MovementSpeed;
                }   
                if (SplashKit.KeyDown(KeyCode.WKey)){
                    base.Y -= base.MovementSpeed;
                }   
            }
            else if(base.X < 650){
                base.X = 651;
                SplashKit.DrawText("BORDER!!!", Color.Red, "minecraft.ttf", 32, Camera.X + 570, Camera.Y + 200);
            }
            else if(base.X > 4050){
                base.X = 4049;
                SplashKit.DrawText("BORDER!!!", Color.Red, "minecraft.ttf", 32, Camera.X + 570, Camera.Y + 200);
            }
            else if(base.Y < 360){
                base.Y = 361;
                SplashKit.DrawText("BORDER!!!", Color.Red, "minecraft.ttf", 32, Camera.X + 570, Camera.Y + 200);
            }
            else if(base.Y > 4150){
                base.Y = 4149;
                SplashKit.DrawText("BORDER!!!", Color.Red, "minecraft.ttf", 32, Camera.X + 570, Camera.Y + 200);
            }

            // Handle animation
            if(SplashKit.KeyReleased(KeyCode.WKey) || SplashKit.KeyReleased(KeyCode.AKey) || SplashKit.KeyReleased(KeyCode.DKey) || SplashKit.KeyReleased(KeyCode.SKey)){
                if(_lastKeyPressed == KeyCode.DKey){
                    SplashKit.SpriteStartAnimation(base.CharacterSprite, "IdleRight");
                }
                if(_lastKeyPressed == KeyCode.AKey){
                    SplashKit.SpriteStartAnimation(base.CharacterSprite, "IdleLeft");
                } 
            }
            if(SplashKit.KeyTyped(KeyCode.WKey)){
                if(_lastKeyPressed == KeyCode.AKey){
                    SplashKit.SpriteStartAnimation(base.CharacterSprite, "WalkLeft");
                    _lastKeyPressed = KeyCode.AKey;
                }
                if(_lastKeyPressed == KeyCode.DKey){
                    SplashKit.SpriteStartAnimation(base.CharacterSprite, "WalkRight");
                    _lastKeyPressed = KeyCode.DKey;
                }
            }
            if(SplashKit.KeyTyped(KeyCode.DKey)){
                SplashKit.SpriteStartAnimation(base.CharacterSprite, "WalkRight");
                _lastKeyPressed = KeyCode.DKey;
            }
            if(SplashKit.KeyTyped(KeyCode.AKey)){
                SplashKit.SpriteStartAnimation(base.CharacterSprite, "WalkLeft");
                _lastKeyPressed = KeyCode.AKey;
            }
            if(SplashKit.KeyTyped(KeyCode.SKey)){
                if(_lastKeyPressed == KeyCode.DKey){
                    SplashKit.SpriteStartAnimation(base.CharacterSprite, "WalkRight");
                    _lastKeyPressed = KeyCode.DKey;
                }
                if(_lastKeyPressed == KeyCode.AKey){
                    SplashKit.SpriteStartAnimation(base.CharacterSprite, "WalkLeft");
                    _lastKeyPressed = KeyCode.AKey;
                }
            }
            if(SplashKit.KeyTyped(KeyCode.SpaceKey)){
                SplashKit.SpriteStartAnimation(base.CharacterSprite, "Die");
            }      
           
            SplashKit.SpriteSetX(base.CharacterSprite, (float)Camera.X + 550);
            SplashKit.SpriteSetY(base.CharacterSprite, (float)Camera.Y + 210);            
        }

        /// <summary>
        /// This is a bool method for player to pick up items dropped by enemies
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool PickUpItem(Item item){
            if(base.CharacterSprite.SpriteCollision(item.ItemSprite)){
                if(item is Weapon weapon){
                    _weapon = weapon;
                    item.StatsBoost(this);
                    return true;
                }
                else if(item is Potion){
                    item.StatsBoost(this);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This is a void method for player object to level up when exp reaches 100
        /// </summary>
        public void LevelUp(){            
            if(_exp >= 100){
                _exp = 0;
                _level += 1;
                base.Hp += 0.5;
                base.Damage += 0.5;
                _lifesteal += 0.02;
                _armor += 20;
                if(base.MovementSpeed < 3)
                base.MovementSpeed += 0.05;
            }
        }

        /// <summary>
        /// This is a void method to draw player object
        /// </summary>
        public void DrawPlayer(){    
            base.CharacterBitmap.SetCellDetails(167, 167, 12, 14, 168);
            SplashKit.DrawSprite(base.CharacterSprite);
            SplashKit.UpdateSpriteAnimation(base.CharacterSprite);
        }

        /// <summary>
        /// This method draw the exp bar
        /// </summary>
        public void DrawExp(){
            Color c = Color.CadetBlue;
            c.A = 128;
            SplashKit.FillRectangle(c, Camera.X, Camera.Y, SplashKit.ScreenWidth(), 50);
            SplashKit.FillRectangle(Color.CornflowerBlue, Camera.X, Camera.Y, (SplashKit.ScreenWidth() / 97) * _exp, 50);
            SplashKit.DrawText("Lvl " + Convert.ToString(_level), Color.White, "minecraft.ttf", 36, Camera.X + 10, Camera.Y + 10);
        }

        /// <summary>
        /// This method draw the hp bar
        /// </summary>
        public void DrawHp(){
            SplashKit.FillRectangle(Color.Red, Camera.X + 100, Camera.Y + 80,  200, 20);
            SplashKit.FillRectangle(Color.LightGreen, Camera.X + 100, Camera.Y + 80, (base.Hp / base.MaxHp) * 200, 20);
            SplashKit.FillRectangle(Color.Black, Camera.X + 300, Camera.Y + 80, _armor, 20);
            SplashKit.DrawText(base.Hp.ToString("0.##") + "/" + Convert.ToString(base.MaxHp), Color.White, "minecraft.ttf", 16, Camera.X + 320 + Armor, Camera.Y + 83);
        }

        /// <summary>
        /// This method draw the mp bar
        /// </summary>
        public void DrawMp(){
            SplashKit.DrawRectangle(Color.LightGoldenrodYellow, Camera.X + 100, Camera.Y + 120, 200, 20);
            SplashKit.FillRectangle(Color.LightGoldenrodYellow, Camera.X + 100, Camera.Y + 120, _mp/_maxMp * 200, 20);
            SplashKit.DrawText(Convert.ToString(Mp) + "/" + Convert.ToString(MaxMp), Color.White, "minecraft.ttf", 16, Camera.X + 320, Camera.Y + 123);
            if(_mp >= _maxMp){
                SplashKit.FillRectangle(Color.Yellow, Camera.X + 100, Camera.Y + 120, _mp/_maxMp * 200, 20);
                SplashKit.DrawText("Ultimate is ready! Right-click on the area to use", Color.White, "minecraft.ttf", 16, Camera.X + 400, Camera.Y + 120);
            }
        }

        /// <summary>
        /// This method draw the kills
        /// </summary>
        public void DrawKills(){
            Bitmap skull = SplashKit.LoadBitmap("skull", "skull.png");
            SplashKit.DrawBitmap(skull, Camera.X + SplashKit.ScreenWidth() - 100 * 1.5f, Camera.Y + 100, SplashKit.OptionScaleBmp(4, 4));
            SplashKit.DrawText(Convert.ToString(_kills), Color.White, "minecraft.ttf", 36, Camera.X + SplashKit.ScreenWidth() - 100, Camera.Y + 90);
        }

        /// <summary>
        /// This method draw the last weapon picked uo by player
        /// </summary>
        public void DrawWeapon(){
            if(_weapon != null){
                _weapon.ItemSprite.Scale = 1;
                _weapon.ItemSprite.X = (float)Camera.X + 250;
                _weapon.ItemSprite.Y = (float)Camera.Y + 150;
                SplashKit.DrawSprite(_weapon.ItemSprite);
            }
        }

        /// <summary>
        /// This method draw the profile photo of player and other stats
        /// </summary>
        public void DrawProfile(){
            Bitmap profileIcon = SplashKit.BitmapNamed("playerIcon.png");
            SplashKit.FillRectangle(Color.DimGray, Camera.X + 5, Camera.Y + 70, 90, 78);
            SplashKit.DrawBitmap(profileIcon, Camera.X + 20, Camera.Y + 80, SplashKit.OptionScaleBmp(1.5f,1.5f));
            SplashKit.DrawText("Damage: " + Convert.ToString(base.Damage), Color.White, "minecraft.ttf", 16, Camera.X + 5, Camera.Y + 150);
            SplashKit.DrawText("Range: " + Convert.ToString(base.Range), Color.White, "minecraft.ttf", 16, Camera.X + 5, Camera.Y + 165);
            SplashKit.DrawText("Movement Speed: " + base.MovementSpeed.ToString("0.##"), Color.White, "minecraft.ttf", 16, Camera.X + 5, Camera.Y + 180);
            SplashKit.DrawText("Lifesteal: " + Lifesteal.ToString("0.##"), Color.White, "minecraft.ttf", 16, Camera.X + 5, Camera.Y + 195);
            SplashKit.DrawText("Armor: " + Armor.ToString("0.##"), Color.White, "minecraft.ttf", 16, Camera.X + 5, Camera.Y + 210);
        }
    }
}