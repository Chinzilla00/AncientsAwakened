using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class IllumantFlail : BaseAAItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.SolarEruption);

            item.damage = 52;            
            item.melee = true;            
            item.width = 56;              
            item.height = 56;             

            item.knockBack = 6;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Lime;
            item.autoReuse = true;   
            item.useTurn = false;
            item.shoot = mod.ProjectileType("IllumantBall");
            item.UseSound = SoundID.Item1;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Illuminant Flail");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.CrystalShard, 20);   
			recipe.AddIngredient(ItemID.BlueMoon, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
