using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class TerratoolEX : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 54;
            item.height = 60;
            item.useStyle = 1;
            item.useTime = 4;
            item.useAnimation = 16;
            item.tileBoost += 25;
            item.knockBack = 3;
            item.value = Item.sellPrice(1, 50, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 120;
            item.pick = 320;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terraformer");
            Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active
Terratool EX");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noUseGraphic = true;
                AAMod.instance.TerratoolEXState.ToggleUI(AAMod.instance.TerratoolInterface);
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                item.damage = 0;
            }
            else
            {
                item.noUseGraphic = false;
                item.pick = UI.TerratoolEXUI.Pick;
                item.axe = UI.TerratoolEXUI.Axe;
                item.hammer = UI.TerratoolEXUI.Hammer;
                item.damage = 120;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Terratool");
            recipe.AddIngredient(mod, "EXSoul");
            recipe.AddTile(mod, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
