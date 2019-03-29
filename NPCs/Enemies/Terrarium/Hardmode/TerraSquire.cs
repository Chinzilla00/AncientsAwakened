using BaseMod;
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
            DisplayName.SetDefault("TerraSquire");
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
            bannerItem = mod.ItemType("MoltenGolemBanner"); //this defines what banner this npc will drop
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
            Color color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.LimeGreen, BaseDrawing.GetLightColor(npc.position), Color.LimeGreen, BaseDrawing.GetLightColor(npc.position));
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, npc.dontTakeDamage ? color : dColor);
            return false;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Underworld.Chance * (NPC.downedBoss3 ? 0.05f : 0f);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
            {
                target.AddBuff(BuffID.OnFire, 200);
            }
        }
    }
}