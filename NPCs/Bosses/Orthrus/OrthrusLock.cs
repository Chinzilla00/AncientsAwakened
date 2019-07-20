using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Orthrus
{
    public class OrthrusLock : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Target Locked.");
        }
        public override void SetDefaults()
        {
            npc.width = 74;
            npc.height = 74;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;
            npc.alpha = 255;
            npc.scale *= 10;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 1, npc.frame, npc.GetAlpha(Color.White), true);
            return false;
        }

        public NPC orthrus = null;

        public override void AI()
        {
            NPC body = Main.npc[BaseAI.GetNPC(npc.Center, mod.NPCType<OrthrusHead1>(), -1)];
            OrthrusHead1 orthrus = (OrthrusHead1)body.modNPC;

            Player player = Main.player[npc.target];

            if (Main.netMode != 1)
            {
                npc.ai[0]++;
            }

            npc.rotation += .1f;

            if (npc.target == -1)
            {
                npc.TargetClosest();
            }

            if (orthrus.internalAI[0] >= 300)
            {
                if (npc.alpha < 255)
                {
                    npc.scale += .5f;
                    npc.alpha += 5;
                }
                else
                {
                    orthrus.internalAI[0] = 0;
                    body.netUpdate = true;
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (orthrus.internalAI[0] > 240)
                {
                    npc.velocity *= 0;
                    npc.netUpdate = true;
                }
                else
                {
                    npc.Center = player.Center;
                }
                if (npc.scale > 1f)
                {
                    npc.scale -= .5f;
                }
                else
                {
                    npc.scale = 1f;
                }
            }
        }
    }
}