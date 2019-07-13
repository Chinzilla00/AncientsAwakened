using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Terratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 54;
            item.height = 60;
			item.useStyle = 1;
            item.useTime = 5;
            item.useAnimation = 20;
            item.tileBoost += 3;
            item.knockBack = 3;
            item.value = 1000000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 60;
            item.pick = 215;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terratool");
            Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active
+3 tile reach");
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
                AAMod.instance.TerratoolState.ToggleUI(AAMod.instance.TerratoolInterface);
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                item.damage = 0;
            }
            else
            {
                item.noUseGraphic = false;
                item.pick = UI.TerratoolTUI.Pick;
                item.axe = UI.TerratoolTUI.Axe;
                item.hammer = UI.TerratoolTUI.Hammer;
                item.damage = 60;
            }
            return true;
        }

        public override void AddRecipes()  
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "TrueNightaxe");
                recipe.AddIngredient(ItemID.Picksaw);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {

                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "TrueScalpel");
                recipe.AddIngredient(ItemID.Picksaw);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
