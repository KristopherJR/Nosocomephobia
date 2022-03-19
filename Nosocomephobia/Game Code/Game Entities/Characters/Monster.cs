using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.World;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.6, 17-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Game_Entities.Characters
{
    class Monster : AnimatedEntity, ICollidable, ICollisionResponder, IPathFinder
    {
        #region FIELDS
        // DECLARE a List<Vector2>, call it path. This will contain the Monsters randomly calculate Path:
        private List<Vector2> path;
        // DECLARE a int, call it i:
        private int i;
        // DECLARE a float, call it speed:
        private float speed;
        // DECLARE a float, call it idleTimer:
        private float idleTimer;
        // DECLARE a float, call it waitDuration:
        private float waitDuration;
        // DECLARE a bool, call it isWaiting:
        private bool isWaiting;
        // DECLARE a List<SoundEffect>, call it soundEffects:
        private List<SoundEffect> soundEffects;
        // DECLARE a bool, call it soundPlayed:
        private bool soundPlayed;
        // DECLARE a bool, call it canPlaySound:
        private bool canPlaySound;
        // DECLARE a float, call it sfxTimer:
        private float sfxTimer;
        // DECLARE a float, call it sfxInterval:
        private float sfxInterval;
        // DECLARE a bool, call it distantSFXPlayed:
        private bool distantSFXPlayed;

        #endregion

        #region PROPERTIES
        public List<Vector2> Path // property
        {
            get { return path; }
            set { path = value; }
        }
        // DECLARE a get-set property for soundPlayed:
        public bool SoundPlayed
        {
            get { return soundPlayed; }
            set { soundPlayed = value; }
        }
        // DECLARE a get-set property for sfxTimer:
        public float SFXTimer
        {
            get { return sfxTimer; }
            set { sfxTimer = value; }
        }
        // DECLARE a get-set property for canPlaySound:
        public bool CanPlaySound
        {
            get { return canPlaySound; }
            set { canPlaySound = value; }
        }

        // DECLARE a get-set property for distantSFXPlayed:
        public bool DistantSFXPlayed
        {
            get { return distantSFXPlayed; }
            set { distantSFXPlayed = value; }
        }
        #endregion

        /// <summary>
        /// Constructor for objects of class Monster.
        /// </summary>
        public Monster() : base(GameContent.GetAnimation(AnimationGroup.MonsterWalkDown))
        {
            // INITIALIZE fields:
            this.path = new List<Vector2>();
            this.speed = 1.0f;
            this.i = 0;
            this.idleTimer = 0.0f;
            this.waitDuration = 0.0f;
            this.isWaiting = false;
            this.soundEffects = new List<SoundEffect>();
            this.soundEffects.Add(GameContent.Monster1);
            this.soundEffects.Add(GameContent.Monster2);
            this.soundEffects.Add(GameContent.Monster3_2);
            this.soundEffects.Add(GameContent.Price_2);
            this.sfxInterval = 10f;
            this.canPlaySound = true;
            this.distantSFXPlayed = false;
            // SET NPC's location in the world:
            this.EntityLocn = new Vector2(3000, 5500);
        }

        /// <summary>
        /// Method that allows the Monster to move along its generated Path.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void FollowPath(GameTime gameTime)
        {
            // IF i < path.Count:
            if (i < path.Count)
            {
                // DECLARE a Vector2, call it location and set it to the Entities current location:
                Vector2 location = this.EntityLocn;
                // DECLARE a Vector2, call it direction and set it to the Entities current location - path[i]:
                Vector2 direction = path[i] - this.EntityLocn;
                // IF the direction is greater than 0
                if (direction.Length() > 0)
                {
                    // NORMALIZE the Vector or else it will be set to infinity!!!:
                    direction.Normalize();
                }
                if (direction.Y == 1)
                {
                    // moving down
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.MonsterWalkDown);

                }
                if (direction.Y == -1)
                {
                    // moving up
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.MonsterWalkUp);
                }
                if (direction.X == 1)
                {
                    // moving right
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.MonsterWalkRight);
                }
                if (direction.X == -1)
                {
                    // moving left
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.MonsterWalkLeft);
                }
                
                // DECLARE a Vector2, call it newVelocity and set it to speed * direction:
                Vector2 newVelocity = direction * speed;
                
                // ADD the velocity to the entity location:
                this.EntityLocn += newVelocity;
                // IF the Distance between the entity location and the current tile in the path is less than 0.5 (I.E the entity is close enough to its goal tile):
                if (Vector2.Distance(this.entityLocn, path[i]) < 0.5)
                {
                    // INCREMENT i:
                    i++;
                }
                // IF the player has moved through the entire path:
                if (i == path.Count - (path.Count -2))
                {
                    // SET the entity velocity to 0:
                    entityVelocity = Vector2.Zero;
                    // SET their path to null, this will prompt the NavigationManager to give them a new one:
                    path = null;
                    // RESET i back to -:
                    i = 0;
                    // SET isWaiting to true so the entity waits before moving again:
                    isWaiting = true;
                }
            }
        }

        public void GoInvisible()
        {
            // MAKE the monster invisible
            this.EntitySprite.Opacity = 0.0f;
        }

        public void Reveal()
        {
            // MAKE the monster visible
            this.EntitySprite.Opacity = 1.0f;
        }

        /// <summary>
        /// Teleports the Monster to a location near its Target.
        /// </summary>
        /// <param name="pTargetsTileIndex">Provides the index of the Tile that the Target is standing on.</param>
        /// <param name="pNavigationGrid">The navigation grid that the Monster can teleport within.</param>
        public void Teleport(Vector2 pTargetsTileIndex, TileMap pNavigationGrid)
        {
            Vector2 randomDistance = RandomiseTeleportationDistance(pTargetsTileIndex, pNavigationGrid);

            // SELECT a Tile near the target (between 5-10 tiles away):
            Tile teleportTile = pNavigationGrid.GetTileAtIndex((int)pTargetsTileIndex.X + (int)randomDistance.X, 
                                                               (int)pTargetsTileIndex.Y + (int)randomDistance.Y);

            // CHECK the Tile is valid and in bounds:
            if(!teleportTile.IsCollidable)
            {
                // CHANGE Monster location to the new tile:
                this.EntityLocn = teleportTile.EntityLocn;
            }
        }

        /// <summary>
        /// Selects a random distance of tiles away from the Player for the Monster to teleport to.
        /// </summary>
        /// <returns>A Vector 2 with the location of the teleportation Tile.</returns>
        private Vector2 RandomiseTeleportationDistance(Vector2 pTargetsTileIndex, TileMap pNavigationGrid)
        {
            bool tileFound = false;
            // DECLARE an instance of Random:
            Random random = new Random();

            while(!tileFound)
            {
                // GET a random number between 1-4:
                int posOrNeg = random.Next(1, 5);
                // DECLARE a randomX and randomY int:
                int randomX = 0;
                int randomY = 0;

                // RANDOMISE if the tile will be negative or positive distance:
                if (posOrNeg == 1)
                {
                    randomX = random.Next(5, 11);
                    randomY = random.Next(5, 11);
                }
                if (posOrNeg == 2)
                {
                    randomX = random.Next(-11, -5);
                    randomY = random.Next(5, 11);
                }
                if (posOrNeg == 3)
                {
                    randomX = random.Next(5, 11);
                    randomY = random.Next(-11, -5);
                }
                if (posOrNeg == 4)
                {
                    randomX = random.Next(-11, -5);
                    randomY = random.Next(-11, -5);
                }
                // IF the new X index is greater than 0 AND less than the width of the nav grid:
                if ((int)pTargetsTileIndex.X + randomX > 0 && (int)pTargetsTileIndex.X + randomX < pNavigationGrid.GetTileMap().GetLength(0))
                {
                    // CHECK the Y is within bounds as well:
                    if ((int)pTargetsTileIndex.Y + randomY > 0 && (int)pTargetsTileIndex.Y + randomY < pNavigationGrid.GetTileMap().GetLength(1))
                    {
                        // TILE is safe to teleport to, flag Tile found:
                        tileFound = true;
                        return new Vector2(randomX, randomY);
                    }

                }

            }
            return new Vector2(1, 1);
        }

        /// <summary>
        /// Plays a random SoundEffect in the soundEffects list.
        /// </summary>
        public void PlayRandomSoundEffect()
        {
            // FLAG that a sound was played:
            soundPlayed = true;
            // SET that the monster can not play another sound:
            canPlaySound = false;
            // INITIALISE Random:
            Random random = new Random();
            // GET a random number for the size of the soundEffects List:
            int n = random.Next(soundEffects.Count);
            // PLAY a random SoundEffect:
            soundEffects[n].Play(0.2f, 0.0f, 0.0f);
        }

        /// <summary>
        /// Update loop for Monster, overrides the parent Update() method. Stores the lastPosition before moving on each update loop.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public override void Update(GameTime gameTime)
        {

            // UPDATE the parent class:
            base.Update(gameTime);
            // IF the Entity IS NOT waiting:
            if (!isWaiting)
            {
                // FOLLOW its path:
                this.FollowPath(gameTime);
            }
            // IF the Entity IS waiting:
            if (isWaiting)
            {
                // INCREMENT the idleTimer by the elapsed GameTime in seconds:
                idleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                // IF the idleTimer reaches the waitDuration:
                if (idleTimer >= waitDuration)
                {
                    // SET isWaiting back to false:
                    isWaiting = false;
                    // RESET the idleTimer:
                    idleTimer = 0.0f;
                }
            }
            if(soundPlayed)
            {
                // INCREMENT sfxTimer by GameTime until it equals sfxInterval:
                sfxTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(sfxTimer >= sfxInterval)
                {
                    // FLAG that the Monster is ready to play another SFX:
                    canPlaySound = true;
                    soundPlayed = false;
                    // RESET the sfxTimer:
                    sfxTimer = 0.0f;
                }
            }
        }

        #region IMPLEMENTATION of ICollisionResponder
        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        public void CheckAndRespond(ICollidable collidee)
        {
            // IF the GameEntitys have collided:
            //if (GameEntity.hasCollided(this, collidee))
            //{
            //    // SET this location to its last position:
            //    this.entityLocn = lastPosition;
            //}
        }
        #endregion
    }
}
