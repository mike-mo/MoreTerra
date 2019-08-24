namespace MoreTerra
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
	using System.IO;
    using System.Windows.Forms;
	using MoreTerra.Utilities;
	using MoreTerra.Structures;
	using System.ComponentModel;
	using MoreTerra.Structures.TerraInfo;

    public class WorldMapper
    {
        private List<Chest> chests;
        private Dictionary<MarkerType, List<MarkerLoc>> tileMarkersToAdd;
        private int[,] tiles;

		private World world;

		int maxX, maxY;

        public int progress = 0;
		
        public WorldMapper()
        {
            chests = new List<Chest>();
        }

        public void Initialize()
        {
			TileData td;
			// Now we set DrawMarker for each of the markers we can potentially draw.
			// This makes for a much faster lookup to see if we need to draw an marker
			// rather than mass calling the DrawMarker function.
			for (Int32 i = 0; i < TileProperties.tileTypeDefs.Length; i++)
			{
				td = TileProperties.tileTypeDefs[i];

				if (td.MarkerType != MarkerType.Unknown)
				{
					td.DrawMarker = SettingsManager.Instance.DrawMarker((Int16) i);
				}
			}
        }

        public void OpenWorld()
        {
			world = new World();
        }

        public void ProcessWorld(String worldPath, BackgroundWorker bw)
        {

			tiles = world.ReadAndProcessWorld(worldPath, bw);
			if (tiles == null)
				return;
	
			progress = 45;

			maxX = world.Header.MaxTiles.X;
			maxY = world.Header.MaxTiles.Y;

            // Reset Marker List
            tileMarkersToAdd = new Dictionary<MarkerType, List<MarkerLoc>>();

			// I got horribly tired of having to constantly to use ContainsKey searches just to be
			// positive we don't try to access a list that is uninitialized so I just started
			// them all up here.  The drawing code will quickly skip empty ones without drawing
			// anyways.  
			Array m = Enum.GetValues(typeof(MarkerType));
			foreach (MarkerType mt in m)
			{
				tileMarkersToAdd.Add(mt, new List<MarkerLoc>());
			}

        

			if (bw != null)
				bw.ReportProgress(45, "Processing Chests");

            List<String> itemFilters = SettingsManager.Instance.FilterItemStates;

			// Read the Chests
			this.chests = world.Chests;

			foreach (Chest chest in this.chests)
			{
				progress = (int)(((float)chest.ChestId / (float)Global.ChestMaxNumber) * 5f + 45f);

				// See if we are bothering to draw chests at all.
				if (SettingsManager.Instance.DrawMarker(chest.Type) == true)
				{
					// Find out if the chest is relevant to our interests based on what is in it.
					foreach (Item item in chest.Items)
					{
						// If we're not filtering or if we want it
						if (!SettingsManager.Instance.FilterChests || itemFilters.Contains(item.Name))
						{
							// It passed all checks, add it to the list.
							if (chest.Type == ChestType.Chest)
								tileMarkersToAdd[MarkerType.Chest].Add(new MarkerLoc(chest.Coordinates, 1));
							else
								tileMarkersToAdd[MarkerType.Chest + (Int32) chest.Type].Add(
									new MarkerLoc(chest.Coordinates, 1));

							break;
						}
					}
				}
            }

			if (bw != null)
				bw.ReportProgress(50, "Processing Signs");

			// Pull all of the Signs out of the file.
			foreach (Sign newSign in world.Signs)
			{
				if (newSign.Active)
				{
					if (SettingsManager.Instance.DrawMarker(MarkerType.Sign))
					{
						tileMarkersToAdd[MarkerType.Sign].Add(new MarkerLoc(newSign.Position, 1));
					}
				}
			}

			
			if (bw != null)
				bw.ReportProgress(50, "Processing Npcs");


			foreach (NPC newNPC in world.Npcs)
			{
				if (newNPC == null)
					continue;

				if (newNPC.Type == NPCType.Unknown)
					continue;

                Enum.TryParse(newNPC.Type.ToString(), true, out MarkerType tt);

				if (SettingsManager.Instance.DrawMarker(newNPC.Type) == true)
				{
					// I didn't think we ever needed more than one of each NPC.  Then I remembered that
					// if conditions are right two nurses and three merchants can spawn.
					tileMarkersToAdd[tt].Add(new MarkerLoc(
						new Point((Int32) (newNPC.Position.X/16), (Int32) (newNPC.Position.Y/16)), 1));
				}

				
			}

			progress = 50;
			
		}
	
		// Called during worker_GenerateMap.  This will only get used if
		// we are only doing a LoadInformation button press call.
        public void ReadChests(String worldPath, BackgroundWorker bw)
        {
            progress = 0;

			chests = world.GetChests(worldPath, bw);
        }

		public Bitmap CreatePreviewPNG(string outputPngPath, BackgroundWorker bw)
        {
            Boolean useOfficialColors = SettingsManager.Instance.OfficialColors;
			Int32 CropAmount;
			int row, col;
			Bitmap bitmap;

			switch(SettingsManager.Instance.CropImageUsing)
			{
				case 1:
					CropAmount = Global.OldLightingCrop;
					break;
				case 2:
					CropAmount = Global.NewLightingCrop;
					break;
				default:
					CropAmount = 0;
					break;
			}

			if (CropAmount > 0)
			{
				bitmap = new Bitmap(maxX - (2 * CropAmount) - 1, maxY - (2 * CropAmount) - 1
					, PixelFormat.Format24bppRgb);
			}
			else
			{
				bitmap = new Bitmap(maxX, maxY, PixelFormat.Format24bppRgb);
			}
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Graphics graphicsHandle = Graphics.FromImage((Image)bitmap);

            //graphicsHandle.FillRectangle(new SolidBrush(Constants.Colors.SKY), 0, 0, bitmap.Width, bitmap.Height);

            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];
            const int byteOffset = 3;


			TileData tileInfo;
			MarkerType markerType;
            int tileType;
			Color color;

			if (bw != null)
				bw.ReportProgress(50, "Drawing Map");

			if (CropAmount > 0)
			{
				for (row = 0; row < CropAmount; row++)
				{
					for (col = 0; col < maxX; col++)
					{
						tiles[col, row] = TileProperties.Cropped;
					}
				}
				for (row = maxY - CropAmount - 1; row < maxY; row++)
				{
					for (col = 0; col < maxX; col++)
					{
						tiles[col, row] = TileProperties.Cropped;
					}
				}
				for (row = 0; row < maxY; row++)
				{
					for (col = 0; col < CropAmount; col++)
					{
						tiles[col, row] = TileProperties.Cropped;
					}
				}
				for (row = 0; row < maxY; row++)
				{
					for (col = maxX - CropAmount - 1; col < maxX; col++)
					{
						tiles[col, row] = TileProperties.Cropped;
					}
				}
			}

            for (row = 0; row < maxY; row++)
            {
                progress = (int)(((float)row / (float)maxY) * 40f + 50f);

				int index = (bmpData.Stride * (row - CropAmount)) - (1 * byteOffset);    //first increment will be 0;
				for (col = 0; col < maxX; col++)
                {
					if (tiles[col, row] == TileProperties.Cropped)
						continue;

					index += byteOffset;    //increase here to avoid adding increments to each continue
                    tileType = tiles[col, row];
                
                    // Skip Walls
                    if (!SettingsManager.Instance.DrawWalls && tileType > TileProperties.BackgroundOffset)
						tileType = TileProperties.BackgroundOffset;

					if (tileType < TileProperties.Processed)
					{
						tileInfo = TileProperties.tileTypeDefs[tileType];

						if (tileInfo.DrawMarker)
						{
							// If we have already processed this then skip it.
							Int32 tileCount = 1;
							Int32 foundCol = col;
							Int32 foundRow = row;
								
							#region AdvancedMarkerComment
							/*
								* First off, we know from the way the level is processed we never have to
								* check up as if something higher than the start existed we'd have hit it
								* while processing down anyways.
								* 
								* We could parse around and find only stuff that is directly connected to
								* each other and have a "perfect" look but that would take more processing
								* than I'm willing to do.  Instead we'll keep a bounding box around what
								* we've found and use that in the end.
								* 
								* To keep things from getting out of hand from player made clumps of things
								* there is a maximum size the box can be.
								* 
								* ----------
								* Pseudocode
								* ----------
								* First we check to see if we expanded since last run.
								* Then we check all rows but the bottom row for further expansion.
								* One pass one we'll skip the top row as it's also the bottom row.
								* 
								* Next we scan the side edges of the bottom row to try and expand.
								* Then we scan the row below the bottom for vertical expansion.
								* 
								* If there was vertical expansion we now have a new bottom row and
								* so we loop through scanning below the bottom row again.
								* 
								* Once we run out of vertical expansion we'll loop back (if we expanded
								* at all) and rescan the edges to see if by expanding we opened up
								* something new in those newly opened sides.
								* 
								* Once we do one full pass without any new expansion we have our final
								* bounding box so we scan straight through from one end to the other
								* and call everything that matches that we find in it our marker block.
								*/
							#endregion

							Int32 boundsMax = 32;
							Rectangle bounds = new Rectangle(col, row, 1, 1);

							Int32 screenMaxWidth = world.Header.MaxTiles.X;
							Int32 screenMaxHeight = world.Header.MaxTiles.Y;
							Boolean expandedHoriz = false;
							Boolean expandedVert = false;
							Boolean doneProcessing = true;
							Int32 i, j;


							do
							{
								doneProcessing = true;

								for (j = bounds.Y; j < row + boundsMax; j++)
								{
									if (bounds.Bottom >= screenMaxHeight || bounds.Height >= boundsMax)
										break;

									expandedVert = false;

									// We only scan the sides if we either changed the width since last
									// loop or we are on the bottom row.
									if (expandedHoriz == true || j == (bounds.Bottom - 1))
									{
										expandedHoriz = false;

										#region ExpandLeft

										if (tiles[bounds.X, j] == tileType)
										{
											for (i = bounds.X - 1; i > (col - boundsMax); i--)
											{
												if (i <= 0)
													break;

												if (tiles[i, j] == tileType)
												{
													bounds.X = i;
													bounds.Width++;
													expandedHoriz = true;
													doneProcessing = false;
												}
												else
												{
													break;
												}

											}
										}
										#endregion

										#region ExpandRight
										if (tiles[bounds.Right - 1, j] == tileType)
										{
											for (i = bounds.Right; i < (bounds.X + boundsMax); i++)
											{
												if (i >= screenMaxWidth)
													break;

												if (tiles[i, j] == tileType)
												{
													bounds.Width++;
													expandedHoriz = true;
													doneProcessing = false;

													if (bounds.Width == boundsMax)
														break;
												}
												else
												{
													break;
												}
											}
										}
										#endregion
									}

									if (j == (bounds.Bottom - 1))
									{
										#region ExpandDown

										if (j + 1 < screenMaxHeight && bounds.Height < boundsMax)
										{
											for (i = bounds.X; i < (bounds.Right); i++)
											{
												if (tiles[i, j] == tileType)
												{
													if (tiles[i, j + 1] == tileType)
													{
														if (j + 1 >= bounds.Bottom)
														{
															bounds.Height++;
															expandedVert = true;
															doneProcessing = false;
															break;
														}
													}
												}
											}

										}
										#endregion
									}
									if (expandedVert == false)
										break;
								}
							} while (doneProcessing == false);

							Int32 tilePos;
							tileCount = 0;

                            if (useOfficialColors == true)
                                color = tileInfo.OfficialColor;
                            else
							    color = tileInfo.Color;

							for (j = bounds.Y; j < bounds.Bottom; j++)
								for (i = bounds.X; i < bounds.Right; i++)
									if (tiles[i, j] == tileType)
									{
										tileCount++;

										tiles[i, j] = TileProperties.Processed;
										tilePos = (j - CropAmount) * bmpData.Stride + 
												  (i - CropAmount) * byteOffset;

										rgbValues[tilePos] = color.B;
										rgbValues[tilePos + 1] = color.G;
										rgbValues[tilePos + 2] = color.R;
                                        
									}

							foundCol = (int)(bounds.X + (bounds.Width / 2));
							foundRow = (int)(bounds.Y + (bounds.Height / 2));

							markerType = TileProperties.tileTypeDefs[tileType].MarkerType;

							tileMarkersToAdd[markerType].Add(new MarkerLoc(new Point(foundCol, foundRow), tileCount));
						}
					}

					// This used to not draw at all if you chose to put a marker down for that type, which makes
					// sense as the marker did cover it.  However this also caused a bug where things you had not
					// chosen to put a marker didn't draw their pixel color in.  There was a different fix for
					// this but combining the fact that skipping that draw saved very little time and that
					// with the new "One marker per set of items" instead of "One marker per item" mechanic
					// means that you might see some of the color hanging out the edge.
					if (tileType != TileProperties.Processed)
					{
                        if (useOfficialColors)
                            color = TileProperties.tileTypeDefs[tileType].OfficialColor;
						else 
                            color = TileProperties.tileTypeDefs[tileType].Color;

						rgbValues[index] = color.B;
						rgbValues[index + 1] = color.G;
						rgbValues[index + 2] = color.R;
					}

                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(bmpData);


            // Add Spawn
			if (SettingsManager.Instance.DrawMarker(MarkerType.Spawn) == true)
			{
				tileMarkersToAdd[MarkerType.Spawn].Add(new MarkerLoc(new Point(
					world.Header.SpawnPoint.X, world.Header.SpawnPoint.Y), 1));
			}

			if (bw != null)
				bw.ReportProgress(90, "Drawing markers");
			Int32 count = 0;
            // Draw Markers
            foreach (KeyValuePair<MarkerType, List<MarkerLoc>> kv in tileMarkersToAdd)
            {
				if (kv.Key == MarkerType.Unknown)
					continue;

				progress = (Int32)(((Double)count / tileMarkersToAdd.Count) * 10f + 90f);
                Bitmap markerBitmap = ResourceManager.Instance.GetMarker(kv.Key);

                foreach (MarkerLoc sl in kv.Value)
                {
                    int x = Math.Max((int)sl.pv.X - (markerBitmap.Width / 2) - CropAmount, 0);
                    int y = Math.Max((int)sl.pv.Y - (markerBitmap.Height / 2) - CropAmount, 0);
					
					if (x > maxX || y > maxY) continue;

                    graphicsHandle.DrawImage(markerBitmap, x, y);
                }
            }
			if (bw != null)
				bw.ReportProgress(99, "Saving image");
            
            bitmap.Save(outputPngPath, ImageFormat.Png);
            progress = 100;
            return bitmap;
        }

		public void Cleanup()
		{
			// We will set everything to null, except for Chests as we still use them
			// to do our Chest list sorting.
			this.tileMarkersToAdd = null;
			this.tiles = null;
			this.world = null;


		}

		// This code is just to runs tests with.

/*		public void ScanAndCompare(String worldPath, BackgroundWorker bw)
		{
			World.TileImportance[] imp;

			world = new World();
			imp = world.ScanWorld(worldPath, bw);

			for (Int32 j = 0; j < imp.GetLength(0); j++)
			{
				for (Int32 i = 0; i < 256; i++)
				{
					if (imp[j].isKnown((Byte)i))
					{
						if (imp[j].isImportant((Byte)i) != tileTypeDefs[i].IsImportant)
							i = i;
					}

				}
			}
		}*/

		#region GetSet Functions
		public List<Chest> Chests
        {
            get
            {
                return this.chests;
            }
        }

		public World World
		{
			get
			{
				return world;
			}
		}

		#endregion
    }
}
