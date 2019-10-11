using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Rajah;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AAMod.Items.BossSummons
{
    public class DiamondCarrot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ten Carat Carrot");
            Tooltip.SetDefault(@"The fury of the Raging Rajah can be felt radiating from this ornate carrot...
Non-consumable");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.rare = 9;
            AARarity = 14;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.noUseGraphic = true;
            item.consumable = false;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            return !(NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<Rajah>()) ||
                NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<Rajah2>()) ||
                NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<Rajah3>()) ||
                NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<Rajah4>()) ||
                NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<Rajah5>()) ||
                NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<Rajah6>()) ||
                NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<Rajah7>()) ||
                NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<Rajah8>()) ||
                NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<Rajah9>()) ||
                NPC.AnyNPCs(Terraria.ModLoader.ModContent.NPCType<SupremeRajah>()));
        }

        public override bool UseItem(Player player)
        {
            if (!AAWorld.downedRajahsRevenge)
            {
                if (Main.netMode != 1) BaseMod.BaseUtility.Chat("GRAVE MISTAKE, TERRARIAN!", 107, 137, 179);
            }
            else
            {
                string Name;
                if (Main.netMode != 0)
                {
                    Name = "Terrarians";
                }
                else
                {
                    Name = Main.LocalPlayer.name;
                }
                if (Main.netMode != 1) BaseMod.BaseUtility.Chat("Show me what you got, " + Name + "!", 107, 137, 179);
            }
            int overrideDirection = Main.rand.Next(2) == 0 ? -1 : 1;
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("SupremeRajah"), false, player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, -1200), "Rajah Rabbit");
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GoldenCarrot", 1);
            recipe.AddIngredient(null, "UnstableSingularity", 3);
            recipe.AddIngredient(null, "CrucibleScale", 3);
            recipe.AddIngredient(null, "DreadScale", 3);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PlatinumCarrot", 1);
            recipe.AddIngredient(null, "UnstableSingularity", 3);
            recipe.AddIngredient(null, "CrucibleScale", 3);
            recipe.AddIngredient(null, "DreadScale", 3);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}