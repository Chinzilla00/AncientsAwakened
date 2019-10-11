using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterMask : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Darkmatter Mask");
			Tooltip.SetDefault(@"9% increased magic damage
Dark, yet still barely visible");

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

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 18;
			item.value = 300000;
			item.rare = 11;
			item.defense = 20;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.09f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = "";
            player.GetModPlayer<DarkmatterMaskEffects>().setBonus = true;
            player.armorEffectDrawShadowLokis = true;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 25);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
    public class DarkmatterMaskEffects : ModPlayer
    {
        public bool setBonus = false;
        public int timer;
        public int auraFrame = 0;
        public int auraFrameCount = 4;
        public override void ResetEffects()
        {
            setBonus = false;
            
        }
        public override void PreUpdate()
        {

            timer++;
            if (timer % 4 == 0)
            {
                auraFrame++;
                if (auraFrame >= auraFrameCount)
                {
                    auraFrame = 0;
                }
            }
        }
        public static readonly PlayerLayer Portal = new PlayerLayer("AAMod", "Portal", PlayerLayer.MiscEffectsBack, delegate (PlayerDrawInfo drawInfo)
        {

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("AAMod");
            Texture2D texture = mod.GetTexture("Items/Armor/Darkmatter/Aura");

            if (drawPlayer.GetModPlayer<DarkmatterMaskEffects>().setBonus)
            {
                Vector2 Center = drawInfo.position + new Vector2(drawPlayer.width / 2, drawPlayer.height / 2)  - Main.screenPosition;

                DrawData data = new DrawData(texture, Center, texture.Frame(1, drawPlayer.GetModPlayer<DarkmatterMaskEffects>().auraFrameCount, 0, drawPlayer.GetModPlayer<DarkmatterMaskEffects>().auraFrame), Color.White, 0f, new Vector2(texture.Size().X, texture.Size().Y / 4) * .5f, 1f, drawInfo.spriteEffects, 0);
                data.shader = drawInfo.bodyArmorShader;
                Main.playerDrawData.Add(data);
            }
        });
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {


            int frontLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("MiscEffectsBack"));
            if (frontLayer != -1)
            {
                Portal.visible = true;
                layers.Insert(frontLayer + 1, Portal);
            }
        }
    }
}