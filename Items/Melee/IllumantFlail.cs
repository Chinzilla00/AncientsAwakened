using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class IllumantFlail : ModItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.SolarEruption);

            item.damage = 52;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 56;              //Sword width
            item.height = 56;             //Sword height

            item.knockBack = 6;      //Sword knockback
            item.value = 10;        
            item.rare = 7;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = false;
            item.shoot = mod.ProjectileType("IllumantBall");
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Illuminant Flail");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.CrystalShard, 20);   //you need 1 DirtBlock
			recipe.AddIngredient(ItemID.BlueMoon, 1);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
