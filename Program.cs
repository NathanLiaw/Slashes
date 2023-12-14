using System;
using SplashKitSDK;

namespace customprogram
{
    public class Program
    {
        public const int WINDOW_WIDTH = 1270;
        public const int WINDOW_HEIGHT = 720;
        public const int SCREEN_BORDER = 100;

        /// <summary>
        /// This method control the camera of the game
        /// </summary>
        /// <param name="player"></param>
        /// <param name="map"></param>
        private static void UpdateCameraPosition(Player player, Bitmap[,] map)
        {
            // Calculate center of screen
            double centerX = Camera.X + SplashKit.ScreenWidth() / 2;
            double centerY = Camera.Y + SplashKit.ScreenHeight() / 2;

            // Calculate camera position to keep player in center. 50 is to keep player centered.
            double cameraX = player.X - 50 - SplashKit.ScreenWidth() / 2;
            double cameraY = player.Y - 50 - SplashKit.ScreenHeight() / 2;

            // Test edge of screen boundaries to adjust the camera
            double leftEdge = cameraX + SCREEN_BORDER;
            double rightEdge = leftEdge + SplashKit.ScreenWidth() - 2 * SCREEN_BORDER;
            double topEdge = cameraY + SCREEN_BORDER;
            double bottomEdge = topEdge + SplashKit.ScreenHeight() - 2 * SCREEN_BORDER;

            if(leftEdge > 50 && topEdge > 50 && rightEdge < 4600 && bottomEdge < 4600){
                if (cameraY < Camera.Y + WINDOW_HEIGHT / 2)
                {
                    SplashKit.MoveCameraTo(Camera.X, topEdge);
                }

                else if (cameraY > Camera.Y + WINDOW_HEIGHT / 2)
                {
                    SplashKit.MoveCameraTo(Camera.X, bottomEdge - SplashKit.ScreenHeight());
                }

                if (cameraX < Camera.X + WINDOW_WIDTH / 2)
                {
                    SplashKit.MoveCameraTo(leftEdge, Camera.Y);
                }
                else if (cameraX > Camera.X + WINDOW_WIDTH / 2)
                {
                    SplashKit.MoveCameraTo(rightEdge - SplashKit.ScreenWidth(), Camera.Y);
                }
            }
            
        }

        /// <summary>
        /// This method draws how-to-play section before game starts
        /// </summary>
        public static void DrawHowToPlay(){
            Color background = Color.SkyBlue;
            background.A = 126;
            SplashKit.FillRectangle(background, Camera.X, Camera.Y, WINDOW_WIDTH, WINDOW_HEIGHT);
            SplashKit.DrawText("How-To-Play", Color.White, "minecraft.ttf", 64, Camera.X + 450, Camera.Y + 50);
            SplashKit.DrawText("1. Kill the boss under 5 minutes to win the game", Color.White, "minecraft.ttf", 32, Camera.X + 50, Camera.Y + 150);
            SplashKit.DrawText("2. Left-Click to attack enemies", Color.White, "minecraft.ttf", 32, Camera.X + 50, Camera.Y + 200);
            SplashKit.DrawText("3. When you kill an enemy, they sometimes drop potions and even weapons", Color.White, "minecraft.ttf", 32, Camera.X + 50, Camera.Y + 250);
            SplashKit.DrawText("4. Level up your character to get stronger", Color.White, "minecraft.ttf", 32, Camera.X + 50, Camera.Y + 300);
            SplashKit.DrawText("5. When your ultimate bar is full, right-click to unleash super attack", Color.White, "minecraft.ttf", 32, Camera.X + 50, Camera.Y + 350);

            Color startGameBox = Color.White;
            startGameBox.A = 20;
            SplashKit.FillRectangle(startGameBox, Camera.X + 505, Camera.Y + 560, 250, 100);
            SplashKit.DrawText("Start Game", Color.Black, "minecraft.ttf", 32, Camera.X + 540, Camera.Y + 600);
        }

