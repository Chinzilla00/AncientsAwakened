
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.NPCs.Enemies.Terrarium.Hardmode
{
    public class TerraSquire : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Squire");
            Main.npcFrameCount[npc.type] = 20;
        }
        public override void SetDefaults()
        {
            npc.width = 58;
            npc.height = 70;

            npc.damage = 40;
            npc.friendly = false;
            npc.defense = 18;
            npc.lifeMax = 300;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 0f;
            npc.knockBackResist = 0.05f;
            npc.aiStyle = 3;
            npc.lavaImmune = true;
            aiType = NPCID.GraniteGolem;  //npc behavior
            animationType = NPCID.GraniteGolem;
            banner = npc.type;
			bannerItem = mod.ItemType("TerraSquireBanner");
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 107, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 107, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 107, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 107, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 107, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
            }
            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 107, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 107, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Color color = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.LimeGreen, BaseDrawing.GetLightColor(npc.position), Color.LimeGreen, BaseDrawing.GetLightColor(npc.position));
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, npc.dontTakeDamage ? color : dColor);
            return false;
        }
    }
}