<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WSWP.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>What should we play?</title>
    <style>/* 
	Blue Dream
	Written by Teylor Feliz  http://www.admixweb.com
*/
table { background:#D3E4E5;
 border:1px solid gray;
 border-collapse:collapse;
 color:#fff;
 font:normal 12px verdana, arial, helvetica, sans-serif;
}
caption { border:1px solid #5C443A;
 color:#5C443A;
 font-weight:bold;
 letter-spacing:20px;
 padding:6px 4px 8px 0px;
 text-align:center;
 text-transform:uppercase;
}
td, th { color:#363636;
 padding:.4em;
 
}
tr { border:1px dotted gray;
}
thead th, tfoot th { background:#5C443A;
 color:#FFFFFF;
 padding:3px 10px 3px 10px;
 text-align:left;
 text-transform:uppercase;
}
tbody td a { color:#363636;
 text-decoration:none;
}
tbody td a:visited { color:gray;
 text-decoration:line-through;
}
tbody td a:hover { text-decoration:underline;
}
tbody th a { color:#363636;
 font-weight:normal;
 text-decoration:none;
}
tbody th a:hover { color:#363636;
}
tbody td+td+td+td a { background-image:url('bullet_blue.png');
 background-position:left center;
 background-repeat:no-repeat;
 color:#03476F;
 padding-left:15px;
}
tbody td+td+td+td a:visited { background-image:url('bullet_white.png');
 background-position:left center;
 background-repeat:no-repeat;
}
tbody th, tbody td { text-align:left;
 vertical-align:top;
}
tfoot td { background:#5C443A;
 color:#FFFFFF;
 padding-top:3px;
}
.odd { background:#fff;
}
tbody tr:hover { background:#99BCBF;
 border:1px solid #03476F;
 color:#000000;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0">
            <tr>
                <td style="font-weight:bold;text-align:right">BoardGameGeek User Name:</td>
                <td><asp:TextBox ID="user" runat="server" ></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td style="font-weight:bold;text-align:right">Num of Players:</td>
                <td><asp:TextBox ID="numOfPlayers" runat="server"></asp:TextBox></td>
                <td style="font-size:small">Leave blank for complete list</td>
            </tr>
            <tr>
                <td style="font-weight:bold;text-align:right">Min time to play:</td>
                <td><asp:TextBox ID="minTTPlay" runat="server"></asp:TextBox></td>
                <td style="font-size:small">(in minutues) Leave blank for complete list</td>
            </tr>
            <tr>
                <td style="font-weight:bold;text-align:right">Max time to play:</td>
                <td><asp:TextBox ID="maxTTPlay" runat="server"></asp:TextBox></td>
                <td style="font-size:small">(in minutes) Leave blank for complete list</td>
            </tr>
            <tr>
                <td style="font-weight:bold;text-align:right">Min user rating:</td>
                <td><asp:TextBox ID="minUserRating" runat="server"></asp:TextBox></td>
                <td style="font-size:small">Leave blank for complete list</td>
            </tr>
            <tr>
                <td style="font-weight:bold;text-align:right">Min BGG rating:</td>
                <td><asp:TextBox ID="minBggRating" runat="server"></asp:TextBox></td>
                <td style="font-size:small">Leave blank for complete list</td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" /></td>
            </tr>
        </table>
        <div>
            <asp:Label ID="msg" runat="server" Visible="False" BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Red">Error Messages goes here.</asp:Label>
        </div>
        <hr />
        <asp:Table ID="games" runat="server"></asp:Table>
    </div>
    </form>
</body>
</html>
