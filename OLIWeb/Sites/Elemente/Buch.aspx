<%@ Page Language="c#" CodeBehind="Buch.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Sites.Elemente.Buch" %>

<%@ Register TagPrefix="uc1" TagName="Kopf" Src="~/Controls/Koerper/Kopf.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Buch</title>
    <link href="/OliWeb.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form  method="post" runat="server">
    <uc1:Kopf ID="Kopf1" runat="server"></uc1:Kopf>
    <asp:Repeater ID="BuchRepeater" runat="server">
        <ItemTemplate>
            <font size="1"><b>F </b><a href='https://www.oli-it.com/P/<%# Eval("PostItGuid") %>.aspx'>
                <%# Eval("PostIt") %>
            </a>
                <br />
                <b>A </b><a href='https://www.oli-it.com/T/<%# Eval("TopLabGuid") %>.aspx'>
                    <%# Eval("TopLab") %>
                </a>
                <br />
            </font>
        </ItemTemplate>
    </asp:Repeater>
    </form>
</body>
</html>
