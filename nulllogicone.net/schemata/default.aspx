<%@ Page Language="c#" CodeBehind="default.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.schemata._default" %>

<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="../FussZeile.ascx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/Schemata/</title>   
    <link href="../nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>Schemata</h1>
        <h2>RDF Schema</h2>
        <p>
            Im RDF Schema <a href="../schema.rdfs">schema.rdfs</a> wird die Ontologie der 
				Klassen [SAPCT] [NKBZ] und Beziehungen zwischen diesen beschrieben.
        </p>
        <h2>Tabellenstruktur</h2>
        <h3 id="SAPCT">SAPCT</h3>
        <p>
            <table id="Table1" cellspacing="1" cellpadding="1" width="600" border="1">
                <tr>
                    <td><a href="StammDataSet.xsd">StammDataSet</a></td>
                    <td>Ein User. Urheber von Nachrichten und Antworten.</td>
                </tr>
                <tr>
                    <td><a href="AnglerDataSet.xsd">AnglerDataSet</a></td>
                    <td>Ein Filterprofil mit Löchern</td>
                </tr>
                <tr>
                    <td><a href="PostItDataSet.xsd">PostItDataSet</a></td>
                    <td>Eine Nachricht</td>
                </tr>
                <tr>
                    <td><a href="CodeDataSet.xsd">CodeDataSet</a></td>
                    <td>Die Markierung mit Ringen</td>
                </tr>
                <tr>
                    <td><a href="TopLabDataSet.xsd">TopLabDataSet</a></td>
                    <td>Die Antwort, das Feedback</td>
                </tr>
            </table>
        </p>
        <h3>NKBZ</h3>
        <p>
            <table id="Table2" cellspacing="1" cellpadding="1" width="600" border="1">
                <tr>
                    <td><a href="NetzDataSet.xsd">NetzDataSet</a></td>
                    <td>übergeordneter Sammelbegriff für Knoten</td>
                </tr>
                <tr>
                    <td><a href="KnotenDataSet.xsd">KnotenDataSet</a></td>
                    <td>allgemeine Ansammlung von Ausprägungen</td>
                </tr>
                <tr>
                    <td><a href="BaumDataSet.xsd">BaumDataSet</a></td>
                    <td>Wurzel einer hierarchischen Ordnung</td>
                </tr>
                <tr>
                    <td><a href="ZweigDataSet.xsd">ZweigDataSet</a></td>
                    <td>sich ausschließende Alternativen</td>
                </tr>
            </table>
            <a href="/nulllogicone.net/schemata/NetzDataSet.xsd"></a>
        </p>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
