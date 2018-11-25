using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class TrueOblivion : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Oblivion");
            Tooltip.SetDefault("Unleash the true power!");
        }
        public override void SetDefaults()
        {

            item.damage = 250;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 68;              //Sword width
            item.height = 68;             //Sword height
            item.useTime = 11;          //how fast 
            item.useAnimation = 11;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5;      //Sword knockback
            item.value = 2000000;        
            item.rare = 10;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;
            
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Oblivion", 1);
			recipe.AddIngredient(3467, 12);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);			//you need 1 DirtBlock
            recipe.AddTile(null, "QuantumFusionAccelerator");   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
