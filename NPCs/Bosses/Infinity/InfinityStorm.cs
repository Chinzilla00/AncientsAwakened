using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Infinity
{
    public class InfinityStorm : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Storm");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
		{

            projectile.width = 80;
            projectile.height = 80;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (projectile.localAI[1] == 0f)
            {
                Main.PlaySound(SoundID.Item121, projectile.position);
                projectile.localAI[1] = 1f;
            }
            if (projectile.ai[0] < 180f)
            {
                projectile.alpha -= 5;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            else
            {
                projectile.alpha += 5;
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                    projectile.Kill();
                    return;
                }
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] % 30f == 0f && projectile.ai[0] < 180f && Main.netMode != 1)
            {
                int[] array4 = new int[5];
                Vector2[] array5 = new Vector2[5];
                int num838 = 0;
                float num839 = 2000f;
                for (int num840 = 0; num840 < 255; num840++)
                {
                    if (Main.player[num840].active && !Main.player[num840].dead)
                    {
                        Vector2 center9 = Main.player[num840].Center;
                        float num841 = Vector2.Distance(center9, projectile.Center);
                        if (num841 < num839 && Collision.CanHit(projectile.Center, 1, 1, center9, 1, 1))
                        {
                            array4[num838] = num840;
                            array5[num838] = center9;
                            if (++num838 >= array5.Length)
                            {
                                break;
                            }
                        }
                    }
                }
                for (int num842 = 0; num842 < num838; num842++)
                {
                    Vector2 vector82 = array5[num842] - projectile.Center;
                    float ai = (float)Main.rand.Next(100);
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 7f;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector83.X, vector83.Y, mod.ProjectileType<InfinityBolt>(), projectile.damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                }
            }
            Lighting.AddLight(projectile.Center, 0.4f, 0.85f, 0.9f);
            if (++projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.alpha < 150 && projectile.ai[0] < 180f)
            {
                for (int num843 = 0; num843 < 1; num843++)
                {
                    float num844 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num844 < -0.5f)
                    {
                        num844 = -0.5f;
                    }
                    if (num844 > 0.5f)
                    {
                        num844 = 0.5f;
                    }
                    Vector2 value47 = new Vector2((float)(-(float)projectile.width) * 0.2f * projectile.scale, 0f).RotatedBy((double)(num844 * 6.28318548f), default(Vector2)).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num845 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, mod.DustType<Dusts.VoidDust>(), -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num845].position = projectile.Center + value47;
                    Main.dust[num845].velocity = Vector2.Normalize(Main.dust[num845].position - projectile.Center) * 2f;
                    Main.dust[num845].noGravity = true;
                }
                for (int num846 = 0; num846 < 1; num846++)
                {
                    float num847 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num847 < -0.5f)
                    {
                        num847 = -0.5f;
                    }
                    if (num847 > 0.5f)
                    {
                        num847 = 0.5f;
                    }
                    Vector2 value48 = new Vector2((float)(-(float)projectile.width) * 0.6f * projectile.scale, 0f).RotatedBy((double)(num847 * 6.28318548f), default(Vector2)).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num848 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, mod.DustType<Dusts.VoidDust>(), -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num848].velocity = Vector2.Zero;
                    Main.dust[num848].position = projectile.Center + value48;
                    Main.dust[num848].noGravity = true;
                }
                return;
            }
        }
        public static int frameWidth = 100, frameHeight = 100;
        public int frameTimer = 0;
        public int frameCount = 3;
        public bool invertFrame = false;
        public Rectangle frame;
        public static Texture2D tex = null;
        public static Texture2D glowTex = null;
        public bool checkedMinPos = false;

        public override void PostAI()
        {
            projectile.rotation = projectile.velocity.X * 0.1f;
            frameTimer--;
            if (frameTimer <= 0)
            {
                frameTimer = 2;
                if (invertFrame) { frameCount--; if (frameCount < 0) { frameCount = 1; invertFrame = false; } }
                else
                { frameCount++; if (frameCount > 2) { frameCount = 1; invertFrame = true; } }
            }
            frame = BaseMod.BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            if (tex == null)
            {
                tex = Main.projectileTexture[projectile.type];
                glowTex =  mod.GetTexture("NPCs/Bosses/Infinity/InfinityStorm_Glow");
            }
            Color lightColor = BaseMod.BaseDrawing.GetLightColor(projectile.Center);
            for (int m = projectile.oldPos.Length - 1; m > 0; m--) { projectile.oldPos[m] = projectile.oldPos[m - 1]; }
            projectile.oldPos[0] = projectile.position;
            BaseMod.BaseDrawing.DrawTexture(sb, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 3, frame, lightColor);
            BaseMod.BaseDrawing.DrawTexture(sb, glowTex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 3, frame, AAColor.Oblivion);
            
            return false;
        }
    }
}
