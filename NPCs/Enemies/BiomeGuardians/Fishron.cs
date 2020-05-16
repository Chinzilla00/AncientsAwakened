using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;


namespace AAMod.NPCs.Enemies.BiomeGuardians
{
    public class Fishron : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fishron");
			Main.npcFrameCount[npc.type] = 7;
		}

		public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 36;
            npc.aiStyle = -1;
            npc.damage = 80;
            npc.defense = 12;
            npc.lifeMax = 210;
            npc.HitSound = SoundID.NPCHit27;
            npc.DeathSound = SoundID.NPCDeath30;
            npc.knockBackResist = 0.5f;
            npc.value = 2000f;
        }

        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public override void AI()
        {
            if (Main.rand.Next(1000) == 0)
            {
                Main.PlaySound(SoundID.Zombie, (int)npc.position.X, (int)npc.position.Y, 9, 1f, 0f);
            }
            npc.noGravity = true;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.ai[3]++;
                if (npc.ai[3] > 400)
                {
                    npc.ai[2] = 1;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
            }
            if (npc.ai[2] == 0)
            {
                if (!npc.noTileCollide)
                {
                    if (npc.collideX)
                    {
                        npc.velocity.X = npc.oldVelocity.X * -0.5f;
                        if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                        {
                            npc.velocity.X = 2f;
                        }
                        if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                        {
                            npc.velocity.X = -2f;
                        }
                    }
                    if (npc.collideY)
                    {
                        npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                        if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                        {
                            npc.velocity.Y = 1f;
                        }
                        if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                        {
                            npc.velocity.Y = -1f;
                        }
                    }
                }
                npc.TargetClosest(true);
                if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    if (npc.ai[1] > 0f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[1] = 0f;
                        npc.ai[0] = 0f;
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[1] == 0f)
                {
                    npc.ai[0] += 1f;
                }
                if (npc.ai[0] >= 300f)
                {
                    npc.ai[1] = 1f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                }
                if (npc.ai[1] == 0f)
                {
                    npc.alpha = 0;
                    npc.noTileCollide = false;
                }
                else
                {
                    npc.wet = false;
                    npc.alpha = 200;
                    npc.noTileCollide = true;
                }
                npc.rotation = npc.velocity.Y * 0.1f * npc.direction;
                npc.TargetClosest(true);
                if (npc.direction == -1 && npc.velocity.X > -4f && npc.position.X > Main.player[npc.target].position.X + Main.player[npc.target].width)
                {
                    npc.velocity.X = npc.velocity.X - 0.08f;
                    if (npc.velocity.X > 4f)
                    {
                        npc.velocity.X = npc.velocity.X - 0.04f;
                    }
                    else if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X - 0.2f;
                    }
                    if (npc.velocity.X < -4f)
                    {
                        npc.velocity.X = -4f;
                    }
                }
                else if (npc.direction == 1 && npc.velocity.X < 4f && npc.position.X + npc.width < Main.player[npc.target].position.X)
                {
                    npc.velocity.X = npc.velocity.X + 0.08f;
                    if (npc.velocity.X < -4f)
                    {
                        npc.velocity.X = npc.velocity.X + 0.04f;
                    }
                    else if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X + 0.2f;
                    }
                    if (npc.velocity.X > 4f)
                    {
                        npc.velocity.X = 4f;
                    }
                }
                if (npc.directionY == -1 && npc.velocity.Y > -2.5 && npc.position.Y > Main.player[npc.target].position.Y + Main.player[npc.target].height)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.1f;
                    if (npc.velocity.Y > 2.5)
                    {
                        npc.velocity.Y = npc.velocity.Y - 0.05f;
                    }
                    else if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - 0.15f;
                    }
                    if (npc.velocity.Y < -2.5)
                    {
                        npc.velocity.Y = -2.5f;
                    }
                }
                else if (npc.directionY == 1 && npc.velocity.Y < 2.5 && npc.position.Y + npc.height < Main.player[npc.target].position.Y)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.1f;
                    if (npc.velocity.Y < -2.5)
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.05f;
                    }
                    else if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.15f;
                    }
                    if (npc.velocity.Y > 2.5)
                    {
                        npc.velocity.Y = 2.5f;
                    }
                }
            }
            else
            {
                npc.rotation = npc.velocity.Y * 0.1f * npc.direction;
                npc.TargetClosest(true);
                if (SelectPoint)
                {
                    float Point = 500 * npc.direction;
                    MovePoint = Main.player[npc.target].Center + new Vector2(Point, 500f);
                    SelectPoint = false;
                    npc.netUpdate = true;
                }
                MeleeMovement(MovePoint);
                npc.netUpdate = true;
                if (Vector2.Distance(MovePoint, npc.Center) < 20 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.ai[2] = 0;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
            }
        }


        public void MeleeMovement(Vector2 point)
        {
            float MeleeSpeed = 16f;
            if (MeleeSpeed < 16f)
            {
                MeleeSpeed += .5f;
            }
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < MeleeSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / MeleeSpeed);
            }
            if (length < 200f)
            {
                MeleeSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                MeleeSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                MeleeSpeed *= 0.5f;
            }
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= MeleeSpeed;
            npc.velocity *= velMultiplier;
        }

        public override void HitEffect(int hitDirection, double damage)
		{
            if (npc.life > 0)
            {
                int num589 = 0;
                while (num589 < damage / npc.lifeMax * 50.0)
                {
                    int num590 = Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 0, default, 1.5f);
                    Main.dust[num590].velocity *= 1.5f;
                    Main.dust[num590].noGravity = true;
                    num589++;
                }
                return;
            }
            for (int num591 = 0; num591 < 10; num591++)
            {
                int num592 = Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<Dusts.InfinityOverloadB>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num592].velocity *= 2f;
                Main.dust[num592].noGravity = true;
            }
            for (int num593 = 0; num593 < 4; num593++)
            {
                int num594 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + npc.height / 2 - 10f), new Vector2(hitDirection, 0f), 99, npc.scale);
                Main.gore[num594].velocity *= 0.3f;
            }
        }

		public override void NPCLoot()
		{
            if (Main.rand.Next(3) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Items.Materials.OceanWhisper>(), 1, false, 0, false, false);
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/Fishron_Glow");

            if (npc.ai[2] == 1f)
            {
                BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 6, false, 0f, 0f, Color.Cyan);
            }

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 7, npc.frame, dColor, true);

            if (npc.ai[2] == 1f)
            {
                BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 7, npc.frame, Color.White, true);
            }
            return false;
        }

        public static void DrawAfterimage(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, Vector2[] oldPoints, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default, float distanceScalar = 1.0F, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, bool drawCentered = false, Color? overrideColor = null)
        {
            Color lightColor = overrideColor != null ? (Color)overrideColor : BaseDrawing.GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
            Vector2 velAddon = default;
            Vector2 originalpos = position;
            Vector2 offset = new Vector2(offsetX, offsetY);
            for (int m = 1; m <= imageCount; m++)
            {
                scale *= sizeScalar;
                Color newLightColor = lightColor;
                newLightColor.R = (byte)(newLightColor.R * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.G = (byte)(newLightColor.G * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.B = (byte)(newLightColor.B * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.A = (byte)(newLightColor.A * (imageCount + 3 - m) / (imageCount + 9));
                if (useOldPos)
                {
                    position = Vector2.Lerp(originalpos, m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1], distanceScalar);
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset, width, height, scale, rotation, direction, framecount, frame, newLightColor, drawCentered ? true : false);
                }
                else
                {
                    Vector2 velocity = m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1];
                    velAddon += velocity * distanceScalar;
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset - velAddon, width, height, scale, rotation, direction, framecount, frame, newLightColor, drawCentered ? true : false);
                }
            }
        }
    }
}