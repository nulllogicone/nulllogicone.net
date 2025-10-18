<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>

<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.NKBZ._default" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/NKBZ/</title>
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h2>Wortraum</h2>
        <p>
            bezeichnet die semantische Abstraktion auf Worte, die in NKBZ strukturiert sind. 
				Es können Merkmale, Klassifizierungen, Tätigkeiten, Motivationen, 
				Gruppierungen, Alternativen usw. beschrieben werden.
        </p>
        <p>Es gibt eine Wurzel, einen Einstiegspunkt in den Wortraum.</p>
        <p><a href="https://nulllogicone.net/Netz/?76035f19-f4ae-4d58-a388-4bbc72c51cef">https://nulllogicone.net/Netz/?76035f19-f4ae-4d58-a388-4bbc72c51cef</a></p>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Wortraum RDF" OnClick="Button1_Click"></asp:Button>
        </p>
        <p>
            Der gesamte Wortraum als RDF über einen URL: <a href="https://nulllogicone.net/NKBZ/nkbz.rdf">https://nulllogicone.net/NKBZ/nkbz.rdf</a>
        </p>
        <p>&nbsp;</p>
        <p>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Wrap="False" Width="100%" Height="456px"></asp:TextBox>
        </p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
        <p>&nbsp;</p>
    </form>
</body>
</html>
