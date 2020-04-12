using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena.Olympian
{
    public class GaleForce : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 200;                        
            item.magic = true;                     
            item.width = 24;
            item.height = 28;
            item.useStyle = 5;        
            item.noMelee = true;
            item.knockBack = 6;
            item.mana = 8;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.shoot = mod.ProjectileType("HurricaneSpawn");
            item.shootSpeed = 9f;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Gale Force");
          Tooltip.SetDefault("");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GaleOfWings", 1);
            recipe.AddIngredient(null, "StormSphere", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
