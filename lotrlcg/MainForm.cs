using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lotrlcg
{
	public partial class MainForm : Form
	{
		// paths
		private const string RingsDB  = "https://ringsdb.com/";
		private const string CacheDir = "cache";
		private const string LogFile  = "log.txt";

		// physical card dimensions
		private const int PhysicalCardWidth   = 246; // 2.460 inch
		private const int PhysicalCardHeight  = 349; // 3.498 inch

		// card back
		Image CardBack = null;



		public MainForm()
		{
			// initialize components
			InitializeComponent();
			StatusLabel.Text = "";
			ProgressBar.Value = 0;

			// create cache directory
			if (!Directory.Exists(CacheDir))
				Directory.CreateDirectory(CacheDir);

			// clear log file
			if (File.Exists(LogFile))
				File.Delete(LogFile);
			File.Create(LogFile);

			// load card back
			Stream bitmapStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("lotrlcg.card_back.png");
			CardBack = Image.FromStream(bitmapStream);
		}



		//----------------------------------------------------------------------------------------------------------------------
		// Utility
		//----------------------------------------------------------------------------------------------------------------------
		private async Task<string> FetchJson(BackgroundWorker worker, string url)
		{
			string cacheFile = $"{CacheDir}/{url.GetHashCode():X8}.json";
			if (File.Exists(cacheFile))
			{
				worker.ReportProgress(0, "Getting card info from cache.");
				return File.ReadAllText(cacheFile);
			}

			worker.ReportProgress(0, "Fetching card info from RingsDB.");
			using (HttpClient client = new HttpClient())
			{
				try
				{
					string response = await client.GetStringAsync(url);
					string cardsJson = JToken.Parse(response).ToString(Formatting.Indented);
					File.WriteAllText(cacheFile, cardsJson);

					return cardsJson;
				}
				catch (Exception ex)
				{
					File.AppendAllLines(LogFile, new string[]
					{
						$"Error fetching Json:",
						$"  URL  : {url}",
						$"  Cache: {cacheFile}",
						$"  Error: {ex.Message}",
					});
					return null;
				}
			}
		}



		private string CachedImagePath(Card c)
		{
			return CacheDir + "/" + Path.GetFileName(c.Imagesrc);
		}



		private bool DownloadCardImage(Card c)
		{
			string imgPath = CachedImagePath(c);
			if (File.Exists(imgPath))
				return true;

			using (WebClient client = new WebClient())
			{
				try
				{
					client.DownloadFile(new Uri(RingsDB + c.Imagesrc), imgPath);
					return true;
				}
				catch (Exception ex)
				{
					File.AppendAllLines(LogFile, new string[]
					{
						$"Error downloading card image:",
						$"  Name : {c.Name}",
						$"  Pack : {c.PackName}",
						$"  URL  : {c.Url}",
						$"  Image: {RingsDB + c.Imagesrc}",
						$"  Error: {ex.Message}",
						""
					});
					return false;
				}
			}
		}



		private Image GetCardImage(Card c)
		{
			if (!DownloadCardImage(c))
				return null;

			// create Image
			return Image.FromFile(CachedImagePath(c));
		}



		//----------------------------------------------------------------------------------------------------------------------
		// Status bar
		//----------------------------------------------------------------------------------------------------------------------
		void SetProcess(int processPercentage, string text)
		{
			ProgressBar.Value = processPercentage;
			StatusLabel.Text = text;
			StatusLabel.ToolTipText = text;
		}



		private void StatusStrip_MouseHover(object sender, EventArgs e)
		{
			HoverToolTip.SetToolTip(sender as StatusStrip, StatusLabel.ToolTipText);
		}



		//----------------------------------------------------------------------------------------------------------------------
		// Database
		//----------------------------------------------------------------------------------------------------------------------
		private void ClearDbButton_Click(object sender, EventArgs e)
		{
			Directory.Delete(CacheDir, true);
			Directory.CreateDirectory(CacheDir);
		}



		private void UpdateDbButton_Click(object sender, EventArgs e)
		{
			UpdateDbButton.Enabled = false;
			if (!DbBackgroundWorker.IsBusy)
				DbBackgroundWorker.RunWorkerAsync();
		}



		private void DbBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;

			// fetch the json code
			worker.ReportProgress(0, "Requesting card info");
			string json = FetchJson(worker, RingsDB + "api/public/cards/").Result;

			worker.ReportProgress(50, "Parsing card info");
			JArray jsonCards = JArray.Parse(json);

			worker.ReportProgress(0, "Download images");
			List<Card> cards = jsonCards.Select(x => new Card(x)).ToList();

			int cardIx = 0;
			foreach (Card c in cards)
			{
				int progress = (int)((cardIx * 100f) / cards.Count);
				string text = $"{cardIx + 1} / {cards.Count}: {c.Name}";
				worker.ReportProgress(progress, $"Checking {text}");
				DownloadCardImage(c);
				cardIx++;
			}
		}



		private void DbBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			SetProcess(e.ProgressPercentage, e.UserState as string);
		}



		private void DbBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			UpdateDbButton.Enabled = true;
			SetProcess(0, e.Cancelled ? "Cancelled" : (e.Error == null ? "Done" : "Error: " + e.Error.Message));
		}



		//----------------------------------------------------------------------------------------------------------------------
		// Placeholders
		//----------------------------------------------------------------------------------------------------------------------
		private void PlaceholdersButton_Click(object sender, EventArgs e)
		{
			PlaceholdersButton.Enabled = false;
			if(!PlaceholdersBackgroundWorker.IsBusy)
				PlaceholdersBackgroundWorker.RunWorkerAsync();
		}



		private void PlaceholdersBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;

			// skip certain packs
			List<string> packsToSkip = new List<string>
			{
				"Starter"
			};

			// fetch the json code
			worker.ReportProgress(0, "Requesting card info");
			string json = FetchJson(worker, RingsDB + "api/public/cards/").Result;

			// parse cards
			worker.ReportProgress(50, "Parsing card info");
			JArray jsonCards = JArray.Parse(json);
			List<Card> cards = jsonCards.Where(c => !packsToSkip.Contains(c.Value<string>("pack_code"))).Select(x => new Card(x)).ToList();

			// create document
			using (PrintDocument doc = new PrintDocument { DocumentName = "LOTR_LCG_Placeholders" })
			{
				// set the required document settings
				doc.DefaultPageSettings.PaperSize = new PrinterSettings().PaperSizes.Cast<PaperSize>().First(size => size.Kind == PaperKind.A4);

				// show the print dialog
				worker.ReportProgress(100, "Printing");
				using (PrintDialog pd = new PrintDialog { AllowSomePages = false, ShowHelp = true, Document = doc })
				{
					if (pd.ShowDialog() == DialogResult.OK)
					{
						// prepare for printing
						int cardIx = 0;
						Font font = new Font("Verdana", 10);
						Pen pen = new Pen(Color.Black, 1.0f);
						Brush brush = Brushes.Black;

						// print the pages
						doc.PrintPage += (object s, PrintPageEventArgs ppeArgs) =>
						{
							// calculate the cards per page
							int cardsHorizontal = 3;
							float cardWidth = ppeArgs.MarginBounds.Width / cardsHorizontal;

							float cardHeight = Card.CardHeight(font.GetHeight(ppeArgs.Graphics));
							int cardsVertical = (int)(ppeArgs.MarginBounds.Height / cardHeight);

							// print the cards
							for (int y = 0; y < cardsVertical; y++)
							{
								for (int x = 0; x < cardsHorizontal; x++)
								{
									float xPos = ppeArgs.MarginBounds.Left + cardWidth * x;
									float yPos = ppeArgs.MarginBounds.Top + cardHeight * y;
									cards[cardIx].Print(ppeArgs.Graphics, pen, brush, font, xPos, yPos, cardWidth, cardHeight);
									cardIx++;
									if (cardIx == cards.Count)
									{
										ppeArgs.HasMorePages = false;
										return;
									}
								}
							}

							// set flag to print more pages
							ppeArgs.HasMorePages = true;
						};
						doc.Print();
					}
				}
			}
		}



		private void PlaceholdersBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			SetProcess(e.ProgressPercentage, e.UserState as string);
		}



		private void PlaceholdersBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			PlaceholdersButton.Enabled = true;
			SetProcess(0, e.Cancelled ? "Cancelled" : (e.Error == null ? "Done" : "Error: " + e.Error.Message));
		}



		//----------------------------------------------------------------------------------------------------------------------
		// Core Proxies
		//----------------------------------------------------------------------------------------------------------------------
		private void CoreProxiesButton_Click(object sender, EventArgs e)
		{
			CoreProxiesButton.Enabled = false;
			if (!CoreProxiesBackgroundWorker.IsBusy)
				CoreProxiesBackgroundWorker.RunWorkerAsync();
		}



		private void CoreProxiesBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;

			// fetch the json code
			worker.ReportProgress(0, "Requesting core set cards info");
			string json = FetchJson(worker, RingsDB + "api/public/cards/core.json").Result;

			// parse cards
			worker.ReportProgress(10, "Parsing card info");
			JArray jsonCards = JArray.Parse(json);
			List<Card> missingCards = jsonCards.Where(c => c.Value<string>("type_code") != "hero" && c.Value<int>("quantity") != 3).Select(x => new Card(x)).ToList();
			List<Image> cardImages = new List<Image>(missingCards.Count * 2);
			for (int i = 0; i < missingCards.Count; i++)
			{
				Card c = missingCards[i];

				// print process
				worker.ReportProgress((i * 100) / missingCards.Count, $"{i} / {missingCards.Count} {missingCards[i].Name}");

				// download card image
				Image img = GetCardImage(c);

				// duplicate single cards
				cardImages.Add(img);
				if (c.Quantity == 1)
					cardImages.Add(img);
			}

			// create document
			using (PrintDocument doc = new PrintDocument { DocumentName = "LOTR_LCG_Placeholders" })
			{
				// set the required document settings
				doc.DefaultPageSettings.PaperSize = new PrinterSettings().PaperSizes.Cast<PaperSize>().First(size => size.Kind == PaperKind.A4);

				// show the print dialog
				worker.ReportProgress(100, "Printing");
				using (PrintDialog pd = new PrintDialog { AllowSomePages = false, ShowHelp = true, Document = doc })
				{
					if (pd.ShowDialog() == DialogResult.OK)
					{
						// prepare for printing
						int cardIx = 0;
						int pageIx = 0;

						// print the pages
						doc.PrintPage += (object s, PrintPageEventArgs ppeArgs) =>
						{
							// calculate the cards per page
							int cardsHorizontal = ppeArgs.MarginBounds.Width / PhysicalCardWidth;
							int cardsVertical   = ppeArgs.MarginBounds.Height / PhysicalCardHeight;

							// calculate the area on the page for each card
							int cardAreaWidth  = ppeArgs.MarginBounds.Width / cardsHorizontal;
							int cardAreaHeight = ppeArgs.MarginBounds.Height / cardsVertical;

							// print the cards
							for (int y = 0; y < cardsVertical; y++)
							{
								for (int x = 0; x < cardsHorizontal; x++)
								{
									int xPos = ppeArgs.MarginBounds.Left + cardAreaWidth * x + (cardAreaWidth - PhysicalCardWidth) / 2;
									int yPos = ppeArgs.MarginBounds.Top + cardAreaHeight * y + (cardAreaHeight - PhysicalCardHeight) / 2;

									if (CardBacksCheckbox.Checked && (pageIx & 1) == 0)
									{
										// card back
										ppeArgs.Graphics.DrawImage(CardBack, xPos, yPos, PhysicalCardWidth, PhysicalCardHeight);
									}
									else
									{
										// card front
										ppeArgs.Graphics.DrawImage(cardImages[cardIx], xPos, yPos, PhysicalCardWidth, PhysicalCardHeight);
										cardIx++;

										if (cardIx == cardImages.Count)
										{
											ppeArgs.HasMorePages = false;
											return;
										}
									}
								}
							}

							// set flag to print more pages
							ppeArgs.HasMorePages = true;
							pageIx++;
						};
						doc.Print();
					}
				}
			}
		}



		private void CoreProxiesBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			SetProcess(e.ProgressPercentage, e.UserState as string);
		}



		private void CoreProxiesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			CoreProxiesButton.Enabled = true;
			SetProcess(0, e.Cancelled ? "Cancelled" : (e.Error == null ? "Done" : "Error: " + e.Error.Message));
		}
	}
}