        /// <summary>
        /// When the game is paused this display the stats of weapon and characters
        /// </summary>
        /// <param name="select"></param>
        /// <param name="characters"></param>
        public static void DisplayGameStats(KeyCode select, List<Character> characters){
            Color background = Color.SkyBlue;
            background.A = 126;
            Color tabBox = Color.White;
            tabBox.A = 50;
            SplashKit.FillRectangle(background, Camera.X, Camera.Y, WINDOW_WIDTH, WINDOW_HEIGHT);
            Color text = Color.AntiqueWhite;
            if(select == KeyCode.IKey){
                Dagger dagger = new Dagger(0, 0);
                Sword sword = new Sword(0, 0);
                Spear spear = new Spear(0, 0);
                Axe axe = new Axe(0, 0);
                HpPotion hp = new HpPotion(0, 0, 10);
                MpPotion mp = new MpPotion(0, 0, 10);
                SplashKit.FillRectangle(tabBox, Camera.X + 250, Camera.Y + 40, 280, 70);
                SplashKit.DrawRectangle(tabBox, Camera.X + 600, Camera.Y + 40, 485, 70);
                SplashKit.DrawText("Item (I)", Color.Black, "minecraft.ttf", 64, Camera.X + 270, Camera.Y + 50);
                SplashKit.DrawText("Character (C)", Color.Black, "minecraft.ttf", 64, Camera.X + 620, Camera.Y + 50);
                SplashKit.DrawText("1. Dagger: " + dagger.Description, text, "minecraft.ttf", 32, Camera.X + 200, Camera.Y + 150);
                SplashKit.DrawText("2. Sword: " + sword.Description, text, "minecraft.ttf", 32, Camera.X + 200, Camera.Y + 200);
                SplashKit.DrawText("3. Spear: " + spear.Description, text, "minecraft.ttf", 32, Camera.X + 200, Camera.Y + 250);
                SplashKit.DrawText("4. Axe: " + axe.Description, text, "minecraft.ttf", 32, Camera.X + 200, Camera.Y + 300);
                SplashKit.DrawText("5. Hp Potion: " + hp.Description, text, "minecraft.ttf", 32, Camera.X + 200, Camera.Y + 350);
                SplashKit.DrawText("6. Mp Potion: " + mp.Description, text, "minecraft.ttf", 32, Camera.X + 200, Camera.Y + 400);
            }
            else if(select == KeyCode.CKey){
                Player player = new Player();
                Golem golem = new Golem(player);
                Skeleton skeleton = new Skeleton(player);
                Assassin assasson = new Assassin(player);
                Boss boss = new Boss(player);
                foreach(Character c in characters){
                    if(c is Player){
                        player = (Player)c;
                    }
                    else if(c is Golem){
                        golem = (Golem)c;
                    }
                    else if(c is Skeleton){
                        skeleton = (Skeleton)c;
                    }
                    else if(c is Assassin){
                        assasson = (Assassin)c;
                    }
                    else if(c is Boss){
                        boss = (Boss)c;
                    }
                }
                SplashKit.DrawRectangle(tabBox, Camera.X +250, Camera.Y + 40, 280, 70);
                SplashKit.FillRectangle(tabBox, Camera.X + 600, Camera.Y + 40, 485, 70);
                SplashKit.DrawText("Item (I)", Color.Black, "minecraft.ttf", 64, Camera.X + 270, Camera.Y + 50);
                SplashKit.DrawText("Character (C)", Color.Black, "minecraft.ttf", 64, Camera.X + 620, Camera.Y + 50);

                SplashKit.DrawText("Player: ", Color.White, "minecraft.ttf", 32, Camera.X + 200, Camera.Y + 150);
                SplashKit.DrawText("1. MaxHp: " + player.MaxHp, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 200);
                SplashKit.DrawText("2. MaxMp: " + player.MaxMp, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 220);
                SplashKit.DrawText("3. Damage: " + player.Damage, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 240);
                SplashKit.DrawText("4. Movement Speed: " + player.MovementSpeed, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 260);
                SplashKit.DrawText("5. Range: " + player.Range, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 280);
                SplashKit.DrawText("6. Lifesteal: " + player.Lifesteal, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 300);
                
                SplashKit.DrawText("Boss: ", Color.White, "minecraft.ttf", 32, Camera.X + 200, Camera.Y + 350);
                SplashKit.DrawText("1. MaxHp: " + boss.MaxHp, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 400);
                SplashKit.DrawText("2. Damage: " + boss.Damage, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 420);
                SplashKit.DrawText("3. Movement Speed: " + boss.MovementSpeed.ToString("0.##"), text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 440);
                SplashKit.DrawText("4. Range: " + boss.Range, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 460);
                SplashKit.DrawText("5. Attack Range: " + boss.AttackRange, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 480);
                SplashKit.DrawText("6. Mana Drain: " + boss.ManaDrain, text, "minecraft.ttf", 16, Camera.X + 200, Camera.Y + 500);
                
                SplashKit.DrawText("Golem: ", Color.White, "minecraft.ttf", 32, Camera.X + 500, Camera.Y + 150);
                SplashKit.DrawText("1. MaxHp: " + golem.MaxHp, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 200);
                SplashKit.DrawText("2. Damage: " + golem.Damage, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 220);
                SplashKit.DrawText("3. Movement Speed: " + golem.MovementSpeed.ToString("0.##"), text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 240);
                SplashKit.DrawText("4. Range: " + golem.Range, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 260);
                SplashKit.DrawText("5. Attack Range: " + golem.AttackRange, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 280);
                SplashKit.DrawText("6. Damage Reduction: " + golem.DamageReduction, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 300);

                SplashKit.DrawText("Skeleton: ", Color.White, "minecraft.ttf", 32, Camera.X + 500, Camera.Y + 350);
                SplashKit.DrawText("1. MaxHp: " + skeleton.MaxHp, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 400);
                SplashKit.DrawText("2. Damage: " + skeleton.Damage, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 420);
                SplashKit.DrawText("3. Movement Speed: " + skeleton.MovementSpeed.ToString("0.##"), text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 440);
                SplashKit.DrawText("4. Range: " + skeleton.Range, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 460);
                SplashKit.DrawText("5. Attack Range: " + skeleton.AttackRange, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 480);
                SplashKit.DrawText("6. Piercing Damage: " + skeleton.PiercingDamage, text, "minecraft.ttf", 16, Camera.X + 500, Camera.Y + 500);

                SplashKit.DrawText("Assassin: ", Color.White, "minecraft.ttf", 32, Camera.X + 800, Camera.Y + 150);
                SplashKit.DrawText("1. MaxHp: " + assasson.MaxHp, text, "minecraft.ttf", 16, Camera.X + 800, Camera.Y + 200);
                SplashKit.DrawText("2. Damage: " + assasson.Damage, text, "minecraft.ttf", 16, Camera.X + 800, Camera.Y + 220);
                SplashKit.DrawText("3. Movement Speed: " + assasson.MovementSpeed.ToString("0.##"), text, "minecraft.ttf", 16, Camera.X + 800, Camera.Y + 240);
                SplashKit.DrawText("4. Range: " + assasson.Range, text, "minecraft.ttf", 16, Camera.X + 800, Camera.Y + 260);
                SplashKit.DrawText("5. Attack Range: " + assasson.AttackRange, text, "minecraft.ttf", 16, Camera.X + 800, Camera.Y + 280);
                SplashKit.DrawText("6. Crit Chance: " + assasson.CritChance, text, "minecraft.ttf", 16, Camera.X + 800, Camera.Y + 300);

                SplashKit.DrawText("Drop Chance: ", Color.White, "minecraft.ttf", 32, Camera.X + 800, Camera.Y + 350);
                SplashKit.DrawText("1. Golem: " + "HP(20%), MP(10%), Sword(5%)", text, "minecraft.ttf", 16, Camera.X + 800, Camera.Y + 400);
                SplashKit.DrawText("2. Skeleton: " + "HP(15%), Dagger(10%), Sword(10%)", text, "minecraft.ttf", 16, Camera.X + 800, Camera.Y + 420);
                SplashKit.DrawText("3. Assassin: " + "Mp(15%), Spear(10%), Axe(10%)", text, "minecraft.ttf", 16, Camera.X + 800, Camera.Y + 440);
            }

            Color resumeGameBox = Color.White;
            resumeGameBox.A = 20;
            SplashKit.FillRectangle(resumeGameBox, Camera.X + 505, Camera.Y + 560, 250, 100);
            SplashKit.DrawText("Resume", Color.Black, "minecraft.ttf", 32, Camera.X + 560, Camera.Y + 600);
        }

