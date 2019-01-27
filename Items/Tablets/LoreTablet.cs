using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tablet
{
    /*public class LoreTablet : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Tablets/LoreTablet"; } }
        public Color color = Color.Purple;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgarian Data Log #001; Contact");
            Tooltip.SetDefault(@"Contact with the world of terraria has been made.
We will be documenting all of the strange anomolies 
detected here on this small planet. First step; Establish 
a base.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,11));
        }
        #region methods

        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;
        public static Texture2D tex = null;
        public static Texture2D glowTex = null;


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, texture2, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, color);
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {

            TabletMethods.TabletDrawInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale, item, color);
            return false;
        }

        public override void AddRecipes()
        {
            TabletMethods.TabletRecipes(mod, this);
        }
        #endregion
    }

    public class InfernoTablet : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Tablets/LoreTablet"; } }
        public Color color = Color.Orange;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgarian Data Log #002; The Inferno");
            Tooltip.SetDefault(@"The volcanic region of this area seems to be extrordinarilly 
The volcanic region of this area seems to be exceedingly 
active with dragons. The temperature also seems to be 
exceedingly high. The volcanoes seem to become active once
nightfall hits, spraying ash everywhere. Too dangerous to 
traverse during this time without proper equipment. We are 
dubbing it as the Inferno.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 11));
        }
        #region methods

        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, texture2, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, color);
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            TabletMethods.TabletDrawInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale, item, color);
            return false;
        }

        public override void AddRecipes()
        {
            TabletMethods.TabletRecipes(mod, this);
        }
        #endregion
    }
    public class MireTablet : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Tablets/LoreTablet"; } }
        public Color color = Color.Indigo;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgarian Data Log #003; The Mire");
            Tooltip.SetDefault(@"The wetlands region of this area is infested with noctournal
amphibious creatures that seem to dislike light. Thick, nearly
unnavigatable fog covers the area during the day, making it 
impossible to avoid getting lost. However, the fog seems to 
clear up in the presence of fire magic, specifically from the
Inferno. Maybe we could use that to study the region during 
the day safely. We are dubbing this area as the Mire.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 11));
        }
        #region methods

        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, texture2, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, color);
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            TabletMethods.TabletDrawInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale, item, color);
            return false;
        }

        public override void AddRecipes()
        {
            TabletMethods.TabletRecipes(mod, this);
        }
        #endregion
    }
    public class CorruptionTablet : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Tablets/LoreTablet"; } }
        public Color color = Color.Violet;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgarian Data Log #004; The Corruption");
            Tooltip.SetDefault(@"An infectious bacterium appears to be tearing its way
through the land, spreading to untouched areas and infesting
it, giving it a purple color to the things it infests. 
This bacterium seems to be unnactural to this ecosystem,
which explains why it's so successful intaking over; It has
no natural predators. We will dub this area as theCorruption.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 11));
        }
        #region methods

        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, texture2, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, color);
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            TabletMethods.TabletDrawInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale, item, color);
            return false;
        }

        public override void AddRecipes()
        {
            TabletMethods.TabletRecipes(mod, this);
        }
        #endregion
    }
    public class CrimsonTablet : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Tablets/LoreTablet"; } }
        public Color color = Color.Red;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgarian Data Log #005; The Crimson");
            Tooltip.SetDefault(@"There is a large, carnal mound of flesh that appears
to be covering the terrain in itself, attempting to expand
and devour everything around it. This flesh mound seems to
be outputting fleshy monsters that appear to attack any who
trespass into the area it has consumed, acting almost hivelike.
due to it's unmistakeable color, we will call it the Crimson.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 11));
        }
        #region methods

        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, texture2, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, color);
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            TabletMethods.TabletDrawInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale, item, color);
            return false;
        }

        public override void AddRecipes()
        {
            TabletMethods.TabletRecipes(mod, this);
        }
        #endregion
    }
    public class MushroomTablet : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Tablets/LoreTablet"; } }
        public Color color = Color.PeachPuff;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgarian Data Log #006; Mushroom Biomes");
            Tooltip.SetDefault(@"There appear to be large fungal deposits around the surface
and underground, larger than ever recorded. The conditions on
this island seem to be just right for these mushrooms to grow
to their exponential rate. They have also been tested to show
immense healing properties, along with other effects. The mushroms
also appear to be able to consume living beings and take them
over, much like cortiseps. Take caution.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 11));
        }
        #region methods

        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, texture2, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, color);
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            TabletMethods.TabletDrawInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale, item, color);
            return false;
        }

        public override void AddRecipes()
        {
            TabletMethods.TabletRecipes(mod, this);
        }
        #endregion
    }
    public class MonarchTablet : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Tablets/LoreTablet"; } }
        public Color color = Color.PeachPuff;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgarian Data Log #007; The Mushroom Monarch");
            Tooltip.SetDefault(@"A large mushroom creeature has been discovered to live in the
surface mushroom biome, and appears to be treated as some kind
of feudal figure amongst the mushroom creatures. Subject is very
hostile to intruders if sighted, and will attempt to run its
target down or stomp them into the ground to get them to leave
its territory. We dub it the Mushroom Monarch.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 11));
        }
        #region methods

        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, texture2, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, color);
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            TabletMethods.TabletDrawInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale, item, color);
            return false;
        }

        public override void AddRecipes()
        {
            TabletMethods.TabletRecipes(mod, this);
        }
        #endregion
    }
    public class SlimeTablet : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Tablets/LoreTablet"; } }
        public Color color = Color.Blue;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fulgarian Data Log #008; Slimes");
            Tooltip.SetDefault(@"Slimes appear to be the most common organism on this island,
making up more than 60% of the ecosystem. They are highly
adaptable, yet they all appear to be made of the same substance
curiously. Much like the mushrooms, there appears to be a sort
of leader amongst the slime, which, funnily enough, actually has
a giant crown it wears around. We will name it the King Slime.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 11));
        }
        #region methods

        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            BaseDrawing.DrawTexture(spriteBatch, texture, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, texture2, 0, item.position, item.width, item.height, scale, rotation, 0, 11, frame, color);
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            TabletMethods.TabletDrawInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale, item, color);
            return false;
        }

        public override void AddRecipes()
        {
            TabletMethods.TabletRecipes(mod, this);
        }
        #endregion
    }



    public class TabletMethods
    {
        public static int frameWidth = 24, frameHeight = 32;
        public int frameTimer = 0;
        public int frameCount = 10;
        public Rectangle frame;
        public static Texture2D tex = null;
        public static Texture2D glowTex = null;
        
        public static void TabletDrawInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale, Item item, Color GlowColor)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D texture2 = AAMod.instance.GetTexture("Items/Tablets/LoreTablet_Glow");
            frame = BaseDrawing.GetFrame(10, 24, 32, 0, 2);
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position + offsetPositon, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture2, position + offsetPositon, null, GlowColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
        }

        public static void TabletRecipes(Mod mod, ModItem tablet)
        {
            ModRecipe recipe = new ModRecipe(mod);
            int DataBank = mod.TileType<Tiles.DataBank>();
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "DragonScale", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(mod.ItemType<InfernoTablet>());
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "MirePod", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(mod.ItemType<MireTablet>());
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(ItemID.RottenChunk, 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(mod.ItemType<CorruptionTablet>());
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(ItemID.Vertebrae, 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(mod.ItemType<CrimsonTablet>());
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(mod, "Mushium", 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(mod.ItemType<MushroomTablet>());
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(ItemID.GlowingMushroom, 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(mod.ItemType<MushroomTablet>());
                recipe.AddRecipe();
            }
            {
                recipe.AddIngredient(tablet);
                recipe.AddIngredient(ItemID.Gel, 1);
                recipe.AddTile(DataBank);
                recipe.SetResult(mod.ItemType<SlimeTablet>());
                recipe.AddRecipe();
            }
        }
    }*/

}