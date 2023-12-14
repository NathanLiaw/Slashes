using System;
using SplashKitSDK;

namespace customprogram {
    /// <summary>
    /// This is an abstract child of Character class with 3 fields, 3 properties, and 6 methods
    /// </summary>
    public abstract class Enemy:Character {
        private string _classType;
        private uint _moveInterval;
        private double _attackRange;
        private EnemyState _currentState;
        
        /// <summary>
        /// This is a parameterised constructor that accepts 1 parameter
        /// </summary>
        /// <param name="player"></param>
        public Enemy(Player player):base(){
            Random random = new Random();
            double angle = random.NextDouble() * 2 * Math.PI;
            double distance = random.NextDouble() * 500 + 500;
            base.X = player.X + distance * Math.Cos(angle);
            base.Y = player.Y + distance * Math.Sin(angle);
            _classType = "";
            _moveInterval = SplashKit.CurrentTicks();
            _attackRange = 100;
            _currentState = EnemyState.Idle;
        }

        /// <summary>
        /// This is a property for _classType field using getter and setter
        /// </summary>
        /// <value></value>
        public string ClassType {
            get {return _classType;}
            set {_classType = value;}
        }
        
        /// <summary>
        /// This is a property for _attackRange field using getter and setter
        /// </summary>
        /// <value></value>
        public double AttackRange {
            get {return _attackRange;}
            set {_attackRange = value;}
        }

        /// <summary>
        /// This is a property for _moveInterval field using getter and setter
        /// </summary>
        /// <value></value>
        public uint MoveInterval {
            get {return _moveInterval;}
            set {_moveInterval = value;}
        }

        public EnemyState CurrentState {
            get {return _currentState;}
            set {_currentState = value;}
        }

        // This is the original move method without applying separation technique
        // public void Move(Character c){
        //     if(c is Player){
        //         double distanceX = c.X - base.X;
        //         double distanceY = c.Y - base.Y;
        //         double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

        //         if (distance <= base.Range && SplashKit.CurrentTicks() - _moveInterval > 5)
        //         {
        //             _moveInterval = SplashKit.CurrentTicks();
        //             double directionX = (c.X > base.X) ? 1 : -1;
        //             double directionY = (c.Y > base.Y) ? 1 : -1;

        //             double moveAmount = Math.Min(distance, base.MovementSpeed);

        //             base.X += moveAmount * directionX;
        //             base.Y += moveAmount * directionY;

        //             SplashKit.SpriteSetX(base.CharacterSprite, (float)base.X);
        //             SplashKit.SpriteSetY(base.CharacterSprite, (float)base.Y);
        //         }
        //     }
        // }

