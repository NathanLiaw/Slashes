using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is a game class for managing most classes and has 11 fields, 9 properties, 3 methods
    /// </summary>
    public class Game {
        private const int WINDOW_WIDTH = 1270;
        private const int WINDOW_HEIGHT = 720;
        private bool _gameStart;
        private bool _gameOver;
        private bool _gamePause;
        private bool _playerWin;
        private Player _player;
        private List<Enemy> _enemies;
        private int _amountOfEnemies;
        private List<Item> _itemsDropped;
        private uint _timePassed;

        /// <summary>
        /// This is a default constructor for game class
        /// </summary>
        public Game(){
            _gameStart = false;
            _gameOver = false;
            _gamePause = false;
            _playerWin = false;
            _player = new Player();
            _enemies = new List<Enemy>();
            _itemsDropped = new List<Item>();
            _amountOfEnemies = _enemies.Count;
            _timePassed = SplashKit.CurrentTicks();
        }

        /// <summary>
        /// This is a property for _gameStart field using getter and setter
        /// </summary>
        /// <value></value>
        public bool GameStart {
            get {return _gameStart;}
            set {_gameStart = value;}
        }

        /// <summary>
        /// This is a property for _gameOver field using getter and setter
        /// </summary>
        /// <value></value>
        public bool GameOver {
            get {return _gameOver;}
            set {_gameOver = value;}
        
        }

        /// <summary>
        /// This is a property for _gamePause field using getter and setter
        /// </summary>
        /// <value></value>
        public bool GamePause {
            get {return _gamePause;}
            set {_gamePause = value;}
        }

        /// <summary>
        /// This is a property for _playerWin field using getter and setter
        /// </summary>
        /// <value></value>
        public bool PlayerWin {
            get {return _playerWin;}
            set {_playerWin = value;}
        }

        /// <summary>
        /// This is a property for _player field using getter and setter
        /// </summary>
        /// <value></value>
         public Player Player {
            get {return _player;}
            set {_player = value;}
        }

        /// <summary>
        /// This is a property for _enemies field using getter and setter
        /// </summary>
        /// <value></value>
        public List<Enemy> Enemies {
            get {return _enemies;}
            set {_enemies = value;}
        }

        /// <summary>
        /// This is a property for _amountOfEnemies field using getter and setter
        /// </summary>
        /// <value></value>
        public int AmountOfEnemies {
            get {return _enemies.Count;}
            set {_amountOfEnemies = value;}
        }

        /// <summary>
        /// This is a property for _itemsDropped field using getter and setter
        /// </summary>
        /// <value></value>
        public List<Item> ItemsDropped {
            get {return _itemsDropped;}
            set {_itemsDropped = value;}
        }

        /// <summary>
        /// This is a property for _timePassed field using getter and setter
        /// </summary>
        /// <value></value>
        public uint TimePassed {
            get {return _timePassed;}
            set {_timePassed = value;}
        }

        /// <summary>
        /// This is a method to start the game and handle object interactions
        /// </summary>
        public void StartGame(){
            foreach(Enemy enemy in Enemies){
                if(enemy is Boss){
                    Boss boss = (Boss)enemy;
                    if(boss.Die()){
                        PlayerWin = true;
                        GameOver = true;
                    }
                    else if(!boss.Die() && SplashKit.CurrentTicks() - _timePassed > 300000){
                        GameOver = true;
                    }
                }
                if(enemy.Die()){
                    Enemies.Remove(enemy);
                    _player.Kills++;
                    if(enemy is Golem){
                        _player.Exp += 5;
                        if(_player.Mp < _player.MaxMp){
                            _player.Mp += 5;
                            _player.Mp = _player.Mp > _player.MaxMp ? _player.MaxMp : _player.Mp;
                        }
                    }
                    else if(enemy is Skeleton){
                        _player.Exp += 6;
                        if(_player.Mp < _player.MaxMp){
                            _player.Mp += 6;
                            _player.Mp = _player.Mp > _player.MaxMp ? _player.MaxMp : _player.Mp;
                        }
                    }
                    else if(enemy is Assassin){
                        _player.Exp += 7;
                        _player.Mp += 6;
                    }

                    _itemsDropped.Add(enemy.DropItem());
                    break;
                }
                else if(!enemy.Die()){
                    enemy.DrawEnemy();
                    // enemy.Move(_player, Enemies);
                    enemy.UpdateState(_player, Enemies);
                    enemy.LevelUp(TimePassed);         
                }
   
                _player.Attack(enemy);
            }
            
            // Player use ultimate when mana is full 
            if(_player.Mp >= _player.MaxMp && SplashKit.MouseClicked(MouseButton.RightButton)){
                _player.UseUltimate(Enemies);
            }

            // Draw Player-related items
            _player.DrawPlayer();
            _player.DrawHp();
            _player.DrawMp();
            _player.DrawExp();
            _player.DrawKills();
            _player.DrawWeapon();
            _player.DrawProfile();

            // Player mechanics
            _player.Move();
            _player.LevelUp();

             // Check if player dies then game over
            if(_player.Die()){
                GameOver = true;
            }
           
            // Draw items dropped by enemies
            foreach(Item item in _itemsDropped){
                if(item is not null){
                    item.DrawItem();
                    SplashKit.DrawText(item.Name, Color.White, "minecraft.ttf", 16, item.X - 5, item.Y + 40);
                    if(_player.PickUpItem(item)){
                        _itemsDropped.Remove(item);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// This method is to generate enemies in the beginning of the game and during the game
        /// </summary>
        public void GenerateEnemy(){
            // When game start and enemies count = 0, spawn 60 total enemies
            if(AmountOfEnemies == 0){
                Boss boss = new Boss(_player);
                Enemies.Add(boss);
                for(int i = 0; i < 25; i++){
                    Golem golem = new Golem(_player);
                    Enemies.Add(golem);
                }
                for(int j = 0; j < 20; j++){
                    Skeleton skeleton = new Skeleton(_player);
                    Enemies.Add(skeleton);
                }
                for(int k = 0; k < 15; k++){
                    Assassin assassin = new Assassin(_player);
                    Enemies.Add(assassin);
                }
            }
            // When the enemies are killed during game the same amount of enemies killed is generated
            else if(AmountOfEnemies < 60){
                for(int i = 0; i < 60 - AmountOfEnemies; i++){
                    Random random = new Random();
                    int randomEnemy = random.Next(100);
                    if(randomEnemy < 50){
                        Golem golem = new Golem(_player);
                        Enemies.Add(golem);
                    }
                    else if(randomEnemy < 80){
                        Assassin assassin = new Assassin(_player);
                        Enemies.Add(assassin);
                    }
                    else {
                        Skeleton skeleton = new Skeleton(_player);
                        Enemies.Add(skeleton);
                    }
                }
            }
        }

        /// <summary>
        /// This method returns a bitmap matrix for the game map
        /// </summary>
        /// <returns></returns>
       public Bitmap[,] DrawMap(){
            Bitmap grass = SplashKit.LoadBitmap("grassBmp", "grass.png");
            Bitmap grassalt = SplashKit.LoadBitmap("grassaltBmp", "grassalt.png");
            Bitmap flower = SplashKit.LoadBitmap("flowerBmp", "flower.png");
            Bitmap flowers = SplashKit.LoadBitmap("flowersBmp", "flowers.png");
            Bitmap stone = SplashKit.LoadBitmap("stoneBmp", "stone.png");
            Bitmap[,] matrix = new Bitmap[50, 50];

            Vector2D cameraPosition = new Vector2D();
            Random random = new Random();
            
            Bitmap[] bmpArray = {
                grass,
                grassalt,
                flower,
                flowers,
                stone
            };
 
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    int randomNumber = random.Next(100) < 90 ? random.Next(2): random.Next(3, 5);
                    // Choose which bitmap to draw randomly
                    Bitmap bitmapToDraw = bmpArray[randomNumber];

                    // Copy the chosen bitmap to the current tile in the matrix
                    matrix[i, j] = bitmapToDraw;
                }
            }

            int tileWidth = grass.Width;
            int tileHeight = grass.Height;

            int leftTileIndex = (int)Math.Floor(cameraPosition.X / tileWidth);
            int topTileIndex = (int)Math.Floor(cameraPosition.Y / tileHeight);

            int rightTileIndex = leftTileIndex + (int)Math.Ceiling((float)WINDOW_WIDTH / tileWidth) - 3;
            int bottomTileIndex = topTileIndex + (int)Math.Ceiling((float)WINDOW_HEIGHT / tileHeight) - 3;

            int xOffset = -(int)(cameraPosition.X % tileWidth);
            int yOffset = -(int)(cameraPosition.Y % tileHeight);
        
            cameraPosition.X = (float)_player.X - WINDOW_WIDTH / 2;
            cameraPosition.Y = (float)_player.Y - WINDOW_HEIGHT / 2;

            return matrix;
        }
    }
}