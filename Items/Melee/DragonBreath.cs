using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class DragonBreath : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.useTime = 25;
            item.CloneDefaults(ItemID.Code2);

            item.damage = 60;
            item.value = 100000;
            item.rare = ItemRarityID.Green;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 18;
            item.useTime = 18;
            item.shoot = mod.ProjectileType("DragonBreathP");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Dragon's Breath");
            Tooltip.SetDefault("It must need to brush it's teeth");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "DragonSpirit", 20);		
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
