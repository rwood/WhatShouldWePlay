using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace WSWP
{
    public partial class Default : System.Web.UI.Page
    {
        private int _MaxPlayers { get; set; }
        private int _MinPlayers { get; set; }

        TableHeaderRow GetHeaderRow()
        {
            TableHeaderRow header = new TableHeaderRow();
            header.BackColor = ColorTranslator.FromHtml("#5C443A");
            header.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
			var name = new TableHeaderCell();
			var nameSort = new LinkButton();
			nameSort.ID = "sortName";
			
            header.Controls.Add(new TableHeaderCell() { Text = "Name", ForeColor = ColorTranslator.FromHtml("#FFFFFF") });
            header.Controls.Add(new TableHeaderCell() { Text = "User Rating", ForeColor = ColorTranslator.FromHtml("#FFFFFF") });
            header.Controls.Add(new TableHeaderCell() { Text = "BGG Rating", ForeColor = ColorTranslator.FromHtml("#FFFFFF") });
            header.Controls.Add(new TableHeaderCell() { Text = "Playing Time", ForeColor = ColorTranslator.FromHtml("#FFFFFF") });
            for (int i = _MinPlayers; i < _MaxPlayers + 1; i++)
            {
                if (i < 1)
                    continue;
                if (i < 13)
                    header.Controls.Add(new TableHeaderCell() { Text = i.ToString(), ForeColor = ColorTranslator.FromHtml("#FFFFFF") });
                else if (i == 13)
                    header.Controls.Add(new TableHeaderCell() { Text = ">12", ForeColor = ColorTranslator.FromHtml("#FFFFFF") });
            }
            return header;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string username;
            int numOfPlayers = -1;
            int minTimeToPlay = -1;
            int maxTimeToPlay = -1;
            int minUserRating = -1;
            int minBggRating = -1;
            msg.Visible = false;
            if (this.Request.Params["user"] != null)
            {
                username = this.Request.Params["user"];
                numOfPlayers = string.IsNullOrEmpty(this.Request.Params["numOfPlayers"]) ? -1 : Convert.ToInt32(this.Request.Params["numOfPlayers"]);
                minTimeToPlay = string.IsNullOrEmpty(this.Request.Params["minTTPlay"]) ? -1 : Convert.ToInt32(this.Request.Params["minTTPlay"]);
                maxTimeToPlay = string.IsNullOrEmpty(this.Request.Params["maxTTPlay"]) ? -1 : Convert.ToInt32(this.Request.Params["maxTTPlay"]);
                minUserRating = string.IsNullOrEmpty(this.Request.Params["minUserRating"]) ? -1 : Convert.ToInt32(this.Request.Params["minUserRating"]);
                minBggRating= string.IsNullOrEmpty(this.Request.Params["minBggRating"]) ? -1 : Convert.ToInt32(this.Request.Params["minBggRating"]);
                try
                {
                    var collection = BGGApi.GetUserGameList(username)
                            .Where(g=> 
                                {
                                    if ((numOfPlayers > 0 && (numOfPlayers < g.MinPlayers || numOfPlayers > g.MaxPlayers)) ||
                                        (minTimeToPlay > 0 && g.PlayingTime < minTimeToPlay) ||
                                        (maxTimeToPlay > 0 && g.PlayingTime > maxTimeToPlay) ||
                                        (minUserRating > 0 && (g.UserRating != -1 && g.UserRating < minUserRating )) ||
                                        (minBggRating > 0 && (g.BGGRating != -1 && g.BGGRating < minBggRating)))
                                        return false;
                                    return true;
                                })
                            .OrderBy(g => g.MaxPlayers)
                            .ThenBy(g => g.MinPlayers)
                            .ThenBy(g => g.PlayingTime)
                            .ThenBy(g => g.Name);
                    _MaxPlayers = collection.Count() < 1 ? 4 : (from t in collection select t.MaxPlayers).Max();
                    _MinPlayers = collection.Count() < 1 ? 1 : (from t in collection select t.MinPlayers).Min();
                    games.Rows.AddAt(0, GetHeaderRow());
                    bool isOdd = true;
                    foreach (Game game in collection)
                    {
                        TableRow row = new TableRow();
                        if (isOdd)
                            row.CssClass = "odd";

                        TableCell name = new TableCell();
                        HtmlAnchor bgglink = new HtmlAnchor()
                        {
                            HRef = "http://boardgamegeek.com/boardgame/" + game.Id.ToString(),
                            InnerText = game.Name,
                        };
                        bgglink.Attributes.Add("target", "_blank");
                        name.Controls.Add(bgglink);
                        row.Controls.Add(name);
                        row.Controls.Add(new TableCell() 
                        { Text = game.UserRating < 0 ? "N/A" : game.UserRating.ToString() });
                        row.Controls.Add(new TableCell() 
                        { Text = game.BGGRating < 0 ? "N/A" : game.BGGRating.ToString() });
                        TableCell playingTime = new TableCell();
                        if (game.PlayingTime < 1)
                            playingTime.Text = "?";
                        else
                            playingTime.Text = game.PlayingTime.ToString();
                        row.Controls.Add(playingTime);
                        for (int i = _MinPlayers; i < _MaxPlayers + 1; i++)
                        {
                            if (i < 1)
                                continue;
                            TableCell t = new TableCell();
                            if (i < 13 && i >= game.MinPlayers && i <= game.MaxPlayers)
                                t.Text = "*";
                            else if (i == 13 && game.MaxPlayers > 12)
                                t.Text = "*";
                            row.Controls.Add(t);
                        }
                        games.Rows.Add(row);
                        isOdd = !isOdd;
                    }
                }
                catch (ApplicationException ex)
                {
                    msg.Text = ex.Message;
                    msg.Visible = true;
                }
            }
        }
    }
}