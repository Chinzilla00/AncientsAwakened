using AAMod.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class ChaosTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 54;
            item.height = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 5;
            item.useAnimation = 20;
            item.tileBoost += 3;
            item.knockBack = 3;
            item.value = 1000000;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 60;
            item.pick = 205;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Terratool");
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
                AAMod.instance.TerratoolCState.ToggleUI(AAMod.instance.TerratoolInterface);
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
                item.pick = TerratoolCUI.Pick;
                item.axe = TerratoolCUI.Axe;
                item.hammer = TerratoolCUI.Hammer;
                item.damage = 60;
            }
            else
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "PerfectStonebreaker");
                recipe.AddIngredient(mod, "PerfectShadowDrill");
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
