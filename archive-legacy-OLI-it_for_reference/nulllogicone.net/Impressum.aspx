<%@ Register TagPrefix="uc1" TagName="FussZeile" Src="FussZeile.ascx" %>

<%@ Page Language="c#" CodeBehind="Impressum.aspx.cs" AutoEventWireup="True" Inherits="nulllogicone.net.Impressum" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>nulllogicone.net/Impressum/</title>
    <link href="nulllogicone.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <h1>Impressum</h1>
        <h2>nulllogicone.net</h2>
        <h3>Nachrichtenvermittlung im Semantic Web</h3>
        <p>
            Der Name kommt von <strong>0</strong> und <strong>1</strong> mit <strong>L</strong>ogik 
				dazwischen.
				<br>
            Also: null logic one - sprich: <em>nal lojik woan</em> .
        </p>
        <h2>Verantwortlich</h2>
        <p>
            <strong>Kathrin Dentler</strong>
            <br>
            <a href="mailto:kathrin@nulllogic.de"><font size="2">mailto:kathrin@nulllogic.de</font></a>
        </p>
        <p>
            <strong>Frederic Luchting
					<br>
            </strong><a href="mailto:frederic@luchting.de"><font size="2">mailto:frederic@luchting.de</font></a>
        </p>
        <h2>Danksagung</h2>
        <p>
            Folgende Dokumente, Tools oder Code haben maßgeblich geholfen und wurden 
				verwendet:
        </p>
        <ul>
            <li>
                <a href="http://www.w3.org/TR/rdf-primer/">RDF Primer</a>
            - ausführliche, einführende Erklärung (Semantic Web)
				<li>
                    <a href="http://www.w3.org/RDF/Validator/">W3C Validator</a>
            - Visualisierung von Graphen
				<li>
                    <a href="http://139.91.183.30:9090/RDF/VRP/index.html">Validating RDF Parser (VRP)</a>&nbsp;- 
				gegen RDF Schema validieren
				<li>
                    <a href="http://www.dfki.uni-kl.de/frodo/RDFSViz/">RDFSViz Tool</a>
            - Schema visualisieren
				<li>
                    <a href="http://www.schemaweb.info/parser/Parser.aspx">VicSoft RDF Parser</a>&nbsp;RDF 
				Statements aus Dokumenten parsen.
				<li>
                    <a href="http://ajax.schwarz-interactive.de/csharpsample/default.aspx">Ajax</a>
            - für Javascript Aufrufe
				<li>
                    <a href="http://manoli.net/csharpformat/">CSharpFormat</a> - für Quellcode 
					Formatierung</li>
        </ul>
        <p>
            <uc1:FussZeile ID="FussZeile1" runat="server"></uc1:FussZeile>
        </p>
    </form>
</body>
</html>
