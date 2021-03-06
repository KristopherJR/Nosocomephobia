using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.World;
using System;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.2, 19-03-2022
/// </summary>
namespace Nosocomephobia.Engine_Code.Entities
{
    public abstract class GameEntity : Entity
    {
        #region FIELDS

        // DECLARE a reference to a Sprite object, call it "entitySprite". This is used to store graphical information about the entity as a Sprite:
        protected Sprite entitySprite;

        // DECLARE a reference to a Vector2 object, call it "entityLocn". This is used to store the GameEntities Location:
        protected Vector2 entityLocn;

        // DECLARE a reference to a Vector2 object, call it "entityVelocity". This is used to represent entityVelocity values of PongEntities:
        protected Vector2 entityVelocity;

        // DECLARE a Vector2, call it 'lastPosition'. Used to keep track of the Entity position and reset it if it collides with something:
        private Vector2 lastPosition;

        // DECLARE a bool, call it isCollidable:
        protected bool isCollidable;

        // DECLARE a bool, call it isCharacter:
        protected bool isCharacter;

        #endregion FIELDS

        #region PROPERTIES

        public Sprite EntitySprite // property
        {
            get { return entitySprite; } // get method
            set { entitySprite = value; } // set method
        }

        public Vector2 EntityLocn
        {
            get { return entityLocn; } // get method
            set { entityLocn = value; } // set method
        }

        public Vector2 EntityVelocity
        {
            get { return entityVelocity; } // get method
            set { entityVelocity = value; } // set method
        }

        public Vector2 LastPosition
        {
            get { return lastPosition; }
            set { lastPosition = value; }
        }

        public Boolean IsCollidable
        {
            get { return isCollidable; }
            set { isCollidable = value; }
        }

        #endregion PROPERTIES

        /// <summary>
        /// Constructor for objects of class GameEntity.
        /// </summary>
        public GameEntity() : base()
        {
            // INITALIZE entityLocn to default 0,0:
            this.entityLocn = new Vector2(0, 0);
            // SET isCollidable to false as default:
            this.isCollidable = false;
            // SET isCharacter to false as default:
            this.isCharacter = false;
        }

        /// <summary>
        /// Called from Kernel, tells the GameEntity to draw itself onto the spriteBatch parameter passed in.
        /// </summary>
        /// <param name="spriteBatch">The games SpriteBatch.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            entitySprite.Draw(spriteBatch, entityLocn);
        }

        /// <summary>
        /// Checks if 2 ICollidables have collided. Returns true if they have, else false.
        /// </summary>
        /// <param name="collider">First ICollidable.</param>
        /// <param name="colidee">Second ICollidable.</param>
        /// <returns></returns>
        public static Boolean hasCollided(ICollidable collider, ICollidable colidee)
        {
            // IF the ICollidables HitBox's intersect:
            if ((collider as GameEntity).GetHitBox().Intersects((colidee as GameEntity).GetHitBox()))
            {
                // RETURN true:
                return true;
            }
            else
            {
                // ELSE return false:
                return false;
            }
        }

        /// <summary>
        /// Calculates an appropriately sized HitBox for the GameEntity. If the GameEntity is a "character", their hitbox will be different
        /// to other objects. This is to allow proper collision, since characters should have a HitBox slightly smaller than their graphical
        /// representation so they do not get stuck on walls and other objects.
        /// </summary>
        /// <returns>A Rectangle representing the GameEntity's new HitBox.</returns>
        private Rectangle GetHitBox()
        {
            // DECLARE a Rectangle, call it 'newHitBox':
            Rectangle newHitBox;
            // IF the GameEntity is a Character:
            if (isCharacter == true)
            {
                // CALCULATE an appropriate HitBox, I.E one that is slightly smaller than the entity texture:
                newHitBox = new Rectangle((int)(this.EntityLocn.X + (this.EntitySprite.TextureWidth * 0.15)),
                                          (int)(this.EntityLocn.Y + (this.EntitySprite.TextureHeight * 0.4)),
                                          (int)(this.EntitySprite.TextureWidth * 0.70),
                                          (int)(this.EntitySprite.TextureHeight * 0.60));
                // RETURN the newHitBox:
                return newHitBox;
            }
            else if (this is Door)
            {
                // CALCULATE a HitBox that fills the entire entity:
                newHitBox = new Rectangle((int)(this.EntityLocn.X),
                                          (int)(this.EntityLocn.Y),
                                          (int)(this.EntitySprite.TextureWidth),
                                          (int)(this.EntitySprite.TextureHeight));
                // RETURN the newHitBox:
                return newHitBox;
            }
            else
            {
                // CALCULATE a HitBox that fills the entire entity:
                newHitBox = new Rectangle((int)(this.EntityLocn.X),
                                          (int)(this.EntityLocn.Y),
                                          (int)(this.EntitySprite.TextureWidth * 0.75),
                                          (int)(this.EntitySprite.TextureHeight * 0.75));
                // RETURN the newHitBox:
                return newHitBox;
            }
        }
    }
}