using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class FulguriteTazerblaster : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Fulgurite Tazerblaster");
            Tooltip.SetDefault(@"Rapidly fires taserblasts
Slim chance to fire 2 taserblasts at once");
            
        }

		public override void SetDefaults()
		{
			item.damage = 45;
			item.ranged = true;
			item.width = 52;
			item.height = 18;
            item.useAnimation = 12;
            item.useTime = 12;
            item.reuseDelay = 2;
            item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 2.5f;
            item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 5;
			item.UseSound = SoundID.Item12;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Taserblast");
            item.shootSpeed = 17f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FulguriteBar", 20);              //example of how to craft with a modded item
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}