        public static void Main()
        {
            Window window = new Window("Slashes", WINDOW_WIDTH, WINDOW_HEIGHT);
            Game game = new Game();
            Bitmap[,] map = game.DrawMap();
            SplashKit.CreateTimer("game");
            KeyCode select = KeyCode.IKey;
            List<Character> characters = new List<Character>();
            characters.Add(game.Player);
            
            
            do {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.White);

                double mouseX = Camera.X + SplashKit.MousePosition().X;
                double mouseY = Camera.Y + SplashKit.MousePosition().Y;

                ulong timerTicks = SplashKit.TimerTicks("game");
                double totalSeconds = timerTicks / 1000; 
                int minutes = (int)(totalSeconds / 60);
                int seconds = (int)(totalSeconds % 60);

                // Draw map
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        // Draw the tile onto the window
                        SplashKit.DrawBitmap(map[i, j], j * 120 * 0.8f, i * 120 * 0.8f, SplashKit.OptionScaleBmp(0.8f, 0.8f));
                    }
                }

                // Player camera
                UpdateCameraPosition(game.Player, map);
 
                // Game Not Start and Draw How-To-Play
                if(!game.GameStart){
                    DrawHowToPlay();
                    if(SplashKit.MouseClicked(MouseButton.LeftButton) && (mouseX > Camera.X + 505) && (mouseX < Camera.X + 755) && (mouseY > Camera.Y + 560) && (mouseY < Camera.Y + 660)){
                        game.GameStart = true;
                        game.GameOver = false;
                        SplashKit.ResumeTimer("game");
                    }
                }

