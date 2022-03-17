using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.5, 17-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Game_Entities.Characters
{
    class Monster : AnimatedEntity, ICollidable, ICollisionResponder, IPathFinder
    {
        #region FIELDS
        // DECLARE a List<Vector2>, call it path. This will contain the Monsters randomly calculate Path:
        private List<Vector2> path;
        // DECLARE a Vector2, call it 'lastPosition'. Used to keep track of the Monster's position and reset it if it collides with something:
        private Vector2 lastPosition;
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
            // SET NPC's location in the world:
            this.EntityLocn = new Vector2(3000, 5000);
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
            soundEffects[n].Play(0.1f, 0.0f, 0.0f);
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
