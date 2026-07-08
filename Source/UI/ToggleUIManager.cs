using Celeste.Mod.Celeste_Multiworld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Celeste.Mod.ItemToggle.UI
{
    public class ToggleUIManager : DrawableGameComponent
    {
        private int OffsetX = 300;
        private int OffsetY = 200;
        private int Padding = 10;
        private int TitleHeight = 100;
        private int ItemMargin = 72;
        private int KeyMenuWidth = 400;

        private int NumRows, NumCols = 0;
        public (int r, int c) SelectedItem = (0, 0);
        public int SelectedKey = 0;

        private MenuContext Context;

        private readonly Color ActiveColor = Calc.HexToColor("30B335");
        private readonly Color SelectBorderColor = Calc.HexToColor("FF8000");
        private readonly Color SelectBodyColor = Calc.HexToColor("45283C");
        private readonly float FontSize = ActiveFont.BaseSize; // 64px

        private readonly Dictionary<long,string> APKeyAPToID = new Dictionary<long, string>();

        public ToggleUIManager(Game game) : base(game)
        {
            game.Components.Add(this);
            Visible = false;

            NumRows = ToggleUIItemData.ItemGrid.GetLength(0);
            NumCols = ToggleUIItemData.ItemGrid.GetLength(1);

            var apItemIDToString = (Dictionary<long,string>) typeof(Celeste_MultiworldModule).Assembly.GetType("Celeste.Mod.Celeste_Multiworld.Items.APItemData").GetProperty("ItemIDToString",BindingFlags.Static | BindingFlags.Public).GetValue(null);
            APKeyAPToID = (Dictionary<long,string>) typeof(Celeste_MultiworldModule).Assembly.GetType("Celeste.Mod.Celeste_Multiworld.Locations.APLocationData").GetProperty("KeyAPToID",BindingFlags.Static | BindingFlags.Public).GetValue(null);

            ToggleUIItemData.KeyMenu.SetActive = (_) => OpenKeyMenu();
            ToggleUIItemData.KeyList = APKeyAPToID.Select(e => {
                string name = apItemIDToString[e.Key];
                if (apItemIDToString[e.Key].Contains(" - ")) name = apItemIDToString[e.Key].Split(" - ")[1];
                return new ToggleableItem(e.Key,name,Celeste_MultiworldModule.SaveData.KeyItems);
            }).ToList();
        }

        private enum MenuContext
        {
            ItemSelect,
            KeySelect
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            bool disabled = MInput.Disabled;
            MInput.Disabled = false;
            // UI Controls
            if (Visible)
            {
                switch (Context)
                {
                    case MenuContext.ItemSelect:
                        if (Visible)
                        {
                            if (Input.MenuRight.Pressed)
                            {
                                do SelectedItem.c = (SelectedItem.c + 1) % NumCols;
                                while (ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c] == null);
                            }
                            if (Input.MenuLeft.Pressed)
                            {
                                do SelectedItem.c = (SelectedItem.c + NumCols - 1) % NumCols;
                                while (ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c] == null);
                            }
                            if (Input.MenuUp.Pressed)
                            {
                                do SelectedItem.r = (SelectedItem.r + NumRows - 1) % NumRows;
                                while (ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c] == null);
                            }
                            if (Input.MenuDown.Pressed)
                            {
                                do SelectedItem.r = (SelectedItem.r + 1) % NumRows;
                                while (ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c] == null);
                            }
                            if (Input.MenuConfirm.Pressed)
                            {
                                var item = ToggleUIItemData.ItemGrid[SelectedItem.r, SelectedItem.c];
                                item?.SetActive(!item.GetActive());
                            }
                        }
                        break;
                    case MenuContext.KeySelect:
                        if (Input.MenuCancel.Pressed)
                        {
                            Context = MenuContext.ItemSelect;
                            Input.MenuCancel.ConsumePress();
                        }
                        if (Input.MenuUp.Pressed)
                        {
                            int numKeys = GetKeysInArea().Count;
                            SelectedKey = (SelectedKey + numKeys - 1) % numKeys;
                        }
                        if (Input.MenuDown.Pressed)
                        {
                            int numKeys = GetKeysInArea().Count;
                            SelectedKey = (SelectedKey + 1) % numKeys;
                        }
                        if (Input.MenuConfirm.Pressed)
                        {
                            var key = GetKeysInArea()[SelectedKey];
                            Logger.Info("item",$"{key.Name}: {key.GetActive()}");
                            key.SetActive(!key.GetActive());
                        }
                        break;
                }
            }
            if (ItemToggleModule.Settings.OpenItemToggleMenu.Pressed || (Visible && Context == MenuContext.ItemSelect && Input.MenuCancel.Pressed))
            {
                Visible = !Visible;
                disabled = Visible;
                Input.MenuCancel.ConsumePress();
            }
            
            MInput.Disabled = disabled;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;

            int width = Math.Max(500, 2 * Padding + ItemMargin * NumCols);
            int height = Math.Max(300, 3 * Padding + ItemMargin * NumRows + TitleHeight);

            base.Draw(gameTime);
            Monocle.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            // Background
            Monocle.Draw.Rect(OffsetX, OffsetY, width, height, new Color(0.1f, 0.1f, 0.1f, 0.75f));

            // Title
            Monocle.Draw.Rect(OffsetX + Padding, OffsetY + Padding, width - 2 * Padding, TitleHeight, new Color(0.1f, 0.1f, 0.1f, 0.5f));
            ActiveFont.Draw("Item Toggle Menu", new Vector2(OffsetX + width / 2, OffsetY + Padding + TitleHeight / 2), new Vector2(0.5f), new Vector2(1f), Color.White);
            Monocle.Draw.SpriteBatch.End();

            // Grid
            Monocle.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            for (int r = 0; r < NumRows; r++)
            {
                for (int c = 0; c < NumCols; c++)
                {
                    ToggleableItem item = ToggleUIItemData.ItemGrid[r, c];
                    if (item == null) continue;

                    int X = OffsetX + Padding + c * ItemMargin;
                    int Y = OffsetY + 2 * Padding + r * ItemMargin + TitleHeight;

                    // Selection highlight
                    if (SelectedItem.Equals((r, c)))
                    {
                        Monocle.Draw.Rect(X  , Y  , ItemMargin  , ItemMargin  , SelectBorderColor);
                        Monocle.Draw.Rect(X+2, Y+2, ItemMargin-4, ItemMargin-4, SelectBodyColor);
                    }
                    // Active highlight
                    if (item.GetActive())
                    {
                        if (!(item.ItemID > 0xCA10080 && item.ItemID < 0xCA1008A))// Dash direction
                        {
                            Monocle.Draw.Rect(X+2, Y+2, ItemMargin-4, ItemMargin-4, ActiveColor);
                        }
                    }
                    // Red highlight on key if there are no keys in the current level
                    if (item.Name == "key" && GetKeysInArea().Count == 0)
                    {
                        Monocle.Draw.Rect(X+2, Y+2, ItemMargin-4, ItemMargin-4, Color.DarkRed);
                    }

                    X += ItemMargin / 2;
                    Y += ItemMargin / 2;
                    MTexture tex = GFX.Game[$"ToggleIcons/{item.Name}"];
                    Color color = Color.White;
                    if (item.GetActive() && item.ItemID > 0xCA10080 && item.ItemID < 0xCA1008A) // Dash direction
                    {
                        color = Color.Magenta;
                    }
                    tex.DrawCentered(new Vector2(X, Y), color, 2f);
                }
            }
            Monocle.Draw.SpriteBatch.End();

            // Key select menu
            Monocle.Draw.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            if (Context == MenuContext.KeySelect)
            {
                var keys = GetKeysInArea();

                int topLeftX = OffsetX+width-KeyMenuWidth;
                int topLeftY = OffsetY+height+Padding;
                float keyMenuHeight = keys.Count * (FontSize/2+16) + 2*Padding;
                Monocle.Draw.Rect(topLeftX, topLeftY, KeyMenuWidth, keyMenuHeight, new Color(0.1f, 0.1f, 0.1f, 0.75f));

                topLeftX += Padding;
                topLeftY += Padding;
                float Y = 0;

                for (int i = 0; i < keys.Count; i++)
                {
                    // Selection highlight
                    if (SelectedKey == i)
                    {
                        Monocle.Draw.Rect(topLeftX  , topLeftY+Y  , KeyMenuWidth-2*Padding  , FontSize/2+8, SelectBorderColor);
                        Monocle.Draw.Rect(topLeftX+2, topLeftY+Y+2, KeyMenuWidth-2*Padding-4, FontSize/2+4, SelectBodyColor);
                    }

                    // Active highlight
                    if (keys[i].GetActive())
                    {
                       Monocle.Draw.Rect(topLeftX+2, topLeftY+Y+2, KeyMenuWidth-2*Padding-4, FontSize/2+4, ActiveColor);
                    }

                    ActiveFont.Draw(keys[i].Name, new Vector2(topLeftX+Padding,topLeftY+Y+4), new Vector2(0f), new Vector2(0.5f), Color.White);

                    Y += FontSize/2+16;
                }
            }
            Monocle.Draw.SpriteBatch.End();
        }

        public void OpenKeyMenu()
        {
            if (SaveData.Instance.CurrentSession_Safe == null) return;
            if (GetKeysInArea().Count == 0) return;

            Context = MenuContext.KeySelect;
            SelectedKey = 0;
        }

        private List<ToggleableItem> GetKeysInArea()
        {
            if (SaveData.Instance.CurrentSession_Safe == null) return new List<ToggleableItem>();
            string currentMapID = $"{SaveData.Instance.CurrentSession_Safe.Area.ID}_{(int)SaveData.Instance.CurrentSession_Safe.Area.Mode}";
            return ToggleUIItemData.KeyList.Where(k => APKeyAPToID[k.ItemID].StartsWith(currentMapID)).ToList();
        }
    }
}