                // Pause Game 
                if(SplashKit.KeyTyped(KeyCode.EscapeKey)){
                    if(!game.GamePause && game.GameStart) {
                        SplashKit.PauseTimer("game");
                        game.GamePause = true;
                    }
                    else if(game.GamePause && game.GameStart) {
                        SplashKit.ResumeTimer("game");
                        game.GamePause = false;
                    }
                }

                // Start Game
                if(game.GameStart && !game.GameOver && !game.GamePause){
                    game.StartGame();
                    game.GenerateEnemy();

                    if(!SplashKit.TimerStarted("game")){
                        SplashKit.StartTimer("game");
                    }
                    
                    Color timerColor;
                    if(SplashKit.TimerTicks("game") < 270000){
                        timerColor = Color.White;
                    }
                    else {
                        timerColor = Color.Red;
                        SplashKit.DrawText("BOSS INCOMING!!!", Color.Red, "minecraft.ttf", 32, Camera.X + 500, Camera.Y + 150);
                    }
                    SplashKit.DrawText(minutes.ToString("D2") + " : " + seconds.ToString("D2"), timerColor, "minecraft.ttf", 64, Camera.X + 550, Camera.Y + 60);
                }
                // Boss killed under 5 mins
                else if(game.GameStart && game.GameOver && game.PlayerWin){
                    Color background = Color.SkyBlue;
                    background.A = 126;
                    SplashKit.FillRectangle(background, Camera.X, Camera.Y, WINDOW_WIDTH, WINDOW_HEIGHT);
                    SplashKit.DrawText("VICTORY!", Color.White, "minecraft.ttf", 64, Camera.X + 500, Camera.Y + 250);
                    SplashKit.DrawText("Total Kills: " + Convert.ToString(game.Player.Kills), Color.White, "minecraft.ttf", 32, Camera.X + 550, Camera.Y + 350);
                    SplashKit.DrawText("Time Taken: " + minutes.ToString("D2") + " : " + seconds.ToString("D2"), Color.White, "minecraft.ttf", 32, Camera.X + 500, Camera.Y + 450);
                    SplashKit.PauseTimer("game");
                }
                // Game over (player died or game reach 5 mins)
                else if(game.GameStart && game.GameOver){
                    Color background = Color.SkyBlue;
                    background.A = 126;
                    SplashKit.FillRectangle(background, Camera.X, Camera.Y, WINDOW_WIDTH, WINDOW_HEIGHT);
                    SplashKit.DrawText("Game Over!", Color.White, "minecraft.ttf", 64, Camera.X + 450, Camera.Y + 250);
                    SplashKit.DrawText("Total Kills: " + Convert.ToString(game.Player.Kills), Color.White, "minecraft.ttf", 32, Camera.X + 550, Camera.Y + 350);
                    SplashKit.DrawText("Time Taken: " + minutes.ToString("D2") + " : " + seconds.ToString("D2"), Color.White, "minecraft.ttf", 32, Camera.X + 500, Camera.Y + 450);
                    SplashKit.PauseTimer("game");
                }
                // Game pause and display game stats
                else if(game.GamePause){
                    foreach (Enemy enemy in game.Enemies)
                    {
                        bool isTypeAlreadyAdded = false;

                        foreach (Character c in characters)
                        {
                            if (c is Enemy)
                            {
                                Enemy e = (Enemy)c;
                                if (enemy.ClassType == e.ClassType)
                                {
                                    isTypeAlreadyAdded = true;
                                    break;
                                }
                            }
                        }
                        if (!isTypeAlreadyAdded)
                        {
                            characters.Add(enemy);
                        }
                    }
                    DisplayGameStats(select, characters);
                    if(SplashKit.KeyTyped(KeyCode.IKey)){
                        select = KeyCode.IKey;
                    }
                    else if(SplashKit.KeyTyped(KeyCode.CKey)){
                        select = KeyCode.CKey;
                    }
                    else if(SplashKit.MouseClicked(MouseButton.LeftButton) && (mouseX > Camera.X + 505) && (mouseX < Camera.X + 755) && (mouseY > Camera.Y + 560) && (mouseY < Camera.Y + 660)){
                        game.GamePause = false;
                        SplashKit.ResumeTimer("game");
                    }
                }

                SplashKit.RefreshScreen(100);
            } while(!SplashKit.QuitRequested());

            SplashKit.CloseWindow("Slashes");
        }
    }
}

// Resource Credits 
// 1. Author: Creative Kind, Title: Nightborne Warrior, Usage: Assassin.png, Link: https://creativekind.itch.io/nightborne-warrior
// 2. Author: Adam Kling, Title: None, Usage: Player.png, Link: https://www.pinterest.com/pin/101612535335940474/
// 3. Author: Astro Bot, Title: Animated Pixel Art Skeleton, Usage: Skeleton.png, Link: https://astrobob.itch.io/animated-pixel-art-skeleton
