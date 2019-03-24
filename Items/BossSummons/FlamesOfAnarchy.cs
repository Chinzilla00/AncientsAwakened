using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.AH.Ashe;
using AAMod.NPCs.Bosses.AH.Haruka;
using System.Collections.Generic;
using BaseMod;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace AAMod.Items.BossSummons
{
    public class FlamesOfAnarchy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flames of Anarchy");
            Tooltip.SetDefault(@"The flames of chaos burn in this antique china
Calls upon the Sisters of Discord");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 4));
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.rare = 11;
        }

        public override bool PreDrawInWorld(SpriteBatch sb, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/BossSummons/FlamesOfAnarchy_Glow");
            Vector2 pos = new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                );
            BaseDrawing.DrawTexture(sb, Main.itemTexture[item.type], 0, pos, item.width, item.height, scale, rotation, 0, 4, new Rectangle(0, 0, Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height), lightColor);
            BaseDrawing.DrawTexture(sb, texture, 0, pos, item.width, item.height, scale, rotation, 0, 4, new Rectangle(0, 0, texture.Width, texture.Height), AAColor.Shen2);
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch sb, Vector2 pos, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("Items/BossSummons/FlamesOfAnarchy_Glow");
            BaseDrawing.DrawTexture(sb, Main.itemTexture[item.type], 0, pos, item.width, item.height, scale, 0, 0, 4, new Rectangle(0, 0, texture.Width, texture.Height), drawColor);
            BaseDrawing.DrawTexture(sb, texture, 0, pos, item.width, item.height, scale, 0, 0, 4, new Rectangle(0, 0, texture.Width, texture.Height), AAColor.Shen2);
            return false;
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType<Ashe>()) && !NPC.AnyNPCs(mod.NPCType<Haruka>());
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Roar, player.position, 0);

            if (AAWorld.SistersSummoned && !AAWorld.downedSisters)
            {
                Main.NewText("Again..!? Didin't you learn from last time? Oh well, I'm gonna have a ball blasting you to shreds!", new Color(102, 20, 48));
                SpawnBoss(player, "Ashe");

                Main.NewText("Whatever, let's just get this overwith.", new Color(72, 78, 117));
                SpawnBoss(player, "Haruka");
                return true;
            }
            else if (AAWorld.SistersSummoned && AAWorld.downedSisters)
            {
                Main.NewText("Sigh...here we go again...", new Color(72, 78, 117));
                SpawnBoss(player, "Haruka");

                Main.NewText("THIS TIME I'M NOT LOSING! You're gonna feel the taste of defeat you disgusting warm-blood!", new Color(102, 20, 48));
                SpawnBoss(player, "Ashe");
                return true;
            }
            else
            {
                SpawnBoss(player, "AHSpawn");
                return true;
            }
        }

        public void SpawnBoss(Player player, string name)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssiumOre", 20);
            recipe.AddIngredient(null, "DaybreakIncineriteOre", 20);
            recipe.AddIngredient(null, "DragonFire", 5);
            recipe.AddIngredient(null, "HydraToxin", 5);
            recipe.AddIngredient(null, "SoulOfSmite", 5);
            recipe.AddIngredient(null, "SoulOfSpite", 5);
            recipe.AddIngredient(null, "BroodScale", 3);
            recipe.AddIngredient(null, "HydraHide", 3);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}