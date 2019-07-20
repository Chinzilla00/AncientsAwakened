using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using BaseMod;
using Terraria.Audio;

namespace AAMod
{
    public abstract class AAProjectile : ParentProjectile
    {
        public override bool CloneNewInstances => true;

        public string name
        {
            get
            {
                return projectile.Name;
            }
            set
            {
                projectile.Name = value;
            }
        }

        public int frameWidth = 0;
        public int frameHeight = 0;
        public int nextFrameCounter = 0;
        public int frameCount = 0;
        public bool invertFrames = false;
        public Color? lightColor = null, drawColor = null;
        public int drawColorType = -1;
        public float lightIntensity = 1f;
        public override Vector4 GetFrameV4() { return new Vector4(0, 0, frameWidth, frameHeight + 2); }

        public bool drawCentered = false, drawCenteredX = false, hurtsTiles = true, firstTick = false;

        public LegacySoundStyle spawnSound = null;
        public short immunityID = -1; //allows for projectiles to _not_ override player attacks

        public virtual void SetMaster(params object[] args) { }
        public virtual void OnSpawnEffects() { }

        public override bool? CanCutTiles()
        {
            return !hurtsTiles ? false : (bool?)null;
        }

        

        public override bool PreAI()
        {
            if (!firstTick)
            {
                OnSpawnEffects();
                if (spawnSound != null)
                {
                    Main.PlaySound(spawnSound, (int)projectile.Center.X, (int)projectile.Center.Y);
                }
                firstTick = true;
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color dColor)
        {
            if (lightColor != null) BaseDrawing.AddLight(projectile.Center, (Color)lightColor, lightIntensity);
            if (drawCentered || drawCenteredX)
            {
                Vector2 oldPos = projectile.position;
                if (drawCenteredX)
                {
                    projectile.position.X += projectile.Center.X - projectile.position.X;
                }
                else
                {
                    projectile.position += projectile.Center - projectile.position;
                }
                BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, GetAlpha(dColor));
                projectile.position = oldPos;
                return false;
            }
            return true;
        }

        public override Color? GetAlpha(Color dColor)
        {
            if (drawColor != null && drawColorType != -1)
            {
                if (drawColorType == 1)
                {
                    Color drawColor2 = (Color)drawColor;
                    if (dColor.R > drawColor2.R) { drawColor2.R = dColor.R; }
                    if (dColor.G > drawColor2.G) { drawColor2.G = dColor.G; }
                    if (dColor.B > drawColor2.B) { drawColor2.B = dColor.B; }
                    //drawColor2.A = (Color)drawColor.A;
                    return drawColor2;
                }
                return (Color)drawColor;
            }
            return base.GetAlpha(dColor);
        }
    }
}