        // Seperation technique and state machine technique
        /// <summary>
        /// This is an override method that allows enemy objects to move towards a player object
        /// </summary>
        /// <param name="c"></param>
        public void Chase(Character c, List<Enemy> enemies)
        {
            if (c is Player)
            {
                // Calculate distance between player and enemy
                double distanceX = c.X - base.X;
                double distanceY = c.Y - base.Y;
                double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

                if(distance > base.Range)
                {
                    // Transition back to idle state if the player is out of range
                    _currentState = EnemyState.Idle;
                }
                else if (distance <= AttackRange)
                {
                    // Transition to attack state when within attack range
                    _currentState = EnemyState.Attack;
                }
                // If distance between player and enemy is within enemy range and time since last moved is more than 5ms
                else if (distance <= base.Range && SplashKit.CurrentTicks() - _moveInterval > 5)
                {
                    _moveInterval = SplashKit.CurrentTicks();
                    double directionX = (c.X > base.X) ? 1 : -1;
                    double directionY = (c.Y > base.Y) ? 1 : -1;

                    // Separation parameters
                    double separationRadius = 60.0;
                    double separationStrength = 5.0; 

                    // Calculate separation vector
                    double separationX = 0.0;
                    double separationY = 0.0;

                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy != this)
                        {
                            double enemydistanceX = enemy.X - base.X;
                            double enemydistanceY = enemy.Y - base.Y;
                            double enemyDistance = Math.Sqrt(enemydistanceX * enemydistanceX + enemydistanceY * enemydistanceY);

                            if (enemyDistance < separationRadius)
                            {
                                separationX += (base.X - enemy.X) / enemyDistance;
                                separationY += (base.Y - enemy.Y) / enemyDistance;
                            }
                        }
                    }

                    // Normalize separation vector
                    double separationMagnitude = Math.Sqrt(separationX * separationX + separationY * separationY);
                    if (separationMagnitude > 0.0)
                    {
                        separationX /= separationMagnitude;
                        separationY /= separationMagnitude;
                    }

                    // Apply separation force
                    double moveAmount = Math.Min(distance, base.MovementSpeed);
                    base.X += moveAmount * directionX + separationStrength * separationX;
                    base.Y += moveAmount * directionY + separationStrength * separationY;

                    SplashKit.SpriteSetX(base.CharacterSprite, (float)base.X);
                    SplashKit.SpriteSetY(base.CharacterSprite, (float)base.Y);
                }   
                
            }
        }

        // Steering Behaviour: Seeking 
        // public void Move(Character c, List<Enemy> enemies)
        // {
        //     if (c is Player)
        //     {
        //         double distanceX = c.X - base.X;
        //         double distanceY = c.Y - base.Y;
        //         double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

        //         if (distance <= base.Range && SplashKit.CurrentTicks() - _moveInterval > 5)
        //         {
        //             _moveInterval = SplashKit.CurrentTicks();
        //             double directionX = (c.X > base.X) ? 1 : -1;
        //             double directionY = (c.Y > base.Y) ? 1 : -1;

        //             // Steering parameters
        //             double seekStrength = 5.0;

        //             // Calculate seek vector
        //             double seekX = (c.X - base.X) / distance;
        //             double seekY = (c.Y - base.Y) / distance;

        //             // Apply seek force
        //             double moveAmount = Math.Min(distance, base.MovementSpeed);
        //             base.X += moveAmount * directionX + seekStrength * seekX;
        //             base.Y += moveAmount * directionY + seekStrength * seekY;

        //             // Apply separation force
        //             double separationRadius = 60.0; 
        //             double separationStrength = 5.0; 

        //             double separationX = 0.0;
        //             double separationY = 0.0;

        //             foreach (Enemy enemy in enemies)
        //             {
        //                 if (enemy != this)
        //                 {
        //                     double enemydistanceX = enemy.X - base.X;
        //                     double enemydistanceY = enemy.Y - base.Y;
        //                     double enemyDistance = Math.Sqrt(enemydistanceX * enemydistanceX + enemydistanceY * enemydistanceY);

        //                     if (enemyDistance < separationRadius)
        //                     {
        //                         separationX += (base.X - enemy.X) / enemyDistance;
        //                         separationY += (base.Y - enemy.Y) / enemyDistance;
        //                     }
        //                 }
        //             }

        //             double separationMagnitude = Math.Sqrt(separationX * separationX + separationY * separationY);
        //             if (separationMagnitude > 0.0)
        //             {
        //                 separationX /= separationMagnitude;
        //                 separationY /= separationMagnitude;
        //             }

        //             base.X += separationStrength * separationX;
        //             base.Y += separationStrength * separationY;

        //             SplashKit.SpriteSetX(base.CharacterSprite, (float)base.X);
        //             SplashKit.SpriteSetY(base.CharacterSprite, (float)base.Y);
        //         }
        //     }
        // }

        /// <summary>
        /// This is an abstract method to draw enemy
        /// </summary>
        public abstract void DrawEnemy();

        /// <summary>
        /// This is a virtual bool method that checks if the player is within the attack range of the player
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public virtual bool WithinAttackRange(Character c){
            double distanceX;
            double distanceY;
            double distance;
            if(c is Player){
                distanceX = Math.Abs(base.X - c.X);
                distanceY = Math.Abs(base.Y - c.Y);
                distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
                if(distance <= _attackRange){
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This is an abstract method to allow enemy objects to levelup after a certain interval of time
        /// </summary>
        /// <param name="timeElapsed"></param>
        public abstract void LevelUp(uint timeElapsed);
        
        #nullable disable
        /// <summary>
        /// This is an abstract method that return an Item after an enemy is killed
        /// </summary>
        /// <returns></returns>
        public abstract Item DropItem();

        // State machine technique
        public void UpdateState(Character c, List<Enemy> enemies)
        {
            switch (_currentState)
            {
                case EnemyState.Idle:
                    Idle(c);
                    break;
                case EnemyState.Chase:
                    Chase(c, enemies);
                    break;
                case EnemyState.Attack:
                    Attack(c);
                    break;
            }
        }

        private void Idle(Character c)
        {
            // Transition to chase state when conditions are met
            if (c is Player)
            {
                double distanceX = c.X - base.X;
                double distanceY = c.Y - base.Y;
                double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

                if (distance <= base.Range)
                {
                    _currentState = EnemyState.Chase;
                }
            }
        }

        /// <summary>
        /// This is an override method that allows enemy objects to attack a player object
        /// </summary>
        /// <param name="c"></param>
        public override abstract void Attack(Character c);
    }
}
        