using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Boss.Retriever
{
    [AutoloadEquip(EquipType.Shield)]
    public class StormRiot : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 32;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = -12;
            item.expert = true;
            item.accessory = true;
            item.defense = 6;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Riot Shield");
            Tooltip.SetDefault(
@"For every hit you land on an enemy, 45 true damage (damage unassigned to any class) is dealt
Allows you to dash into enemies, damaging them
Non-autoswing weapons can be swung faster");
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<AAPlayer>().clawsOfChaos = true;
            player.GetModPlayer<AAPlayer>().StormClaw = true;
            player.dash = 2;
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BulwarkOfChaos", 1);
            recipe.AddIngredient(null, "StormClaw", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<StormClaw>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<Grips.ClawOfChaos>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}