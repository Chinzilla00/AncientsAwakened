using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.AH;
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
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 4));
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 46;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.rare = 11;
            item.noUseGraphic = true;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType<Ashe>()) && !NPC.AnyNPCs(mod.NPCType<Haruka>()) && !NPC.AnyNPCs(mod.NPCType<AHSpawn>());
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundID.Roar, player.position, 0);
            AAWorld.SistersSummoned = true;

            if (AAWorld.SistersSummoned && !AAWorld.downedSisters)
            {
                AAModGlobalNPC.SpawnBoss(mod, player, "Ashe", false);
                AAModGlobalNPC.SpawnBoss(mod, player, "Haruka", false);
                Main.NewText("Again..!? Didin't you learn from last time? Oh well, I'm gonna have a ball blasting you to shreds!", new Color(102, 20, 48));

                Main.NewText("Whatever, let's just get this overwith.", new Color(72, 78, 117));
                return true;
            }
            else if (AAWorld.SistersSummoned && AAWorld.downedSisters)
            {
                AAModGlobalNPC.SpawnBoss(mod, player, "Ashe", false);
                AAModGlobalNPC.SpawnBoss(mod, player, "Haruka", false);
                Main.NewText("Sigh...here we go again...", new Color(72, 78, 117));
                Main.NewText("THIS TIME I'M NOT LOSING! You're gonna feel the taste of defeat you disgusting warm-blood!", new Color(102, 20, 48));
                return true;
            }
            else
            {
                AAPlayer.SilentBossSpawn(mod, player, "AHSpawn");
                return true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiantIncinerite", 10);
            recipe.AddIngredient(null, "DeepAbyssium", 10);
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