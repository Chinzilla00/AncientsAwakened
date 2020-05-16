using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class ShenTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 54;
            item.height = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 4;
            item.useAnimation = 16;
            item.tileBoost += 25;
            item.knockBack = 3;
            item.value = Item.sellPrice(1, 50, 0, 0);
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 120;
            item.pick = 320;
            AARarity = 14;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Terratool");
            Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }


        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && Main.mouseRight && Main.mouseRightRelease)
            {
                item.autoReuse = false;
                item.noUseGraphic = true;
                AAMod.instance.TerratoolSState.ToggleUI(AAMod.instance.TerratoolInterface);
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                item.damage = 0;
                return false;
            }
            else if(player.altFunctionUse != 2)
            {
                item.autoReuse = true;
                item.noUseGraphic = false;
                item.pick = UI.TerratoolSUI.Pick;
                item.axe = UI.TerratoolSUI.Axe;
                item.hammer = UI.TerratoolSUI.Hammer;
                item.damage = 120;
            }
            else
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AkumaTerratool", 1);
            recipe.AddIngredient(null, "YamataTerratool", 1);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
